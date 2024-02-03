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
    AudioSource audio;
    public AudioClip attackSound;
    public PhotonView PV;    

    // Start is called before the first frame update
    void Start()
    {
        box = GetComponent<BoxCollider2D>();
        audio = GetComponent<AudioSource>();
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

        // 상대 플레이어와 충돌
        if (collision.gameObject.CompareTag("OtherPlayer") 
            && squareViewID != colPV.ViewID
            && !colPV.IsMine)
        {
            PV.RPC("SoundRPC", RpcTarget.All);
            PV.RPC("AttackRPC", RpcTarget.AllBuffered, colPV.ViewID);
            PV.RPC("DestroyRPC", RpcTarget.AllBuffered);            
        }

        // 구조물과 충돌
        if (collision.gameObject.CompareTag("Block") && !collision.gameObject.CompareTag("OtherPlayer"))
        {
            PV.RPC("DestroyRPC", RpcTarget.AllBuffered);
        }          
    }        

    [PunRPC]    // 탄환 발사
    void ShootRPC(Vector3 right, int myViewID)     
    {
        squareViewID = myViewID;
        Rigidbody2D rigid = this.GetComponent<Rigidbody2D>();
        rigid.AddForce(right * 10.0f, ForceMode2D.Impulse);
    }

    [PunRPC]    // 소리 효과
    void SoundRPC()
    {
        audio.Play();
    }

    [PunRPC]
    void AttackRPC(int ViewID)
    {
        PhotonView tmp = PhotonView.Find(ViewID);

        tmp.GetComponent<Player>().hp -= 1;
    }

    [PunRPC]    // 오브젝트 제거
    void DestroyRPC() => Destroy(gameObject);
}
