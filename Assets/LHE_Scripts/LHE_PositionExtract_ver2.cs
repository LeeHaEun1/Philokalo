using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using Photon.Pun;

public class LHE_PositionExtract_ver2 : MonoBehaviourPun
{
    List<long> seconds = new List<long>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    #region 중간 위치 정보 전송(10초마다 & 키뗄때마다)
    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {
            if(Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow))
            {
                // 정상 출력 테스트 완료 -> 추후 api열리면 연결
                //long currentTime = GetCurrentTime();
                // 잠시 꺼두기!!!!!!!!!!!!!!!
                //print("currentTime " + currentTime);
                //print("xCoord " + transform.position.x);
                //print("zCoord " + transform.position.z);
                //print("id " + TokenManager.Instance.childId);

                // 추후 주석 해제
                SendPositionData();
            }

            int second = GetCurrentSecond();
            if (second % 10 == 0 && seconds.IndexOf(second) == -1)
            {
                seconds.Add(second);

                // 추후 주석 해제
                SendPositionData();
            }
        }
    }

    private void SendPositionData()
    {
        HttpRequester requester = new HttpRequester();

        // ******************************* 중간 위치 받는 API
        requester.url = "http://3.38.39.121:8001/log"; 
        requester.requestType = RequestType.POST;

        PositionData positionData = new PositionData();
        positionData.time = GetCurrentTime();
        positionData.x = transform.position.x;
        positionData.z = transform.position.z;
        positionData.id = TokenManager.Instance.childId;

        string jsonData = JsonUtility.ToJson(positionData, true);
        byte[] sendData = Encoding.UTF8.GetBytes(jsonData);
        requester.jsonText = jsonData;
        requester.body = sendData;

        print(requester.jsonText);

        requester.onComplete = OnCompleteSendPosition;
        requester.onError = OnFailSendPosition;

        HttpManager.Instance.SendRequest(requester);
    }

    private void OnCompleteSendPosition(DownloadHandler handler)
    {
        print("위치정보 전송 성공");
    }

    private void OnFailSendPosition()
    {
        print("위치정보 전송 실패");
    }

    public static long GetCurrentTime()
    {
        System.DateTime now = System.DateTime.Now.ToLocalTime();
        System.TimeSpan span = (now - new System.DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime());
        //int nowTime = (int)span.TotalSeconds;

        double milli = span.TotalMilliseconds;
        long nowTime = (long)Math.Truncate(milli);

        return nowTime;
    }

    public static int GetCurrentSecond()
    {
        System.DateTime now = System.DateTime.Now.ToLocalTime();
        System.TimeSpan span = (now - new System.DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime());
        int nowTime = (int)span.TotalSeconds;

        return nowTime;
    }

    // 위치정보 형식
    public class PositionData
    {
        public long time;
        public float x;
        public float z;
        public int id; // 자녀계정 ID
    }
    #endregion


    #region 로그아웃
    public void OnApplicationQuit()
    {
        if (photonView.IsMine)
        {
            OnQuitLogout();
        }

    }

    private void OnDestroy()
    {
        if (photonView.IsMine)
        {
            OnQuitLogout();
        }
    }

    private void OnQuitLogout()
    {
        HttpRequester requester = new HttpRequester();

        //requester.url = "http://3.38.39.121:8080/auth/ver2/logout";
        requester.url = "http://purpleprint-pillokallo.ap-northeast-2.elasticbeanstalk.com/auth/v2/logout";
        requester.requestType = RequestType.POST;

        LogoutData logoutData = new LogoutData();
        logoutData.xCoord = transform.position.x;
        logoutData.yCoord = transform.position.y;
        logoutData.zCoord = transform.position.z;

        string jsonData = JsonUtility.ToJson(logoutData, true);
        byte[] sendData = Encoding.UTF8.GetBytes(jsonData);
        requester.jsonText = jsonData;
        requester.body = sendData;

        print(requester.jsonText);

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

    public class LogoutData
    {
        public float xCoord;
        public float yCoord;
        public float zCoord;
    }
    #endregion
}
