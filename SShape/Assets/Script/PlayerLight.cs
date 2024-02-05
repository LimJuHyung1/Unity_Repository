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

    public float rotationSpeed = 5.0f; // 회전 속도

    void Awake()
    {
        if (isPlayerExisting)
        {
            // ExampleScript가 존재하는 경우 해당 스크립트를 반환, 없으면 null 반환
            Square haveSquareScript = player.GetComponent<Square>();
            Circle haveCircleScript = player.GetComponent<Circle>();

            // 스크립트가 존재하는 경우 실행할 코드
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

            // 2D 오브젝트의 회전값을 가져와서 Spotlight 오브젝트에 적용
            float currentRotation = player.transform.eulerAngles.z;
            transform.rotation = Quaternion.Euler(0f, 0f, currentRotation);

            // 원하는 회전 방향 및 속도로 Spotlight 오브젝트를 회전
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

        // 2D 오브젝트의 회전값을 가져와서 Spotlight 오브젝트에 적용
        float currentRotation = player.transform.eulerAngles.z;
        transform.rotation = Quaternion.Euler(0f, 0f, currentRotation);

        // 원하는 회전 방향 및 속도로 Spotlight 오브젝트를 회전
        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
    }
}
