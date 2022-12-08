using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

// Unity의 기본 제공 Text 기능을 약간 Custom하고싶음

public class LHE_CustomChatText : Text
{
    // 크기가 변경되었을 때 호출되는 함수를 가지고 있는 변수
    public Action onChangedSize;

    public override void CalculateLayoutInputVertical()
    {
        base.CalculateLayoutInputVertical();
        if (onChangedSize != null)
        {
            onChangedSize();
        }
    }

    public override void CalculateLayoutInputHorizontal()
    {
        base.CalculateLayoutInputHorizontal();
        if (onChangedSize != null)
        {
            onChangedSize();
        }
    }
}
