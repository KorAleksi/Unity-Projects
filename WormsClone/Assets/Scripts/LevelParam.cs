using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelParam : MonoBehaviour {
	[Range(-1f,1f)] public float wind;
	public float windMultiplier;
	public GameObject [] players;
	public Camera cameraPrefab;
	public Camera cam;
	public GameObject currentPlayer;
	public float timer;
	public float turnTime = 45;
	public Text timerUI;
	public bool shooting = false;
	public int wormsHit;
	public GameObject cameraCanvasPrefab;
	public TextAsset dataFile;
	
	public float CurrentWind(){
		return wind*windMultiplier;
	}
	private int playerIndex;
	private int playerAmount;
	
	void NextPlayer(){
		playerIndex += 1; 
		if (playerIndex >= playerAmount){
			playerIndex = 0;
		}
		currentPlayer = players[playerIndex];
		CameraTarget();
		ActivatePlayer(true);
	}
	void Shooting(bool toggleShoot){
		shooting = toggleShoot;
	}	
	
	void WormsHit(int hit){
		wormsHit += hit;
	}
	
		
	void Timer(){
		timer -= Time.deltaTime;
		//print (timer);
		if (timer < -5 && wormsHit <= 0){
			NextPlayer();
		}
		else if (timer < 0){
			ActivatePlayer(false);
			timerUI.text = "0";
			
		}
		else{
			
			string text = Mathf.RoundToInt(timer).ToString();
			
			timerUI.text = text;
		}
	}
	
	void ActivatePlayer(bool activate){
		currentPlayer.GetComponent<Player>().MyTurn(activate);
		if (activate){
			timer = turnTime;
			wind = Random.Range(-1.0f,1.0f);
		}
		
		
	}
	
	void CameraTarget(){
		cam.GetComponent<CameraMove>().player = currentPlayer;
	}
	void Awake(){
		Instantiate(cameraCanvasPrefab,Vector3.zero,Quaternion.identity);
		cam = GameObject.FindObjectOfType<Camera>();
	}
	
	void GenerateNames(){
		string[] dataList = dataFile.text.Split('\n');
		
		for (int i=0; i < playerAmount; i++){
			int length = dataList.Length;
			int index = Random.Range(0,length - 1);
			players[i].name = dataList[index];
			dataList[index].Remove(0);
			AddNameAndHealth(players[i]);
		}
	}
	void AddNameAndHealth(GameObject player){
		Transform name = player.transform.FindChild("Name");
		Transform health = player.transform.FindChild("Health");
		name.GetComponent<TextMesh>().text = player.name;
		health.GetComponent<TextMesh>().text = "100";
	}
	
	void Start(){
		wormsHit = 0;
		//timerUI = GameObject.Find("Timer") as Text;
		players = GameObject.FindGameObjectsWithTag("Player");
		playerAmount = players.Length;
		playerIndex = Random.Range(0,playerAmount - 1);
		GenerateNames();
		
		//print(playerIndex);
		currentPlayer = players[playerIndex];
		CameraTarget();
		ActivatePlayer(true);
		
	}
	
	
	void Update(){
		if (shooting){
		
		}
		else{
		Timer ();
		}
		if(Input.GetKey(KeyCode.PageUp)){
			wind = Mathf.Clamp(wind + Time.deltaTime,-1,1);
		}
		else if(Input.GetKey(KeyCode.PageDown)){
			wind = Mathf.Clamp(wind - Time.deltaTime,-1,1);
			
		}
	}
}
