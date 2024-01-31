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
    public Image screen;
    public Text countdownText;
    public Text hp;

    GameObject player;

    private float timer = 10f;
    private bool isChoosed = false;
    private bool isSpawned = false; // �÷��̾� ���� �� true�� �����

    Vector2[] spawnVec = new Vector2[8];    

    void Awake()
    {
        // PhotonNetwork.ConnectUsingSettings();

        PV = GetComponent<PhotonView>();

        SetSpawnPosition();
        hp.gameObject.SetActive(false);
    }

    void Start()
    {
        // �ʱ�ȭ
        UpdateCountdownText();
        // 1�ʸ��� UpdateTimer �޼ҵ� ȣ��
        InvokeRepeating("UpdateTimer", 1f, 1f);

        Application.targetFrameRate = 90;
    }

    void Update()
    {
        if(isSpawned == true)
            hp.text = "HP : " + player.GetComponent<Player>().hp.ToString();
    }

    public void ChooseCircle()
    {

        //int viewID = player.GetComponent<PhotonView>().ViewID;

        //PhotonView playerView = PhotonView.Find(viewID);

        //PV.RPC("AddPlayer", RpcTarget.AllBuffered, player);

        SetPlayer("Circle");

        isChoosed = true;
    }

    public void ChooseSquare()
    {
        SetPlayer("Square");

        isChoosed = true;
    }

    // �÷��̾� ���� ���� ����
    void SetSpawnPosition()
    {
        spawnVec[0] = new Vector2(-35, 35);
        spawnVec[1] = new Vector2(0, 35);
        spawnVec[2] = new Vector2(35, 35);

        spawnVec[3] = new Vector2(-35, 0);
        spawnVec[4] = new Vector2(35, 0);

        spawnVec[5] = new Vector2(-35, -35);
        spawnVec[6] = new Vector2(0, -35);
        spawnVec[7] = new Vector2(35, -35);
    }

    public void SetPlayer(string playerName)    // �÷��̾� ����
    {
        player =
            PhotonNetwork.Instantiate
            (playerName, spawnVec[Random.Range(0, 8)], Quaternion.identity);
        
        isSpawned = true;

        // ���̾� �̸����� ���̾ ã�Ƽ� ����
        if (player.GetComponent<PhotonView>().IsMine)
        {
            int setLayer = LayerMask.NameToLayer("Player");
            player.layer = setLayer;

            player.tag = "Player";

            // �θ� ������Ʈ�� �ڽ� ������Ʈ���� ã��
            int childCount = player.transform.childCount;

            for (int i = 0; i < childCount; i++)
            {
                // �� �ڽ� ������Ʈ�� ����
                Transform child = player.transform.GetChild(i);

                child.gameObject.layer = setLayer;
                child.gameObject.tag = "Player";
            }
        }

        camera.GetComponent<CameraScript>().FindPlayer(player);

        circleChoiceButton.gameObject.SetActive(false);
        squareChoiceButton.gameObject.SetActive(false);
    }

    void UpdateTimer()
    {
        timer -= 1f;
        UpdateCountdownText();

        // Ÿ�̸Ӱ� 0 ���Ϸ� �������� Ÿ�̸� ����
        if (timer <= 0f)
        {
            screen.gameObject.SetActive(false); // ��ũ�� ����
            hp.gameObject.SetActive(true);

            // ĳ���͸� �������� �ʾ��� ���
            if (isChoosed == false)
            {
                // �������� ��, �簢�� ���õ�
                if (Random.Range(0, 2) == 0)
                {
                    ChooseCircle();
                }
                else
                {
                    ChooseSquare();
                }
            }

            CancelInvoke("UpdateTimer");
        }
    }

    void UpdateCountdownText()
    {
        // UI Text ������Ʈ
        countdownText.text = "���۱��� " + timer.ToString("F0") + "��"; // "F0"�� �Ҽ��� ���ϸ� ǥ������ ����
    }
}
