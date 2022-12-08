using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

public class LHE_CMFreeLookCamManager : MonoBehaviour
{
    CinemachineFreeLook freelookCam;
    public Transform character;

    // Start is called before the first frame update
    void Start()
    {
        freelookCam = GetComponent<CinemachineFreeLook>();
        

        // MainScene(2)이 아닌경우 비활성화
        if (SceneManager.GetActiveScene().buildIndex != 3)
        {
            this.gameObject.GetComponent<LHE_CMFreeLookCamManager>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // MainScene(2) 되면 활성화
        if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            this.gameObject.GetComponent<LHE_CMFreeLookCamManager>().enabled = true;

            // **************************************************************
            // 다른 캐릭터인 경우 
            //character = GameObject.Find("unitychan(Clone)").transform;

            character = LHE_GameManager.Instance.character.transform;

            //if (LHE_CharacterInstantiateManager.Instance.characterNum == 0)
            //{
            //    character = GameObject.Find("unitychan(Clone)").transform;
            //}
            //else if (LHE_CharacterInstantiateManager.Instance.characterNum == 1)
            //{
            //    character = GameObject.Find("Man_03(Clone)").transform;
            //}

            freelookCam.m_Follow = character;
            freelookCam.m_LookAt = character;
        }
    }
}
