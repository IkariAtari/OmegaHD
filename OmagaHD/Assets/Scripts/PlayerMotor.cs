using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Omega.Player
{
    public class PlayerMotor : MonoBehaviour
    {
        [SerializeField]
        private float Speed;

        private CharacterController Controller;

        private void Start() 
        {
            Controller = GetComponent<CharacterController>();
        }

        private void Update() 
        {
            float xMovement = Input.GetAxisRaw("Horizontal");
            float zMovement = Input.GetAxisRaw("Vertical");
            
            Vector3 xVector = transform.right * xMovement;
            Vector3 zVector = transform.forward * zMovement;

            Vector3 Velocity = (xVector + zVector).normalized * Speed;

            Move(Velocity);
        }

        private void Move(Vector3 Velocity)
        {
            Controller.Move(Velocity);
        }
    }
}
