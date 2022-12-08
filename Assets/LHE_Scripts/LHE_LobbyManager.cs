using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Photon.Realtime;
using System.IO;
using UnityEngine.Networking;
using System;
using System.Text;
using SimpleJSON;

// 1. �г��� ����
//    => MainScene�� UI�� ������ ĳ���� Instantiate�� ��
// 2. ��ĸ ����
// 3. ��ĸ ���� ����

// 1 & 2 �����ؾ� Join Button Ȱ��ȭ�ǵ��� => CreateRoom �Լ��� ����

public class LHE_LobbyManager : MonoBehaviourPunCallbacks
{
    [Header("Nickname")]
    //public TMP_InputField inputNickname;
    public InputField inputNickname;

    [Header("Webcam")]
    //public TMP_Dropdown webcamDropdown;
    public Dropdown webcamDropdown;
    WebCamTexture webcamTexture;
    public RawImage webcamDisplay;

    [Header("Character")]
    public Button btnJoin;
    public Text faceResult;
    public string faceType;
    public bool fixedFaceType;

    // Start is called before the first frame update
    void Start()
    {
        // [Nickname]
        // Ŀ�� ��ġ
        inputNickname.Select();

        // [Webcam]
        // ��ĸ Dropdown option�߰�
        //webcamDropdown.options = new List<TMP_Dropdown.OptionData>();
        webcamDropdown.options = new List<Dropdown.OptionData>();
        for (int i = 0; i < WebCamTexture.devices.Length; i++)
        {
            //webcamDropdown.options.Add(new TMP_Dropdown.OptionData(WebCamTexture.devices[i].name));
            webcamDropdown.options.Add(new Dropdown.OptionData(WebCamTexture.devices[i].name));
        }
        // 0�� option ǥ��
        webcamDropdown.value = 0;
        // If you have modified the list of options, you should call this method afterwards to ensure that the visual state of the dropdown corresponds to the updated options.
        webcamDropdown.RefreshShownValue();
        StartCoroutine(CaptureVideoStart());

        // [Join]
        // �г��� �Է� ������ join��ư ��Ȱ��ȭ
        btnJoin.interactable = false;

        // [Child Charcter]
        if(TokenManager.Instance.fileName == null)
        {
            faceResult.text = "��ĸ ȭ���� ĸó�� ĳ���͸� �������ּ���.";
        }
        else
        {
            faceResult.text = "�ֱٿ� ������ ĳ���� ������ �ֽ��ϴ�. (" + TokenManager.Instance.fileName + ")\r\n���ο� ĳ���� ������ ���Ͻø� ��ĸ ȭ���� ĸó���ּ���.";
            faceType = TokenManager.Instance.fileName;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // [Join]
        if (inputNickname.text != "" && faceType != "")
        {
            btnJoin.interactable = true;
        }
        else
        {
            btnJoin.interactable = false;
        }
        // join��ư Ȱ��ȭ ���¿��� ����Ű ������ �� ����
        if(btnJoin.interactable == true && (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)))
        {
            CreateRoom();
        }

        TokenManager.Instance.fileName = faceType;
    }

    #region Room
    // [�� ���� ��û]
    public void CreateRoom()
    {
        // User NickName
        PhotonNetwork.LocalPlayer.NickName = inputNickname.text;

        RoomOptions room = new RoomOptions(); // ����Ʈ�� �ִ��ο� & IsVisible
        room.PublishUserId = true;
        PhotonNetwork.CreateRoom("Room", room);
    }

    // �� ���� ���� ��
    public override void OnCreatedRoom()
    {
        base.OnCreatedRoom();
        print(System.Reflection.MethodBase.GetCurrentMethod().Name);
    }

    // �� ���� ���� ��
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        base.OnCreateRoomFailed(returnCode, message);
        print("OnCreateRoomFailed: " + returnCode + ", " + message);
        JoinRoom();
    }

    // [�� ���� ��û]
    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom("Room");
    }

    // �� ���� ���� ��
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        print(System.Reflection.MethodBase.GetCurrentMethod().Name);
        PhotonNetwork.LoadLevel(3);
    }

    // �� ���� ���� ��
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        base.OnJoinRoomFailed(returnCode, message);
        print("OnJoinedRoomFailed: " + returnCode + ", " + message);
    }
    #endregion

    #region Webcam
    // ��ĸ ���� ����
    private IEnumerator CaptureVideoStart()
    {
        if (WebCamTexture.devices.Length == 0)
        {
            Debug.LogFormat("WebCam device not found");
            yield break;
        }

        yield return Application.RequestUserAuthorization(UserAuthorization.WebCam);
        if (!Application.HasUserAuthorization(UserAuthorization.WebCam))
        {
            Debug.LogFormat("authorization for using the device is denied");
            yield break;
        }

        webcamTexture = new WebCamTexture(WebCamTexture.devices[0].name);
        webcamDisplay.texture = webcamTexture;
        webcamTexture.Play();
        yield return new WaitUntil(() => webcamTexture.didUpdateThisFrame);
    }

    public void OnCameraDropdownValueChanged(int value)
    {
        //VideoSetting.SelectedDevice = WebCamTexture.devices[value];
        StartCoroutine(OnCaptureVideoChange(value));
    }

    // ��ĸ ���� ����
    private IEnumerator OnCaptureVideoChange(int value)
    {
        // ********************************************************
        // �ӽ÷� 0 �־����, ���߿� ���� �ʿ� => ���� displaywnddls option number������ �� �ֳ�..?
        webcamTexture = new WebCamTexture(WebCamTexture.devices[value].name);
        webcamDisplay.texture = webcamTexture;
        webcamTexture.Play();
        yield return new WaitUntil(() => webcamTexture.didUpdateThisFrame);
    }
    #endregion

    // ��ķ ���� ĸó
    public int captureCount = 0;
    string path;
    public void TakeSnapshot(int value)
    {
        Texture2D snap = new Texture2D(webcamTexture.width, webcamTexture.height);
        snap.SetPixels(webcamTexture.GetPixels());
        snap.Apply();

        // ���� ���� �̸��� ���̵� �� �ĺ� ������ ������ �߰�
        // EncodeToPNG�� �ڷ����� byte[], �̰� 
        // File.WriteAllBytes(Application.dataPath + "/LHE_WebcamCaptures/" + captureCount.ToString() + ".png", snap.EncodeToPNG());
        // path = Application.dataPath + "/LHE_WebcamCaptures/" + captureCount.ToString() + ".png";
        File.WriteAllBytes(Application.streamingAssetsPath + "/" + captureCount.ToString() + ".png", snap.EncodeToPNG());
        path = Application.streamingAssetsPath + "/" + captureCount.ToString() + ".png";
        captureCount++;


        print("webcam image captured");

        StartCoroutine(SendSnapshot(path));
    }


    IEnumerator SendSnapshot(string path)
    {
        List<IMultipartFormSection> pictureData = new List<IMultipartFormSection>();
        pictureData.Add(new MultipartFormFileSection("imageFile", File.ReadAllBytes(path), captureCount.ToString()+".png", "application/json"));

        //WWWForm pictureData = new WWWForm();
        //pictureData.AddBinaryData("imageFile", File.ReadAllBytes(path));
        //pictureData.AddField("imageFile", );

        //UnityWebRequest www = UnityWebRequest.Post("http://3.38.39.121:8080/character/recommend", pictureData);
        UnityWebRequest www = UnityWebRequest.Post("http://purpleprint-pillokallo.ap-northeast-2.elasticbeanstalk.com/character/recommend", pictureData);
        www.SetRequestHeader("Accept", "*/*");
        //www.SetRequestHeader("Content-Type", "application/json");
        www.SetRequestHeader("Authorization", "Bearer " + TokenManager.Instance.token);

        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error); // null ����
            print("���� ���� ����");

            print("���� �������� "+www.downloadHandler.text);
        }
        else
        {
            Debug.Log("Form upload complete!");

            print("���� ���� ��� " + www.downloadHandler.text);

            JSONNode node = JSON.Parse(www.downloadHandler.text);
            //TokenManager.Instance.fileName = node["results"]["fileName"];
            print("���� " + node["results"]["fileName"]);
            faceType = node["results"]["fileName"];

            faceResult.text = "�� �м��� ���� ������ ĳ���Ͱ� ��õ�Ǿ����ϴ�: " + faceType + "\r\n�ش� ĳ���͸� ����Ͻðڽ��ϱ�?";

            // ���� ��� ������ ����
            //SendFaceResult(node["results"]["fileName"]);
        }
    }

   public void SendFaceResult()
   {
        HttpRequester requester = new HttpRequester();

        //requester.url = "http://3.38.39.121:8080/character";
        requester.url = "http://purpleprint-pillokallo.ap-northeast-2.elasticbeanstalk.com/character";
        requester.requestType = RequestType.POST;

        FaceSelect faceSelect = new FaceSelect();
        faceSelect.fileName = faceType;

        string jsonData = JsonUtility.ToJson(faceSelect, true);
        byte[] sendData = Encoding.UTF8.GetBytes(jsonData);
        requester.jsonText = jsonData;
        requester.body = sendData;

        print(requester.jsonText); 

        requester.onComplete = OnCompleteFaceResult;
        requester.onError = OnFailFaceResult;

        HttpManager.Instance.SendRequest(requester);
   }

    private void OnCompleteFaceResult(DownloadHandler handler)
    {
        print("�� Ȯ�� ��� ���� ����");

        faceResult.text = faceType + " ������ ĳ���Ͱ� �����˴ϴ�";
        //faceResult.color = Color.blue;
    }

    private void OnFailFaceResult()
    {
        print("�� Ȯ�� ��� ���� ����");

        faceResult.text = "ĸó�� �ٽ� �õ����ּ���";
        //faceResult.color = Color.red;
    }

    public class FaceSelect
    {
        public int characterId;
        public string fileName; // �길 �������
        public string url;
    }

    // ĸó���� ������ ����(multipart/form-data ����)
    //public void SendSnapshot(string path)
    //{
    //    // �̷����ϸ� ����� ���� �� ���ٱ���.. ��¼¡..

    //    //HttpRequester requester = new HttpRequester();

    //    //requester.url = "http://3.38.39.121:8080/character/recommend";
    //    //requester.requestType = RequestType.POST;

    //    List<IMultipartFormSection> pictureData = new List<IMultipartFormSection>();
    //    pictureData.Add(new MultipartFormFileSection("imageFile", File.ReadAllBytes(path)));

    //    UnityWebRequest www = UnityWebRequest.Post("http://3.38.39.121:8080/character/recommend", pictureData);


    //    www.SendWebRequest();

    //    if (www.result != UnityWebRequest.Result.Success)
    //    {
    //        Debug.Log(www.error); // null ����
    //        print("���� ���� ����");
    //    }
    //    else
    //    {
    //        Debug.Log("Form upload complete!");
    //    }
    //}


    // ************ �̹����δ� ���Ӿ� �������� ���������� �����ɶ� �����ؼ� ������
    //private void OnApplicationQuit()
    //{
    //    OnQuitLogout();
    //}

    public List<PositionInfo> posInfoList = new List<PositionInfo>();
    private void OnQuitLogout()
    {
        HttpRequester requester = new HttpRequester();

        //requester.url = "http://3.38.39.121:8080/auth/logout";
        requester.url = "http://purpleprint-pillokallo.ap-northeast-2.elasticbeanstalk.com/auth/logout";
        requester.requestType = RequestType.POST;

        ArrayPositionInfo arrayLog = new ArrayPositionInfo();
        arrayLog.log = posInfoList;
        arrayLog.xCoord = transform.position.x;
        arrayLog.yCoord = transform.position.y;
        arrayLog.zCoord = transform.position.z;
        arrayLog.jumping = 0;
        arrayLog.pointing = 0;
        arrayLog.punching = 0;

        string jsonData = JsonUtility.ToJson(arrayLog, true);
        byte[] sendData = Encoding.UTF8.GetBytes(jsonData);
        requester.jsonText = jsonData;
        requester.body = sendData;

        print(requester.jsonText); // ��µ�

        requester.onComplete = OnCompleteLogout;
        requester.onError = OnFailLogout;

        HttpManager.Instance.SendRequest(requester);
    }

    private void OnCompleteLogout(DownloadHandler handler)
    {
        print("�α׾ƿ� ����");
    }

    private void OnFailLogout()
    {
        print("�α׾ƿ� ����");
    }
}

