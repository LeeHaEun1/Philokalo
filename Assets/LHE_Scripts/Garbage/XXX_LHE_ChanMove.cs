using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class XXX_LHE_ChanMove : MonoBehaviour
{
    CharacterController cc;

    public float rotSpeed = 5;

    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        // [1. 이동]
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 dir = new Vector3(h, 0, v);
        dir = transform.TransformDirection(dir);
        dir.Normalize();

        if (!(h == 0 && v == 0))
        {
            cc.SimpleMove(dir);

            // 몸체 회전
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), rotSpeed * Time.deltaTime);
        }
    }
}
