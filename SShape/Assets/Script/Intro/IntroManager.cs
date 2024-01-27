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

    float fadeSpeed = 0.5f; // ���� ���� �ӵ�
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

    // �� �����ϱ�
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

        // ������ 0 ���Ϸ� ���� �ʵ��� ����
        newAlpha = Mathf.Max(newAlpha, 0f);

        // Color ���� �����Ͽ� ������ ����
        screen.color = new Color(currentColor.r, currentColor.g, currentColor.b, newAlpha);

        if (newAlpha <= 0) screen.gameObject.SetActive(false);
    }
}
