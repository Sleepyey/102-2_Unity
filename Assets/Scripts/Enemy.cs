using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public enum EnemyState { Idle, Trace, Attack, RunAway }
    public EnemyState state = EnemyState.Idle;

    public float maxHP = 5f;
    public float currentHP;
    public float moveSpeed = 2f;        // �̵� �ӵ�

    public float traceRange = 15f;      // ���� ���� �Ÿ�
    public float attackRange = 6f;      // ���� ���� �Ÿ�
    public float attackCooldown = 1.5f;
    private float lastAttackTime;

    public GameObject projectilePrefab; // ����ü ������
    public Transform firePoint;         // �߻� ��ġ

    private Transform player;           // �÷��̾� ������

    [Header("HP Bar")]
    public Slider hpBar;
    Camera cam;

    // Start
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        cam = Camera.main;
        lastAttackTime = -attackCooldown;
        currentHP = maxHP;

        hpBar.transform.localPosition = new Vector3(0f, 2f, 0f);
    }

    // Update
    void Update()
    {
        if (player == null) return;

        hpBar.transform.forward = cam.transform.forward;

        float dist = Vector3.Distance(player.position, transform.position);

        if (currentHP <= maxHP * 0.2 && dist < attackRange)
        {
            state = EnemyState.RunAway;
        }

        // FSM ���� ��ȯ
        switch (state)
        {
            case EnemyState.Idle:
                if (dist < traceRange)
                    state = EnemyState.Trace;
                break;

            case EnemyState.Trace:
                if (dist < attackRange)
                    state = EnemyState.Attack;
                else if (dist > traceRange)
                    state = EnemyState.Idle;
                else
                    TracePlayer();
                break;

            case EnemyState.Attack:
                if (dist > attackRange)
                    state = EnemyState.Trace;
                else
                    AttackPlayer();
                break;
            case EnemyState.RunAway:
                RunAwayEnemy();
                if (dist > attackRange)
                    state = EnemyState.Idle;
                break;
        }
    }

    void TracePlayer()
    {
        // �÷��̾������ ���� ���ϱ�
        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;
        transform.LookAt(player.position);
    }

    void RunAwayEnemy()
    {
        // �÷��̾� �ݴ� �������� �̵�
        Vector3 RunA = new Vector3 (transform.position.x - player.position.x, 0f, transform.position.z - player.position.z).normalized;
        transform.position += RunA * moveSpeed * Time.deltaTime;
    }

    void AttackPlayer()
    {
        // ���� ��Ÿ�Ӹ��� �߻�
        if (Time.time >= lastAttackTime + attackCooldown)
        {
            lastAttackTime = Time.time;
            ShootProjectile();
        }
    }

    void ShootProjectile()
    {
        if (projectilePrefab != null && firePoint != null)
        {
            transform.LookAt(player.position);
            GameObject proj = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
            EnemyProjectile ep = proj.GetComponent<EnemyProjectile>();
            if (ep != null)
            {
                Vector3 dir = (player.position - firePoint.position).normalized;
                ep.SetDirection(dir);
            }
        }
    }

    public void TakeDamage(float damage)
    {
        currentHP -= damage;
        hpBar.value = (float)currentHP / maxHP;

        if (currentHP <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
