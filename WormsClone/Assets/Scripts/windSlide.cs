using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class windSlide : MonoBehaviour {
	private LevelParam levelP;
	private float currentWind;
	float multiplier;
	// Use this for initialization
	void Start () {
		levelP = GameObject.FindGameObjectWithTag("LevelP").GetComponent<MonoBehaviour>() as LevelParam;
		
		if (transform.eulerAngles.z == 0){
			multiplier = 1;
		}
		else {
			multiplier = -1;
		}
	}
	
	// Update is called once per frame
	void Update () {
		currentWind = levelP.wind;
		if (currentWind * multiplier > 0){
			gameObject.GetComponent<Slider>().value = currentWind*multiplier;
		}
		else{
			gameObject.GetComponent<Slider>().value = 0;
		}
		
	}
}
