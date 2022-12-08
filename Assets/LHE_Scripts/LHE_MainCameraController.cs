using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

public class LHE_MainCameraController : MonoBehaviour
{
    // 3��Ī ī�޶�: Hierarchy���� �Ҵ� ����
    public CinemachineFreeLook thirdPersonCam;
    // 1��Ī ī�޶�: ĳ���� ���� �� ã�� �� ����
    public Camera firstPersonCam;

    public bool isThirdPersonView;

    // Start is called before the first frame update
    void Start()
    {
        thirdPersonCam.enabled = true;


        isThirdPersonView = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            firstPersonCam = GameObject.Find("FirstPersonCamera").GetComponent<Camera>();
            //firstPersonCam.enabled = false;
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            // 3��Ī�� ��� -> 1��Ī����
            if (isThirdPersonView == true)
            {
                thirdPersonCam.enabled = false;
                firstPersonCam.enabled = true;

                isThirdPersonView = false;
            }
            // 1��Ī�� ��� -> 3��Ī����
            else if (isThirdPersonView == false)
            {
                thirdPersonCam.enabled = true;
                firstPersonCam.enabled = false;

                isThirdPersonView = true;
            }
        }
    }
}
