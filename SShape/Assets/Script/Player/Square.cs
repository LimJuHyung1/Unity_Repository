using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : Player
{
    BoxCollider2D box;
    PolygonCollider2D polygon;

    [SerializeField] GameObject squareBullet;
    [SerializeField] GameObject squareLight;

    private float timer = 0f;
    public float interval = 1f; // 1초마다 실행하도록 설정

    protected override void Start()
    {
        base.Start();
        PV.RPC("InitStatus", RpcTarget.All);        

        box = GetComponent<BoxCollider2D>();
        polygon = GetComponent<PolygonCollider2D>();

        squareBullet = Resources.Load<GameObject>("SquareBullet");
        squareLight = Resources.Load<GameObject>("SquareLight");

        if(squareLight != null)
        {
            GameObject tmpLight = 
                PhotonNetwork.Instantiate("SquareLight", this.transform.position, Quaternion.identity);
            tmpLight.GetComponent<PhotonView>().RPC("FindPlayer", RpcTarget.All, PV.ViewID);
        }
            
    }

    void Update()
    {
        if (isDiscovering)
        {
            timer += Time.deltaTime;

            if (timer >= interval)
            {
                // 원하는 동작 수행
                ShootBullet();

                // 타이머 초기화
                timer = 0f;
            }
        }    
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("OtherPlayer"))
        {
            isDiscovering = true;
            //collision.GetComponent<Player>().FadeIn();

            //PV.RPC("ShootBullet", RpcTarget.All);
        }
    }
    /*
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("OtherPlayer"))
        {            
            elapsedTime += Time.deltaTime;

            if (elapsedTime >= this.atkDelay)
            {
                ShootBullet();
                elapsedTime = 0;
            }
        }
    }
    */
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("OtherPlayer"))
        {
            isDiscovering = false;
            //collision.GetComponent<Player>().FadeOut();

            timer = 0f;
        }
    }

    [PunRPC]
    protected override void InitStatus()
    {
        base.InitStatus();
        this.hp = 10;
        this.hpMax = 10;
        this.atkDamage = 3;
        this.atkDelay = 2;
    }

    void ShootBullet()  // 탄환 발사
    {
        if(squareBullet != null)
        {
            GameObject bullet = 
                PhotonNetwork.Instantiate
                ("SquareBullet", this.transform.position, Quaternion.identity);

            bullet.GetComponent<PhotonView>().RPC("ShootRPC", RpcTarget.All, this.transform.right, PV.ViewID);
        }
    }
}
