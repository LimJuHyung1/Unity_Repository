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
            collision.gameObject.GetComponent<Player>().Damaged(this.atkDamage);
            Debug.Log("상대 플레이어 피해 입힘");

            Invoke("InvokeAtkDelay", atkDelay);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("OtherPlayer"))
        {
            isAttacking = false;
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
        this.atkDelay = 1;
    }    
}
