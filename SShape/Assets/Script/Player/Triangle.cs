using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triangle : Player
{    
    PolygonCollider2D polygon;
    PolygonCollider2D light;

    protected override void Start()
    {
        base.Start();
        
        polygon = GetComponent<PolygonCollider2D>();
        light = GetComponentInChildren<PolygonCollider2D>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // 피해 입음
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

    protected override void InitStatus()
    {
        base.InitStatus();
        this.hp = 10;
        this.hpMax = 10;
        this.atkDamage = 2;
    }
}
