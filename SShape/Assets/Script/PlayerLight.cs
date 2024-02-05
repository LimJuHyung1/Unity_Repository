using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLight : MonoBehaviourPunCallbacks
{
    public PhotonView PV;
    PhotonView player;
    bool isPlayerExisting = false;
    bool isSquare = false;
    bool isCircle = false;

    public float rotationSpeed = 5.0f; // ȸ�� �ӵ�

    void Awake()
    {
        if (isPlayerExisting)
        {
            // ExampleScript�� �����ϴ� ��� �ش� ��ũ��Ʈ�� ��ȯ, ������ null ��ȯ
            Square haveSquareScript = player.GetComponent<Square>();
            Circle haveCircleScript = player.GetComponent<Circle>();

            // ��ũ��Ʈ�� �����ϴ� ��� ������ �ڵ�
            if (haveSquareScript != null)
            {
                isSquare = true;
            }
            else if (haveCircleScript != null)
            {
                isCircle = true;
            }
        }
    }

    void Update()
    { 
        if (isSquare)
        {
            transform.position = player.transform.position;

            // 2D ������Ʈ�� ȸ������ �����ͼ� Spotlight ������Ʈ�� ����
            float currentRotation = player.transform.eulerAngles.z;
            transform.rotation = Quaternion.Euler(0f, 0f, currentRotation);

            // ���ϴ� ȸ�� ���� �� �ӵ��� Spotlight ������Ʈ�� ȸ��
            transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
        }

        else if (isCircle)
        {
            Debug.Log("Circle is existing");
        }
    }

    [PunRPC]
    void FindPlayer(int viewID)
    {
        player = PhotonView.Find(viewID);
        isPlayerExisting = true;
    }

    [PunRPC]
    void LightSquare()
    {
        transform.position = player.transform.position;

        // 2D ������Ʈ�� ȸ������ �����ͼ� Spotlight ������Ʈ�� ����
        float currentRotation = player.transform.eulerAngles.z;
        transform.rotation = Quaternion.Euler(0f, 0f, currentRotation);

        // ���ϴ� ȸ�� ���� �� �ӵ��� Spotlight ������Ʈ�� ȸ��
        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
    }
}
