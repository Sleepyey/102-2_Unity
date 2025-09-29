using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject projectilePrefab1;     // Projectile Prefab 1
    public GameObject projectilePrefab2;     // Projectile Prefab 2
    public Transform firePoint;                     // 발사 위치 (총구)
    Camera cam;

    public int projectileZ = 0;

    // Start
    void Start()
    {
        cam = Camera.main;      // 메인 카메라 가져오기
    }

    // Update
    void Update()
    {
        if (Input.GetMouseButtonDown(0))        // 좌클릭 발사
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
        // 화면에서 마우스 → 광선(Ray) 쏘기
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        Vector3 targetPoint;
        targetPoint = ray.GetPoint(50f);
        Vector3 direction = (targetPoint - firePoint.position).normalized;      // 방향 벡터


        // Projectile 생성
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
