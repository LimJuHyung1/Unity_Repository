using Photon.Pun.Demo.Asteroids;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triangle : Player
{    
    PolygonCollider2D polygon;
    PolygonCollider2D light;

    [SerializeField] GameObject triangleBullet;
    GameObject[] bullets = new GameObject[3];
    Vector2[] dirs = new Vector2[3];
    public float projectileSpeed = 5f; // �߻� �ӵ�

    protected override void Start()
    {
        base.Start();
        
        polygon = GetComponent<PolygonCollider2D>();
        light = GetComponentInChildren<PolygonCollider2D>();

        triangleBullet = Resources.Load<GameObject>("TriangleBullet");
        SetTriangleBullet();    // �ﰢ�� �Ѿ� �ڽ� ������Ʈ�� ����
        SetDirection();         // �Ѿ��� ���� ����
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("OtherPlayer"))
        {
            isDiscovering = true;
            //collision.GetComponent<Player>().FadeIn();

            ShootBullet();
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

    void SetTriangleBullet()    // �ﰢ�� źȯ ���� �� �ʱ�ȭ
    {
        for (int i = 0; i < 3; i++)
        {
            //quareBullet = PhotonNetwork.Instantiate(triangleBullet, this.transform.position, Quaternion.identity);
            bullets[i] = Instantiate(triangleBullet, transform.position, Quaternion.identity);
            bullets[i].transform.parent = this.transform;    // �ڽ� ������Ʈ�� ����
            bullets[i].SetActive(false);
        }
    }

    void SetDirection()     // �ﰢ�� źȯ ���� �ʱ�ȭ
    {
        dirs[0] = Quaternion.Euler(0, 0, 30) * Vector2.right;
        dirs[1] = Vector2.right;
        dirs[2] = Quaternion.Euler(0, 0, -30) * Vector2.right;
    }

    void ShootBullet()  // źȯ �߻�
    {
        if (triangleBullet != null)
        {
            for(int i = 0; i < 3; i++)
            {
                bullets[i].gameObject.SetActive(true);
                bullets[i].GetComponent<TriangleBullet>().isThrowing = true;
                // �߻� ���� ����
                bullets[i].GetComponent<Rigidbody2D>().velocity = dirs[i] * projectileSpeed;

                // rigid.AddForce(this.transform.right * 10.0f, ForceMode2D.Impulse);
            }
        }
    }            
}
