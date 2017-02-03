using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Health : MonoBehaviour {
	public float playerHealth;
	public float healthToBeLost = 0;
	public float waitTime = 1;
	public bool colliding = true;
	public GameObject level;
	private TextMesh healthText;
	// Use this for initialization
	void Start () {
		playerHealth = 100;
		level = GameObject.FindWithTag("LevelP");
		healthText = transform.FindChild("Health").GetComponent<TextMesh>();
		
		
		
	}
	public void ChangeHealth(float amount){
		healthToBeLost = amount;
	}
	
	void Die(){
		Destroy(gameObject);
	}
	void OnCollisionEnter2D(Collision2D other){
		colliding = true;
	}
	void OnCollisionExit2D(Collision2D other){
		colliding = false;
	}
	void WaitToDecreaseHealth(){
		waitTime -= Time.deltaTime;
		
		if (waitTime <= 0 && colliding == true && transform.GetComponent<Rigidbody2D>().velocity == Vector2.zero){
			print ("Player is ready");
			GetComponent<Rigidbody2D>().mass = 1000;
			playerHealth += healthToBeLost;
			healthText.text = Mathf.RoundToInt(playerHealth).ToString();
			level.GetComponent<LevelParam>().wormsHit = -1;
			healthToBeLost = 0;
			
		}
	}
	// Update is called once per frame
	void Update () {
		if (healthToBeLost != 0){
			print ("lel");
			WaitToDecreaseHealth();
		}
		if(playerHealth <= 0){
			Die();
		}
	}
}
