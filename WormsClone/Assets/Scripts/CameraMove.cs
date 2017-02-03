using UnityEngine;
using System.Collections;

public class CameraMove : MonoBehaviour {
	public GameObject player;
	private Vector3 pos;
	GameObject bullet; 
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		bullet = GameObject.FindGameObjectWithTag("Bullet");
		//print (bullet);
		if (bullet == null){
			pos = player.transform.position;
			//transform.position = new Vector3(pos.x,pos.y,transform.position.z);
			transform.position = Vector3.Lerp(transform.position, new Vector3(pos.x,pos.y,transform.position.z),1f*Time.deltaTime);
		}
		else{
			pos = bullet.transform.position;
			transform.position = new Vector3(pos.x,pos.y,transform.position.z);
		}
	}
}
