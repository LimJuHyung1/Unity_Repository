using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TriangleBullet : MonoBehaviour
{
    public float rotationSpeed = 360f;      // 회전 속도 (1초에 360도)
    public bool isThrowing = false;         // 처음 생성시에 날라가지 않도록 설정
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
        // 회전 방향 및 속도 설정
        float rotation = rotationSpeed * Time.deltaTime;

        // 현재 오브젝트를 회전
        transform.Rotate(Vector3.forward * rotation);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("OtherPlayer"))
        {
            Debug.Log("삼각형 탄환 피해 입힘");
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
