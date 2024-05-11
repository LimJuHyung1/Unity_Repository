using UnityEngine;
using Photon.Pun;

public class tmpScript : MonoBehaviourPunCallbacks
{
    void Start()
    {
        // Photon 서버에 접속
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Photon Server!");
        // 접속 성공 시 다음 작업 수행
        // 예: 플레이어 생성, 로비에 입장 등
    }
}
