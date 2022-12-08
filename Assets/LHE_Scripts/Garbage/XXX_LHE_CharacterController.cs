using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class XXX_LHE_CharacterController : MonoBehaviour
{
    [Header("Move")]
    public LHE_CharacterMove characterMove;

    [Header("Rotate")]
    public float rotSpeed = 200;
    float rx;
    float ry;

    // Start is called before the first frame update
    void Start()
    {
        //characterMove = GetComponentInChildren<LHE_CharacterMove>();
    }

    // Update is called once per frame
    void Update()
    {
        // [1. 이동]
        // adws & 화살표로 이동
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 dir = new Vector3(h, 0, v);
        dir.Normalize();

        if (!(h == 0 && v == 0))
        {
            //characterMove.Move(dir);
        }


        // [2. 회전]
        // 마우스 좌우 움직임으로 회전
        float mx = Input.GetAxis("Mouse X");
        //float my = Input.GetAxis("Mouse Y");

        //rx += rotSpeed * my * Time.deltaTime;
        ry += rotSpeed * mx * Time.deltaTime;

        Vector3 rotV = new Vector3(0, ry, 0);

        if(!(ry == 0))
        {
            //characterMove.Rotate(rotV);
        }
    }
}
