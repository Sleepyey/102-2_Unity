using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 2f;        // 이동 속도
    private Transform player;           // 플레이어 추적용

    // Start
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update
    void Update()
    {
        if (player == null) return;

        // 플레이어까지의 방향 구하기
        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;
        transform.LookAt(player.position);
    }
}
