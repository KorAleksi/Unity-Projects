using UnityEngine;
using System.Collections;

public class ParticleLife : MonoBehaviour {
	
	float timer = 0.2f;
	Transform child;
	float a;
	// Use this for initialization
	void Start () {
		child = transform.GetChild(0);
	}
	
	// Update is called once per frame
	void Update () {
		if(timer<0){Destroy(gameObject);}
		timer -= Time.deltaTime;
		a= 1-timer*2;
		a = Mathf.Clamp(a,0,1);
		print (a);
		child.localScale = new Vector3(a,a,1);
	}
}
