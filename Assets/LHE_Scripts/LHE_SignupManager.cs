using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;
using System.Text;
using SimpleJSON;

public class LHE_SignupManager : MonoBehaviour
{
    [Header("Email Verification")]
    public InputField inputEmail;
    public InputField inputCode;
    public string verifCode;
    public Text emailSendResult;
    public Text verifCodeCheckResult;

    [Header("ID Duplicaton Check")]
    public InputField inputID;
    public string idCheckResult;
    public Text idDuplicationResult;

    [Header("Sign Up")]
    public Button btnSignup;
    public InputField inputName;
    public InputField inputPassword;
    public Text signupResult;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // 모든 값이 입력되었을 때 가입하기 버튼 활성화
        if (inputName.text != "" && inputID.text != "" && inputPassword.text != "" && inputEmail.text != "" && inputCode.text != "")
        {
            btnSignup.interactable = true;
        }
    }

    #region 이메일 인증 & 인증코드 확인
    // 이메일로 인증번호 전송 요청
    public void OnClickSendCode()
    {
        HttpRequester requester = new HttpRequester();

        //requester.url = "http://3.38.39.121:8080/auth/email";
        requester.url = "http://purpleprint-pillokallo.ap-northeast-2.elasticbeanstalk.com/auth/email";
        requester.requestType = RequestType.POST;

        EmailForCode emailForCode = new EmailForCode();
        emailForCode.email = inputEmail.text;

        string jsonData = JsonUtility.ToJson(emailForCode, true);
        byte[] sendData = Encoding.UTF8.GetBytes(jsonData);
        requester.jsonText = jsonData;
        requester.body = sendData;

        //print("requester.body " + requester.body);
        print(requester.jsonText);

        requester.onComplete = OnCompleteSendCode;
        requester.onError = OnFailSendCode;

        HttpManager.Instance.SendRequest(requester);

        // ai 예시
        //Packet_Login_Result result = JsonUtility.FromJson<Packet_Login_Result>(requester.jsonText);

        //Debug.Log(result.results["userInfo"]["id"]);
    }


    public void OnCompleteSendCode(DownloadHandler handler)
    {
        print("인증번호 전송 완료");
        emailSendResult.text = "인증번호 전송 완료";
        emailSendResult.color = new Color(0.96f, 0.62f, 0, 1);

        print("22222222222222222 " + handler.text);

        EmailForCodeResponse emailForCodeResponse = JsonUtility.FromJson<EmailForCodeResponse>(handler.text);
        print("44444444 " + JsonUtility.ToJson(emailForCodeResponse, true)); // -> results가 빠져있다!!!

        //// NullReferenceException: Object reference not set to an instance of an object
        //print("인증번호 " + emailForCodeResponse.results["certCode"]);

        JSONNode node = JSON.Parse(handler.text);
        print("nnnnnnnnnnn " + node["results"]["certCode"]);
        verifCode = node["results"]["certCode"];
        print("vvvvvvv " + verifCode);
    }

    private void OnFailSendCode()
    {
        emailSendResult.text = "인증번호 전송 실패";
        emailSendResult.color = Color.red;
    }

    // 이메일로 인증번호 발송
    public class EmailForCode
    {
        public string email;
    }

    // 이메일로 인증번호 발송 시 서버 응답
    [Serializable]
    public class EmailForCodeResponse
    {
        public int httpStatus;
        public string message;
        public Dictionary<string, int> results;
    }

    // ai
    //public class Packet_Login_Result
    //{
    //    public int httpStatus;
    //    public string message;
    //    public Dictionary<string, Dictionary<string, string>> results;
    //}

    // 인증번호 일치 여부 확인
    public void CheckInputCode()
    {
        if (inputCode.text == verifCode)
        {
            print("인증번호 일치");
            verifCodeCheckResult.text = "인증번호 일치";
            verifCodeCheckResult.color = new Color(0.96f, 0.62f, 0, 1);
        }
        else
        {
            verifCodeCheckResult.text = "인증번호 불일치";
            verifCodeCheckResult.color = Color.red;
        }
    }
    #endregion

    #region 아이디 중복 체크
    // 아이디 중복 체크 요청
    public void OnClickCheckID()
    {
        HttpRequester requester = new HttpRequester();

        //requester.url = "http://3.38.39.121:8080/auth/id-check";
        requester.url = "http://purpleprint-pillokallo.ap-northeast-2.elasticbeanstalk.com/auth/id-check";
        requester.requestType = RequestType.POST;

        CheckIDAvailability checkIDAvailability = new CheckIDAvailability();
        checkIDAvailability.username = inputID.text;

        string jsonData = JsonUtility.ToJson(checkIDAvailability, true);
        byte[] sendData = Encoding.UTF8.GetBytes(jsonData);
        requester.jsonText = jsonData;
        requester.body = sendData;

        print(requester.jsonText);

        requester.onComplete = OnCompleteCheckID;
        requester.onError = OnErrorChangeID;

        HttpManager.Instance.SendRequest(requester);
    }


    private void OnCompleteCheckID(DownloadHandler handler)
    {
        print("아이디 중복 체크 완료");

        JSONNode node = JSON.Parse(handler.text);
        idCheckResult = node["httpStatus"];
        print("idCheckResult " + idCheckResult);

        if (idCheckResult == "200")
        {
            idDuplicationResult.text = "사용 가능한 아이디입니다.";
            idDuplicationResult.color = new Color(0.96f, 0.62f, 0, 1);
        }
        // protocol error로 나와서 아마 이렇게 하면 안되긴 할듯,,?
        //else if(idCheckResult == "400")
        //{
        //    idDuplicationResult.text = "아이디 사용 불가";
        //}
    }

    private void OnErrorChangeID()
    {
        idDuplicationResult.text = "다른 이용자가 사용 중인 아이디입니다.";
        idDuplicationResult.color = Color.red;
    }

    // 아이디 중복 체크
    [Serializable]
    public class CheckIDAvailability
    {
        public string username;
    }
    #endregion

    #region 입력된 값들로 회원가입 진행
    // 모든 값이 입력되었을 때 가입하기 버튼 활성화(Update에서) -> 가입하기 버튼에 해당 기능 연결
    public void OnClickSignUp()
    {
        HttpRequester requester = new HttpRequester();

        //requester.url = "http://3.38.39.121:8080/auth/signup";
        requester.url = "http://purpleprint-pillokallo.ap-northeast-2.elasticbeanstalk.com/auth/signup";
        requester.requestType = RequestType.POST;

        SignupForm signupForm = new SignupForm();
        signupForm.email = inputEmail.text;
        signupForm.name = inputName.text;
        signupForm.password = inputPassword.text;
        signupForm.username = inputID.text;

        string jsonData = JsonUtility.ToJson(signupForm, true);
        byte[] sendData = Encoding.UTF8.GetBytes(jsonData);
        requester.jsonText = jsonData;
        requester.body = sendData;

        print(requester.jsonText);

        requester.onComplete = OnCompleteSignup;
        requester.onError = OnErrorSignup;

        HttpManager.Instance.SendRequest(requester);
    }


    public void OnCompleteSignup(DownloadHandler handler)
    {
        print("회원가입 완료");
        signupResult.text = "회원가입 완료";
        signupResult.color = new Color(0.96f, 0.62f, 0, 1);
    }
    private void OnErrorSignup()
    {
        print("회원가입 실패");
        signupResult.text = "회원가입 실패";
        signupResult.color = Color.red;
    }

    // 회원가입 정보
    [Serializable]
    public class SignupForm
    {
        public string email;
        public string name;
        public string password;
        public string username;
    }

    #endregion
}