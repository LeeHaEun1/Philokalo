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

        // (X) ���� �ʰ� ������ ������
        //if (GetCurrentTime() % 10 == 0)
        //{
        //    print("currenttime " + GetCurrentTime());
        //    print("position x " + transform.position.x);
        //    print("position z " + transform.position.z);
        //}

        // (O) ���� �� �� ���� ����
        // �÷��̾� �ĺ� ���� InstanceID �߰�?? -> ID�� Load�� ������ �ٲ����?? ������ �� ��������..
        int second = GetCurrentSecond();
        if(second % 10 == 0 && seconds.IndexOf(second) == -1)
        {
            seconds.Add(second);

            // Console �׽�Ʈ
            print("currenttime " + second);
            print("position x " + transform.position.x);
            print("position z " + transform.position.z);

            // �ð� & ��ġ ���� ����
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

    // ������ ��ư���� �ش� �Լ� ���� -> �� ĳ���͵鿡 �ٿ������Ŷ� �������... ã�ƿͼ� �Ҵ��ؾ��ϳ�..
    //public void OnApplicationQuit()
    //{
    //    // ���⿡ logout ����� ����
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

        print(requester.jsonText); // ��µ�

        requester.onComplete = OnCompleteLogout;
        requester.onError = OnFailLogout;

        HttpManager.Instance.SendRequest(requester);
    }

    // �̹� �������� ������¶� ��� ����� �ȵǴµ� ���� �α׾ƿ� �Ǵµ�..? 
    private void OnCompleteLogout(DownloadHandler handler)
    {
        print("�α׾ƿ� ����");
    }

    private void OnFailLogout()
    {
        print("�α׾ƿ� ����");
    }

    //public static int GetCurrentTime()
    public static long GetCurrentTime()
    {
        System.DateTime now = System.DateTime.Now.ToLocalTime();
        System.TimeSpan span = (now - new System.DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime());
        //int nowTime = (int)span.TotalSeconds;
        double milli = span.TotalMilliseconds; // �Ҽ��� �� �ΰ� �߶�
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
