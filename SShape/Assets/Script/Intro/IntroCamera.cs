using UnityEngine;

public class IntroCamera : MonoBehaviour
{
    public Transform target; // ���� ��� ������Ʈ�� Transform
    Camera cam;
    Vector3 dis;

    void Start()
    {
        cam = GetComponent<Camera>();

        dis = new Vector3(0, 0, -15);
    }

    void LateUpdate()
    {
        if (target != null)
        {
            // ī�޶� ��ġ ����
            this.transform.position = target.position + dis;

            // ī�޶� �׻� ��� ������Ʈ�� �ٶ󺸰Բ� ȸ�� ����
            transform.LookAt(target);
        }
    }
}
