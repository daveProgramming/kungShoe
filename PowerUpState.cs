using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PowerUpState : MonoBehaviour {

    public enum PowerUpStates { Magnet, None, Shoemerang, Bubble, Shoegun };
    public PowerUpStates currentState;

    public enum MagnetStates {On, Off };
    public MagnetStates currentMagnetState;

    private GameObject particalSystem;
    private GameObject bubble;
    //private variables
    private float timer = 0;
    private bool magnetsOn;

    //public for Inspector variables
    public float magnetCooldown = 4f;
    public float shoemerangCooldown = 4f;

    private ShoeManager sm;

	private List<GameObject> shoes;

    void Awake()
    {
        bubble = transform.GetChild(4).gameObject;
        particalSystem = transform.GetChild (3).gameObject;
		particalSystem.SetActive (false);
        particalSystem.GetComponent<ParticleSystem>().startColor = new Color(0, 0, 1, 1);
        sm = GetComponent<ShoeManager>();
    }
	// Use this for initialization
	void Start () {
        currentState = PowerUpStates.None;
        currentMagnetState = MagnetStates.Off;

	}
	
	// Update is called once per frame
	void Update () {
        switch (currentState)
        {
		case PowerUpStates.Magnet:
			particalSystem.SetActive (true);
                particalSystem.GetComponent<ParticleSystem>().startColor = new Color(0,1,0,1);
            
			TurnOnMagnets ();
                //bit messy I know
            if (sm.currentShoeNumber == ShoeManager.ShoeNumber.two)
            {
                currentState = PowerUpStates.None;
                timer = 0;
                if (shoes != null)
                {
                    foreach (GameObject g in shoes)
                    {
                        if (g != null)
                        {
                            g.GetComponent<Magnet>().enabled = false;
                            g.GetComponent<Magnet>().endMarker = null;
                        }

                    }
                }

                currentMagnetState = MagnetStates.Off;
            }
                break;
            case PowerUpStates.Shoemerang:
                particalSystem.SetActive(true);
                timer += Time.deltaTime;
                if (timer > shoemerangCooldown)
                {
                    currentState = PowerUpStates.None;
                    timer = 0;
                   
                }
                break;
            case PowerUpStates.Bubble:
                particalSystem.SetActive(true);
                bubble.GetComponent<SphereCollider>().enabled = true;
                bubble.GetComponent<MeshRenderer>().enabled = true;
                currentState = PowerUpStates.None;
                break;
            case PowerUpStates.Shoegun:
                break;
            case PowerUpStates.None:
			particalSystem.SetActive (false);
                break;

            default:
                break;
        }
    }
	void TurnOnMagnets()
	{
        if (currentMagnetState == MagnetStates.Off)
        {
            shoes = new List<GameObject> (GameObject.FindGameObjectsWithTag("Shoe"));
            if(shoes != null)
            {
                foreach (GameObject g in shoes)
                {
	                g.GetComponent<Magnet> ().enabled = true;
                    g.GetComponent<Magnet>().endMarker = this.transform;
                }
            }
            
            currentMagnetState = MagnetStates.On;
        }
        timer += Time.deltaTime;
        if (timer > magnetCooldown)
        {
            currentState = PowerUpStates.None;
            timer = 0;
            if (shoes != null)
            {
                foreach (GameObject g in shoes)
                {
                    if(g != null)
                    {
                        g.GetComponent<Magnet>().enabled = false;
                        g.GetComponent<Magnet>().endMarker = null;
                    }
                    
                }
            }
            
            currentMagnetState = MagnetStates.Off;
        }
		
	}
}
