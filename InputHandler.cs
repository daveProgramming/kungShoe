using UnityEngine;
using System.Collections;

public class InputHandler : MonoBehaviour {

    public string xAxis;
    public string yAxis;
    public string dashButton;
    public string kick;

    public enum InputState {dash, move, idle, kick};
    public InputState inputState;

    public Vector3 direction;

    private Player player;
    // Use this for initialization
    private void Awake()
    {
        player = GetComponent<Player>();
    }
    void Start ()
    {
        xAxis = "LeftJoystickX_P" + player.playerNumber;
        yAxis = "LeftJoystickY_P" + player.playerNumber;
        dashButton = "Dash_P" + player.playerNumber;
        kick = "Fire1_P" + player.playerNumber; 
    }
	
	// Update is called once per frame
	void Update ()
    {
        CheckInput();
	}
    public void CheckInput()
    {
        if (Input.GetButtonDown(dashButton))
        {
            inputState = InputState.dash;
        }
        else if (Input.GetAxisRaw(xAxis) > 0 || Input.GetAxisRaw(xAxis) < 0 || Input.GetAxisRaw(yAxis) > 0 || Input.GetAxisRaw(yAxis) < 0)
        {
            inputState = InputState.move;
        }
        else if (Input.GetButtonDown(kick))
        {
            inputState = InputState.kick;
        }
        else
        {
            inputState = InputState.idle;
        }
    }
    public void SetDirection()
    {

    }
}
