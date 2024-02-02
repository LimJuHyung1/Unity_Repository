using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Circle : Player
{
    CircleCollider2D circle;
    

    protected override void Start()
    {
        base.Start();
        PV.RPC("InitStatus", RpcTarget.All);

        circle = GetComponent<CircleCollider2D>();        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("OtherPlayer") && isAttacking == false)
        {
            isAttacking = true;
            PV.RPC("AttackRPC", RpcTarget.AllBuffered, collision.gameObject.GetComponent<PhotonView>().ViewID);

            Invoke("InvokeAtkDelay", atkDelay);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("OtherPlayer"))
        {
            isDiscovering = true;
            //collision.GetComponent<Player>().FadeIn();
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("OtherPlayer"))
        {
            isDiscovering = false;
            //collision.GetComponent<Player>().FadeOut();
        }
    }

    [PunRPC]
    protected override void InitStatus()
    {
        base.InitStatus();
        this.hp = 10;
        this.hpMax = 10;
        this.atkDamage = 2;
        this.atkDelay = 2;
    }

    [PunRPC]
    void AttackRPC(int ViewID)
    {
        PhotonView tmp = PhotonView.Find(ViewID);
        
        tmp.GetComponent<Player>().hp -= this.atkDamage;
    }
}
