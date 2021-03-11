using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    public bool isJumping { get; set; }

    public Transform cameraSlot;

    [HideInInspector] public CharacterController controller;
    PlayerInputs playerInputs;


    [Header("Player settings")]
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float moveDelayLength = 1.0f;
    float currMoveDelay = 1.0f;
    [SerializeField] bool currentDelay = false;

    [Header("Jump Settings")]
    [SerializeField] float Gravity = 9.81f;
    [SerializeField] float jumpHeight = 4f;
    [SerializeField] float jumpForwardAppliedForce = .5f;
    [SerializeField] float airControl = 5f;
    [SerializeField] float stepDown = .2f;
    Animator animator;
    public GameObject axe;

    Vector3 velocity;

    float cameraXrotation;
    float playerYrotation;

    [HideInInspector] public bool _jumpInput;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        playerInputs = GetComponent<PlayerInputs>();
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        playerInputs.JumpInput();
        axe.GetComponent<Animator>().ResetTrigger("Chop");
        if (_jumpInput)
        {
            _jumpInput = false;
            Jump();
        }

        animator.SetBool("moving", (playerInputs.Movement().magnitude > 0.25f));
    }

    private void FixedUpdate()
    {
        PlayerMovement();
        //LookCamera();
    }

    void Jump()
    {
        axe.GetComponent<Animator>().SetTrigger("Chop");



        if (!isJumping)
        {
            isJumping = true;
            velocity = controller.velocity * jumpForwardAppliedForce;

            //calculating jump force
            velocity.y = Mathf.Sqrt(2 * Gravity * jumpHeight);
        }
    }

    void PlayerMovement()
    {
        if (!currentDelay)
        {
            if (isJumping)
            {
                velocity.y -= Gravity * Time.fixedDeltaTime;

                Vector3 playerDisplacement = velocity * Time.fixedDeltaTime;
                playerDisplacement += CalculateAirControl();
                controller.Move(playerDisplacement);
                isJumping = !controller.isGrounded;
            }
            else
            {
                Vector3 movement = move() * moveSpeed * Time.fixedDeltaTime;
                Vector3 _stepDown = Vector3.down * stepDown;

                Move(movement, _stepDown);

                //in case we are falling  
                if (!controller.isGrounded)
                {
                    isJumping = true;
                    velocity = controller.velocity * jumpForwardAppliedForce;
                    velocity.y = 0f;
                }
            }
        }
        
    }

    Vector3 CalculateAirControl()
    {
        return move() * (airControl / 100f);
    }

    void Move(Vector3 move, Vector3 _stepdown)
    {
        controller.Move(move + _stepdown);
    }

    Vector3 move()
    {
        return (Camera.main.transform.forward *
            (playerInputs.Movement().y < 0 ? playerInputs.Movement().y / 2f : playerInputs.Movement().y)
                + Camera.main.transform.right * (playerInputs.Movement().x / 2f));
    }



}
