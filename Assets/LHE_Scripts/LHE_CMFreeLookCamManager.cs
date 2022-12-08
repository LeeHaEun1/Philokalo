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
        

        // MainScene(2)�� �ƴѰ�� ��Ȱ��ȭ
        if (SceneManager.GetActiveScene().buildIndex != 3)
        {
            this.gameObject.GetComponent<LHE_CMFreeLookCamManager>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // MainScene(2) �Ǹ� Ȱ��ȭ
        if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            this.gameObject.GetComponent<LHE_CMFreeLookCamManager>().enabled = true;

            // **************************************************************
            // �ٸ� ĳ������ ��� 
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
