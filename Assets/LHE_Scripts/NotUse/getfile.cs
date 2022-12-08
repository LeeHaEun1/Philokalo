using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class getfile : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Application.OpenURL("https://purpleprint-bucket.s3.ap-northeast-2.amazonaws.com/character/Eve+By+J.Gonzales+(1).fbx");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
