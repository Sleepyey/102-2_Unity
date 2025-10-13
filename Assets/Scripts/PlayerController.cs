using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public CinemachineSwitcher cameraS;

    private float fovD = 40f;
    private float speedD = 5f;
    public float speed = 5f;
    public float jumpPower = 5f;
    public float gravity = -9.81f;

    public int maxHP = 100;
    private int currentHP;

    public CinemachineVirtualCamera virtualCam;
    public float rotationSpeed = 10f;
    private CinemachinePOV pov;
    private CharacterController controller;
    private Vector3 velocity;
    public bool isGrounded;

    public Slider hpSlider;

    // Start
    void Start()
    {
        controller = GetComponent<CharacterController>();
        pov = virtualCam.GetCinemachineComponent<CinemachinePOV>();
        // Virtual Camera�� POV ������Ʈ ��������

        currentHP = maxHP;
        hpSlider.value = 1f;
    }

    // Update
    void Update()
    {
        // Tab�� ������ ���콺 ����Ʈ�� ȭ�� �߾����� �̵�
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            pov.m_HorizontalAxis.Value = transform.eulerAngles.y;
            pov.m_VerticalAxis.Value = 0f;
        }

        // ���� ��� �ִ��� Ȯ��
        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // ���鿡 ���̱�
        }

        if (cameraS.usingFreeLook)
        {
            speed = 0f;
        }
        else
        {
            speed = speedD;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // ī�޶� ���� ���� ���
        Vector3 camForward = virtualCam.transform.forward;
        camForward.y = 0f;
        camForward.Normalize();

        Vector3 camRight = virtualCam.transform.right;
        camRight.y = 0;
        camRight.Normalize();

        Vector3 move = (camForward * z + camRight * x).normalized; // �̵� ���� = ī�޶� forward/right ���
        controller.Move(move * speed * Time.deltaTime);

        float cameraYaw = pov.m_HorizontalAxis.Value; // ���콺 �¿� ȸ����
        Quaternion targetRot = Quaternion.Euler(0f, cameraYaw, 0f);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, rotationSpeed * Time.deltaTime);

        // ����
        if (isGrounded && Input.GetKeyDown(KeyCode.Space) && cameraS.usingFreeLook == false)
        {
            velocity.y = jumpPower;
        }

        // �߷� ����
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        // �޸���
        if (Input.GetKey(KeyCode.LeftShift) && cameraS.usingFreeLook == false)
        {
            speed = 10f;
            virtualCam.m_Lens.FieldOfView = 60;
        }
        else
        {
            speed = 5f;
            virtualCam.m_Lens.FieldOfView = fovD;
        }
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        hpSlider.value = (float)currentHP / maxHP;

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
