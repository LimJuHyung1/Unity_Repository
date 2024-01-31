using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareBullet : MonoBehaviourPunCallbacks
{
    public float rotationSpeed = 360f; // 회전 속도 (1초에 360도)
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
        // 회전 방향 및 속도 설정
        float rotation = rotationSpeed * Time.deltaTime;

        // 현재 오브젝트를 회전
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

    [PunRPC]    // 상대 플레이어에게 데미지를 입힘
    void AttackOtherPlayer(int targetPhotonViewID)
    {
        // PhotonView ID를 사용하여 상대 플레이어에게 데미지 입힘
        PhotonView targetPhotonView = PhotonView.Find(targetPhotonViewID);
        if (targetPhotonView != null)
        {
            Player targetPlayer = targetPhotonView.GetComponent<Player>();
            if (!PV.IsMine && targetPlayer != null && targetPlayer.GetComponent<PhotonView>().IsMine)
            {
                // 데미지를 입히는 로직 구현
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
