using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class LHE_ObjectInteract : MonoBehaviourPun
{
    public List<GameObject> nearObject = new List<GameObject>();

    float currentTime = 0;
    public float rotateTime = 10;
    //Transform originTr;

    bool xxx = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {
            if (!(nearObject.Count == 0))
            {
                // [1. ���ɺи���]
                if (nearObject[0].GetComponent<LHE_RotatingPlate>())
                {
                    //if (Input.GetKeyDown(KeyCode.X))
                    //{
                    //    // ���� ��ġ ������ �����غ���
                    //    transform.rotation = nearObject[0].GetComponent<LHE_RotatingPlate>().point1.rotation;
                    //    transform.position = nearObject[0].GetComponent<LHE_RotatingPlate>().point1.position;
                    //}
                    if (Input.GetKeyDown(KeyCode.X))
                    {
                        transform.rotation = nearObject[0].GetComponent<LHE_RotatingPlate>().point1.localRotation;
                        transform.position = nearObject[0].GetComponent<LHE_RotatingPlate>().point1.position;
                        print("����Ʈ ��ġ"+nearObject[0].GetComponent<LHE_RotatingPlate>().point1.position);
                        print("�� ��ġ"+transform.position);
                    }
                    if (Input.GetKeyDown(KeyCode.Z))
                    {
                        //nearObject[0].GetComponent<LHE_RotatingPlate>().Rotate();
                        nearObject[0].GetComponent<Rigidbody>().AddTorque(transform.up * 50, ForceMode.Impulse);
                        xxx = true;
                    }
                }

                // [2. �׳�]
                if (nearObject[0].GetComponent<LHE_RotatingPlate>())
                {

                }
            }

            if (xxx)
            {

                        transform.position = nearObject[0].GetComponent<LHE_RotatingPlate>().point1.localPosition;
                        transform.rotation = nearObject[0].GetComponent<LHE_RotatingPlate>().point1.rotation;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Object")
        {
            if (nearObject.Contains(other.gameObject) == false)
            {
                nearObject.Add(other.gameObject);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Object")
        {
            nearObject.Remove(other.gameObject);
        }
    }
}
