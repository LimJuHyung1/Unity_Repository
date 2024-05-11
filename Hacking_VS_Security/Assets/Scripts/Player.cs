using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    private Vector3 target; // �̵��� ��ǥ ��ġ
    private NavMeshAgent agent; // NavMesh Agent ������Ʈ

    void Start()
    {
        agent = GetComponent<NavMeshAgent>(); // NavMesh Agent ������Ʈ ��������
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
