using UnityEngine;

public class IntroCamera : MonoBehaviour
{
    public float moveSpeed = 3f; // 이동 속도
    private Vector3 moveDirection; // 이동 방향
    private float lastDirectionChangeTime; // 마지막 방향 변경 시간
    public float directionChangeInterval = 3f; // 방향 변경 간격

    void Start()
    {
        // 초기 이동 방향 설정
        moveDirection = Random.insideUnitSphere.normalized;
        lastDirectionChangeTime = Time.time;
    }

    void FixedUpdate()
    {
        // 이동 방향으로 이동합니다.
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);

        // 일정 시간이 경과하면 새로운 무작위 이동 방향을 설정합니다.
        if (Time.time - lastDirectionChangeTime > directionChangeInterval)
        {
            ChangeDirection();
            lastDirectionChangeTime = Time.time; // 마지막 방향 변경 시간 업데이트
        }
    }

    void ChangeDirection()
    {
        // 새로운 무작위 이동 방향 설정
        moveDirection = Random.insideUnitSphere.normalized;
    }
}
