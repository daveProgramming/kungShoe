using UnityEngine;
using System.Collections;

public class ShoeMovement : MonoBehaviour {

    public enum MovementType {Forward, Shoemerang, None }
    public MovementType currentMovementType;
    public BasicShoe bs;

    private Rigidbody rb;
	// Use this for initialization
	void Start () {
        bs = GetComponent<BasicShoe>();
        rb = GetComponent<Rigidbody>();
        currentMovementType = MovementType.Forward;
	}
	
	// Update is called once per frame
	void Update () {
        //rb.AddForce(transform.forward * speed * Time.deltaTime, ForceMode.Force);
        
        switch (currentMovementType)
        {
            case MovementType.Forward:
                transform.position += transform.forward * bs.Speed * Time.deltaTime;
                break;
            case MovementType.Shoemerang:
                break;
            case MovementType.None:
                break;
            default:
                break;
        }

    }
}
