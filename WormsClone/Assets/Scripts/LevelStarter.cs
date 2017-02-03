using UnityEngine;
using System.Collections;

public class LevelStarter : MonoBehaviour {
	public GameObject levelPref;
	public GameObject level;
	
	// Use this for initialization
	void Start () {
		level = Instantiate(levelPref,Vector3.zero,Quaternion.identity) as GameObject;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
