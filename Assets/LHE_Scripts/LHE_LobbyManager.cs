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

// 1. 닉네임 설정
//    => MainScene의 UI와 연결은 캐릭터 Instantiate할 때
// 2. 웹캡 선택
// 3. 웹캡 영상 송출

// 1 & 2 설정해야 Join Button 활성화되도록 => CreateRoom 함수와 연결

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
        // 커서 위치
        inputNickname.Select();

        // [Webcam]
        // 웹캡 Dropdown option추가
        //webcamDropdown.options = new List<TMP_Dropdown.OptionData>();
        webcamDropdown.options = new List<Dropdown.OptionData>();
        for (int i = 0; i < WebCamTexture.devices.Length; i++)
        {
            //webcamDropdown.options.Add(new TMP_Dropdown.OptionData(WebCamTexture.devices[i].name));
            webcamDropdown.options.Add(new Dropdown.OptionData(WebCamTexture.devices[i].name));
        }
        // 0번 option 표시
        webcamDropdown.value = 0;
        // If you have modified the list of options, you should call this method afterwards to ensure that the visual state of the dropdown corresponds to the updated options.
        webcamDropdown.RefreshShownValue();
        StartCoroutine(CaptureVideoStart());

        // [Join]
        // 닉네임 입력 전에는 join버튼 비활성화
        btnJoin.interactable = false;

        // [Child Charcter]
        if(TokenManager.Instance.fileName == null)
        {
            faceResult.text = "웹캡 화면을 캡처해 캐릭터를 생성해주세요.";
        }
        else
        {
            faceResult.text = "최근에 생성한 캐릭터 정보가 있습니다. (" + TokenManager.Instance.fileName + ")\r\n새로운 캐릭터 생성을 원하시면 웹캡 화면을 캡처해주세요.";
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
        // join버튼 활성화 상태에서 엔터키 누르면 방 입장
        if(btnJoin.interactable == true && (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)))
        {
            CreateRoom();
        }

        TokenManager.Instance.fileName = faceType;
    }

    #region Room
    // [방 생성 요청]
    public void CreateRoom()
    {
        // User NickName
        PhotonNetwork.LocalPlayer.NickName = inputNickname.text;

        RoomOptions room = new RoomOptions(); // 디폴트는 최대인원 & IsVisible
        room.PublishUserId = true;
        PhotonNetwork.CreateRoom("Room", room);
    }

    // 방 생성 성공 시
    public override void OnCreatedRoom()
    {
        base.OnCreatedRoom();
        print(System.Reflection.MethodBase.GetCurrentMethod().Name);
    }

    // 방 생성 실패 시
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        base.OnCreateRoomFailed(returnCode, message);
        print("OnCreateRoomFailed: " + returnCode + ", " + message);
        JoinRoom();
    }

    // [방 참가 요청]
    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom("Room");
    }

    // 방 참가 성공 시
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        print(System.Reflection.MethodBase.GetCurrentMethod().Name);
        PhotonNetwork.LoadLevel(3);
    }

    // 방 참가 실패 시
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        base.OnJoinRoomFailed(returnCode, message);
        print("OnJoinedRoomFailed: " + returnCode + ", " + message);
    }
    #endregion

    #region Webcam
    // 웹캡 영상 송출
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

    // 웹캡 영상 변경
    private IEnumerator OnCaptureVideoChange(int value)
    {
        // ********************************************************
        // 임시로 0 넣어놓음, 나중에 변경 필요 => 현재 displaywnddls option number가져올 수 있나..?
        webcamTexture = new WebCamTexture(WebCamTexture.devices[value].name);
        webcamDisplay.texture = webcamTexture;
        webcamTexture.Play();
        yield return new WaitUntil(() => webcamTexture.didUpdateThisFrame);
    }
    #endregion

    // 웹캠 사진 캡처
    public int captureCount = 0;
    string path;
    public void TakeSnapshot(int value)
    {
        Texture2D snap = new Texture2D(webcamTexture.width, webcamTexture.height);
        snap.SetPixels(webcamTexture.GetPixels());
        snap.Apply();

        // 추후 파일 이름에 아이디 등 식별 가능한 데이터 추가
        // EncodeToPNG의 자료형이 byte[], 이걸 
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
            Debug.Log(www.error); // null 에러
            print("사진 전송 실패");

            print("사진 에러사유 "+www.downloadHandler.text);
        }
        else
        {
            Debug.Log("Form upload complete!");

            print("사진 전송 결과 " + www.downloadHandler.text);

            JSONNode node = JSON.Parse(www.downloadHandler.text);
            //TokenManager.Instance.fileName = node["results"]["fileName"];
            print("얼굴형 " + node["results"]["fileName"]);
            faceType = node["results"]["fileName"];

            faceResult.text = "얼굴 분석을 통해 다음의 캐릭터가 추천되었습니다: " + faceType + "\r\n해당 캐릭터를 사용하시겠습니까?";

            // 얼굴형 결과 서버에 전송
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
        print("얼굴 확정 결과 전송 성공");

        faceResult.text = faceType + " 얼굴형의 캐릭터가 생성됩니다";
        //faceResult.color = Color.blue;
    }

    private void OnFailFaceResult()
    {
        print("얼굴 확정 결과 전송 실패");

        faceResult.text = "캡처를 다시 시도해주세요";
        //faceResult.color = Color.red;
    }

    public class FaceSelect
    {
        public int characterId;
        public string fileName; // 얘만 보내면됨
        public string url;
    }

    // 캡처사진 서버에 전송(multipart/form-data 형식)
    //public void SendSnapshot(string path)
    //{
    //    // 이렇게하면 헤더를 보낼 수 없겟구나.. 어쩌징..

    //    //HttpRequester requester = new HttpRequester();

    //    //requester.url = "http://3.38.39.121:8080/character/recommend";
    //    //requester.requestType = RequestType.POST;

    //    List<IMultipartFormSection> pictureData = new List<IMultipartFormSection>();
    //    pictureData.Add(new MultipartFormFileSection("imageFile", File.ReadAllBytes(path)));

    //    UnityWebRequest www = UnityWebRequest.Post("http://3.38.39.121:8080/character/recommend", pictureData);


    //    www.SendWebRequest();

    //    if (www.result != UnityWebRequest.Result.Success)
    //    {
    //        Debug.Log(www.error); // null 에러
    //        print("사진 전송 실패");
    //    }
    //    else
    //    {
    //        Debug.Log("Form upload complete!");
    //    }
    //}


    // ************ 이밑으로는 게임씬 입장전에 디버깅등으로 강종될때 종료해서 넣은것
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

        print(requester.jsonText); // 출력됨

        requester.onComplete = OnCompleteLogout;
        requester.onError = OnFailLogout;

        HttpManager.Instance.SendRequest(requester);
    }

    private void OnCompleteLogout(DownloadHandler handler)
    {
        print("로그아웃 성공");
    }

    private void OnFailLogout()
    {
        print("로그아웃 실패");
    }
}

