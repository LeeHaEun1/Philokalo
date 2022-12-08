using Photon.Pun;
using SimpleJSON;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class LHE_ChildAccountManager : MonoBehaviour
{
    public static LHE_ChildAccountManager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // 로그인 응답으로 받은 childList 개수
    [Header("Child Account Count")]
    public int currChildCount;

    // 자녀계정 추가
    [Header("Add Child")]
    public GameObject addChild1;
    public GameObject addChild2;
    public InputField inputChild1Name;
    public InputField inputChild2Name;
    public Button btnCreateChild1;
    public Button btnCreateChild2;

    // 자녀계정 입장
    [Header("Connect as Child")]
    public GameObject child1;
    public GameObject child2;
    public Text child1Name;
    public Text child2Name;

    // 로그인 후 받은 자녀1 정보
    [Header("Child 1 Info")]
    public int child1_childId;
    public string child1_childName;
    public int child1_connectNum;
    public int child1_givenHeart;
    public int child1_grantHeart;
    public int child1_userId;

    // 로그인 후 받은 자녀2 정보
    [Header("Child 2 Info")]
    public int child2_childId;
    public string child2_childName;
    public int child2_connectNum;
    public int child2_givenHeart;
    public int child2_grantHeart;
    public int child2_userId;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // 왼쪽부터 순차적으로 추가하는 형태로 만들고싶지만 복잡해질 것 같다
        // 우선 이렇게 하고 시연 시에는 왼쪽부터 만드는 것으로
        if (inputChild1Name.text != "")
        {
            btnCreateChild1.interactable = true;
        }
        if (inputChild2Name.text != "")
        {
            btnCreateChild2.interactable = true;
        }
    }

    #region 초기 UI 세팅
    public void SetStartUI(int currChildCount)
    {
        if (currChildCount == 0)
        {
            addChild1.SetActive(true);
            addChild2.SetActive(true);
            child1.SetActive(false);
            child2.SetActive(false);
        }
        else if (currChildCount == 1)
        {
            addChild1.SetActive(false);
            addChild2.SetActive(true);
            child1.SetActive(true);
            child2.SetActive(false);
        }
        else if (currChildCount == 2)
        {
            addChild1.SetActive(false);
            addChild2.SetActive(false);
            child1.SetActive(true);
            child2.SetActive(true);
        }
    }
    #endregion

    #region 자녀계정 1번 접속
    // 자녀계정 접속 버튼(이미지 패널)
    public void OnClickConnectChild1()
    {
        HttpRequester requester = new HttpRequester();

        //requester.url = "http://3.38.39.121:8080/user/child/connect";
        requester.url = "http://purpleprint-pillokallo.ap-northeast-2.elasticbeanstalk.com/user/child/connect";
        requester.requestType = RequestType.POST;

        ChildInfo child1Info = new ChildInfo();
        child1Info.childId = child1_childId;
        child1Info.childName = child1_childName;
        child1Info.connectNum = child1_connectNum;
        child1Info.givenHeart = child1_givenHeart;
        child1Info.grantHeart = child1_grantHeart;
        child1Info.userId = child1_userId;

        string jsonData = JsonUtility.ToJson(child1Info, true);
        byte[] sendData = Encoding.UTF8.GetBytes(jsonData);
        requester.jsonText = jsonData;
        requester.body = sendData;

        print(requester.jsonText);

        requester.onComplete = OnCompleteConnectChild1;
        requester.onError = OnFailConnectChild1;

        HttpManager.Instance.SendRequest(requester);
    }

    private void OnCompleteConnectChild1(DownloadHandler handler)
    {
        print("자녀계정 1번 접속 완료");

        // 자녀계정 1번 토큰
        JSONNode node = JSON.Parse(handler.text);
        TokenManager.Instance.token = node["results"]["childInfo"]["accessToken"];
        print("자녀계정 1번 토큰 " + node["results"]["childInfo"]["accessToken"]);

        // 자녀계정 1번 ID
        TokenManager.Instance.childId = node["results"]["childInfo"]["id"];
        print("자녀계정 1번 ID " + node["results"]["childInfo"]["id"]);

        // 자녀계정 1번 얼굴형
        TokenManager.Instance.fileName = node["results"]["childInfo"]["character"]["fileName"];
        print("자녀계정 1번 얼굴형 " + node["results"]["childInfo"]["character"]["fileName"]);

        PhotonNetwork.LoadLevel(2);
    }

    private void OnFailConnectChild1()
    {
        print("자녀계정 1번 접속 실패");
    }
    #endregion

    #region 자녀계정 2번 접속
    // 자녀계정 접속 버튼(이미지 패널)
    public void OnClickConnectChild2()
    {
        HttpRequester requester = new HttpRequester();

        //requester.url = "http://3.38.39.121:8080/user/child/connect";
        requester.url = "http://purpleprint-pillokallo.ap-northeast-2.elasticbeanstalk.com/user/child/connect";
        requester.requestType = RequestType.POST;

        // **** 여기서 childName만 넘겨주면 된다는건가,,?
        ChildInfo child2Info = new ChildInfo();
        child2Info.childId = child2_childId;
        child2Info.childName = child2_childName;
        child2Info.connectNum = child2_connectNum;
        child2Info.givenHeart = child2_givenHeart;
        child2Info.grantHeart = child2_grantHeart;
        child2Info.userId = child2_userId;

        string jsonData = JsonUtility.ToJson(child2Info, true);
        byte[] sendData = Encoding.UTF8.GetBytes(jsonData);
        requester.jsonText = jsonData;
        requester.body = sendData;

        print(requester.jsonText);

        requester.onComplete = OnCompleteConnectChild2;
        requester.onError = OnFailConnectChild2;

        HttpManager.Instance.SendRequest(requester);
    }

    private void OnCompleteConnectChild2(DownloadHandler handler)
    {
        print("자녀계정 2번 접속 완료");

        // 자녀계정 2번 토큰
        JSONNode node = JSON.Parse(handler.text);
        TokenManager.Instance.token = node["results"]["childInfo"]["accessToken"];
        print("자녀계정 2번 토큰 " + node["results"]["childInfo"]["accessToken"]);

        // 자녀계정 2번 ID
        TokenManager.Instance.childId = node["results"]["childInfo"]["id"];
        print("자녀계정 2번 ID " + node["results"]["childInfo"]["id"]);

        // 자녀계정 2번 얼굴형
        TokenManager.Instance.fileName = node["results"]["childInfo"]["character"]["fileName"];
        print("자녀계정 2번 얼굴형 " + node["results"]["childInfo"]["character"]["fileName"]);

        PhotonNetwork.LoadLevel(2);
    }

    private void OnFailConnectChild2()
    {
        print("자녀계정 2번 접속 실패");
    }
    #endregion

    #region 자녀계정 1번 생성
    public void OnClickCreateChild1()
    {
        HttpRequester requester = new HttpRequester();

        //requester.url = "http://3.38.39.121:8080/user/child";
        requester.url = "http://purpleprint-pillokallo.ap-northeast-2.elasticbeanstalk.com/user/child";
        requester.requestType = RequestType.POST;

        // 자녀계정 생성 시에는 childName만 설정하면 된다
        ChildName child1Name = new ChildName();
        child1Name.childName = inputChild1Name.text;

        string jsonData = JsonUtility.ToJson(child1Name, true);
        byte[] sendData = Encoding.UTF8.GetBytes(jsonData);
        requester.jsonText = jsonData;
        requester.body = sendData;

        print(requester.jsonText);

        requester.onComplete = OnCompleteCreateChild1;
        requester.onError = OnFailCreateChild1;

        HttpManager.Instance.SendRequest(requester);
    }

    private void OnCompleteCreateChild1(DownloadHandler handler)
    {
        print("자녀계정 1번 생성 완료");

        // response 파싱해서 접속 시 사용
        JSONNode node = JSON.Parse(handler.text);
        child1_childId = node["results"]["childInfo"][0]["id"];
        child1_childName = node["results"]["childInfo"][0]["name"];
        child1_connectNum = node["results"]["childInfo"][0]["connectNum"];
        child1_givenHeart = node["results"]["childInfo"][0]["givenHeart"];
        child1_grantHeart = node["results"]["childInfo"][0]["grantHeart"];
        child1_userId = node["results"]["childInfo"][0]["userId"];

        // 생성 UI 접고, 접속 UI 표시
        addChild1.SetActive(false);
        child1.SetActive(true);
        child1Name.text = node["results"]["childInfo"][0]["name"];
    }

    private void OnFailCreateChild1()
    {
        print("자녀계정 1번 생성 실패");
    }
    #endregion

    #region 자녀계정 2번 생성
    public void OnClickCreateChild2()
    {
        HttpRequester requester = new HttpRequester();

        //requester.url = "http://3.38.39.121:8080/user/child";
        requester.url = "http://purpleprint-pillokallo.ap-northeast-2.elasticbeanstalk.com/user/child";
        requester.requestType = RequestType.POST;

        // 자녀계정 생성 시에는 childName만 설정하면 된다
        ChildName child2Name = new ChildName();
        child2Name.childName = inputChild2Name.text;

        string jsonData = JsonUtility.ToJson(child2Name, true);
        byte[] sendData = Encoding.UTF8.GetBytes(jsonData);
        requester.jsonText = jsonData;
        requester.body = sendData;

        print(requester.jsonText);

        requester.onComplete = OnCompleteCreateChild2;
        requester.onError = OnFailCreateChild2;

        HttpManager.Instance.SendRequest(requester);
    }

    private void OnCompleteCreateChild2(DownloadHandler handler)
    {
        print("자녀계정 2번 생성 완료");

        // response 파싱해서 접속 시 사용
        JSONNode node = JSON.Parse(handler.text);
        child2_childId = node["results"]["childInfo"][1]["id"];
        child2_childName = node["results"]["childInfo"][1]["name"];
        child2_connectNum = node["results"]["childInfo"][1]["connectNum"];
        child2_givenHeart = node["results"]["childInfo"][1]["givenHeart"];
        child2_grantHeart = node["results"]["childInfo"][1]["grantHeart"];
        child2_userId = node["results"]["childInfo"][1]["userId"];

        // 생성 UI 접고, 접속 UI 표시
        addChild2.SetActive(false);
        child2.SetActive(true);
        child2Name.text = node["results"]["childInfo"][1]["name"];
    }

    private void OnFailCreateChild2()
    {
        print("자녀계정 2번 생성 실패");
    }
    #endregion

    // 자녀계정 생성 & 접속 형식
    // 생성 시 지정해줘야 하는 것은 childName
    // ****** 생성 & 접속 모두 헤더에 토큰 정보 같이 보내줘야함
    public class ChildInfo
    {
        public int childId;
        public string childName;
        public int connectNum;
        public int givenHeart;
        public int grantHeart;
        public int userId;
    }

    // 자녀계정 생성 시에는 childName만 설정하면 된다
    public class ChildName
    {
        public string childName;
    }
}
