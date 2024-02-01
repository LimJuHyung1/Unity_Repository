using Photon.Pun;
using Photon.Pun.Demo.Asteroids;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareBullet : MonoBehaviourPunCallbacks
{
    public float rotationSpeed = 360f; // ȸ�� �ӵ� (1�ʿ� 360��)
    int dir;

    BoxCollider2D box;
    public PhotonView PV;    

    // Start is called before the first frame update
    void Start()
    {
        box = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // ȸ�� ���� �� �ӵ� ����
        float rotation = rotationSpeed * Time.deltaTime;

        // ���� ������Ʈ�� ȸ��
        transform.Rotate(Vector3.forward * rotation);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Block")
        {
            PV.RPC("DestroyRPC", RpcTarget.AllBuffered);
        }

        if (collision.gameObject.tag == "OtherPlayer")
        {
            // collision.gameObject.GetComponent<Player>().Damaged(2);
            PV.RPC("AttackOtherPlayer", RpcTarget.AllBuffered, collision.gameObject.GetComponent<PhotonView>().ViewID);
            Debug.Log("��� �÷��̾ ���ظ� �Ծ����ϴ�.");

            PV.RPC("DestroyRPC", RpcTarget.AllBuffered);
        }
    }

    [PunRPC]
    void ShootRPC(Vector3 right)
    {
        Rigidbody2D rigid = this.GetComponent<Rigidbody2D>();
        rigid.AddForce(right * 10.0f, ForceMode2D.Impulse);
    }

    [PunRPC]
    void DestroyRPC() => Destroy(gameObject);

    [PunRPC]    // ��� �÷��̾�� �������� ����
    void AttackOtherPlayer(int targetPhotonViewID)
    {
        if (PV.IsMine)
        {
            // PhotonView ID�� ����Ͽ� ��� �÷��̾�� ������ ����
            PhotonView targetPhotonView = PhotonView.Find(targetPhotonViewID);
            if (targetPhotonView != null)
            {
                Player targetPlayer = targetPhotonView.GetComponent<Player>();
                if (targetPlayer != null && targetPlayer.GetComponent<PhotonView>().IsMine)
                {
                    // �������� ������ ���� ����
                    // targetPlayer.hp -= GetComponent<Player>().atkDamage;
                    targetPlayer.hp -= 3;
                    Debug.Log(targetPlayer.name + " : " + targetPlayer.hp);
                }
            }
        }
    }       
}
