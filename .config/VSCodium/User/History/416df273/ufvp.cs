using Metroidvania.Animations;
using System.Collections;
using UnityEngine;

namespace Metroidvania
{
    /// <summary>
    /// Class which controls the player actions
    /// </summary>
    public class PlayerController : MonoBehaviour
    {
        #region Animations
        public static readonly int IdleAnimHash = Animator.StringToHash("Idle");
        public static readonly int RunAnimHash = Animator.StringToHash("Run");

        public static readonly int JumpAnimHash = Animator.StringToHash("Jump");
        public static readonly int FallAnimHash = Animator.StringToHash("Fall");

        public static readonly int RollAnimHash = Animator.StringToHash("Roll");

        public static readonly int SlideAnimHash = Animator.StringToHash("Slide");
        public static readonly int SlideEndAnimHash = Animator.StringToHash("SlideEnd");

        public static readonly int WallslideAnimHash = Animator.StringToHash("Wallslide");

        public static readonly int CrouchIdleAnimHash = Animator.StringToHash("CrouchIdle");
        public static readonly int CrouchWalkAnimHash = Animator.StringToHash("CrouchWalk");
        public static readonly int CrouchTransitionAnimHash = Animator.StringToHash("CrouchTransition");
        public static readonly int CrouchAttackAnimHash = Animator.StringToHash("CrouchAttack");

        public static readonly int FirstAttackAnimHash = Animator.StringToHash("FirstAttack");
        public static readonly int SecondAttackAnimHash = Animator.StringToHash("SecondAttack");

        public static readonly int HurtAnimHash = Animator.StringToHash("Hurt");
        public static readonly int DieAnimHash = Animator.StringToHash("Die");
        #endregion
        /// <summary>
        /// Number of continous jumps that the plyer can do (NUMBER 0 COUNTS)
        /// </summary>
        [SerializeField] private float maxJumps = 1.0f;

        /// <summary>
        /// Reference to the SpriteSheetAnimator in the child
        /// </summary>
        [SerializeField] private SpriteSheetAnimator spriteSheetAnimation;

        /// <summary>
        /// Reference to the spriteController
        /// </summary>
       // [SerializeField] private SpriteController spriteController;

        /// <summary>
        /// How much time does the duration of the attack long
        /// </summary>
        [SerializeField] private float attackDuration = 0.1f;

        /// <summary>
        /// Represents the currentAnimation playing
        /// </summary>
        private int currentAnimationHash { get; set; }

        /// <summary>
        /// Actual Jumps of the Player
        /// </summary>
        private float numJumps = 1.0f;

        /// <summary>
        /// If the player is making an important animation we stop checking
        /// </summary>
        private bool priorityAnimationPlaying = false;

        /// <summary>
        /// Detects if the player has already performe an attack before
        /// </summary>
        private bool playerAttackedBefore = false;

        /// <summary>
        /// If the player canCrouch
        /// </summary>
        private bool canCrouch = true;
         
        /// <summary>
        /// Detects if the player is crouching
        /// </summary>
        private bool isPlayerCrouching = false;

        /// <summary>
        /// Reference to the Input
        /// </summary>
        private PlayerInputManager _myInputManager;

        /// <summary>
        /// Reference to the MoveMentControler
        /// </summary>
        private MovementController _myMovementController;

        /// <summary>
        /// Reference to the HealthHandler
        /// </summary>
        private HealthHandler _myHealthHandler;

        /// <summary>
        /// Detects if the animation which is gonna be played is already playing, if not it change it
        /// </summary>
        /// <param name="animHash"></param> --> Which animation is gonna play
        private void changeAnimation(int animHash)
        {
            if (currentAnimationHash != animHash)
            {
                currentAnimationHash = animHash;
                spriteSheetAnimation.SetSheet(animHash);
            }
        }
        /// <summary>
        /// Gets the the value of the Player Input and send it to the MovementController
        /// </summary>
        private void CheckMove()
        {
            
            float dir = _myInputManager.GetMoveAction();
            //Animation controller, if isGrounded change the sprite into the 
            if (_myMovementController.isGrounded)
            {
                if (dir == 0)
                {
                    changeAnimation(IdleAnimHash);
                }
                else
                {
                    canCrouch = false;
                    changeAnimation(RunAnimHash);
                }
            }
            _myMovementController.setDirection(dir);
        }

        /// <summary>
        /// If the player wants to Dash call the MovementController to check if it can Dash
        /// </summary>
        private void CheckDash()
        {
            if (_myInputManager.GetDashButtonPressed())
            {
                playerAttackedBefore = false;
                canCrouch = false;
                StartCoroutine(PlayPriorityAnimationRoutine(RollAnimHash, _myMovementController.getDashDuration()));
                _myMovementController.Dash();
            }
        }

        /// <summary>
        /// Checks if the player can jump, if it is true sets the info to the MovementController
        /// </summary>
        private void CheckJump()
        {
            if (numJumps > 1 && _myInputManager.GetJumpButtonPressed())
            {
                playerAttackedBefore = false;
                canCrouch = false;
                StartCoroutine(PlayPriorityAnimationRoutine(JumpAnimHash,0.1f));
                _myMovementController.jump();
                numJumps--;
            }
        }

        private void takeDamage()
        {
           
            StartCoroutine(PlayPriorityAnimationRoutine(HurtAnimHash, 0.25f)); 
        }


        private void CheckCrouch()
        {
            if (_myInputManager.GetCrouchButtonPressed())
            {
                isPlayerCrouching = true;
                changeAnimation(CrouchIdleAnimHash);
                //TODO bajar la HitBox
            }
            else
            {
                isPlayerCrouching = false;
            }
        }

        private void CheckAttack()
        {
            if (_myInputManager.GetAttackButtonPressed())
            {

                if (isPlayerCrouching)
                {
                    StartCoroutine(PlayPriorityAnimationRoutine(CrouchAttackAnimHash, attackDuration));
                }
                else if (playerAttackedBefore)
                {
                    StartCoroutine(PlayPriorityAnimationRoutine(SecondAttackAnimHash, attackDuration * 2));
                    playerAttackedBefore = !playerAttackedBefore;
                }
                else
                {
                    StartCoroutine(PlayPriorityAnimationRoutine(FirstAttackAnimHash, attackDuration));
                    playerAttackedBefore = !playerAttackedBefore;
                }

                //TODO Logica de activar hitbox de da�o de la espada
            }
        }

        private void CheckHealth()
        {
           
        }

        private void die()
        {

        }


        private IEnumerator PlayPriorityAnimationRoutine(int animHash, float duration)
        {
            priorityAnimationPlaying = true;
            changeAnimation(animHash);

            yield return new WaitForSeconds(duration);

            priorityAnimationPlaying = false;
        }

        private void OnEnable()
        {
            _myHealthHandler.onEntityTakesDamage += takeDamage;
            _myHealthHandler.onEntityHealth += CheckHealth;
            _myHealthHandler.onEntityDeath += die;
        }

        private void OnDisable()
        {
            _myHealthHandler.onEntityTakesDamage -= takeDamage;
            _myHealthHandler.onEntityHealth -= CheckHealth;
            _myHealthHandler.onEntityDeath -= die;
        }
        private void Awake()
        {
            _myInputManager = GetComponent<PlayerInputManager>();
            _myMovementController = GetComponent<MovementController>();
            _myHealthHandler = GetComponent<HealthHandler>();
        }
        void Start()
        {
            currentAnimationHash = 0;
            numJumps = maxJumps;
        }

        void Update()
        {
            if (!priorityAnimationPlaying)
            {
                CheckMove();
                CheckDash();
                CheckJump();
                if (canCrouch && _myMovementController.isGrounded)
                {
                    CheckCrouch();
                }
                else
                {
                    canCrouch = true;
                }
                CheckAttack();
                CheckHealth();
            }
            if (_myMovementController.isGrounded)
            {
                numJumps = maxJumps;
            }
        }
    }
}
