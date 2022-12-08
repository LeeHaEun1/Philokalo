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
        // Login Canvas���� ID �Է� �� TabŰ�� PWĭ���� �Ѿ��
        if(inputLoginID.isFocused && Input.GetKeyDown(KeyCode.Tab))
        {
            inputLoginPW.ActivateInputField();
        }

        // Login Canvas�� Login ��ư Ȱ��ȭ
        if (inputLoginID.text != "" && inputLoginPW.text != "")
        {
            loginBtn.interactable = true;
        }

        // �α��� ���� �� �ڳ���� ���� ��ư Ȱ��ȭ
        if(loginResult.text == "�α��� ����")
        {
            btnToChildManager.interactable = true;
        }
    }

    // Signup -> Login ��ư
    public void ToLoginCanvas()
    {
        signupGroup.enabled = false;
        signupCanvas.enabled = false;

        loginGroup.enabled = true;
        loginCanvas.enabled = true;
    }

    // �α��� ���� �� �ڳ���� ���� �������� �̵�
    public void ToChildCanvas()
    {
        loginGroup.enabled = false;
        loginCanvas.enabled = false;

        childGroup.enabled = true;
        childCanvas.enabled = true;
    }

    // �ڳ���� ���� �� LobbyScene���� �̵�
    public void ToLobbyScene()
    {
        PhotonNetwork.LoadLevel(2);
    }
}
