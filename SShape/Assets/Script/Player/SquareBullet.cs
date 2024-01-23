using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SquareBullet : MonoBehaviour
{
    public float rotationSpeed = 360f; // ȸ�� �ӵ� (1�ʿ� 360��)
    BoxCollider2D box;

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
        if (collision.gameObject.CompareTag("OtherPlayer"))
        {
            Destroy(gameObject);
        }
    }
}
