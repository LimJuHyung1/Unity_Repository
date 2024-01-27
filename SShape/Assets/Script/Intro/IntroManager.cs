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
    public Image screen;    
    public GameObject networkManager;

    float fadeSpeed = 0.5f; // 투명도 감소 속도
    bool isConnected = false;

    void Awake()
    {
       // PhotonNetwork.ConnectUsingSettings();
        networkManager.GetComponent<NetworkManager>().Connect();
    }

    void Update()
    {
        if(PhotonNetwork.IsConnected)            
        {
            FadeOut();

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

    void FadeOut()
    {
        Color currentColor = screen.color;
        float newAlpha = currentColor.a - fadeSpeed * Time.deltaTime;

        // 투명도가 0 이하로 가지 않도록 보정
        newAlpha = Mathf.Max(newAlpha, 0f);

        // Color 값을 변경하여 투명도를 조절
        screen.color = new Color(currentColor.r, currentColor.g, currentColor.b, newAlpha);

        if (newAlpha <= 0) screen.gameObject.SetActive(false);
    }
}
