using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LHE_Swing : MonoBehaviour
{
    public float speed = 10;
    public Transform seat;

    public List<GameObject> playerList = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        print("transform.rotation.x " + transform.rotation.x);
        print("transform.rotation.y " + transform.rotation.y);
        print("transform.rotation.z " + transform.rotation.z);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.rotation.z <= -0.2)
        {
            //transform.Rotate(0, 0, speed * Time.deltaTime);
            transform.RotateAround(transform.position, transform.forward, 50* Time.deltaTime);
        }
        
        if(transform.rotation.z >= 0.2)
        {
            //transform.Rotate(0, 0, speed * Time.deltaTime);
            transform.RotateAround(transform.position, transform.forward, -50* Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            playerList.Add(other.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerList.Remove(other.gameObject);
        }
    }
}
