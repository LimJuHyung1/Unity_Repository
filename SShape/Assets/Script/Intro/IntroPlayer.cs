using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroPlayer : MonoBehaviour
{
    Rigidbody2D rigid;

    public float moveSpeed = 5f; // �̵� �ӵ� ����
    public float rotationSpeed = 180f; // ȸ�� �ӵ� ����
    private float directionChangeTime = 2f; // ���� ��ȯ �ð� ����
    private Vector2 currentDirection;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();

        InvokeRepeating("ChangeDirection", 1f, directionChangeTime); // ���� �ð����� ���� ���� �Լ� ȣ��
    }

    void FixedUpdate()
    {
        rigid.velocity = currentDirection * moveSpeed; // ������Ʈ�� ���� �������� �̵�

        // ���� ���⿡ ���� ������Ʈ ȸ��
        if (rigid.velocity.magnitude > 0.1f) // �ӵ��� ���� �� �̻��� ���� ȸ��
        {
            float angle = Mathf.Atan2(rigid.velocity.y, rigid.velocity.x) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    void ChangeDirection()
    {
        // ������ �������� ���� (���� ����)
        currentDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }
}
