using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Omega.Player
{
    public class PlayerMotor : MonoBehaviour
    {
        [SerializeField]
        private Camera Cam;

        [SerializeField]
        private float Speed;

        [SerializeField]
        private float LookSensitivity;

        private CharacterController Controller;

        private void Start() 
        {
            Controller = GetComponent<CharacterController>();
        }

        private void Update() 
        {
            float xMovement = Input.GetAxisRaw("Horizontal");
            float zMovement = Input.GetAxisRaw("Vertical");
            float xRotation = Input.GetAxisRaw("Mouse Y");
            float yRotation = Input.GetAxisRaw("Mouse X");
            
            Vector3 xVector = transform.right * xMovement * Time.deltaTime;
            Vector3 zVector = transform.forward * zMovement * Time.deltaTime;

            Vector3 xRotationVector = new Vector3(-xRotation, 0, 0) * LookSensitivity;
            Vector3 yRotationVector = new Vector3(0, yRotation, 0) * LookSensitivity;

            Vector3 Velocity = (xVector + zVector).normalized * Speed;

            Move(Velocity);
            Rotation(xRotationVector, yRotationVector);
        }

        private void Move(Vector3 Velocity)
        {
            Controller.Move(Velocity);
        }

        private void Rotation(Vector3 X, Vector3 Y)
        {
            this.transform.Rotate(transform.rotation * Y);

            Cam.transform.Rotate(X);
        }
    }
}
