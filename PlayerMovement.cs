using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
    
    public enum MovementState { Idle, Move, Dash };
    public MovementState currentMovementState;

    public Vector3 movementVector;
    public float speed = 8;

    private InputHandler ih;

    //dash
    public float dashSpeed = 6f;
    public ForceMode forceMode;
    public float dashCooldown;
    private bool hasDashed;
    private bool canDash;
    private float dashTimer;

    private Rigidbody rb;
    private Player player;
    void Awake()
    {
        ih = GetComponent<InputHandler>();
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        dashTimer = 0;
        hasDashed = false;
        
    }


    void Update()
    {

        ih.CheckInput();
        SetStateBasedOnInput();

        switch (currentMovementState)
        {
            case MovementState.Idle:
                break;
            case MovementState.Move:
                Move();
                break;
            case MovementState.Dash:
                Dash();
                break;
            default:
                break;
        }

        transform.LookAt(transform.position + movementVector);
        HandleDashCooldown();

    }
    void Move()
    {
        movementVector.x = Input.GetAxisRaw(ih.xAxis) * speed;
        movementVector.z = Input.GetAxisRaw(ih.yAxis) * speed;
        transform.position += movementVector * Time.deltaTime;
    }
    
    void Dash()
    {
        rb.velocity = movementVector * dashSpeed;
        hasDashed = true;
        canDash = false;
        currentMovementState = MovementState.Move;
    }
    void HandleDashCooldown()
    {
        if (hasDashed == true)
        {
            hasDashed = false;
            dashTimer += Time.deltaTime;
        }
        if (dashTimer >= dashCooldown)
        {
            canDash = true;
        }

    }
    void SetStateBasedOnInput()
    {
        switch (ih.inputState)
        {
            case InputHandler.InputState.dash:
                currentMovementState = MovementState.Dash;
                break;
            case InputHandler.InputState.move:
                currentMovementState = MovementState.Move;
                break;
            case InputHandler.InputState.idle:
                currentMovementState = MovementState.Idle;
                break;
            default:
                break;
        }
    }
    
    
}
