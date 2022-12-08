using Ookii.Dialogs;
using OpenCvSharp.Flann;
using Photon.Pun;
using Photon.Pun.Demo.PunBasics;
using SimpleJSON;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Unity.VisualScripting;
// using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using EasyUI.Popup;

using static LKK_HeartCounter;


public class LKK_HeartCounter : MonoBehaviourPunCallbacks
{
    // ����Ƽ ���θ� ó���ϱ� ����.
    public List<string> heartGiverList = new List<string>();


    public static List<int> heart_giver_List = new List<int>();
    public static List<int> heart_recipient_List = new List<int>();

    public string id;

    public int myHeartCount = 0;

    public Text heartText;

    public int charactorID;
    public int friendID;

    private void Awake()
    {

    }

    void Start()
    {
        // PlayersManager.pm.OnEnterScene(id.ToString(), gameObject);
        PlayersManager.pm.OnEnterScene(id, gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        #region 2022-11-13 Alpha ������ ����
        // // ����䰡 ������ �پ��ִٸ�
        // if(photonView.IsMine == true)
        // {
        //     // 1. "1"�� �����ϸ�
        //     if (Input.GetKeyDown("1"))
        //     {
        //         // 2. ī�޶� �߽�, ī�޶� �չ������� ������ Ray�� ����
        //         Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        // 
        //         // 3. ������ Ray�� �߻��ؼ� ��򰡿� �ε����ٸ�
        //         RaycastHit hit;
        //         
        //         if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
        //         {
        //             // 4. ���� ���濡�� LHE_CharacterMove ������Ʈ�� �����´�.
        //             LHE_CharacterMove pm = hit.transform.GetComponent<LHE_CharacterMove>();
        //             if(pm != null)
        //             {
        //                 SendMyHeart(id);
        //             }
        //         }
        //     }
        // }
        #endregion

        // ����䰡 ������ �پ��ִٸ�
        if (photonView.IsMine == true)
        {
            // 1. ���콺 ���� Ŭ�� ��!
            // if (Input.GetKeyDown(KeyCode.Alpha1))
            if (Input.GetKeyDown(KeyCode.F1))
            {
                // 2. ī�޶� �߽�, ī�޶� �չ������� ������ Ray�� ����
                //Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

                // 3. ������ Ray�� �߻��ؼ� ��򰡿� �ε����ٸ�
                RaycastHit hit;
                Debug.Log("shoot");

                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
                {
                    Debug.LogWarning(hit.transform.name);
                    if (gameObject.transform.GetComponent<LHE_MyID>().myID == hit.transform.GetComponent<LHE_MyID>().myID)
                    {
                        Debug.Log("Dont do it");
                    }
                    else
                    {
                        // 4. ���� ���濡�� LHE_CharacterMove ������Ʈ�� �����´�.
                        LKK_HeartCounter pm = hit.transform.GetComponent<LKK_HeartCounter>();
                        Debug.Log(pm);

                        // ������ ���̵� LHE_MyID �� �پ��ִ� ������ myID�� �����´�
                        // int yourID = hit.transform.GetComponent<LHE_GetOthersID>().othersID;
                        int yourID = hit.transform.GetComponent<LHE_MyID>().myID;
                        Debug.Log(yourID);

                        // �׸��� ���� ��� ����Ʈ�� �ش��ϴ� ���̵� �ִ´�.
                        heart_recipient_List.Add(yourID);
                        Debug.Log(yourID);

                        // ���ڽ��� ���̵� LHE_MyID �� �پ��ִ� �ڱ��ڽ����� ���� �����´�.
                        int myID = gameObject.GetComponent<LHE_MyID>().myID;
                        Debug.Log(myID);

                        // �׸��� �ִ� ��� ����Ʈ�� �ش��ϴ� ���̵� �ִ´�.
                        heart_giver_List.Add(myID);
                        Debug.Log(myID);

                        if (pm != null)
                        {
                            // ���࿡ ������ ���̸� ���� �´´ٸ� ���帶����Ʈ �Լ��� �ߵ��Ѵ�.
                            //photonView.RPC("SendMyHeart", RpcTarget.AllBuffered, pm.id);

                            SendMyHeartInfo();
                            //photonView.RPC("SendMyHeartInfo", RpcTarget.AllBuffered, pm.id);

                            // LKK_Particle_Manager.Instance.LoadParticleEffect();
                            // spawnedParticle.SetActive(true);
                        }
                        else
                        {
                            Debug.LogWarning("NONONONONONONO");
                        }
                    }
                }
            }
        }
        //UpdateHeartCount();

    }

    // IEnumerator StopEffect() 
    // {
    //     particleobj.SetActive(false);
    //     yield return new WaitForSeconds(1.0f);
    // }

    #region 2022-11-13 Alpha ������ ����
    // �ߺ��Ͽ� �ٴ� �������� �ش���Ʈ�� ����
    // Heart Counter�� ���� �� ������ �޾ƿ����� �ϱ� ���Ͽ� �ۼ���.
    // public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    // {
    //     // ������ ������
    //     if (stream.IsWriting)
    //     {
    //         stream.SendNext(heartText.text);
    //         
    //     }
    //     // ������ �ޱ�
    //     else if (stream.IsReading)
    //     {
    //         heartText.text = (string)stream.ReceiveNext();            
    //     }
    // }
    #endregion

    // 2022-11-13 Alpha ������ ����
    // �� �ʿ� ����
    // [PunRPC]
    public void UpdateHeartCount()
    {
        heartText.text = myHeartCount.ToString();
    }


    [PunRPC]
    public void SendMyHeart(string receiver_id)
    {
        myHeartCount++;

        #region 2022-11-13 Alpha ������ ����
        // �� �ʿ� ���� (������� �������� ��. �� ������ �����
        // GameObject receiver = PlayersManager.pm.GetPlayer(receiver_id);
        // 
        // int yourID = receiver.GetComponent<LHE_MyID>().myID;
        // Debug.Log(yourID);
        // 
        // 
        // LKK_HeartCounter receiverHeart = receiver.GetComponent<LKK_HeartCounter>();
        //  if(receiverHeart)
        //  {
        //      if(receiverHeart.heartGiverList.Contains(id))
        //      {
        //          print("������ ���̻� ���� �� �����ϴ�.");
        //          return;
        //      }
        //      else
        //      {
        //          receiverHeart.heartGiverList.Add(PhotonNetwork.NickName);
        //          receiverHeart.myHeartCount++;
        //          receiverHeart.UpdateHeartCount();
        //      }
        //  }
        #endregion
    }

    // ��Ʈ������ ���� Ŭ���� ����
    public class HeartInfo
    {
        public int giver;
        public int recipient;
    }

    class GetIdFromTokenID
    {
        public int myID;
        public int yourID;
        public int LKK_myID
        {
            get
            {
                int findIndex = -1;
                int temp = 0;
                for (int i = 0; i < heart_giver_List.Count; i++)
                {
                    findIndex = i;
                    break;
                }
                if (findIndex == -1)
                {
                    Debug.Log("����Ʈ�� �� ���� ���ҷ�����");
                }
                else
                {
                    temp = heart_giver_List[findIndex];
                }
                int LKK_myID = temp;
                return myID = LKK_myID;
            }
        }

        public int LKK_yourID
        {
            get
            {
                int findIndex = -1;
                int temp = 0;
                for (int i = 0; i < heart_recipient_List.Count; i++)
                {
                    findIndex = i;
                    break;
                }
                if (findIndex == -1)
                {
                    Debug.Log("����Ʈ�� �� ���� ���ҷ�����");
                }
                else
                {
                    temp = heart_recipient_List[findIndex];
                }
                int LKK_yourID = temp;

                // int LKK_yourID = heart_recipient_List.Index
                return yourID = LKK_yourID;
            }
        }
    }

    #region �̰� ����
    // ���������� ������ �ɸ��ͺ� ���̵�
    // public class ChildID_From_Token_Manager
    // {
    //     // ��Ʈ��ũ�󿡼��� Int�� �Ǿ�����.
    //     public int childID;
    // }
    #endregion

    private void OnDestroy()
    {
        PlayersManager.pm.OnRemoveScene(id, gameObject);
    }

    // [PunRPC]
    // ��Ʈ�� ������ ������ ��Ʈ��ũ�� ������ ����
    // public void SendMyHeartInfo(string receiver_id)
    public void SendMyHeartInfo()
    {
        HttpRequester requester = new HttpRequester();

        // requester.url = "http://3.38.39.121:8080/heart";
        requester.url = "http://purpleprint-pillokallo.ap-northeast-2.elasticbeanstalk.com/heart";
        requester.requestType = RequestType.POST;

        HeartInfo heartInfo = new HeartInfo();
        // heartInfo.giver = �����ĺ��ڷν� �� ����� ���ڷ� ǥ�� (��ū�� �ִ� id ���� �޾ƿ´ٰ� �����Ѵ�)
        // heartInfo.recipient = �����ĺ��ڷν� ���� ����� ���ڷ� ǥ�� (��ū�� �ִ� id ���� �޾ƿ´ٰ� �����Ѵ�)
        // heartInfo.giver = TokenManager.Instance.childId;
        // heartInfo.recipient = TokenManager.Instance.childId;

        // LHE_MyID
        // myID
        GetIdFromTokenID getidfromtokenid = new GetIdFromTokenID();

        heartInfo.giver = getidfromtokenid.LKK_myID;
        heartInfo.recipient = getidfromtokenid.LKK_yourID;


        // heartInfo.giver = TokenManager.Instance.childId;
        // heartInfo.recipient = TokenManager.Instance.childId;              


        // jsonData �Ľ�
        string jsonData = JsonUtility.ToJson(heartInfo, true);
        byte[] sendData = Encoding.UTF8.GetBytes(jsonData);
        requester.jsonText = jsonData;
        requester.body = sendData;

        print(requester.jsonText);

        requester.onComplete = OnCompleteSendHeart;
        requester.onError = OnFailSendHeart;

        HttpManager.Instance.SendRequest(requester);

        // ��Ʈ �ִ°� �������ϱ� ����Ʈ�� �ִ��� �Ѵ� ��������
        heart_recipient_List.Clear();
        heart_giver_List.Clear();
    }


    

    private void OnCompleteSendHeart(DownloadHandler handler)
    {
        print("��Ʈ ���� ����");
        ButtonClickon_Success_sendMessage_HeartGiven_Popup();

        if (photonView.IsMine)
        {
            myHeartCount++;
            photonView.RPC("UpdateMyHeart", RpcTarget.AllBuffered, myHeartCount);
            UpdateHeartCount();
        }

        // responsee �Ľ��ؼ� ���� �� ���
        JSONNode node = JSON.Parse(handler.text);

    }

    private void OnFailSendHeart()
    {
        print("��Ʈ ���� ����");
    }

    [PunRPC]
    public void UpdateMyHeart(int myheart)
    {
        heartText.text = myheart.ToString();
    }

    [PunRPC]
    public void heartDrop_particle(int sender)
    {
        if (photonView.IsMine)
        {
            // photonView.RPC("castSpell", PhotonTargets.Others, index);
        }
    }

    public void ButtonClickon_Success_sendMessage_HeartGiven_Popup()
    {
        Popup.Show("ģ������ ��Ʈ�� �־����~!");
    }
}