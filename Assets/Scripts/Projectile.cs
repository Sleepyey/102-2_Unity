using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float damage = 1f;       // 대미지
    public float speed = 20f;       // 이동 시간
    public float lifeTime = 2f;     // 생존 시간 (초)

    private Vector3 direction;

    // Start
    void Start()
    {
        // 일정 시간 후 자동 삭제 (메모리 관리)
        Destroy(gameObject, lifeTime);
    }

    // Update
    void Update()
    {
        // 로컬 forward 방향(앞)으로 이동
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            var enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
            // Projectile 제거
            Destroy(gameObject);
        }
    }
}
