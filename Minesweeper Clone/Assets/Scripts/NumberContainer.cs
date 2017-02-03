using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class NumberContainer : MonoBehaviour {

	public int minesNear;
	public SpriteRenderer mineNumber;
	public Sprite [] numberArray = new Sprite[9];
	public LevelInformation level;
	Vector2 gamePosition;
	
	void OnMouseOver(){
		if(Input.GetMouseButtonDown(2)){
		}
		if(Input.GetMouseButtonUp(2)){
			level.MiddleMouseUp (gamePosition);
		}
	}
	
	public void addMineCount(){
		minesNear++;
		ChangeSprite();
		
	}
	void ChangeSprite(){
		mineNumber = transform.GetComponentInChildren<SpriteRenderer>();
		mineNumber.sprite = numberArray[minesNear];
	}
	// Use this for initialization
	void Start () {
		minesNear = 0;
		ChangeSprite();
		level = GameObject.FindGameObjectWithTag("Manager").GetComponent<LevelInformation>();
		gamePosition = transform.position;
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
