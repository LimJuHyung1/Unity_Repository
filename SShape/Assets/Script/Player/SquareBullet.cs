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
        // ȸ�� ���� �� �ӵ� ����
        float rotation = rotationSpeed * Time.deltaTime;

        // ���� ������Ʈ�� ȸ��
        transform.Rotate(Vector3.forward * rotation);
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        PhotonView colPV = collision.gameObject.GetComponent<PhotonView>();

        // ��� �÷��̾�� �浹
        if (collision.gameObject.CompareTag("OtherPlayer") 
            && squareViewID != colPV.ViewID
            && !colPV.IsMine)
        {
            PV.RPC("SoundRPC", RpcTarget.All);
            PV.RPC("AttackRPC", RpcTarget.AllBuffered, colPV.ViewID);
            PV.RPC("DestroyRPC", RpcTarget.AllBuffered);            
        }

        // �������� �浹
        if (collision.gameObject.CompareTag("Block") && !collision.gameObject.CompareTag("OtherPlayer"))
        {
            PV.RPC("DestroyRPC", RpcTarget.AllBuffered);
        }          
    }        

    [PunRPC]    // źȯ �߻�
    void ShootRPC(Vector3 right, int myViewID)     
    {
        squareViewID = myViewID;
        Rigidbody2D rigid = this.GetComponent<Rigidbody2D>();
        rigid.AddForce(right * 10.0f, ForceMode2D.Impulse);
    }

    [PunRPC]    // �Ҹ� ȿ��
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

    [PunRPC]    // ������Ʈ ����
    void DestroyRPC() => Destroy(gameObject);
}
