using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareBullet : MonoBehaviourPunCallbacks
{
    public float rotationSpeed = 360f; // ȸ�� �ӵ� (1�ʿ� 360��)
    BoxCollider2D box;
    public PhotonView PV;

    // Start is called before the first frame update
    void Start()
    {
        box = GetComponent<BoxCollider2D>();        
        PV = GetComponent<PhotonView>();
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
        if (!collision.gameObject.CompareTag("Player"))
        {
            if (collision.gameObject.CompareTag("OtherPlayer"))
            {
                PhotonView otherPlayerPhotonView = collision.gameObject.GetComponent<PhotonView>();
                Debug.Log(otherPlayerPhotonView.ViewID);

                if (otherPlayerPhotonView != null)
                {
                    PV.RPC("AttackOtherPlayer", RpcTarget.AllBuffered, otherPlayerPhotonView.ViewID);
                }
            }

            PV.RPC("DestroyBullet", RpcTarget.AllBuffered);
        }            
    }

    [PunRPC]    // ��� �÷��̾�� �������� ����
    void AttackOtherPlayer(int targetPhotonViewID)
    {
        // PhotonView ID�� ����Ͽ� ��� �÷��̾�� ������ ����
        PhotonView targetPhotonView = PhotonView.Find(targetPhotonViewID);
        if (targetPhotonView != null)
        {
            Player targetPlayer = targetPhotonView.GetComponent<Player>();
            if (!PV.IsMine && targetPlayer != null && targetPlayer.GetComponent<PhotonView>().IsMine)
            {
                // �������� ������ ���� ����
                // targetPlayer.hp -= GetComponent<Player>().atkDamage;
                targetPlayer.hp -= 3;
                Debug.Log(targetPlayer.name + " : " +  targetPlayer.hp);
            }
        }
    }

    [PunRPC]
    void DestroyBullet() 
    {
        Invoke("WaitDestroy", 1f);
        this.gameObject.SetActive(false);
    }

    void WaitDestroy()
    {
        Destroy(gameObject);
    }
}
