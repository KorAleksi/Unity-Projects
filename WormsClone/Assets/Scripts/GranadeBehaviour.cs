using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GranadeBehaviour : MonoBehaviour {
	public float rotateSpeed;
	public bool shooting = false;
	public Rigidbody2D myBullet;
	private float velBullet = 0f;
	public float maxVelBullet = 1f;
	public float velMultiplier = 30f;
	public Slider mySlider;
	public Slider powerSlider;
	private float angleZ;
	GameObject childTo;
	public GameObject level;
	public float timer; 
	
	// Use this for initialization
	void Start () {
	timer = 3;
	level = GameObject.FindGameObjectWithTag("LevelP");
	childTo = GameObject.FindGameObjectWithTag("CameraCanvas");
	angleZ = transform.eulerAngles.z;
	}
	
	void RotationLimit(){
		angleZ = transform.eulerAngles.z;
		if (angleZ >= 90 && angleZ < 180){
			transform.localEulerAngles = new Vector3(0,0,90);
		}
		else if (angleZ > 180 && angleZ <= 270){
			transform.localEulerAngles = new Vector3(0,0,270);
		}
	}
	
	void SetTimer(){
		if(Input.GetKeyDown(KeyCode.Alpha1)){timer = 1;}
		else if(Input.GetKeyDown(KeyCode.Alpha2)){timer = 2;}
		else if(Input.GetKeyDown(KeyCode.Alpha3)){timer = 3;}
		else if(Input.GetKeyDown(KeyCode.Alpha4)){timer = 4;}
		else if(Input.GetKeyDown(KeyCode.Alpha5)){timer = 5;}
	}
	
	void Shoot(){
		Rigidbody2D newBullet = Instantiate(myBullet,transform.position + transform.right*1.2f,transform.rotation) as Rigidbody2D;
		newBullet.velocity = transform.right*velBullet*velMultiplier;
		newBullet.GetComponent<Bullet_Granade>().timer = timer;
		velBullet = 0f;
		shooting = false;
		level.GetComponent<LevelParam>().shooting = false;
		level.GetComponent<LevelParam>().timer = 0;
		Destroy(powerSlider.gameObject);
	}
	void PowerSlider(){
		powerSlider.value = velBullet;
		powerSlider.transform.position = transform.position;
		powerSlider.transform.rotation = transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
		float rotation = Input.GetAxis("Vertical");
		SetTimer();
		transform.Rotate(new Vector3(0,0,rotation));
		//transform.rotation = Mathf.Clamp(transform.eulerAngles.z,-90f,90f);
		//angleZ = transform.eulerAngles.z;
		RotationLimit();
		
		//Debug.DrawLine(transform.position,transform.right);
		if (shooting){
			
			velBullet += Time.deltaTime*0.5f;
			PowerSlider();
			if (Input.GetKeyUp("space")){
				Shoot ();
			}
			else if (velBullet >= maxVelBullet){
				Shoot ();
				
			}
			
		}
		
		else{
			
			//mySlider.enabled = false;
			if (Input.GetKeyDown("space")){
				shooting = true;
				level.GetComponent<LevelParam>().shooting = true;
				powerSlider = Instantiate(mySlider,transform.position,transform.rotation) as Slider;
				powerSlider.transform.SetParent(childTo.transform);
			}
			
		}
		
	}
}
