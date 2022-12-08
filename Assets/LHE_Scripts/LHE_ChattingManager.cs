using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Photon.Pun;

public class LHE_ChattingManager : MonoBehaviourPun
{
    // [Panel]
    // ChatItem ����
    public GameObject chatItemFactory;
    // InputChat
    public InputField inputChat;
    // ScreenView�� Content Transform
    public RectTransform trContent;

    // [Bubble]
    public GameObject chatBubbleFactory;
    Transform bubbleCanvas;


    // ���� �г��� ����
    Color nickColor;

    // Start is called before the first frame update
    void Start()
    {
        // inputChat���� ���͸� ������ �� ȣ��Ǵ� �Լ� ���
        inputChat.onSubmit.AddListener(OnSubmit);

        nickColor = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));


        // ��ǳ���� �θ� ĵ������ Transform
        // ********* �̸����� ã�Ƽ� ������ ���°ǰ�... ��ǳ���� �ٸ� ĳ���� �Ӹ� ���� �����
        // Singleton���� ����� GameManager���� �����ñ�?
        bubbleCanvas = GameObject.Find("Canvas_TextBubble").transform;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    string myNickname;
    //string chatText;

    // inputChat���� ���͸� ������ �� ȣ��Ǵ� �Լ�
    public void OnSubmit(string s)
    {
        //myNickname = PhotonNetwork.NickName;

        // Html ������ ���� ����
        // <color=#FF0000>�г���</color>
        // �г����� �տ� �ٿ��� ���� ������
        string chatText = "<color=#" + ColorUtility.ToHtmlStringRGB(nickColor) + ">" + PhotonNetwork.NickName + "</color>" + " : " + s;

        //GameObject bubble = Instantiate(chatBubbleFactory, bubbleCanvas);
        //LHE_ChatBubble bubbleContent = bubble.GetComponent<LHE_ChatBubble>();
        //bubbleContent.SetText(chatText);


        // 1. �ۼ��ϴ� ���͸� ġ�� RPC�Լ� ȣ��
        photonView.RPC("RpcAddChat", RpcTarget.All, chatText); // ���� ����� ������ ��� ��

        // 4. InputField ���� �ʱ�ȭ => �̰� ����ȭ�Ǹ� �ȵȴ�
        inputChat.text = "";

        // 5. InputChat�� ����(��Ȱ��ȭ, Ŀ�� ��ġ)�ǵ����Ѵ� => �̰� ����ȭ�Ǹ� �ȵȴ�
        inputChat.ActivateInputField();

        // *********** RPC �Լ��� �̵�
        //// 2. ChatItem�� �ϳ� �����(�θ�� ScrollView�� Content��)
        //GameObject item = Instantiate(chatItemFactory, trContent);

        //// 3. Text ������Ʈ �����ͼ� InputField�� ������ ����
        //Text t = item.GetComponent<Text>();
        //t.text = inputChat.text;

        //// 4. InputField ���� �ʱ�ȭ
        //inputChat.text = "";

        //// 5. InputChat�� ����(��Ȱ��ȭ, Ŀ�� ��ġ)�ǵ����Ѵ�
        //inputChat.ActivateInputField();

        // [��ǳ�� �θ� ����]
        // ä�øŴ����� �̸��� ��ȯ�Ѵ�
        //print("photonView.gameObject.name " + photonView.gameObject.name);
        //print("photonView.Owner.UserId " + photonView.Owner.UserId);
        //print("PhotonNetwork.NickName " + PhotonNetwork.NickName); // ���� �г��� ��ȯ
    }


    public RectTransform rtScrollView;
    float prevContentH;

    [PunRPC]
    void RpcAddChat(string chat)
    {
        // [��ǳ��]
        // ĳ������ Canvas_TextBubble ������ ��ǳ�� ����
        // �۽����� photonView�� ���� ĳ���͸� ã�Ƽ�
        //GameObject bubble = Instantiate(chatBubbleFactory, LHE_GameManager.Instance.bubbleCanvas);
        //GameObject bubble = Instantiate(chatBubbleFactory, PhotonView.Find(photonView.ViewID).gameObject.transform.Find("Canvas_TextBubble"));
        //GameObject bubble = Instantiate(chatBubbleFactory, LHE_GameManager.Instance.characterList[myNickname].transform.Find("Canvas_TextBubble"));
        //print("LHE_GameManager.Instance.characterList[myNickname] "+LHE_GameManager.Instance.characterList[myNickname].name);

        //GameObject bubble = PhotonNetwork.Instantiate("TextBubble", bubbleCanvas.position, bubbleCanvas.rotation);
        //bubble.transform.parent = bubbleCanvas;

        //LHE_ChatBubble bubbleContent = bubble.GetComponent<LHE_ChatBubble>();
        //bubbleContent.SetText(chat);


        // [�г�ä��]
        // Contents�� ���� H�� ����
        prevContentH = trContent.sizeDelta.y;

        // 2. ChatItem�� �ϳ� �����(�θ�� ScrollView�� Content��)
        GameObject item = Instantiate(chatItemFactory, trContent);

        // 3. Text ������Ʈ �����ͼ� InputField�� ������ ����
        //Text t = item.GetComponent<Text>();
        //t.text = chat;
        LHE_ChattingItem chatItem = item.GetComponent<LHE_ChattingItem>();
        chatItem.SetText(chat);

        // 4. ������ ä��â�� �� ���� ���� �־��ٸ�
        StartCoroutine(AutoScrollBottom());
    }

    // �� �������� ���� ��������� ���� contentH���� ���̰� �����
    IEnumerator AutoScrollBottom()
    {
        yield return null;

        // ScrollView H�� contentH���� Ŭ ���� ����(ä��â�� �� ���� ������ ���� ���ʿ�)
        if (trContent.sizeDelta.y > rtScrollView.sizeDelta.y)
        {
            // Content�� y�� >= (����Ǳ� ���� Content�� Height - ��ũ�Ѻ��� Height)
            if (trContent.anchoredPosition.y >= prevContentH - rtScrollView.sizeDelta.y)
            {
                // 5. �߰��� ���̸�ŭ Content�� y���� �����ϰڴ�
                trContent.anchoredPosition = new Vector2(0, trContent.sizeDelta.y - rtScrollView.sizeDelta.y);
            }
        }
    }
}
