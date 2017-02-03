using UnityEngine;
using System.Collections;

public class Bullet_Granade : MonoBehaviour {
	public float timer = 3f;
	public float windMultiplier = 5f;
	public GameObject explosionPrefab;
	public float spin = 0;
	//
	private LevelParam levelP;
	// Use this for initialization
	void Start () {
		gameObject.GetComponent<Rigidbody2D>().AddTorque(-spin); 
	}
	
	void Explode(){
		Instantiate (explosionPrefab,transform.position,Quaternion.identity);
	}
	

	
	// Update is called once per frame
	void Update () {
		if (transform.position.y <= -30){Destroy(gameObject);}
		
		timer -= Time.deltaTime;
		if(timer <= 0f){
			Explode();
			Destroy (gameObject);
		}
	}
}
