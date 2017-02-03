// Tekijä: Aleksi Korkee
// Muokattu: 12.9.2015
//DONE 	-Menu (WORKING)
//		-Generating game randomly based on selection in the start menu (WORKING).
//		-Flags (WORKING)
//		-Mouse click (WORKING)
//		-Game logic (WORKING)

//TODO 	-Making Win and lose conditions "wisely" and Splashscreens for both.
//		-Timer
//		-Save highscores
//		-Highlight surrounding tiles when middleclicking down.
//		-Custom game generation (width, height, mines).
//		-Particle effects (explosion, eye-candy...)
//		-Terrain and 3d models (flags, mines)


using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class LevelInformation : MonoBehaviour {
	
	public int mineAmount = 10;
	public int boardWidth = 9;
	public int boardHeight = 9;
	public int flags = 0; 
	float time = 0;
	int minesLeft;
	bool gamePlaying = true;
	public Transform flagsContainer;
	bool gameStarted = false;
	
	public GameObject minePrefab;
	public Transform mineParent;
	public List<int> gridNumbers;
	int boxes;
	public List<int> mineLocations;
	public List<int> blankLocations;
	
	public Transform boardPrefab;
	public Transform gridBoxPrefab;
	
	public RectTransform menuCanvas;
	public RectTransform numberCanvas;
	public RectTransform tileButtonPrefab;
	public Transform minesText;
	List<Vector2> tilesToOpen;
	
	
	void Start(){
		tilesToOpen = new List<Vector2>();
	}
	
		
	void GenerateNumbers(){
		int boxes = boardWidth*boardHeight;
		for (int i = 0; i<boxes;i++){
			gridNumbers.Add(i+1);
			
					
		}
	}
	
	public void SetMineAmount(int mines){
		mineAmount = mines;
	}
	public void SetWidth(int width){
		boardWidth = width;
	}
	public void SetHeight(int height){
		boardHeight = height;
	}
	public void ChangeFlagAmount(int addflags){
		flags += addflags;
		minesLeftUI();
	}
	
	public void GenerateGame(){
		Transform board = Instantiate (boardPrefab,Vector3.zero,Quaternion.identity) as Transform;
		board.localScale = new Vector3(boardWidth*0.1f,boardHeight*0.1f,1);
		numberCanvas.sizeDelta = new Vector2(boardWidth,boardHeight);
		InstantiateButtons();
		for (int i = 0; i<boardWidth;i++){
			for (int j = 0; j<boardHeight;j++){
				Transform gridBox = Instantiate(gridBoxPrefab,new Vector2(i,j),Quaternion.identity) as Transform;
				gridBox.SetParent(board,true);
			}
		}
		GenerateNumbers();
		menuCanvas.gameObject.SetActive (false);
		minesLeftUI();
		gameStarted = true;
	}
	
	List<Vector2> TilesAround(Vector2 middleTile){
		int minLoc = 0;
		List<Vector2> tileList = new List<Vector2>();
		List<int> iList = new List<int>(){1,2,3,4,6,7,8,9};
		foreach(int i in iList){
			Vector2 tileLocation = NumberToLocation(i,3) - new Vector2(1,1) + middleTile;
			if (tileLocation.x < minLoc || tileLocation.x >= boardWidth){}
			else if (tileLocation.y < minLoc || tileLocation.y >= boardHeight){}
			else{
				tileList.Add(tileLocation);
			}
		}
		return tileList;
	}
	
	public void FirstClick(Vector2 firstTileLocation){
		List<int> removeList = new List<int>();
		int firstTileI = LocationToNumber(firstTileLocation,boardWidth);
		removeList.Add(firstTileI);
		foreach(Vector2 tileV in TilesAround(firstTileLocation)){
			int tileI = LocationToNumber(tileV,boardWidth);
			removeList.Add(tileI);
		}
		SpawnMines(removeList);
		foreach(Transform buttonI in numberCanvas){
			buttonI.GetComponent<ButtonBehaviour>().gameStarted = true;
		}
		emptyNumbers();
		OpenAroundBlank(firstTileLocation);
	}
	
	void emptyNumbers(){
		blankLocations = new List<int>(gridNumbers);
		foreach (int mineI in mineLocations){	
			blankLocations.Remove(mineI);	
			foreach (Vector2 aroundTile in TilesAround(NumberToLocation(mineI,boardWidth))){
				blankLocations.Remove (LocationToNumber(aroundTile,boardWidth));
			}
		}
	}
	
	public void OpenTile(Vector2 clickedTile){
		int tileLoc = LocationToNumber(clickedTile,boardWidth);
		if(blankLocations.Contains(tileLoc) == false){
			if (mineLocations.Contains(tileLoc) == true) {
				Lose();
			}
		}
		else{
			OpenAroundBlank(clickedTile);
		}
	}
	
	void Lose(){
		foreach(RectTransform buttonI in numberCanvas){
			buttonI.GetComponent<Button>().interactable = false;
			buttonI.GetComponent<ButtonBehaviour>().gameEnded = true;
			int posInt = LocationToNumber(buttonI.GetComponent<ButtonBehaviour>().gamePosition,boardWidth);
			if (mineLocations.Contains(posInt)){
				Destroy(buttonI.gameObject);
			}
		}
	}
	
	public void OpenAroundBlank(Vector2 clickedTile){
		if(blankLocations.Contains(LocationToNumber(clickedTile,boardWidth)) == false){
			return;
		}
		if(tilesToOpen.Contains(clickedTile) == false){
			tilesToOpen.Add(clickedTile);
			}
		foreach (Vector2 tileV in TilesAround(clickedTile)){
			int tileI = LocationToNumber(tileV,boardWidth);
			if(tilesToOpen.Contains(tileV) == false){
				tilesToOpen.Add(tileV);
				if (blankLocations.Contains(tileI) == true){
					OpenAroundBlank(tileV);
				}
			}
		}
		foreach(Transform buttonI in numberCanvas){
			if (tilesToOpen.Contains(buttonI.GetComponent<ButtonBehaviour>().gamePosition)){
				Destroy(buttonI.gameObject);
			} 
		}
		
	}

	//When MiddleMouse is clicked up check mines and flags amount around. If the amounts match check if they are in the same location. 
	//if they are, open all tiles with no flags. If not open the tile with mine and game over happens. 
	public void MiddleMouseUp(Vector2 centerTile){
		List<Vector2> aroundMiddleClick = new List<Vector2>();
		List<int> aroundMiddleClickI = new List<int>();
		aroundMiddleClick = TilesAround(centerTile);
		List<Vector2> middleClickMines = new List<Vector2>();
		List<int> middleClickMinesI = new List<int>();
		List<Vector2> middleClickFlags = new List<Vector2>();
		List<int> middleClickFlagsI = new List<int>();
		foreach(Vector2 aroundV in aroundMiddleClick){
			int aroundI = LocationToNumber(aroundV,boardWidth);
			aroundMiddleClickI.Add(aroundI);
		}
		foreach(int mineI in mineLocations){
			Vector2 mineV = NumberToLocation(mineI,boardWidth);
			if(aroundMiddleClickI.Contains (mineI)){
				middleClickMinesI.Add(mineI);
				middleClickMines.Add (mineV);
			}
		}
		foreach(Transform flagTransform in flagsContainer){
			Vector2 flagV = flagTransform.position;
			int flagI = LocationToNumber(flagV,boardWidth);
			if(aroundMiddleClickI.Contains(flagI)){
				middleClickFlagsI.Add (flagI);
				middleClickFlags.Add(flagV);
			}
			
		}
		int flagCount = middleClickFlagsI.Count;
		int mineCount = middleClickMinesI.Count;
		if (flagCount == mineCount){
			foreach(int MineI in middleClickMinesI){
				if(middleClickFlagsI.Contains (MineI)){
					//print ("JEE");	
				}
				else{
					//print ("MÖH");
					Lose();
					return;
				}
			}
			List<int> tilesToOpenI = new List<int>(aroundMiddleClickI);
			foreach(int mineI in middleClickMinesI){
				if (tilesToOpenI.Contains(mineI) == true){
					tilesToOpenI.Remove(mineI);
				}
			}
			foreach(Transform buttonT in numberCanvas){
				foreach(int openI in tilesToOpenI){
					Vector2 buttonV = buttonT.GetComponent<ButtonBehaviour>().gamePosition;
					if (openI == LocationToNumber(buttonV,boardWidth)){
						OpenAroundBlank(buttonV);
						Destroy(buttonT.gameObject);
					}
				}
			}
			return;
		}
		else if(flagCount > mineCount) {
			Lose ();
		}
		else{
			//Not enough flags
			//flash button
		}
	}
	
	void StartGame(){	
		SpawnMines(null); 
		gamePlaying = true;
	}
	
	void PauseGame(){
		gamePlaying = false;
	}
	
	void CheckWin(){
		if (numberCanvas.childCount <= mineAmount &&  gameStarted == true){
			print ("You Win!");
			minesText.GetComponent<Text>().text = "You Win!";
			
		}
	}
	void Update(){
		if (gamePlaying){
			time += Time.deltaTime; 
		}
		if (Input.GetKey(KeyCode.R)){
			Application.LoadLevel("Game");
		}	
		CheckWin();
	}
	
	void InstantiateButtons(){
		for (int i = 0; i<boardWidth;i++){
			for(int j = 0; j<boardHeight;j++){
				RectTransform tileButton = Instantiate(tileButtonPrefab,new Vector2(i,j),Quaternion.identity) as RectTransform;
				int xPos = (int)tileButton.position.x;
				int yPos = (int)tileButton.position.y;
				int tilePos = LocationToNumber(new Vector2(xPos,yPos),boardWidth);
				tileButton.GetComponentInChildren<Text>().text = tilePos.ToString();
				tileButton.SetParent(numberCanvas,false);
			}
		}
	}
	
	public void minesLeftUI(){
		minesLeft = mineAmount - flags;
		minesText.GetComponent<Text>().text = minesLeft.ToString();
	}
	
	// Luo miinoja satunnaisiin ruutuihin
	void SpawnMines(List<int> removeFromList){
		// Luo kokonaan uuden listan (Ei voi käyttää List<int> numbersForSpawn = gridNumbers, koska muutokset listoissa vaikuttaisi molempiin.)
		List<int> numbersForSpawn = new List<int>(gridNumbers);
		foreach (int removeTile in removeFromList){
			numbersForSpawn.Remove(removeTile);
		}
		for (int i = 0; i < mineAmount; i++){
			int emptyNum = numbersForSpawn.Count-1;	
			int randNum = Random.Range(0,emptyNum);
			int locNumber = numbersForSpawn[randNum];		
			numbersForSpawn.RemoveAt(randNum);
			GameObject mine = Instantiate(minePrefab, NumberToLocation(locNumber,boardWidth),Quaternion.identity) as GameObject;
			mine.transform.SetParent(mineParent);
			mineLocations.Add (locNumber); //Tallentaa miinojen sijainnit erilliseen listaan	
		}
	}
	
	int LocationToNumber(Vector2 locationV, int gridWidth){
		int locX = Mathf.RoundToInt(locationV.x) + 1;
		int locY = Mathf.RoundToInt(locationV.y) + 1;
		int gridNumber = gridWidth*(locY - 1) + locX;
		return gridNumber; 
	}
	
	Vector2 NumberToLocation(int gridNumber,int gridWidth){
		int locX = (gridNumber - 1)%gridWidth;
		int locY = (gridNumber - 1)/gridWidth;
		return new Vector2(locX,locY);
	}
}



