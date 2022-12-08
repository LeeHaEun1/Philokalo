using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LHE_FindFaceCamTexture : MonoBehaviour
{
    public RawImage rawImage;

    // Start is called before the first frame update
    void Start()
    {
        //GameObject faceCam = GameObject.Find("FaceCamera");
        //rawImage.texture = faceCam.GetComponent<Camera>().targetTexture;

        GameObject faceCam = LHE_GameManager.Instance.character.transform.Find("FaceCamera").gameObject;
        //faceCam.SetActive(true);
        rawImage.texture = faceCam.GetComponent<Camera>().targetTexture;

    }

    // Update is called once per frame
    void Update()
    {
        //if (SceneManager.GetActiveScene().buildIndex == 3)
        //{
        //    GameObject faceCam = GameObject.Find("FaceCamera");
        //    rawImage.texture = faceCam.GetComponent<Camera>().targetTexture;
        //}

        //GameObject faceCam = LHE_GameManager.Instance.character.transform.Find("FaceCamera").gameObject;
        //rawImage.texture = faceCam.GetComponent<Camera>().targetTexture;
    }
}
