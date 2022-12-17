using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class CharacterMovement : MonoBehaviour
    {
        public float speed;

        private Animator animator;
        private Rigidbody2D rigidbody;
        private ParticleSystem dust;

        private Vector2 lastDir;

        bool gamePaused = false;

        private void Start()
        {
            animator = GetComponent<Animator>();
            rigidbody = GetComponent<Rigidbody2D>();
            dust = GetComponentInChildren<ParticleSystem>();
        }


        private void Update()
        {
            if (gamePaused) return;
            
            Vector2 dir = Vector2.zero;
            if (Input.GetKey(KeyCode.A))
            {
                dir.x = -1;
                animator.SetInteger("Direction", 3);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                dir.x = 1;
                animator.SetInteger("Direction", 2);
            }

            if (Input.GetKey(KeyCode.W))
            {
                dir.y = 1;
                animator.SetInteger("Direction", 1);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                dir.y = -1;
                animator.SetInteger("Direction", 0);
            }

            dir.Normalize();

            if (lastDir != dir && dir != Vector2.zero)
            {
                CreateDust();
            }

            lastDir = dir;

            animator.SetBool("IsMoving", dir.magnitude > 0);

            rigidbody.velocity = speed * dir;
        }

        private void CreateDust()
        {
            dust.Play();
        }

        public void ChangePauseState(bool state)
        {
            gamePaused = state;
        }
}
