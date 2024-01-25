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
    [SerializeField] private float elapsedTime = 0f; // 경과 시간을 저장할 변수

    protected override void Start()
    {
        base.Start();
        InitStatus();

        box = GetComponent<BoxCollider2D>();
        polygon = GetComponent<PolygonCollider2D>();

        squareBullet = Resources.Load<GameObject>("SquareBullet");
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("OtherPlayer"))
        {
            isDiscovering = true;
            //collision.GetComponent<Player>().FadeIn();

            PV.RPC("ShootBullet", RpcTarget.All);
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

            //CancelInvoke("ShootBullet");
        }
    }

    protected override void InitStatus()
    {
        base.InitStatus();
        this.hp = 10;
        this.hpMax = 10;
        this.atkDamage = 3;
        this.atkDelay = 2;
    }

    [PunRPC]
    void ShootBullet()  // 탄환 발사
    {
        if(squareBullet != null)
        {
            //GameObject bullet = Instantiate(squareBullet, this.transform.position, Quaternion.identity);
            GameObject bullet = PhotonNetwork.Instantiate("SquareBullet", this.transform.position, Quaternion.identity);
            Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
            rigid.AddForce(this.transform.right * 10.0f, ForceMode2D.Impulse);
        }
    }
}
