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
    private bool isSpawned = false; // 플레이어 생성 시 true로 변경됨

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
        // 초기화
        UpdateCountdownText();
        // 1초마다 UpdateTimer 메소드 호출
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

    // 플레이어 스폰 지점 설정
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

    public void SetPlayer(string playerName)    // 플레이어 생성
    {
        player =
            PhotonNetwork.Instantiate
            (playerName, spawnVec[Random.Range(0, 8)], Quaternion.identity);
        
        isSpawned = true;

        // 레이어 이름으로 레이어를 찾아서 설정
        if (player.GetComponent<PhotonView>().IsMine)
        {
            int setLayer = LayerMask.NameToLayer("Player");
            player.layer = setLayer;

            player.tag = "Player";

            // 부모 오브젝트의 자식 오브젝트들을 찾기
            int childCount = player.transform.childCount;

            for (int i = 0; i < childCount; i++)
            {
                // 각 자식 오브젝트에 접근
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

        // 타이머가 0 이하로 떨어지면 타이머 중지
        if (timer <= 0f)
        {
            screen.gameObject.SetActive(false); // 스크린 제거
            hp.gameObject.SetActive(true);

            // 캐릭터를 선택하지 않았을 경우
            if (isChoosed == false)
            {
                // 무작위로 원, 사각형 선택됨
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
        // UI Text 업데이트
        countdownText.text = "시작까지 " + timer.ToString("F0") + "초"; // "F0"는 소수점 이하를 표시하지 않음
    }
}
