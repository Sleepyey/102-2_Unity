using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinemachineSwitcher : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCam;     // �⺻ TPS ī�޶�
    public CinemachineFreeLook freeLookCam;         // ���� ȸ�� TPS ī�޶�
    public bool usingFreeLook = false;

    // Start
    void Start()
    {
        //������ Virtual Camera Ȱ��ȭ
        virtualCam.Priority = 10;
        freeLookCam.Priority = 0;
    }

    // Update
    void Update()
    {
        if (Input.GetMouseButtonDown(1))     // ��Ŭ��
        {
            usingFreeLook = !usingFreeLook;
            if (usingFreeLook)
            {
                freeLookCam.Priority = 20;          // FreeLook Ȱ��ȭ
                virtualCam.Priority = 0;
            }
            else
            {
                virtualCam.Priority = 20;           // Virtual Camera Ȱ��ȭ
                freeLookCam.Priority = 0;
            }
        }
    }
}
