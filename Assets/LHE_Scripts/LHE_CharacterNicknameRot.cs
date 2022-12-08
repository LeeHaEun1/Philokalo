using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LHE_CharacterNicknameRot : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.forward = -transform.parent.gameObject.transform.forward;
    }

    // Update is called once per frame
    void Update()
    {
        transform.forward = -transform.parent.gameObject.transform.forward;
    }
}
