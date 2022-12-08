using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

// Tab key�� ����ؼ� 3��Ī-1��Ī ���� ��ȯ
public class LHE_CharacterCamView : MonoBehaviourPun
{
    public GameObject thirdPersonCam;
    public GameObject firstPersonCam;

    public bool isThirdPersonView;

    // Start is called before the first frame update
    void Start()
    {
        thirdPersonCam.SetActive(true);

        // photon instantiate�� ĳ������ 1��Ī ���� ī�޶� ã��
        firstPersonCam = GameObject.Find("FirstPersonCamera");
        firstPersonCam.SetActive(false);

        isThirdPersonView = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            // 3��Ī�� ��� -> 1��Ī����
            if(isThirdPersonView == true)
            {
                thirdPersonCam.SetActive(false);
                firstPersonCam.SetActive(true);

                isThirdPersonView = false;
            }
            // 1��Ī�� ��� -> 3��Ī����
            else if(isThirdPersonView == false)
            {
                thirdPersonCam.SetActive(true);
                firstPersonCam.SetActive(false);

                isThirdPersonView = true;
            }
        }
    }
}
