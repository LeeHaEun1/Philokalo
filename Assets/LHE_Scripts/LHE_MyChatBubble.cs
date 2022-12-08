using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class LHE_MyChatBubble : MonoBehaviourPun
{
    public InputField inputChat;
    public Transform canvasBubble;

    Color nickColor;

    public GameObject chatBubbleFactory;

    //public bool muteBubble = false;

    // Start is called before the first frame update
    void Start()
    {
        nickColor = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));

        if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            inputChat = GameObject.Find("InputField (Legacy)_Chat").GetComponent<InputField>();
        }

        inputChat.onSubmit.AddListener(OnSend);

        //print("donedonedonedonedonedonedonedonedonedone");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnSend(string s)
    {
        string chatText = "<color=#" + ColorUtility.ToHtmlStringRGB(nickColor) + ">" + PhotonNetwork.NickName + "</color>" + " : " + s;

        if (photonView.IsMine)
        {
            photonView.RPC("RpcAddBubble", RpcTarget.All, chatText);
        }
    }

    //public void MuteBubble()
    //{
    //    if(muteBubble == false)
    //    {
    //        muteBubble = true;
    //    }
    //    else if(muteBubble == true)
    //    {
    //        muteBubble = false;
    //    }
    //}


    [PunRPC]
    void RpcAddBubble(string chat)
    {
        if(LHE_CurrentInteractableChatManager.Instance.muteBubble == false)
        {
            GameObject bubble = Instantiate(chatBubbleFactory, canvasBubble);
            LHE_ChatBubble bubbleContent = bubble.GetComponent<LHE_ChatBubble>();
            bubbleContent.SetText(chat);
        }
        else
        {
            return;
        }
    }
}
