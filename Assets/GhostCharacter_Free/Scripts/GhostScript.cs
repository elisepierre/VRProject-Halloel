using System.Collections.Generic;
using UnityEngine;

namespace Sample
{
    public class GhostScript : MonoBehaviour
    {
        private Animator Anim;
        private CharacterController Ctrl;
        private Vector3 MoveDirection = Vector3.zero;

        // Cache hash values
        private static readonly int IdleState = Animator.StringToHash("Base Layer.idle");
        private static readonly int MoveState = Animator.StringToHash("Base Layer.move");
        private static readonly int SurprisedState = Animator.StringToHash("Base Layer.surprised");
        private static readonly int AttackState = Animator.StringToHash("Base Layer.attack_shift");
        private static readonly int AttackTag = Animator.StringToHash("Attack");

        // dissolve (optionnel si tu veux garder un effet)
        [SerializeField] private SkinnedMeshRenderer[] MeshR;

        // moving speed
        [SerializeField] private float Speed = 4;

        // player status (pour animations)
        private const int Attack = 2;
        private const int Surprised = 3;
        private Dictionary<int, bool> PlayerStatus = new Dictionary<int, bool>
    {
        {Attack, false },
        {Surprised, false },
    };

        void Start()
        {
            Anim = this.GetComponent<Animator>();
            Ctrl = this.GetComponent<CharacterController>();
        }

        void Update()
        {
            STATUS();
            GRAVITY();
            Respawn();

            // Si aucune animation spéciale en cours
            if (!PlayerStatus.ContainsValue(true))
            {
                MOVE();
                PlayerAttack();
            }
            else
            {
                int status_name = 0;
                foreach (var i in PlayerStatus)
                {
                    if (i.Value == true)
                    {
                        status_name = i.Key;
                        break;
                    }
                }
                if (status_name == Attack)
                {
                    PlayerAttack();
                }
                else if (status_name == Surprised)
                {
                    // rien, animation surprise
                }
            }
        }

        //---------------------------------------------------------------------
        // character status
        //---------------------------------------------------------------------
        private void STATUS()
        {
            // during attacking
            if (Anim.GetCurrentAnimatorStateInfo(0).tagHash == AttackTag)
            {
                PlayerStatus[Attack] = true;
            }
            else
            {
                PlayerStatus[Attack] = false;
            }
            // during surprised
            if (Anim.GetCurrentAnimatorStateInfo(0).fullPathHash == SurprisedState)
            {
                PlayerStatus[Surprised] = true;
            }
            else
            {
                PlayerStatus[Surprised] = false;
            }
        }

        //---------------------------------------------------------------------
        // play attack animation
        //---------------------------------------------------------------------
        private void PlayerAttack()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                Anim.CrossFade(AttackState, 0.1f, 0, 0);
            }
        }

        //---------------------------------------------------------------------
        // gravity for fall of this character
        //---------------------------------------------------------------------
        private void GRAVITY()
        {
            if (Ctrl.enabled)
            {
                if (CheckGrounded())
                {
                    if (MoveDirection.y < -0.1f)
                    {
                        MoveDirection.y = -0.1f;
                    }
                }
                MoveDirection.y -= 0.1f;
                Ctrl.Move(MoveDirection * Time.deltaTime);
            }
        }

        //---------------------------------------------------------------------
        // check if grounded
        //---------------------------------------------------------------------
        private bool CheckGrounded()
        {
            if (Ctrl.isGrounded && Ctrl.enabled)
            {
                return true;
            }
            Ray ray = new Ray(this.transform.position + Vector3.up * 0.1f, Vector3.down);
            float range = 0.2f;
            return Physics.Raycast(ray, range);
        }

        //---------------------------------------------------------------------
        // movement
        //---------------------------------------------------------------------
        private void MOVE()
        {
            if (Anim.GetCurrentAnimatorStateInfo(0).fullPathHash == MoveState)
            {
                if (Input.GetKey(KeyCode.UpArrow))
                {
                    MOVE_Velocity(new Vector3(0, 0, -Speed), new Vector3(0, 180, 0));
                }
                else if (Input.GetKey(KeyCode.DownArrow))
                {
                    MOVE_Velocity(new Vector3(0, 0, Speed), new Vector3(0, 0, 0));
                }
                else if (Input.GetKey(KeyCode.LeftArrow))
                {
                    MOVE_Velocity(new Vector3(Speed, 0, 0), new Vector3(0, 90, 0));
                }
                else if (Input.GetKey(KeyCode.RightArrow))
                {
                    MOVE_Velocity(new Vector3(-Speed, 0, 0), new Vector3(0, 270, 0));
                }
            }
            KEY_DOWN();
            KEY_UP();
        }

        private void MOVE_Velocity(Vector3 velocity, Vector3 rot)
        {
            MoveDirection = new Vector3(velocity.x, MoveDirection.y, velocity.z);
            if (Ctrl.enabled)
            {
                Ctrl.Move(MoveDirection * Time.deltaTime);
            }
            MoveDirection.x = 0;
            MoveDirection.z = 0;
            this.transform.rotation = Quaternion.Euler(rot);
        }

        private void KEY_DOWN()
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow) ||
                Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                Anim.CrossFade(MoveState, 0.1f, 0, 0);
            }
        }

        private void KEY_UP()
        {
            if (!Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow) &&
                !Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
            {
                Anim.CrossFade(IdleState, 0.1f, 0, 0);
            }
        }

        //---------------------------------------------------------------------
        // respawn (reset position)
        //---------------------------------------------------------------------
        private void Respawn()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Ctrl.enabled = false;
                this.transform.position = Vector3.zero;
                this.transform.rotation = Quaternion.Euler(Vector3.zero);
                Ctrl.enabled = true;

                Anim.CrossFade(IdleState, 0.1f, 0, 0);
            }
        }
    }
}
