using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private const float _minY = -20f;
    private const float _maxY = 20f;
    
    [SerializeField] private CharacterController controller;
    
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotationSpeed;

    private float _currentY;

    public GameObject cameras;

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        _currentY = cameras.transform.rotation.eulerAngles.x;
    }

    private void FixedUpdate()
    {
        
            var vertical = Input.GetAxis("Vertical");
            var horizontal = Input.GetAxis("Horizontal");
            var mx = Input.GetAxis("Mouse X");
            var my = Input.GetAxis("Mouse Y");

            if (vertical != 0)
            {
                controller.Move(transform.forward * (vertical * moveSpeed * Time.deltaTime));
            }

            if (horizontal != 0)
            {
                controller.Move(transform.right * (horizontal * moveSpeed * Time.deltaTime));
            }

            if (mx != 0)
            {
                transform.Rotate(transform.up * (mx * rotationSpeed * Time.deltaTime));
            }

            if (my != 0)
            {
                _currentY = Math.Clamp(_currentY - my * rotationSpeed * Time.deltaTime, _minY, _maxY);
                
                Vector3 camRotation = cameras.transform.rotation.eulerAngles;
                
                cameras.transform.rotation = Quaternion.Euler(_currentY, camRotation.y, camRotation.z);
            }
            
            controller.Move(Physics.gravity * Time.deltaTime);
    }
}
