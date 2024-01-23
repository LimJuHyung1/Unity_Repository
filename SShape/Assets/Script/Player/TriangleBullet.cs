using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TriangleBullet : MonoBehaviour
{
    public float rotationSpeed = 360f;      // ȸ�� �ӵ� (1�ʿ� 360��)
    public bool isThrowing = false;         // ó�� �����ÿ� ������ �ʵ��� ����
    PolygonCollider2D polygon;

    Transform parentTransform;

    // Start is called before the first frame update
    void Start()
    {
        polygon = GetComponent<PolygonCollider2D>();
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
            Debug.Log("�ﰢ�� źȯ ���� ����");
            //Destroy(gameObject);
        }
    }

    void OnEnable()
    {
        if (isThrowing)
        {
            parentTransform = transform.parent;
            Invoke("ReturnBullet", 1f);
        }
    }

    void ReturnBullet()
    {
        this.transform.Translate(parentTransform.position);        
    }
}
