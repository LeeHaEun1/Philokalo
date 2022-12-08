using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class XXX_LHE_CharacterMove : MonoBehaviour
{
    CharacterController cc;

    [Header("Move")]
    public float speed = 5;

    //[Header("Rotate")]
    //public Transform cameraTransform;

    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // [1. �̵�]
    internal void Move(Vector3 dir)
    {
        // dir �������� ��ü ȸ��
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime);

        // �̵�
        cc.SimpleMove(dir * speed);

        // �ִϸ��̼�
    }

    // [2. ȸ��]
    internal void Rotate(Vector3 rotV)
    {
        transform.eulerAngles = rotV;
    }
}
