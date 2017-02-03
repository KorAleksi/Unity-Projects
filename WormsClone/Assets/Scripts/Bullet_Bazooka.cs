using UnityEngine;
using System.Collections;

public class Bullet_Bazooka : MonoBehaviour {
	public float timer = 5f;
	public float windMultiplier = 5f;
	public GameObject explosionPrefab;
	public float spin = 0;
	//
	private LevelParam levelP;
	// Use this for initialization
	void Start () {
		levelP = GameObject.FindGameObjectWithTag("LevelP").GetComponent<MonoBehaviour>() as LevelParam;
		GetComponent<Rigidbody2D>().angularDrag += spin; 
	}
	
	void Explode(){
		Instantiate (explosionPrefab,transform.position,Quaternion.identity);
	}
	
	void OnCollisionEnter2D(Collision2D col){
		Explode();
		Destroy (gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.y <= -30){Destroy(gameObject);}
		gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector3(levelP.CurrentWind(),0,0));
		timer -= Time.deltaTime;
		if(timer <= 0f){
			Destroy (gameObject);
		}
	}
}
