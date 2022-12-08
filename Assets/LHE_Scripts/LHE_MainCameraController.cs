using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

public class LHE_MainCameraController : MonoBehaviour
{
    // 3인칭 카메라: Hierarchy에서 할당 가능
    public CinemachineFreeLook thirdPersonCam;
    // 1인칭 카메라: 캐릭터 생성 후 찾을 수 있음
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
            // 3인칭인 경우 -> 1인칭으로
            if (isThirdPersonView == true)
            {
                thirdPersonCam.enabled = false;
                firstPersonCam.enabled = true;

                isThirdPersonView = false;
            }
            // 1인칭인 경우 -> 3인칭으로
            else if (isThirdPersonView == false)
            {
                thirdPersonCam.enabled = true;
                firstPersonCam.enabled = false;

                isThirdPersonView = true;
            }
        }
    }
}
