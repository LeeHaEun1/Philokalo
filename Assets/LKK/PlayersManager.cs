using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersManager : MonoBehaviour
{
    public static PlayersManager pm;
    public delegate void AddEvent(string id, GameObject player);
    public delegate void RemoveEvent(string id, GameObject player);

    public AddEvent OnEnterScene;
    public RemoveEvent OnRemoveScene;

    Dictionary<string, GameObject> playerList = new Dictionary<string, GameObject>();

    private void Awake()
    {
        if(pm == null)
        {
            pm = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        OnEnterScene += AddPlayer;
        OnRemoveScene += RemovePlayer;
    }

    void AddPlayer(string id, GameObject player)
    {
        if(playerList.ContainsKey(id))
        {
            return;
        }
        else
        {
            playerList.Add(id, player);
        }
    }

    void RemovePlayer(string id, GameObject player)
    {
        if (playerList.ContainsKey(id))
        {
            return;
        }
        else
        {
            playerList.Remove(id);
        }
    }

    public GameObject GetPlayer(string id)
    {
        if (playerList.ContainsKey(id))
        {
            return playerList[id];
        }
        else
        {
            return null;
        }
    }
}
