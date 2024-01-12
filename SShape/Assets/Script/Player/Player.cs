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
    protected Vector2 moveVec;    // rigid.MovePosition을 통해 움직이도록 설정
    protected float walkSpeed = 5f;

    protected float x;
    protected float y;

    protected float fadeDuration = 1.0f; // 페이드 지속 시간 설정    
    public PhotonView PV;

    protected virtual void Start()
    {
        // "Canvas"라는 이름을 가진 부모 오브젝트를 찾음
        canvasObject = GameObject.Find("Canvas");

        if (canvasObject != null)
        {
            // 부모 오브젝트 내에서 "joystick"이라는 이름을 가진 자식 오브젝트를 찾음
            joystickTransform = canvasObject.transform.Find("Joystick");

            if (joystickTransform != null)
            {
                // 찾은 자식 오브젝트에 대한 작업 수행
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

            Quaternion dirQuat = Quaternion.LookRotation(moveVec);  // 회전하려는 방향

            float angle = Mathf.Atan2(moveVec.y, moveVec.x) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 0.3f);
        }
        else
        {
            //Debug.Log("조이스틱 참조하지 못 함");
        }
    }

    protected virtual void InitStatus()   // 스탯 초기화
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
        finalColor.a = 1f; // 완전히 페이드 인되었을 때 알파 값 1로 설정
        spriteRenderer.color = finalColor;
    }

    // 페이드 아웃 함수
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
        finalColor.a = 0f; // 완전히 페이드 아웃되었을 때 알파 값 0으로 설정
        spriteRenderer.color = finalColor;
    }
}
