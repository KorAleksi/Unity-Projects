using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SpriteChange : MonoBehaviour {
	public Sprite [] windSprite;
	public float timer = 5;
	private float sliderSpeed;
	int currentSprite;
	// Use this for initialization
	void Start () {
		sliderSpeed = timer;
		currentSprite = 0;
	}
	
	void loadSprite(){
		if (currentSprite >= 4){
			currentSprite = 0;
		}
		else {
			currentSprite +=1;
		}
		gameObject.GetComponent<Image>().sprite = windSprite[currentSprite];
		sliderSpeed = timer;
	}
	// Update is called once per frame
	void Update () {
		if (sliderSpeed <= 0){
			loadSprite();
		}
		sliderSpeed -= 1;
		
	}
}
