using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class IntroManager : MonoBehaviourPunCallbacks
{
    public Button playButton;
    public Button[] positionButtons;
    public Image loadingImg;
    public GameObject networkManager;

    RoomOptions roomOption;

    void Awake()
    {
        PhotonNetwork.ConnectUsingSettings();        
    }

    void Update()
    {
        if (PhotonNetwork.IsConnected)
        {
            // FadeOut();

            if (PhotonNetwork.InRoom && PhotonNetwork.CurrentRoom.PlayerCount == 2)
                SceneManager.LoadScene("BattleScene");
        }
    }

    public override void OnConnectedToMaster()
    {
        roomOption = new RoomOptions();
        roomOption.MaxPlayers = 2;
        roomOption.CustomRoomProperties = new Hashtable() { { "마스터", 0 }, { "로컬플레이어", 1 } };
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();

        Hashtable CP = PhotonNetwork.CurrentRoom.CustomProperties;

        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LocalPlayer.SetCustomProperties(new Hashtable { { "asdf", "asdf" } });

            Hashtable playerCP = PhotonNetwork.LocalPlayer.CustomProperties;
        }
    }

    // 방 접속하기
    public void PlayButton()
    {
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.JoinOrCreateRoom("tmp", roomOption, null);

            ActiveLoadingImage();
        }
    }

    public void ActiveLoadingImage()
    {
        loadingImg.gameObject.SetActive(true);
    }

    public void UnactiveLoadingImage()
    {
        loadingImg.gameObject.SetActive(false);
        networkManager.GetComponent<NetworkManager>().Disconnect();
    }
}
