using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour {

    float maxHealth;
    public float currentHealth = 1;
    Player p;
    public Slider slider;
    public GameObject gameManager;

    void Awake()
    {
        gameManager = GameObject.Find("GameManager");
    }

    void Start () {
        maxHealth = 1;
        currentHealth = 1;
        p = GetComponent<Player>();
        slider = GameObject.Find("HealthSlider" + p.playerNumber).GetComponent<Slider>();
        
	}
	
	// Update is called once per frame
	void Update () {
        slider.value = currentHealth;
        if(currentHealth == 0)
        {
            gameManager.GetComponent<WinnerManager>().players.Remove(this.gameObject);
            Destroy(this.gameObject);
        }
	}

    public void RemoveHealth(float damage)
    {
        currentHealth = currentHealth - damage;
        
    }
    //void OnTriggerEnter(Collider col)
    //{
    //    if(col.gameObject.tag == "Shoe")
    //    {
    //        RemoveHealth(.5f);
    //        print("getting to collision");
    //    }
    //}
}
