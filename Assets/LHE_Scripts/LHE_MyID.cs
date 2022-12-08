using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class LHE_MyID : MonoBehaviourPun
{
    public int myID;

    // Start is called before the first frame update
    void Start()
    {
        //myID = TokenManager.Instance.childId;
        if (photonView.IsMine)
        {
            myID = TokenManager.Instance.childId;
            photonView.RPC("SetID", RpcTarget.AllBuffered, myID);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    [PunRPC]
    public void SetID(int val)
    {
        myID = val;
    }
}
