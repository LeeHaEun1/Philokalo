using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LHE_ChattingItem : MonoBehaviour
{
    // Text
    LHE_CustomChatText chatText;
    // Rect Transform
    RectTransform rt;

    // Start�� �ϸ� �����Ǳ����� SetItem()ȣ��Ǿ� ���� �߻� ����
    // �̷��� Awake�� ���ְų�, ���� Text�� public���� �� �̸� assign���༭ ã�� ���ϴ� �� ������ ����
    void Awake()
    {
        chatText = GetComponent<LHE_CustomChatText>();
        chatText.onChangedSize = AAA;
        rt = GetComponent<RectTransform>();
    }

    public void SetText(string chat)
    {
        // Text ����
        chatText.text = chat;
    }

    void AAA()
    {
        //print("ũ�Ⱑ ����Ǿ���");
        // Height ��������
        rt.sizeDelta = new Vector2(rt.sizeDelta.x, chatText.preferredHeight);
    }
}
