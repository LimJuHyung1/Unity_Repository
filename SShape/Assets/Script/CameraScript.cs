using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class CameraScript : MonoBehaviour
{
    [SerializeField] GameObject player; // ���� ��� ������Ʈ�� Transform
    Camera cam;
    Vector3 dis;

    bool nowDiscovering = false;
    int otherPlayerLayer = 8;   // ��� �÷��̾� ���̾�

    void Start()
    {
        cam = GetComponent<Camera>();

        dis = new Vector3(0, 0, -15);
    }

    void Update()
    {
        if (player != null)    // �ڽ��� ĳ���Ͱ� ������ ���
        {
            // ��� �÷��̾ �߰�
            if (player.GetComponent<Player>().isDiscovering
                && !nowDiscovering)
            {
                nowDiscovering = true;
                ActiveOtherPlayerLayer();
            }

            // ��� �÷��̾� �þ߿��� ���
            if (!player.GetComponent<Player>().isDiscovering
                && nowDiscovering)
            {
                nowDiscovering = false;
                UnactiveOtherPlayerLayer();
            }
        }
    }

    void LateUpdate()
    {        
        if (player != null)
        {
            // ī�޶� ��ġ ����
            this.transform.position = player.transform.position + dis;

            // ī�޶� �׻� ��� ������Ʈ�� �ٶ󺸰Բ� ȸ�� ����
            transform.LookAt(player.transform);
        }
    }
    
    void ActiveOtherPlayerLayer()   // ��� �÷��̾� ���̰� �ϱ�
    {
        // ���� culling mask ��������
        int currentCullingMask = cam.cullingMask;

        // ���̾ Ȱ��ȭ�ϱ� ���� �ش� ���̾��� ��Ʈ�� OR �������� �߰�
        currentCullingMask |= 1 << otherPlayerLayer;

        // ������Ʈ�� culling mask ����
        cam.cullingMask = currentCullingMask;
    }

    void UnactiveOtherPlayerLayer() // ��� �÷��̾� ������ �ʰ� �ϱ�
    {
        // ���� culling mask ��������
        int currentCullingMask = cam.cullingMask;

        // ���� ���̾�� OtherPlayer ���̾� ����
        currentCullingMask &= ~(1 << otherPlayerLayer);

        cam.cullingMask = currentCullingMask;
    }

    public void FindPlayer()
    {
        if (PhotonNetwork.IsMasterClient)
            player = GameObject.Find("Square(Clone)");
        else
            player = GameObject.Find("Circle(Clone)");
    }
}
