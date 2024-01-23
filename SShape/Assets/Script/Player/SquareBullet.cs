using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SquareBullet : MonoBehaviour
{
    public float rotationSpeed = 360f; // 회전 속도 (1초에 360도)
    BoxCollider2D box;

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
        if (collision.gameObject.CompareTag("OtherPlayer"))
        {
            Destroy(gameObject);
        }
    }
}
