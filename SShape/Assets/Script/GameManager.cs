using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    public GameObject camera;

    void Start()
    {
        if(PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.Instantiate("Square", Vector2.zero, Quaternion.identity);
        }
        else
        {
            PhotonNetwork.Instantiate("Circle", Vector2.zero, Quaternion.identity);
        }
        
        camera.GetComponent<CameraScript>().FindPlayer();

        Application.targetFrameRate = 90;
    }
}
