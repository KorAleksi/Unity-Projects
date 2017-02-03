using UnityEngine;
using System.Collections;

public class BombBehaviour : MonoBehaviour {
	RaycastHit hit;
	Vector3 [] surroundings = new []{new Vector3(-1f,-1f,0), new Vector3(0f,-1f,0), new Vector3(1f,-1f,0),
									new Vector3(-1f,0f,0), new Vector3(1f,0f,0),
									new Vector3(-1f,1f,0), new Vector3(0f,1f,0), new Vector3(1f,1f,0)
									};
		
	// Use this for initialization
	void Start () {
		Vector3 origin = transform.position;
		for (int i = 0; i<surroundings.Length;i++){
			SendRay(new Vector3(0.5f,0.5f,0) + origin + surroundings[i]);
		}	
	}
	
	
	void SendRay(Vector3 origin){
		if (Physics.Raycast(origin,new Vector3(0,0,1), out hit,2f)){
			Debug.DrawRay(origin, new Vector3(0,0,1));
			if (hit.transform.tag == "Grid"){
				hit.transform.GetComponent<NumberContainer>().addMineCount();
			}
			
		}
	}
	
	
	
	
	void OnDrawGizmos(){
	}
}