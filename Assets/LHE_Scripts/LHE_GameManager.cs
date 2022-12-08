using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;
//using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class LHE_GameManager : MonoBehaviourPunCallbacks
{
    public static LHE_GameManager Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public GameObject character;
    //public Transform bubbleCanvas;

    public Dictionary<string, GameObject> characterList = new Dictionary<string, GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        // [Rate]
        //OnPhotonSerializeView »£√‚ ∫Ûµµ
        PhotonNetwork.SerializationRate = 60;
        //Rpc »£√‚ ∫Ûµµ
        PhotonNetwork.SendRate = 60;

        // [Character]
        //GameObject character = PhotonNetwork.Instantiate("unitychan", Vector3.zero, Quaternion.identity);
        //GameObject character = PhotonNetwork.Instantiate("Man_03", Vector3.zero, Quaternion.identity);

        if(LHE_CharacterInstantiateManager.Instance.characterNum == 0)
        {
            //character = PhotonNetwork.Instantiate("unitychan", Vector3.zero, Quaternion.identity);

            character = PhotonNetwork.Instantiate(TokenManager.Instance.fileName+"0", Vector3.zero, Quaternion.identity);
            //bubbleCanvas = character.transform.Find("Canvas_TextBubble");
            //characterList.Add(PhotonNetwork.NickName, character);

            //character.GetComponent<LHE_MyID>().myID = TokenManager.Instance.childId;
            //character.GetComponent<LHE_MyID>().myID = LHE_ChildAccountManager.Instance.child1_childId;
        }
        else if(LHE_CharacterInstantiateManager.Instance.characterNum == 1)
        {
            //character = PhotonNetwork.Instantiate("Man_03", Vector3.zero, Quaternion.identity);

            character = PhotonNetwork.Instantiate(TokenManager.Instance.fileName + "1", Vector3.zero, Quaternion.identity);
            //bubbleCanvas = character.transform.Find("Canvas_TextBubble");
            //characterList.Add(PhotonNetwork.NickName, character);

            //character.GetComponent<LHE_MyID>().myID = TokenManager.Instance.childId;
            character.GetComponent<LHE_MyID>().myID = LHE_ChildAccountManager.Instance.child2_childId;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //print("characterList.Count "+characterList.Count);
    }
}
