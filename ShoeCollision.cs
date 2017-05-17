using UnityEngine;
using System.Collections;

public class ShoeCollision : MonoBehaviour {


    private ShoeMovement sm;
    private BoxCollider bx;



    private bool canDestroyPlayer;
    //Sound 
    public AudioClip aClip;
    public AudioSource aSource;
    public GameObject soundTest;
    public SoundTestingScript sts;
    public bool playSound;

    public float damage = .5f;
    
    //Win Management
    public GameObject gameManager;

    void Awake()
    {
        sm = GetComponent<ShoeMovement>();
        bx = GetComponent<BoxCollider>();

        soundTest = GameObject.Find("SoundManager");
        sts = soundTest.GetComponent<SoundTestingScript>();
        canDestroyPlayer = true;
    }
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        aSource = GetComponent<AudioSource>();
        playSound = false;
        soundTest = GameObject.Find("SoundManager"); 
    }

    void OnCollisionEnter(Collision col)
    {
        
        if (col.gameObject.tag == "Environment" || col.gameObject.tag == "Shoe")
        {
            canDestroyPlayer = false;
            sm.currentMovementType = ShoeMovement.MovementType.None;

            //turn box collider on then off so that the physics.ignorecollision in projectile goes away

            bx.enabled = false;
            bx.enabled = true;
            Debug.Log("Hitting Environment");
            transform.GetChild(0).transform.GetComponent<BoxCollider>().enabled = true;
        }
       
        if (col.gameObject.tag == "Player")
        {
            
            //col.gameObject.GetComponent<IsDeadBool>().isDead = true;
            
            sm.currentMovementType = ShoeMovement.MovementType.None;
            aSource.clip = aClip;
            aSource.Play();
            sts.playVO();
            if (canDestroyPlayer)
            {
                col.gameObject.GetComponent<HealthManager>().RemoveHealth(damage);
            }
            canDestroyPlayer = false;
            
            transform.GetChild(0).transform.GetComponent<BoxCollider>().enabled = true;
        }
        //if (col.gameObject.tag == "BubblePowerUp")
        //{

        //    if (col.gameObject.transform.GetComponent<BubbleID>().bubbleID != GetComponent<BasicShoe>().shoeID)
        //    {
        //        col.gameObject.transform.GetComponent<SphereCollider>().enabled = false;
        //        col.gameObject.transform.GetComponent<MeshRenderer>().enabled = false;
        //        canDestroyPlayer = false;
        //        sm.currentMovementType = ShoeMovement.MovementType.None;
        //    }
        //    else
        //    {
        //        print("some shit");
        //    }
        //}
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Fence")
        {
            canDestroyPlayer = false;
            sm.currentMovementType = ShoeMovement.MovementType.None;

            sts.playFenceHit();
            canDestroyPlayer = false;
            bx.enabled = false;
            bx.enabled = true;
            //sb.canPickUpShoe = true;
            Physics.IgnoreCollision(transform.GetComponentInChildren<BoxCollider>(), col.gameObject.GetComponent<BoxCollider>());
            
        }
        if (col.gameObject.tag == "BubblePowerUp")
        {

            if (col.gameObject.transform.GetComponent<BubbleID>().bubbleID != GetComponent<BasicShoe>().shoeID)
            {
                col.GetComponent<SphereCollider>().enabled = false;
                col.GetComponent<MeshRenderer>().enabled = false;
                canDestroyPlayer = false;
                sm.currentMovementType = ShoeMovement.MovementType.None;

            }
            else
            {
                
            }
        }
    }
	void OnTriggerExit(Collider col)
	{
		if (col.gameObject.tag == "BubblePowerUp") 
		{
            print("does this work?");
		}
	}
		
   
}
