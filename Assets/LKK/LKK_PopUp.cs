using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyUI.Popup;

public class LKK_PopUp : MonoBehaviour
{
    [TextArea(5, 20)] public string longText;

    public void ButtonClickon_Record_Video_Popup()
    {
        Popup.Show("영상녹화를 시작합니다.");
    }
    public void ButtonClickon_Stop_Record_Video_Popup()
    {
        Popup.Show("영상녹화를 저장/정지합니다.");
    }
    public void ButtonClickon_Load_Record_Video_Popup()
    {
        Popup.Show("영상녹화를 불러옵니다.");
    }

}
