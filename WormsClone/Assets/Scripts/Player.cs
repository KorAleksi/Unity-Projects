using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	private bool moving = false;
	public float playerSpeed = 1f;
	private float direction;
	public GameObject weapon;
	private float facing;
	private float angle;
	GameObject player;
	public float jumpStartTimer;
	private bool jumping = false;
	private int jumpButtonType; // 0 and 1 = one button, 2 and 3 = two buttons
	private int jumpType;
	//private bool colliding;
	private float jumpDir = 1;
	private bool turn = false;
	
	
	
	// Use this for initialization
	void Awake () {
		weapon = transform.GetChild(0).transform.GetChild(0).gameObject;
		player = transform.FindChild("Player").gameObject;
		GetComponent<Rigidbody2D>().mass = 1000;
		
	}
	
	
	bool checkMoving(){
		if (direction != 0){
			return true;
		}
		else {
			return false;
		}
	}
	
	public void MyTurn(bool myTurn){
		//print ("myTurn is " + myTurn);
		weapon.SetActive(myTurn);
		turn = myTurn;
		GetComponent<Rigidbody2D>().mass = 1;
	}
	
	void TurnAround(){
		if (direction < 0){
			angle = 180;
			jumpDir = -1;
		}
		else if (direction > 0){
			angle = 0;
			jumpDir = 1;
		}
		player.transform.eulerAngles = new Vector3(0,angle,0);
	}
	
	bool JumpStarting(){
		jumpStartTimer -= Time.deltaTime;
		if(jumpStartTimer <= 0){
			return false;
		}
		else {
			return true;
		}
	}
	
	bool CheckJumpButton(){
		if (Input.GetKeyDown(KeyCode.Backspace)){
			jumpButtonType = 0;
			return true;
		}
		else if  (Input.GetKeyDown(KeyCode.Return)){
			jumpButtonType = 1;
			return true;
		}
		else{
			return false;
		}
		
	}
	void Move(){
		direction = Input.GetAxis("Horizontal");
		
		if(checkMoving() == !moving){
			moving = !moving;
			weapon.SetActive(!moving);
			TurnAround();
			//print(moving);
			
		}
		transform.position += new Vector3(direction*playerSpeed,0f,0f)*Time.deltaTime;
		//print(moving);
	}
	
	
	void Jump(){
		
		if (jumpType == 0){
			
			GetComponent<Rigidbody2D>().velocity = new Vector3(0,5,0);
		}
		else if (jumpType == 1){
			GetComponent<Rigidbody2D>().velocity = new Vector3(4f*jumpDir,4,0);
		}
		else if (jumpType == 2){
			GetComponent<Rigidbody2D>().velocity = new Vector3(-jumpDir*0.5f,8,0);
		}
		else{
			GetComponent<Rigidbody2D>().velocity = new Vector3(0.3f*-jumpDir,5,0);
		}
	}
	void OnCollisionEnter2D(Collision2D col){
		//colliding = true;
	}
	void OnCollisionExit2D(){
		//colliding = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(!turn){
		
		}
		
		else if(jumping){
			if(JumpStarting()){
				if (CheckJumpButton()){
					if (jumpType == jumpButtonType){
						jumpType += 2; 
					}
				}
			}
			else{
				Jump();
				jumping = false;
				weapon.SetActive(false);
			}
		}
		else{
			if(CheckJumpButton()){
				jumping = true;
				jumpStartTimer = 0.3f;
				weapon.SetActive(!jumping);
				jumpType = jumpButtonType;
			}
			else if(Input.GetKeyDown(KeyCode.Insert)){
				weapon = gameObject.GetComponent<WeaponPack>().NextWeapon(weapon.transform.rotation);
				
				
			}
			else{
				Move();
			}
			
		}
		
				
	}
}
