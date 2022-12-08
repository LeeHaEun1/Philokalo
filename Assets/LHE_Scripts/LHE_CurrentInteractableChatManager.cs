using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 현재 상호작용 가능한 Text 및 Voice 채팅 대상을 담는 리스트
public class LHE_CurrentInteractableChatManager : MonoBehaviour
{
    public static LHE_CurrentInteractableChatManager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public List<GameObject> currTextChat = new List<GameObject>();

    public bool isTextChatOpen = false;

    // 현재 상호작용 가능한 채팅패널의 focus 여부 관리
    public bool isTextChatFocused = false;
    InputField currTextChatInputfield;

    // 말풍선 Mute
    public bool muteBubble = false;
    public Image muteBubbleImage;

    // Start is called before the first frame update
    void Start()
    {
        // 월드 채팅패널 추가
        currTextChat.Add(GameObject.Find("ChattingPanel_World"));

        // 현재 상호작용 가능한 채팅패널(World 채팅패널)의 focus 여부 판단
        currTextChatInputfield = currTextChat[0].GetComponentInChildren<InputField>();
        isTextChatFocused = currTextChatInputfield.isFocused;

        isTextChatOpen = true;
        //currTextChat[0].SetActive(false); -> mychatbubble에서 동적으로 assign하기 위해 삭제
    }

    // Update is called once per frame
    void Update()
    {
        // 현재 상호작용 가능한 채팅패널의 focus 여부 판단
        currTextChatInputfield = currTextChat[0].GetComponentInChildren<InputField>();
        isTextChatFocused = currTextChatInputfield.isFocused;

        // 'C' key를 이용해 채팅패널 Open/Close
        if (Input.GetKeyDown(KeyCode.C))
        {
            // Close 상태라면 Open
            if (isTextChatOpen == false)
            {
                currTextChat[0].SetActive(true);
                isTextChatOpen = true;
            }
            // Open 상태이며 && Inputfield가 focused 상태가 아니라면 Close
            else if (isTextChatOpen == true && !isTextChatFocused)
            {
                currTextChat[0].SetActive(false);
                isTextChatOpen = false;
            }
        }
    }

    // 버튼 이용해서 채팅 패널 on/off하는 함수
    public void TextChatActivate()
    {
        // Close 상태라면 Open
        if (isTextChatOpen == false)
        {
            currTextChat[0].SetActive(true);
            isTextChatOpen = true;
        }
        // Open 상태이며 && Inputfield가 focused 상태가 아니라면 Close
        else if (isTextChatOpen == true && !isTextChatFocused)
        {
            currTextChat[0].SetActive(false);
            isTextChatOpen = false;
        }
    }

    public void MuteBubble()
    {
        if (muteBubble == false)
        {
            muteBubble = true;
            muteBubbleImage.color = new Color(1, 1, 1, 0.4f);
        }
        else if (muteBubble == true)
        {
            muteBubble = false;
            muteBubbleImage.color = new Color(1, 1, 1, 1);
        }
    }
}
