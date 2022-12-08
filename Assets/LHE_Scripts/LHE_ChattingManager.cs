using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Photon.Pun;

public class LHE_ChattingManager : MonoBehaviourPun
{
    // [Panel]
    // ChatItem 공장
    public GameObject chatItemFactory;
    // InputChat
    public InputField inputChat;
    // ScreenView의 Content Transform
    public RectTransform trContent;

    // [Bubble]
    public GameObject chatBubbleFactory;
    Transform bubbleCanvas;


    // 나의 닉네임 색상
    Color nickColor;

    // Start is called before the first frame update
    void Start()
    {
        // inputChat에서 엔터를 눌렀을 때 호출되는 함수 등록
        inputChat.onSubmit.AddListener(OnSubmit);

        nickColor = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));


        // 말풍선의 부모 캔버스의 Transform
        // ********* 이름으로 찾아서 오류가 나는건가... 말풍선이 다른 캐릭터 머리 위에 생긴다
        // Singleton으로 선언된 GameManager에서 가져올까?
        bubbleCanvas = GameObject.Find("Canvas_TextBubble").transform;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    string myNickname;
    //string chatText;

    // inputChat에서 엔터를 눌렀을 때 호출되는 함수
    public void OnSubmit(string s)
    {
        //myNickname = PhotonNetwork.NickName;

        // Html 구조로 구현 가능
        // <color=#FF0000>닉네임</color>
        // 닉네임을 앞에 붙여서 내용 보내기
        string chatText = "<color=#" + ColorUtility.ToHtmlStringRGB(nickColor) + ">" + PhotonNetwork.NickName + "</color>" + " : " + s;

        //GameObject bubble = Instantiate(chatBubbleFactory, bubbleCanvas);
        //LHE_ChatBubble bubbleContent = bubble.GetComponent<LHE_ChatBubble>();
        //bubbleContent.SetText(chatText);


        // 1. 작성하다 엔터를 치면 RPC함수 호출
        photonView.RPC("RpcAddChat", RpcTarget.All, chatText); // 쓰는 사람의 내용을 담는 것

        // 4. InputField 내용 초기화 => 이건 동기화되면 안된다
        inputChat.text = "";

        // 5. InputChat이 선택(≒활성화, 커서 위치)되도록한다 => 이건 동기화되면 안된다
        inputChat.ActivateInputField();

        // *********** RPC 함수로 이동
        //// 2. ChatItem을 하나 만든다(부모는 ScrollView의 Content로)
        //GameObject item = Instantiate(chatItemFactory, trContent);

        //// 3. Text 컴포넌트 가져와서 InputField의 내용을 세팅
        //Text t = item.GetComponent<Text>();
        //t.text = inputChat.text;

        //// 4. InputField 내용 초기화
        //inputChat.text = "";

        //// 5. InputChat이 선택(≒활성화, 커서 위치)되도록한다
        //inputChat.ActivateInputField();

        // [말풍선 부모 관련]
        // 채팅매니저의 이름을 반환한다
        //print("photonView.gameObject.name " + photonView.gameObject.name);
        //print("photonView.Owner.UserId " + photonView.Owner.UserId);
        //print("PhotonNetwork.NickName " + PhotonNetwork.NickName); // 본인 닉네임 반환
    }


    public RectTransform rtScrollView;
    float prevContentH;

    [PunRPC]
    void RpcAddChat(string chat)
    {
        // [말풍선]
        // 캐릭터의 Canvas_TextBubble 밑으로 말풍선 생성
        // 송신자의 photonView와 같은 캐릭터를 찾아서
        //GameObject bubble = Instantiate(chatBubbleFactory, LHE_GameManager.Instance.bubbleCanvas);
        //GameObject bubble = Instantiate(chatBubbleFactory, PhotonView.Find(photonView.ViewID).gameObject.transform.Find("Canvas_TextBubble"));
        //GameObject bubble = Instantiate(chatBubbleFactory, LHE_GameManager.Instance.characterList[myNickname].transform.Find("Canvas_TextBubble"));
        //print("LHE_GameManager.Instance.characterList[myNickname] "+LHE_GameManager.Instance.characterList[myNickname].name);

        //GameObject bubble = PhotonNetwork.Instantiate("TextBubble", bubbleCanvas.position, bubbleCanvas.rotation);
        //bubble.transform.parent = bubbleCanvas;

        //LHE_ChatBubble bubbleContent = bubble.GetComponent<LHE_ChatBubble>();
        //bubbleContent.SetText(chat);


        // [패널채팅]
        // Contents의 이전 H값 저장
        prevContentH = trContent.sizeDelta.y;

        // 2. ChatItem을 하나 만든다(부모는 ScrollView의 Content로)
        GameObject item = Instantiate(chatItemFactory, trContent);

        // 3. Text 컴포넌트 가져와서 InputField의 내용을 세팅
        //Text t = item.GetComponent<Text>();
        //t.text = chat;
        LHE_ChattingItem chatItem = item.GetComponent<LHE_ChattingItem>();
        chatItem.SetText(chat);

        // 4. 이전에 채팅창의 맨 밑을 보고 있었다면
        StartCoroutine(AutoScrollBottom());
    }

    // 한 프레임을 쉬고 실행해줘야 이전 contentH값과 차이가 생긴다
    IEnumerator AutoScrollBottom()
    {
        yield return null;

        // ScrollView H가 contentH보다 클 때만 실행(채팅창이 꽉 차기 전에는 실행 불필요)
        if (trContent.sizeDelta.y > rtScrollView.sizeDelta.y)
        {
            // Content의 y값 >= (변경되기 전의 Content의 Height - 스크롤뷰의 Height)
            if (trContent.anchoredPosition.y >= prevContentH - rtScrollView.sizeDelta.y)
            {
                // 5. 추가된 높이만큼 Content의 y값을 변경하겠다
                trContent.anchoredPosition = new Vector2(0, trContent.sizeDelta.y - rtScrollView.sizeDelta.y);
            }
        }
    }
}
