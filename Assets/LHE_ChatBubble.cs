using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LHE_ChatBubble : MonoBehaviour
{
    //public TextMeshProUGUI chatText;
    public Text chatText;

    float currentTime = 0;
    public float deleteTime = 3;

    public void SetText(string chat)
    {
        // Text ¼¼ÆÃ
        chatText.text = chat;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if(currentTime > deleteTime)
        {
            Destroy(gameObject);
        }
    }
}
