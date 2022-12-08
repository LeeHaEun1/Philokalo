using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LHE_RoundAbout : MonoBehaviour
{
    public float speed = 10;
    public GameObject axis;
    public Transform point1;

    public List<GameObject> onBlade = new List<GameObject>();

    Vector3 originPos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(transform.up * speed * Time.deltaTime);

        for (int i = 0; i < onBlade.Count; i++)
        {
            //onBlade[i].transform.RotateAround(onBlade[i].transform.position, Vector3.up, speed * Time.deltaTime);
            onBlade[i].transform.RotateAround(onBlade[i].transform.position, transform.up, speed * Time.deltaTime);
            //onBlade[i].transform.position = originPos;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            onBlade.Add(other.gameObject);
            //other.gameObject.transform.parent = point1.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            onBlade.Remove(other.gameObject);
            //other.gameObject.transform.parent = null;
        }
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.tag == "Player")
    //    {
    //        //if (onBlade.Contains(collision.gameObject)==false)
    //        //{
    //            onBlade.Add(collision.gameObject);
    //            originPos = collision.gameObject.transform.localPosition;
    //        //}
    //    }
    //}

    //private void OnCollisionExit(Collision collision)
    //{
    //    if (collision.gameObject.tag == "Player")
    //    {
    //        onBlade.Remove(collision.gameObject);
    //    }
    //}

    //private void OnControllerColliderHit(ControllerColliderHit hit)
    //{
        
    //}
}
