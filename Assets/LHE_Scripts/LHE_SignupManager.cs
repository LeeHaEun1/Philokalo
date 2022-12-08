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
        // ��� ���� �ԷµǾ��� �� �����ϱ� ��ư Ȱ��ȭ
        if (inputName.text != "" && inputID.text != "" && inputPassword.text != "" && inputEmail.text != "" && inputCode.text != "")
        {
            btnSignup.interactable = true;
        }
    }

    #region �̸��� ���� & �����ڵ� Ȯ��
    // �̸��Ϸ� ������ȣ ���� ��û
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

        // ai ����
        //Packet_Login_Result result = JsonUtility.FromJson<Packet_Login_Result>(requester.jsonText);

        //Debug.Log(result.results["userInfo"]["id"]);
    }


    public void OnCompleteSendCode(DownloadHandler handler)
    {
        print("������ȣ ���� �Ϸ�");
        emailSendResult.text = "������ȣ ���� �Ϸ�";
        emailSendResult.color = new Color(0.96f, 0.62f, 0, 1);

        print("22222222222222222 " + handler.text);

        EmailForCodeResponse emailForCodeResponse = JsonUtility.FromJson<EmailForCodeResponse>(handler.text);
        print("44444444 " + JsonUtility.ToJson(emailForCodeResponse, true)); // -> results�� �����ִ�!!!

        //// NullReferenceException: Object reference not set to an instance of an object
        //print("������ȣ " + emailForCodeResponse.results["certCode"]);

        JSONNode node = JSON.Parse(handler.text);
        print("nnnnnnnnnnn " + node["results"]["certCode"]);
        verifCode = node["results"]["certCode"];
        print("vvvvvvv " + verifCode);
    }

    private void OnFailSendCode()
    {
        emailSendResult.text = "������ȣ ���� ����";
        emailSendResult.color = Color.red;
    }

    // �̸��Ϸ� ������ȣ �߼�
    public class EmailForCode
    {
        public string email;
    }

    // �̸��Ϸ� ������ȣ �߼� �� ���� ����
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

    // ������ȣ ��ġ ���� Ȯ��
    public void CheckInputCode()
    {
        if (inputCode.text == verifCode)
        {
            print("������ȣ ��ġ");
            verifCodeCheckResult.text = "������ȣ ��ġ";
            verifCodeCheckResult.color = new Color(0.96f, 0.62f, 0, 1);
        }
        else
        {
            verifCodeCheckResult.text = "������ȣ ����ġ";
            verifCodeCheckResult.color = Color.red;
        }
    }
    #endregion

    #region ���̵� �ߺ� üũ
    // ���̵� �ߺ� üũ ��û
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
        print("���̵� �ߺ� üũ �Ϸ�");

        JSONNode node = JSON.Parse(handler.text);
        idCheckResult = node["httpStatus"];
        print("idCheckResult " + idCheckResult);

        if (idCheckResult == "200")
        {
            idDuplicationResult.text = "��� ������ ���̵��Դϴ�.";
            idDuplicationResult.color = new Color(0.96f, 0.62f, 0, 1);
        }
        // protocol error�� ���ͼ� �Ƹ� �̷��� �ϸ� �ȵǱ� �ҵ�,,?
        //else if(idCheckResult == "400")
        //{
        //    idDuplicationResult.text = "���̵� ��� �Ұ�";
        //}
    }

    private void OnErrorChangeID()
    {
        idDuplicationResult.text = "�ٸ� �̿��ڰ� ��� ���� ���̵��Դϴ�.";
        idDuplicationResult.color = Color.red;
    }

    // ���̵� �ߺ� üũ
    [Serializable]
    public class CheckIDAvailability
    {
        public string username;
    }
    #endregion

    #region �Էµ� ����� ȸ������ ����
    // ��� ���� �ԷµǾ��� �� �����ϱ� ��ư Ȱ��ȭ(Update����) -> �����ϱ� ��ư�� �ش� ��� ����
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
        print("ȸ������ �Ϸ�");
        signupResult.text = "ȸ������ �Ϸ�";
        signupResult.color = new Color(0.96f, 0.62f, 0, 1);
    }
    private void OnErrorSignup()
    {
        print("ȸ������ ����");
        signupResult.text = "ȸ������ ����";
        signupResult.color = Color.red;
    }

    // ȸ������ ����
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