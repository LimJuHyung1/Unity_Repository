using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroPlayer : MonoBehaviour
{
    Rigidbody2D rigid;

    public float moveSpeed = 5f; // 이동 속도 설정
    public float rotationSpeed = 180f; // 회전 속도 설정
    private float directionChangeTime = 2f; // 방향 전환 시간 간격
    private Vector2 currentDirection;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();

        InvokeRepeating("ChangeDirection", 1f, directionChangeTime); // 일정 시간마다 방향 변경 함수 호출
    }

    void FixedUpdate()
    {
        rigid.velocity = currentDirection * moveSpeed; // 오브젝트를 현재 방향으로 이동

        // 현재 방향에 따라 오브젝트 회전
        if (rigid.velocity.magnitude > 0.1f) // 속도가 일정 값 이상일 때만 회전
        {
            float angle = Mathf.Atan2(rigid.velocity.y, rigid.velocity.x) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    void ChangeDirection()
    {
        // 랜덤한 방향으로 변경 (단위 벡터)
        currentDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }
}
