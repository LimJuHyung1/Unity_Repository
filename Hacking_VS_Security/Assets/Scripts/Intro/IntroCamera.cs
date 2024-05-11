using UnityEngine;

public class IntroCamera : MonoBehaviour
{
    public float moveSpeed = 3f; // �̵� �ӵ�
    private Vector3 moveDirection; // �̵� ����
    private float lastDirectionChangeTime; // ������ ���� ���� �ð�
    public float directionChangeInterval = 3f; // ���� ���� ����

    void Start()
    {
        // �ʱ� �̵� ���� ����
        moveDirection = Random.insideUnitSphere.normalized;
        lastDirectionChangeTime = Time.time;
    }

    void FixedUpdate()
    {
        // �̵� �������� �̵��մϴ�.
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);

        // ���� �ð��� ����ϸ� ���ο� ������ �̵� ������ �����մϴ�.
        if (Time.time - lastDirectionChangeTime > directionChangeInterval)
        {
            ChangeDirection();
            lastDirectionChangeTime = Time.time; // ������ ���� ���� �ð� ������Ʈ
        }
    }

    void ChangeDirection()
    {
        // ���ο� ������ �̵� ���� ����
        moveDirection = Random.insideUnitSphere.normalized;
    }
}
