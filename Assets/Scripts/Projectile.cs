using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float damage = 1f;       // �����
    public float speed = 20f;       // �̵� �ð�
    public float lifeTime = 2f;     // ���� �ð� (��)

    private Vector3 direction;

    // Start
    void Start()
    {
        // ���� �ð� �� �ڵ� ���� (�޸� ����)
        Destroy(gameObject, lifeTime);
    }

    // Update
    void Update()
    {
        // ���� forward ����(��)���� �̵�
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
            // Projectile ����
            Destroy(gameObject);
        }
    }
}
