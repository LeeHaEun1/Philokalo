using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

// Tab key를 사용해서 3인칭-1인칭 시점 변환
public class LHE_CharacterCamView : MonoBehaviourPun
{
    public GameObject thirdPersonCam;
    public GameObject firstPersonCam;

    public bool isThirdPersonView;

    // Start is called before the first frame update
    void Start()
    {
        thirdPersonCam.SetActive(true);

        // photon instantiate된 캐릭터의 1인칭 시점 카메라 찾기
        firstPersonCam = GameObject.Find("FirstPersonCamera");
        firstPersonCam.SetActive(false);

        isThirdPersonView = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            // 3인칭인 경우 -> 1인칭으로
            if(isThirdPersonView == true)
            {
                thirdPersonCam.SetActive(false);
                firstPersonCam.SetActive(true);

                isThirdPersonView = false;
            }
            // 1인칭인 경우 -> 3인칭으로
            else if(isThirdPersonView == false)
            {
                thirdPersonCam.SetActive(true);
                firstPersonCam.SetActive(false);

                isThirdPersonView = true;
            }
        }
    }
}
