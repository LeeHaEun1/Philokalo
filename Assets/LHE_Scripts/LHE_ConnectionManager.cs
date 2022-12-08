using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class LHE_ConnectionManager : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        // NameServer ����(AppId, GameVersion, ������ ��� ���� ��� ���� ����)
        PhotonNetwork.ConnectUsingSettings();
    }

    // Lobby ���� �� ���� �Ұ� ����
    public override void OnConnected()
    {
        base.OnConnected();
        print(System.Reflection.MethodBase.GetCurrentMethod().Name); // ����Ǵ� �Լ� �̸� ��ȯ
    }

    // Master Server ���� ����, Lobby ���� �� ���� ����
    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        print(System.Reflection.MethodBase.GetCurrentMethod().Name);

        // �⺻ Lobby ���� ��û
        PhotonNetwork.JoinLobby();
    }

    // Lobby ���� ����
    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        print(System.Reflection.MethodBase.GetCurrentMethod().Name);

        PhotonNetwork.LoadLevel(1);
    }
}
