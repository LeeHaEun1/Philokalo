using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

[System.Serializable]
public class PositionInfo
{
    public float xCoord;
    public float zCoord;
    public long currentTime;
}

[System.Serializable]
public class ArrayPositionInfo
{
    public List<PositionInfo> log;
    public float xCoord;
    public float yCoord;
    public float zCoord;

    public float jumping;
    public float pointing;
    public float punching;
}

public class LHE_PositionExtract : MonoBehaviour
{
    //List<int> seconds = new List<int>();
    List<long> seconds = new List<long>();

    public List<PositionInfo> posInfoList = new List<PositionInfo>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Test
        //if(Input.GetKeyDown(KeyCode.T))
        //{
        //    print("currenttime " + GetCurrentTime());
        //}

        // (X) 동일 초가 여러번 찍힌다
        //if (GetCurrentTime() % 10 == 0)
        //{
        //    print("currenttime " + GetCurrentTime());
        //    print("position x " + transform.position.x);
        //    print("position z " + transform.position.z);
        //}

        // (O) 동일 초 한 번만 찍힘
        // 플레이어 식별 위해 InstanceID 추가?? -> ID는 Load될 때마다 바뀌려나?? 고유의 값 뭐있을까..
        int second = GetCurrentSecond();
        if(second % 10 == 0 && seconds.IndexOf(second) == -1)
        {
            seconds.Add(second);

            // Console 테스트
            print("currenttime " + second);
            print("position x " + transform.position.x);
            print("position z " + transform.position.z);

            // 시간 & 위치 정보 저장
            SaveData(transform.position.x, transform.position.z, GetCurrentTime());
        }
    }

    //private void SaveData(float x, float z, int time)
    private void SaveData(float x, float z, long time)
    {
        PositionInfo posInfo = new PositionInfo();
        posInfo.xCoord = x;
        posInfo.zCoord = z;
        posInfo.currentTime = time;

        posInfoList.Add(posInfo);
    }

    // 나가기 버튼에도 해당 함수 연결 -> 아 캐릭터들에 붙여놓은거라 힘들수도... 찾아와서 할당해야하나..
    //public void OnApplicationQuit()
    //{
    //    // 여기에 logout 기능을 연결
    //    OnQuitLogout();

    //    //ArrayPositionInfo arrayLog = new ArrayPositionInfo();
    //    //arrayLog.log = posInfoList;
    //    //arrayLog.xCoord = transform.position.x;
    //    //arrayLog.yCoord = transform.position.y;
    //    //arrayLog.zCoord = transform.position.z;

    //    //string jsonData = JsonUtility.ToJson(arrayLog, true);
    //    //print(jsonData);
    //}

    private void OnQuitLogout()
    {
        HttpRequester requester = new HttpRequester();

        requester.url = "http://3.38.39.121:8080/auth/logout";
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

    // 이미 빌드파일 종료상태라 결과 출력은 안되는데 정상 로그아웃 되는듯..? 
    private void OnCompleteLogout(DownloadHandler handler)
    {
        print("로그아웃 성공");
    }

    private void OnFailLogout()
    {
        print("로그아웃 실패");
    }

    //public static int GetCurrentTime()
    public static long GetCurrentTime()
    {
        System.DateTime now = System.DateTime.Now.ToLocalTime();
        System.TimeSpan span = (now - new System.DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime());
        //int nowTime = (int)span.TotalSeconds;
        double milli = span.TotalMilliseconds; // 소수점 뒤 두개 잘라서
        //int nowTime = (long)Math.Truncate(milli);
        //print("trunc " + (long)Math.Truncate(milli));
        long nowTime = (long)Math.Truncate(milli);

        //print("nowTime " + nowTime);

        //print("milli " + milli);

        //print("nowtime " + nowTime);
        return nowTime;
    }

    public static int GetCurrentSecond()
    {
        System.DateTime now = System.DateTime.Now.ToLocalTime();
        System.TimeSpan span = (now - new System.DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime());
        int nowTime = (int)span.TotalSeconds;
        //print(nowTime);

        return nowTime;

    }
}
