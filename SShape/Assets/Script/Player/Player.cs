using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviourPunCallbacks
{
    [SerializeField] GameObject joystick;
    [SerializeField] GameObject canvasObject;
    [SerializeField] Transform joystickTransform;

    [SerializeField] protected int hp;
    [SerializeField] protected int hpMax;
    [SerializeField] protected int atkDamage;

    protected Rigidbody2D rigid;
    protected SpriteRenderer spriteRenderer;

    public bool isDiscovering = false;
    protected Vector2 moveVec;    // rigid.MovePosition�� ���� �����̵��� ����
    protected float walkSpeed = 5f;

    protected float x;
    protected float y;

    protected float fadeDuration = 1.0f; // ���̵� ���� �ð� ����    
    public PhotonView PV;

    protected virtual void Start()
    {
        // "Canvas"��� �̸��� ���� �θ� ������Ʈ�� ã��
        canvasObject = GameObject.Find("Canvas");

        if (canvasObject != null)
        {
            // �θ� ������Ʈ ������ "joystick"�̶�� �̸��� ���� �ڽ� ������Ʈ�� ã��
            joystickTransform = canvasObject.transform.Find("Joystick");

            if (joystickTransform != null)
            {
                // ã�� �ڽ� ������Ʈ�� ���� �۾� ����
                joystick = joystickTransform.gameObject;
                Debug.Log("Joystick found!");
            }
            else
            {
                Debug.LogError("Joystick not found!");
            }
        }
        else
        {
            Debug.LogError("Canvas not found!");
        }

        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected virtual void FixedUpdate()
    {
        if(joystick != null && PV.IsMine)
        {
            x = joystick.GetComponent<VariableJoystick>().Horizontal;
            y = joystick.GetComponent<VariableJoystick>().Vertical;

            // Move
            moveVec = new Vector2(x, y) * walkSpeed * Time.deltaTime;
            rigid.MovePosition(rigid.position + moveVec);

            if (moveVec.sqrMagnitude == 0)
                return; // no input = no rotation

            Quaternion dirQuat = Quaternion.LookRotation(moveVec);  // ȸ���Ϸ��� ����

            float angle = Mathf.Atan2(moveVec.y, moveVec.x) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 0.3f);
        }
        else
        {
            //Debug.Log("���̽�ƽ �������� �� ��");
        }
    }

    protected virtual void InitStatus()   // ���� �ʱ�ȭ
    {

    }

    protected virtual IEnumerator FadeIn()
    {
        float timer = 0f;

        while (timer < fadeDuration)
        {
            float alpha = Mathf.Lerp(0f, 1f, timer / fadeDuration);
            Color spriteColor = spriteRenderer.color;
            spriteColor.a = alpha;
            spriteRenderer.color = spriteColor;
            timer += Time.deltaTime;
            yield return null;
        }

        Color finalColor = spriteRenderer.color;
        finalColor.a = 1f; // ������ ���̵� �εǾ��� �� ���� �� 1�� ����
        spriteRenderer.color = finalColor;
    }

    // ���̵� �ƿ� �Լ�
    protected virtual IEnumerator FadeOut()
    {
        float timer = 0f;

        while (timer < fadeDuration)
        {
            float alpha = Mathf.Lerp(1f, 0f, timer / fadeDuration);
            Color spriteColor = spriteRenderer.color;
            spriteColor.a = alpha;
            spriteRenderer.color = spriteColor;
            timer += Time.deltaTime;
            yield return null;
        }

        Color finalColor = spriteRenderer.color;
        finalColor.a = 0f; // ������ ���̵� �ƿ��Ǿ��� �� ���� �� 0���� ����
        spriteRenderer.color = finalColor;
    }
}
