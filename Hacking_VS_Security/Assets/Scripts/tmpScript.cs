using UnityEngine;
using Photon.Pun;

public class tmpScript : MonoBehaviourPunCallbacks
{
    void Start()
    {
        // Photon ������ ����
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Photon Server!");
        // ���� ���� �� ���� �۾� ����
        // ��: �÷��̾� ����, �κ� ���� ��
    }
}
