using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Omega.Misc;

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

        [SerializeField]
        private float Viewrange;

        private CharacterController Controller;

        private float Gravilocity;

        private bool HasJumped = false;

        [SerializeField]
        private float JumpPower;

        private void Start() 
        {
            Controller = GetComponent<CharacterController>();
        }

        private void Update() 
        {
            if(Input.GetKeyDown(KeyCode.Space) && Controller.isGrounded && HasJumped != true)
            {
                HasJumped = true;
            }

            if(Controller.isGrounded != true)
            {
                Gravilocity -= GlobalConstants.GRAVITATIONAL_ACCELERATION * Time.deltaTime;
            }
            else if(HasJumped)
            {
                Gravilocity += JumpPower;
                HasJumped = false;
            }
            else
            {
                Gravilocity = -1f;
            }

            float xMovement = Input.GetAxisRaw("Horizontal");
            float zMovement = Input.GetAxisRaw("Vertical");
            float yRotation = Input.GetAxisRaw("Mouse Y");
            float xRotation = Input.GetAxisRaw("Mouse X");
            
            Vector3 xVector = transform.right * xMovement * Time.deltaTime;
            Vector3 zVector = transform.forward * zMovement * Time.deltaTime;

            Vector3 yRotationVector = new Vector3(0, xRotation, 0) * LookSensitivity;
            Vector3 Velocity = (xVector + zVector).normalized * Speed;

            Move(Velocity);
            Move(new Vector3(0, Gravilocity, 0));
            Rotation(yRotation * LookSensitivity, yRotationVector);
        }

        private void Move(Vector3 Velocity)
        {
            Controller.Move(Velocity);
        }

        private void Rotation(float CameraRotation, Vector3 BodyRotation)
        {
            this.transform.Rotate(transform.rotation * BodyRotation);
            Cam.transform.localRotation *= Quaternion.Euler(-CameraRotation, 0, 0);
            Cam.transform.localRotation = Quaternion.Euler(Mathf.Max(Cam.transform.eulerAngles.x, -Viewrange), 0, 0);
        }
    }
}