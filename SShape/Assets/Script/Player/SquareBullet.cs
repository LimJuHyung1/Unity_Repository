using Photon.Pun;
using Photon.Realtime;
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
        if (collision.gameObject.CompareTag("OtherPlayer"))
        {         
            Debug.Log("��� �÷��̾�� �������� �������ϴ�.");
        }        
        
        PV.RPC("DestroyBullet", RpcTarget.AllBuffered);
    }

    [PunRPC]
    void DestroyBullet() => Destroy(gameObject);        
}
