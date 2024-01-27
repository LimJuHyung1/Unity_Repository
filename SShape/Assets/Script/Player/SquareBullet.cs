using Photon.Pun;
using Photon.Realtime;
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
        if (collision.gameObject.CompareTag("OtherPlayer"))
        {         
            Debug.Log("상대 플레이어에게 데미지를 입혔습니다.");
        }        
        
        PV.RPC("DestroyBullet", RpcTarget.AllBuffered);
    }

    [PunRPC]
    void DestroyBullet() => Destroy(gameObject);        
}
