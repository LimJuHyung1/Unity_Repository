using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class GameManager : MonoBehaviourPunCallbacks
{
    [SerializeField] List<GameObject> players = new List<GameObject>();

    private PhotonView PV;
    public GameObject camera;
    public Button circleChoiceButton;
    public Button squareChoiceButton;

    Vector2[] spawnVec = new Vector2[9];
    int startX = -35;
    int startY = 35;
    int addX = 35;
    int addY = -35;

    void Awake()
    {
        for(int i = 0; i < spawnVec.Length; i++)
        {
            if (i % 3 == 0 && i != 0)
            {
                startY += addY;
                startX -= 105;
            }              
            
            spawnVec[i] = new Vector2(startX, startY);
            startX += addX;            
        }

        PV = GetComponent<PhotonView>();
    }

    void Start()
    {
        /*
        if(PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.Instantiate("Square", Vector2.zero, Quaternion.identity);
        }
        else
        {
            PhotonNetwork.Instantiate("Circle", Vector2.zero, Quaternion.identity);
        }
        */

        Application.targetFrameRate = 90;
    }

    //[PunRPC]
    public void ChooseCircle()
    {

        //int viewID = player.GetComponent<PhotonView>().ViewID;

        //PhotonView playerView = PhotonView.Find(viewID);

        //PV.RPC("AddPlayer", RpcTarget.AllBuffered, player);

        SetPlayer("Circle");
    }

    //[PunRPC]
    public void ChooseSquare()
    {
        SetPlayer("Square");
    }

    public void SetPlayer(string playerName)
    {
        GameObject player =
            PhotonNetwork.Instantiate
            (playerName, spawnVec[Random.Range(0, 9)], Quaternion.identity);

        camera.GetComponent<CameraScript>().FindPlayer(player);

        circleChoiceButton.gameObject.SetActive(false);
        squareChoiceButton.gameObject.SetActive(false);
    }

    [PunRPC]
    void AddPlayer(GameObject player)
    {
        players.Add(player);
    }
}
