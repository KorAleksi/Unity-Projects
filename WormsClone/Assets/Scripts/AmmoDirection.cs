using UnityEngine;
using System.Collections;

public class AmmoDirection : MonoBehaviour {
	
	Vector3 direction;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		direction = GetComponent<Rigidbody2D>().velocity;
		float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
		Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
		transform.rotation = q;
	}
}
