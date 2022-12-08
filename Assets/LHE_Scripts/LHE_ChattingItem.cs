using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LHE_ChattingItem : MonoBehaviour
{
    // Text
    LHE_CustomChatText chatText;
    // Rect Transform
    RectTransform rt;

    // Start로 하면 생성되기전에 SetItem()호출되어 에러 발생 가능
    // 이렇게 Awake로 해주거나, 위의 Text를 public으로 해 미리 assign해줘서 찾지 못하는 일 없도록 방지
    void Awake()
    {
        chatText = GetComponent<LHE_CustomChatText>();
        chatText.onChangedSize = AAA;
        rt = GetComponent<RectTransform>();
    }

    public void SetText(string chat)
    {
        // Text 세팅
        chatText.text = chat;
    }

    void AAA()
    {
        //print("크기가 변경되었다");
        // Height 맞춰주자
        rt.sizeDelta = new Vector2(rt.sizeDelta.x, chatText.preferredHeight);
    }
}
