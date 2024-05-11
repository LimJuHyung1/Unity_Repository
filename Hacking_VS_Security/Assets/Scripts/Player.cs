using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    private Vector3 target; // 이동할 목표 위치
    private NavMeshAgent agent; // NavMesh Agent 컴포넌트

    void Start()
    {
        agent = GetComponent<NavMeshAgent>(); // NavMesh Agent 컴포넌트 가져오기
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    void Update()
    {
        SetTargetPosition();
        SetAgentPosition();
    }    

    void SetTargetPosition()
    {
        if (Input.GetMouseButtonDown(1))
        {
            target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }

    void SetAgentPosition()
    {
        agent.SetDestination(new Vector3(target.x, target.y, transform.position.z));
    }
}
