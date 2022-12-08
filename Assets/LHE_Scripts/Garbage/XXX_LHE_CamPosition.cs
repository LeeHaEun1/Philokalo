using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XXX_LHE_CamPosition : MonoBehaviour
{
    public Transform thirdPersonCamPos;
    public Transform firstPersonCamPos;

    public bool isThirdPersonView = true;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = thirdPersonCamPos.transform.position;    
        transform.rotation = thirdPersonCamPos.transform.rotation;

        isThirdPersonView = true;
    }

    // Update is called once per frame
    void Update()
    {
        // [1. 현재 시점에 해당하는 CamPos Follow]
        if (isThirdPersonView == true)
        {
            //transform.position = thirdPersonCamPos.transform.position;
            //transform.rotation = thirdPersonCamPos.transform.rotation;

            transform.position = Vector3.Lerp(transform.position, thirdPersonCamPos.transform.position, 5* Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, thirdPersonCamPos.transform.rotation, 5*Time.deltaTime);
        }
        else if (isThirdPersonView == false)
        {
            transform.position = firstPersonCamPos.transform.position;
            transform.rotation = firstPersonCamPos.transform.rotation;
        }
            
        // [2. 시점 변환]
        // Tab key를 누르면 시점 변환
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if(isThirdPersonView == true)
            {
                transform.position = firstPersonCamPos.transform.position;
                transform.rotation = firstPersonCamPos.transform.rotation;

                isThirdPersonView = false;
            }
            else if(isThirdPersonView == false)
            {
                transform.position = thirdPersonCamPos.transform.position;
                transform.rotation = thirdPersonCamPos.transform.rotation;

                isThirdPersonView = true;
            }
        }
    }
}
