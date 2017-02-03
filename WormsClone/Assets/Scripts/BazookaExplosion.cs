using UnityEngine;
using System.Collections;

public class BazookaExplosion : MonoBehaviour {
	
	ParticleSystem [] exp;
	float timer = 1;
	public float forceMultiplier = 1;
	GameObject level;
	// Use this for initialization
	void Start () {
		level = GameObject.FindWithTag("LevelP");
		exp = Resources.LoadAll<ParticleSystem>("Prefabs/Explosion/");
		Instantiate(exp[0],transform.position,Quaternion.identity);
	}
	
	float DamageAmount(float norm){
		float damage;
		if (norm >= 4.0f){
			damage = Random.Range(0.0f,1.0f)*norm + 1;
			//print (damage);
		}
		
		else {
			damage = 50 - norm*Random.Range(9f,10f) ;
		}
		return damage;
	}
	
	void SendImpact(GameObject player,Vector3 distance){
		float norm = distance.magnitude;
		player.GetComponent<Health>().ChangeHealth(-DamageAmount(norm));
		player.GetComponent<Rigidbody2D>().velocity = distance.normalized*forceMultiplier/(norm+1) + new Vector3(0,1,0);
		
	}
	
	void OnTriggerEnter2D(Collider2D col){
		GameObject other = col.gameObject;
		if(other.tag == "Player"){
			
			
			Vector3 distanceFromCenter = col.transform.position - transform.position;
			//print (distanceFromCenter);
			other.GetComponent<Rigidbody2D>().mass = 1;
			SendImpact(other,distanceFromCenter);
			
			level.GetComponent<LevelParam>().wormsHit += 1;
			
		}
	}
	void Frame(){
		if (timer <= 0){
			Destroy(gameObject);
		}
		timer -= 1;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Frame();
	}
}
