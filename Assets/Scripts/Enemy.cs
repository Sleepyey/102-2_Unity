using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float hp = 5f;
    public float moveSpeed = 2f;        // �̵� �ӵ�

    private Transform player;           // �÷��̾� ������

    // Start
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update
    void Update()
    {
        if (player == null) return;

        // �÷��̾������ ���� ���ϱ�
        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;
        transform.LookAt(player.position);
    }

    public void TakeDamage(float damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }
}
