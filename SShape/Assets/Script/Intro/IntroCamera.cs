using UnityEngine;

public class IntroCamera : MonoBehaviour
{
    public Transform target; // 따라갈 대상 오브젝트의 Transform
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
            // 카메라 위치 설정
            this.transform.position = target.position + dis;

            // 카메라가 항상 대상 오브젝트를 바라보게끔 회전 설정
            transform.LookAt(target);
        }
    }
}
