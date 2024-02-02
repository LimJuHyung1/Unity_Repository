using Photon.Pun;
using Photon.Pun.Demo.Asteroids;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareBullet : MonoBehaviourPunCallbacks
{
    public float rotationSpeed = 360f; // 회전 속도 (1초에 360도)
    int squareViewID;

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
        // 회전 방향 및 속도 설정
        float rotation = rotationSpeed * Time.deltaTime;

        // 현재 오브젝트를 회전
        transform.Rotate(Vector3.forward * rotation);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        PhotonView colPV = collision.gameObject.GetComponent<PhotonView>();

        if (collision.gameObject.CompareTag("OtherPlayer") 
            && squareViewID != colPV.ViewID
            && !colPV.IsMine)
        {
            PV.RPC("AttackRPC", RpcTarget.AllBuffered, colPV.ViewID);
            PV.RPC("DestroyRPC", RpcTarget.AllBuffered);
        }

        if (collision.gameObject.CompareTag("Block") && !collision.gameObject.CompareTag("OtherPlayer"))
        {
            PV.RPC("DestroyRPC", RpcTarget.AllBuffered);
        }


        // if()        
    }

    [PunRPC]
    void ShootRPC(Vector3 right, int myViewID)
    {
        squareViewID = myViewID;
        Rigidbody2D rigid = this.GetComponent<Rigidbody2D>();
        rigid.AddForce(right * 10.0f, ForceMode2D.Impulse);
    }

    [PunRPC]
    void DestroyRPC() => Destroy(gameObject);

    [PunRPC]    // 상대 플레이어에게 데미지를 입힘
    void AttackOtherPlayer(int targetPhotonViewID)
    {
        if (PV.IsMine)
        {
            // PhotonView ID를 사용하여 상대 플레이어에게 데미지 입힘
            PhotonView targetPhotonView = PhotonView.Find(targetPhotonViewID);
            if (targetPhotonView != null)
            {
                Player targetPlayer = targetPhotonView.GetComponent<Player>();
                if (targetPlayer != null && targetPlayer.GetComponent<PhotonView>().IsMine)
                {
                    // 데미지를 입히는 로직 구현
                    // targetPlayer.hp -= GetComponent<Player>().atkDamage;
                    targetPlayer.hp -= 3;
                    Debug.Log(targetPlayer.name + " : " + targetPlayer.hp);
                }
            }
        }
    }

    [PunRPC]
    void AttackRPC(int ViewID)
    {
        PhotonView tmp = PhotonView.Find(ViewID);

        tmp.GetComponent<Player>().hp -= 1;
    }
}
