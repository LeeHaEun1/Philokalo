using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ���� ��ȣ�ۿ� ������ Text �� Voice ä�� ����� ��� ����Ʈ
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

    // ���� ��ȣ�ۿ� ������ ä���г��� focus ���� ����
    public bool isTextChatFocused = false;
    InputField currTextChatInputfield;

    // ��ǳ�� Mute
    public bool muteBubble = false;
    public Image muteBubbleImage;

    // Start is called before the first frame update
    void Start()
    {
        // ���� ä���г� �߰�
        currTextChat.Add(GameObject.Find("ChattingPanel_World"));

        // ���� ��ȣ�ۿ� ������ ä���г�(World ä���г�)�� focus ���� �Ǵ�
        currTextChatInputfield = currTextChat[0].GetComponentInChildren<InputField>();
        isTextChatFocused = currTextChatInputfield.isFocused;

        isTextChatOpen = true;
        //currTextChat[0].SetActive(false); -> mychatbubble���� �������� assign�ϱ� ���� ����
    }

    // Update is called once per frame
    void Update()
    {
        // ���� ��ȣ�ۿ� ������ ä���г��� focus ���� �Ǵ�
        currTextChatInputfield = currTextChat[0].GetComponentInChildren<InputField>();
        isTextChatFocused = currTextChatInputfield.isFocused;

        // 'C' key�� �̿��� ä���г� Open/Close
        if (Input.GetKeyDown(KeyCode.C))
        {
            // Close ���¶�� Open
            if (isTextChatOpen == false)
            {
                currTextChat[0].SetActive(true);
                isTextChatOpen = true;
            }
            // Open �����̸� && Inputfield�� focused ���°� �ƴ϶�� Close
            else if (isTextChatOpen == true && !isTextChatFocused)
            {
                currTextChat[0].SetActive(false);
                isTextChatOpen = false;
            }
        }
    }

    // ��ư �̿��ؼ� ä�� �г� on/off�ϴ� �Լ�
    public void TextChatActivate()
    {
        // Close ���¶�� Open
        if (isTextChatOpen == false)
        {
            currTextChat[0].SetActive(true);
            isTextChatOpen = true;
        }
        // Open �����̸� && Inputfield�� focused ���°� �ƴ϶�� Close
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
