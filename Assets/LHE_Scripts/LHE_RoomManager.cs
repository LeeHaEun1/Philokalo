using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 1. �ؽ�Ʈ ä���г� ����
// 2. ���� ä�� ����

public class LHE_RoomManager : MonoBehaviour
{
    // Scroll View, Inputfield ��Ʈ
    public GameObject currChattingPanel;

    public GameObject worldChattingPanel;

    // Start is called before the first frame update
    void Start()
    {
        worldChattingPanel = GameObject.Find("ChattingPanel_World");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            // World�� ä���г� ����
            LHE_CurrentInteractableChatManager.Instance.currTextChat.Remove(worldChattingPanel);

            // �÷��̾��� ��ȣ�ۿ� ���� ä���г� ����Ʈ�� ���� �� ä���г� �߰�
            LHE_CurrentInteractableChatManager.Instance.currTextChat.Add(currChattingPanel);

            // World ä���г� �������·� �� ���� �ڵ����� �г� �ݾ��ֱ�
            if (worldChattingPanel.activeSelf == true)
            {
                worldChattingPanel.SetActive(false);
                LHE_CurrentInteractableChatManager.Instance.isTextChatOpen = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            // �÷��̾��� ��ȣ�ۿ� ���� ä���г� ����Ʈ���� ���� �� ä���г� ����
            LHE_CurrentInteractableChatManager.Instance.currTextChat.Remove(currChattingPanel);

            // World�� ä���г� �߰�
            LHE_CurrentInteractableChatManager.Instance.currTextChat.Add(worldChattingPanel);

            // �� ä���г� �������·� �� ������ �ڵ����� �г� �ݾ��ֱ�
            if(currChattingPanel.activeSelf == true)
            {
                currChattingPanel.SetActive(false);
                LHE_CurrentInteractableChatManager.Instance.isTextChatOpen = false;
            }
        }
    }
}
