using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject projectilePrefab1;     // Projectile Prefab 1
    public GameObject projectilePrefab2;     // Projectile Prefab 2
    public Transform firePoint;                     // �߻� ��ġ (�ѱ�)
    Camera cam;

    public int projectileZ = 0;

    // Start
    void Start()
    {
        cam = Camera.main;      // ���� ī�޶� ��������
    }

    // Update
    void Update()
    {
        if (Input.GetMouseButtonDown(0))        // ��Ŭ�� �߻�
        {
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (projectileZ == 0)
            {
                projectileZ++;
            }
            else
            {
                projectileZ = 0;
            }
        }
    }

    void Shoot()
    {
        // ȭ�鿡�� ���콺 �� ����(Ray) ���
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        Vector3 targetPoint;
        targetPoint = ray.GetPoint(50f);
        Vector3 direction = (targetPoint - firePoint.position).normalized;      // ���� ����


        // Projectile ����
        if (projectileZ == 0)
        {
            GameObject proj = Instantiate(projectilePrefab1, firePoint.position, Quaternion.LookRotation(direction));
        }
        else
        {
            GameObject proj = Instantiate(projectilePrefab2, firePoint.position, Quaternion.LookRotation(direction));
        }
    }
}
