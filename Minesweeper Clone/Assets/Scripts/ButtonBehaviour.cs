using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;


public class ButtonBehaviour : MonoBehaviour, IPointerClickHandler {
	
	public Transform flagPrefab;
	Transform flag;
	public bool flagOn = false;
	public GameObject flags;
	public LevelInformation level;
	public List<Transform> flagList;
	public bool gameStarted;
	public bool gameEnded;
	public bool opened;
	public Vector2 gamePosition;
	
	// Use this for initialization
	void Start () {
		flags = GameObject.FindWithTag("Flags");
		level = GameObject.FindGameObjectWithTag("Manager").GetComponent<LevelInformation>();
		gameStarted = false;
		gameEnded = false;
		opened = false;
		
		int locX = (int)transform.localPosition.x;
		int locY = (int)transform.localPosition.y;
		gamePosition = new Vector2(locX,locY);
		
		
	}	
		
		
		
	
	public void OnPointerClick(PointerEventData eventData){
		if (gameEnded == true){
			return;
		}

		if (eventData.button == PointerEventData.InputButton.Left){
			
			if(gameStarted == false){
				SendFirstClick();
			}
			else if(flagOn == false){
				level.OpenTile(gamePosition);
				Destroy(gameObject);
				
			}
			
		}
		
		else if (eventData.button == PointerEventData.InputButton.Right){
			if (gameStarted == false){}
			else if(flagOn == false){
				flag = Instantiate(flagPrefab,transform.position,Quaternion.identity) as Transform;
				flag.SetParent(flags.transform,false);
				flagOn = true;
				level.ChangeFlagAmount(1);
			}
			else {
				Destroy(flag.gameObject);
				flagOn = false;
				level.ChangeFlagAmount(-1);
			}
		}
	}
	public void SendFirstClick(){
		level.FirstClick(gamePosition);
	}
	
}