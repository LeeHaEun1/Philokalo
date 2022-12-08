using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class LHE_GetOthersID : MonoBehaviourPun
{
    public int othersID;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //if (photonView.IsMine)
        //{
            if(other.gameObject.tag == "Player" && (other.gameObject.GetComponent<PhotonView>().ViewID != photonView.ViewID))
            {
                othersID = other.gameObject.GetComponent<LHE_MyID>().myID;
            }
        //}
    }
}
