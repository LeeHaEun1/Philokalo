using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyUI.Popup;

public class LKK_PopUp : MonoBehaviour
{
    [TextArea(5, 20)] public string longText;

    public void ButtonClickon_Record_Video_Popup()
    {
        Popup.Show("�����ȭ�� �����մϴ�.");
    }
    public void ButtonClickon_Stop_Record_Video_Popup()
    {
        Popup.Show("�����ȭ�� ����/�����մϴ�.");
    }
    public void ButtonClickon_Load_Record_Video_Popup()
    {
        Popup.Show("�����ȭ�� �ҷ��ɴϴ�.");
    }

}
