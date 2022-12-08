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
        // NameServer 접속(AppId, GameVersion, 지역이 모두 같은 경우 같이 묶임)
        PhotonNetwork.ConnectUsingSettings();
    }

    // Lobby 생성 및 진입 불가 상태
    public override void OnConnected()
    {
        base.OnConnected();
        print(System.Reflection.MethodBase.GetCurrentMethod().Name); // 진행되는 함수 이름 반환
    }

    // Master Server 접속 성공, Lobby 생성 및 진입 가능
    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        print(System.Reflection.MethodBase.GetCurrentMethod().Name);

        // 기본 Lobby 진입 요청
        PhotonNetwork.JoinLobby();
    }

    // Lobby 진입 성공
    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        print(System.Reflection.MethodBase.GetCurrentMethod().Name);

        PhotonNetwork.LoadLevel(1);
    }
}
