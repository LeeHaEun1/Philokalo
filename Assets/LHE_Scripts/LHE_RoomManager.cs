using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 1. 텍스트 채팅패널 관리
// 2. 음성 채팅 관리

public class LHE_RoomManager : MonoBehaviour
{
    // Scroll View, Inputfield 세트
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
            // World의 채팅패널 삭제
            LHE_CurrentInteractableChatManager.Instance.currTextChat.Remove(worldChattingPanel);

            // 플레이어의 상호작용 가능 채팅패널 리스트에 현재 방 채팅패널 추가
            LHE_CurrentInteractableChatManager.Instance.currTextChat.Add(currChattingPanel);

            // World 채팅패널 열린상태로 방 들어가면 자동으로 패널 닫아주기
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
            // 플레이어의 상호작용 가능 채팅패널 리스트에서 현재 방 채팅패널 삭제
            LHE_CurrentInteractableChatManager.Instance.currTextChat.Remove(currChattingPanel);

            // World의 채팅패널 추가
            LHE_CurrentInteractableChatManager.Instance.currTextChat.Add(worldChattingPanel);

            // 방 채팅패널 열린상태로 방 나가면 자동으로 패널 닫아주기
            if(currChattingPanel.activeSelf == true)
            {
                currChattingPanel.SetActive(false);
                LHE_CurrentInteractableChatManager.Instance.isTextChatOpen = false;
            }
        }
    }
}
