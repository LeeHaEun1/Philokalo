using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class LHE_CharacterNickname : MonoBehaviourPun
{
    string photonNickname;
    //public TextMeshProUGUI characterNickname;
    public Text characterNickname;

    // Start is called before the first frame update
    void Start()
    {
        // MainScene(2)�� �ƴѰ�� ��Ȱ��ȭ
        if (SceneManager.GetActiveScene().buildIndex != 3)
        {
            this.gameObject.GetComponent<LHE_CharacterNickname>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // MainScene(2) �Ǹ� Ȱ��ȭ
        if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            this.gameObject.GetComponent<LHE_CharacterNickname>().enabled = true;
            photonNickname = photonView.Owner.NickName;
        }

        characterNickname.text = photonNickname;
    }
}
