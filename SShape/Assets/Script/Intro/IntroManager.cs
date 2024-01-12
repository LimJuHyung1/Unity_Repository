using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Realtime;

public class IntroManager : MonoBehaviour
{
    public Image loadingImage;
    public GameObject networkManager;

    void Awake()
    {
        PhotonNetwork.ConnectUsingSettings();
        networkManager.GetComponent<NetworkManager>().Connect();
    }

    void Update()
    {
        if(PhotonNetwork.IsConnected)            
        {
            if (PhotonNetwork.InRoom && PhotonNetwork.CurrentRoom.PlayerCount == 2)
                SceneManager.LoadScene("Battle1");
        } 
    }

    // 방 접속하기
    public void PlayButton()
    {
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.JoinOrCreateRoom("tmp", new RoomOptions { MaxPlayers = 2 }, null);
            /*
            if(PhotonNetwork.CountOfRooms == 0)
            {
                networkManager.GetComponent<NetworkManager>().CreateRoom();
            }            
            else if (PhotonNetwork.CountOfRooms == 1)
            {
                networkManager.GetComponent<NetworkManager>().JoinRoom();
            }
            */
        }        
    }

    public void ActiveLoadingImage()
    {
        loadingImage.gameObject.SetActive(true);
    }

    public void UnactiveLoadingImage()
    {
        loadingImage.gameObject.SetActive(false);
        networkManager.GetComponent<NetworkManager>().Disconnect();
    }
}
