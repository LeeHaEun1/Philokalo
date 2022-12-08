using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LHE_SignupSceneManager : MonoBehaviour
{
    [Header("Canvas Group")]
    public CanvasGroup signupGroup;
    public CanvasGroup loginGroup;
    public CanvasGroup childGroup;

    [Header("Canvas")]
    public Canvas signupCanvas;
    public Canvas loginCanvas;
    public Canvas childCanvas;

    [Header("LoginCanvas")]
    public InputField inputLoginID;
    public InputField inputLoginPW;
    public Button loginBtn;
    public Text loginResult;
    public Button btnToChildManager;

    // Start is called before the first frame update
    void Start()
    {
        signupGroup.enabled = true;
        signupCanvas.enabled = true;

        loginGroup.enabled = false;
        loginCanvas.enabled = false;
        childGroup.enabled = false;
        childCanvas.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Login Canvas에서 ID 입력 후 Tab키로 PW칸으로 넘어가기
        if(inputLoginID.isFocused && Input.GetKeyDown(KeyCode.Tab))
        {
            inputLoginPW.ActivateInputField();
        }

        // Login Canvas의 Login 버튼 활성화
        if (inputLoginID.text != "" && inputLoginPW.text != "")
        {
            loginBtn.interactable = true;
        }

        // 로그인 성공 시 자녀계정 관리 버튼 활성화
        if(loginResult.text == "로그인 성공")
        {
            btnToChildManager.interactable = true;
        }
    }

    // Signup -> Login 버튼
    public void ToLoginCanvas()
    {
        signupGroup.enabled = false;
        signupCanvas.enabled = false;

        loginGroup.enabled = true;
        loginCanvas.enabled = true;
    }

    // 로그인 성공 후 자녀계정 관리 페이지로 이동
    public void ToChildCanvas()
    {
        loginGroup.enabled = false;
        loginCanvas.enabled = false;

        childGroup.enabled = true;
        childCanvas.enabled = true;
    }

    // 자녀계정 선택 시 LobbyScene으로 이동
    public void ToLobbyScene()
    {
        PhotonNetwork.LoadLevel(2);
    }
}
