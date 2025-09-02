using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed = 5f;
    private CharacterController controller;

    // Start
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 move = new Vector3 (h, 0, v);
        controller.Move(move * speed * Time.deltaTime);
    }
}
