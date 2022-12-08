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
    // 유니티 내부를 처리하기 위함.
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
        #region 2022-11-13 Alpha 병합전 원본
        // // 포톤뷰가 나한테 붙어있다면
        // if(photonView.IsMine == true)
        // {
        //     // 1. "1"을 선택하면
        //     if (Input.GetKeyDown("1"))
        //     {
        //         // 2. 카메라 중심, 카메라 앞방향으로 나가는 Ray를 생성
        //         Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        // 
        //         // 3. 생성된 Ray를 발사해서 어딘가에 부딪혔다면
        //         RaycastHit hit;
        //         
        //         if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
        //         {
        //             // 4. 맞은 상대방에게 LHE_CharacterMove 컴포넌트를 가져온다.
        //             LHE_CharacterMove pm = hit.transform.GetComponent<LHE_CharacterMove>();
        //             if(pm != null)
        //             {
        //                 SendMyHeart(id);
        //             }
        //         }
        //     }
        // }
        #endregion

        // 포톤뷰가 나한테 붙어있다면
        if (photonView.IsMine == true)
        {
            // 1. 마우스 왼쪽 클릭 시!
            // if (Input.GetKeyDown(KeyCode.Alpha1))
            if (Input.GetKeyDown(KeyCode.F1))
            {
                // 2. 카메라 중심, 카메라 앞방향으로 나가는 Ray를 생성
                //Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

                // 3. 생성된 Ray를 발사해서 어딘가에 부딪혔다면
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
                        // 4. 맞은 상대방에게 LHE_CharacterMove 컴포넌트를 가져온다.
                        LKK_HeartCounter pm = hit.transform.GetComponent<LKK_HeartCounter>();
                        Debug.Log(pm);

                        // 상대방의 아이디를 LHE_MyID 가 붙어있는 상대방의 myID를 가져온다
                        // int yourID = hit.transform.GetComponent<LHE_GetOthersID>().othersID;
                        int yourID = hit.transform.GetComponent<LHE_MyID>().myID;
                        Debug.Log(yourID);

                        // 그리고 받을 사람 리스트에 해당하는 아이디를 넣는다.
                        heart_recipient_List.Add(yourID);
                        Debug.Log(yourID);

                        // 나자신의 아이디를 LHE_MyID 가 붙어있는 자기자신으로 부터 가져온다.
                        int myID = gameObject.GetComponent<LHE_MyID>().myID;
                        Debug.Log(myID);

                        // 그리고 주는 사람 리스트에 해당하는 아이디를 넣는다.
                        heart_giver_List.Add(myID);
                        Debug.Log(myID);

                        if (pm != null)
                        {
                            // 만약에 상대방이 레이를 쏴서 맞는다면 센드마이하트 함수가 발동한다.
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

    #region 2022-11-13 Alpha 병합전 원본
    // 중복하여 다는 것임으로 해당파트는 변경
    // Heart Counter가 연결 된 변수를 받아오도록 하기 위하여 작성함.
    // public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    // {
    //     // 데이터 보내기
    //     if (stream.IsWriting)
    //     {
    //         stream.SendNext(heartText.text);
    //         
    //     }
    //     // 데이터 받기
    //     else if (stream.IsReading)
    //     {
    //         heartText.text = (string)stream.ReceiveNext();            
    //     }
    // }
    #endregion

    // 2022-11-13 Alpha 병합전 원본
    // 달 필요 없음
    // [PunRPC]
    public void UpdateHeartCount()
    {
        heartText.text = myHeartCount.ToString();
    }


    [PunRPC]
    public void SendMyHeart(string receiver_id)
    {
        myHeartCount++;

        #region 2022-11-13 Alpha 병합전 원본
        // 달 필요 없음 (사용하지 않음으로 뺌. 뺀 사유는 멘토님
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
        //          print("오늘은 더이상 보낼 수 없습니다.");
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

    // 하트인포를 담을 클레스 선언
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
                    Debug.Log("리스트에 들어간 값을 못불러왔음");
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
                    Debug.Log("리스트에 들어간 값을 못불러왔음");
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

    #region 이건 생략
    // 고유값으로 가져올 케릭터별 아이디
    // public class ChildID_From_Token_Manager
    // {
    //     // 네트워크상에서는 Int로 되어있음.
    //     public int childID;
    // }
    #endregion

    private void OnDestroy()
    {
        PlayersManager.pm.OnRemoveScene(id, gameObject);
    }

    // [PunRPC]
    // 하트를 보내는 순간에 네트워크에 정보를 보냄
    // public void SendMyHeartInfo(string receiver_id)
    public void SendMyHeartInfo()
    {
        HttpRequester requester = new HttpRequester();

        // requester.url = "http://3.38.39.121:8080/heart";
        requester.url = "http://purpleprint-pillokallo.ap-northeast-2.elasticbeanstalk.com/heart";
        requester.requestType = RequestType.POST;

        HeartInfo heartInfo = new HeartInfo();
        // heartInfo.giver = 고유식별자로써 쏜 사람을 숫자로 표현 (토큰에 있는 id 값을 받아온다고 가정한다)
        // heartInfo.recipient = 고유식별자로써 받은 사람을 숫자로 표현 (토큰에 있는 id 값을 받아온다고 가정한다)
        // heartInfo.giver = TokenManager.Instance.childId;
        // heartInfo.recipient = TokenManager.Instance.childId;

        // LHE_MyID
        // myID
        GetIdFromTokenID getidfromtokenid = new GetIdFromTokenID();

        heartInfo.giver = getidfromtokenid.LKK_myID;
        heartInfo.recipient = getidfromtokenid.LKK_yourID;


        // heartInfo.giver = TokenManager.Instance.childId;
        // heartInfo.recipient = TokenManager.Instance.childId;              


        // jsonData 파싱
        string jsonData = JsonUtility.ToJson(heartInfo, true);
        byte[] sendData = Encoding.UTF8.GetBytes(jsonData);
        requester.jsonText = jsonData;
        requester.body = sendData;

        print(requester.jsonText);

        requester.onComplete = OnCompleteSendHeart;
        requester.onError = OnFailSendHeart;

        HttpManager.Instance.SendRequest(requester);

        // 하트 주는거 끝났으니까 리스트에 있던거 둘다 날려버림
        heart_recipient_List.Clear();
        heart_giver_List.Clear();
    }


    

    private void OnCompleteSendHeart(DownloadHandler handler)
    {
        print("하트 전송 성공");
        ButtonClickon_Success_sendMessage_HeartGiven_Popup();

        if (photonView.IsMine)
        {
            myHeartCount++;
            photonView.RPC("UpdateMyHeart", RpcTarget.AllBuffered, myHeartCount);
            UpdateHeartCount();
        }

        // responsee 파싱해서 접속 시 사용
        JSONNode node = JSON.Parse(handler.text);

    }

    private void OnFailSendHeart()
    {
        print("하트 전송 실패");
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
        Popup.Show("친구에게 하트를 주었어요~!");
    }
}