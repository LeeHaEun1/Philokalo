using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Text;
using UnityEngine.Networking;
using SimpleJSON;

public class LHE_LoginManager : MonoBehaviour
{
    [Header("Login")]
    public InputField inputUsername;
    public InputField inputPassword;
    public Text loginResult;

    string token;

    public Button btnToChildPage;

    [Header("Child Account")]
    int currChildCount;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    #region 로그인
    public void OnClickTryLogin()
    {
        HttpRequester requester = new HttpRequester();

        //requester.url = "http://3.38.39.121:8080/auth/login";
        requester.url = "http://purpleprint-pillokallo.ap-northeast-2.elasticbeanstalk.com/auth/login";
        requester.requestType = RequestType.POST;

        LoginIDPW loginIDPW = new LoginIDPW();
        loginIDPW.username = inputUsername.text;
        loginIDPW.password = inputPassword.text;

        string jsonData = JsonUtility.ToJson(loginIDPW, true);
        byte[] sendData = Encoding.UTF8.GetBytes(jsonData);
        requester.jsonText = jsonData;
        requester.body = sendData;

        print(requester.jsonText);

        requester.onComplete = OnCompleteTryLogin;
        requester.onError = OnFailLogin;

        HttpManager.Instance.SendRequest(requester);

        // ai 예시
        //Packet_Login_Result result = JsonUtility.FromJson<Packet_Login_Result>(requester.jsonText);

        //Debug.Log(result.results["userInfo"]["id"]);
    }


    public void OnCompleteTryLogin(DownloadHandler handler)
    {
        print("로그인 완료");
        loginResult.text = "로그인 성공";
        //loginResult.color = Color.blue;
        loginResult.color = new Color(0.96f, 0.62f, 0, 1);

        JSONNode node = JSON.Parse(handler.text);

        // 자녀계정 개수
        print("자녀계정 개수 " + node["results"]["userInfo"]["childList"].Count);
        currChildCount = node["results"]["userInfo"]["childList"].Count;
        LHE_ChildAccountManager.Instance.currChildCount = currChildCount;
        // 자녀계정 캔버스의 초기 UI 세팅
        LHE_ChildAccountManager.Instance.SetStartUI(currChildCount);

        // 자녀계정 정보
        //print("자녀계정 0번 정보 " + node["results"]["userInfo"]["childList"][0]); // 안되네,,일일히 해줘야하는건가,, count를 먼저세고 해당 개수만큼 for문 돌려주면 되겠다

        if(currChildCount == 1)
        {
            LHE_ChildAccountManager.Instance.child1_childId = node["results"]["userInfo"]["childList"][0]["childId"];
            LHE_ChildAccountManager.Instance.child1_childName = node["results"]["userInfo"]["childList"][0]["childName"];
            LHE_ChildAccountManager.Instance.child1_connectNum = node["results"]["userInfo"]["childList"][0]["connectNum"];
            LHE_ChildAccountManager.Instance.child1_grantHeart = node["results"]["userInfo"]["childList"][0]["grantHeart"];
            LHE_ChildAccountManager.Instance.child1_givenHeart = node["results"]["userInfo"]["childList"][0]["givenHeart"];
            LHE_ChildAccountManager.Instance.child1_userId = node["results"]["userInfo"]["childList"][0]["userId"];

            // 자녀계정 이름
            LHE_ChildAccountManager.Instance.child1Name.text = node["results"]["userInfo"]["childList"][0]["childName"];
        }
        else if(currChildCount == 2)
        {
            LHE_ChildAccountManager.Instance.child1_childId = node["results"]["userInfo"]["childList"][0]["childId"];
            LHE_ChildAccountManager.Instance.child1_childName = node["results"]["userInfo"]["childList"][0]["childName"];
            LHE_ChildAccountManager.Instance.child1_connectNum = node["results"]["userInfo"]["childList"][0]["connectNum"];
            LHE_ChildAccountManager.Instance.child1_grantHeart = node["results"]["userInfo"]["childList"][0]["grantHeart"];
            LHE_ChildAccountManager.Instance.child1_givenHeart = node["results"]["userInfo"]["childList"][0]["givenHeart"];
            LHE_ChildAccountManager.Instance.child1_userId = node["results"]["userInfo"]["childList"][0]["userId"];

            LHE_ChildAccountManager.Instance.child2_childId = node["results"]["userInfo"]["childList"][1]["childId"];
            LHE_ChildAccountManager.Instance.child2_childName = node["results"]["userInfo"]["childList"][1]["childName"];
            LHE_ChildAccountManager.Instance.child2_connectNum = node["results"]["userInfo"]["childList"][1]["connectNum"];
            LHE_ChildAccountManager.Instance.child2_grantHeart = node["results"]["userInfo"]["childList"][1]["grantHeart"];
            LHE_ChildAccountManager.Instance.child2_givenHeart = node["results"]["userInfo"]["childList"][1]["givenHeart"];
            LHE_ChildAccountManager.Instance.child2_userId = node["results"]["userInfo"]["childList"][1]["userId"];

            // 자녀계정 이름
            LHE_ChildAccountManager.Instance.child1Name.text = node["results"]["userInfo"]["childList"][0]["childName"]; 
            LHE_ChildAccountManager.Instance.child2Name.text = node["results"]["userInfo"]["childList"][1]["childName"];
        }
        

        // 부모 토큰 저장
        print("토큰 " + node["results"]["userInfo"]["accessToken"]);
        TokenManager.Instance.token = node["results"]["userInfo"]["accessToken"];
    }

    private void OnFailLogin()
    {
        loginResult.text = "아이디, 비밀번호를 확인해주세요.";
        loginResult.color = Color.red;
    }

    [Serializable]
    public class LoginIDPW
    {
        public string username;
        public string password;
    }

    //public class Packet_Login_Result
    //{
    //    public int httpStatus;
    //    public string message;
    //    public Dictionary<string, Dictionary<string, string>> results;
    //}
    #endregion


    #region 자녀계정 관리

    #endregion
}
