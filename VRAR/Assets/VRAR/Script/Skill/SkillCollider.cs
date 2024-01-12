using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillCollider : MonoBehaviour
{
    BoxCollider box;

    public int skillLevel = 0;    // ������ 0���� �����ϱ�!
    public static bool isDrain = false;
    float walkSpeedOrigin;

    void Start()
    {
        if(this.name != "Debuff" && this.name != "Healing")
            box = GetComponent<BoxCollider>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Monster"))
        {
            switch (this.name)
            {
                case "Meteors AOE": //���׿� ȿ��
                    other.GetComponent<Monsters>().DamagedHp(10, skillLevel);
                    break;

                case "Snow AOE":    // ���̽� ���Ǿ� ȿ��
                    other.GetComponent<Monsters>().DamagedHp(6, skillLevel);
                    other.GetComponent<Monsters>().walkSpeed /= 2;
                    break;

                case "Holy hit":    // ����Ʈ�� ��Ʈ����ũ ȿ��
                    other.GetComponent<Monsters>().DamagedHp(8, skillLevel);
                    break;

                case "Green hit":   // ������ ȿ��
                    if (ZeroOrOne() == 0)
                    {
                        other.GetComponent<Monsters>().DamagedHp(5, skillLevel);
                    }
                    else if (ZeroOrOne() == 1)
                    {
                        other.GetComponent<Monsters>().DamagedHp(5, skillLevel * 2);
                    }
                    break;

                case "Explosion":   // �ظ� ũ���� ȿ��
                    walkSpeedOrigin = other.GetComponent<Monsters>().walkSpeed;
                    other.GetComponent<Monsters>().DamagedHp(8, skillLevel);
                    other.GetComponent<Monsters>().walkSpeed = 0;
                    Invoke("StunOff", 1 * skillLevel + 1);
                    break;
            }
            if (isDrain)
            {
                GameObject player = GameObject.Find("Slime_01");
                player.GetComponent<Player>().hp += 2 * skillLevel;
            }
        }

        else if (other.CompareTag("Dragon"))
        {
            switch (this.name)
            {
                case "Meteors AOE": //���׿� ȿ��
                    other.GetComponent<Dragons>().DamagedHp(10, skillLevel);
                    break;

                case "Snow AOE":    // ���̽� ���Ǿ� ȿ��
                    other.GetComponent<Dragons>().DamagedHp(6, skillLevel);
                    other.GetComponent<Dragons>().walkSpeed /= 2;
                    break;

                case "Holy hit":    // ����Ʈ�� ��Ʈ����ũ ȿ��
                    other.GetComponent<Dragons>().DamagedHp(8, skillLevel);
                    break;

                case "Green hit":   // ������ ȿ��
                    if (ZeroOrOne() == 0)
                    {
                        other.GetComponent<Dragons>().DamagedHp(5, skillLevel);
                    }
                    else if (ZeroOrOne() == 1)
                    {
                        other.GetComponent<Dragons>().DamagedHp(5, skillLevel * 2);
                    }
                    break;

                case "Explosion":   // �ظ� ũ���� ȿ��
                    walkSpeedOrigin = other.GetComponent<Dragons>().walkSpeed;
                    other.GetComponent<Dragons>().DamagedHp(8, skillLevel);
                    if (other != null)
                    {
                        other.GetComponent<Dragons>().walkSpeed = 0;
                        Invoke("StunOff", 1 + skillLevel);
                    }
                    break;
            }
            if (isDrain)
            {
                GameObject player = GameObject.Find("Slime_01");
                player.GetComponent<Player>().hp += 2 * skillLevel;
            }
        }

        else if (other.CompareTag("Resources"))
        {
            other.GetComponent<Resources>().ActivateParticleSystem();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Monster"))
            other.GetComponent<Monsters>().Idle();
    }

    int ZeroOrOne()
    {
        System.Random random = new System.Random();

        return random.Next(0, 2); // 0�� 1 �� ������ ��ȯ
    }

    void StunOff(Collider other)
    {
        other.GetComponent<Monsters>().walkSpeed = walkSpeedOrigin;
    }
}