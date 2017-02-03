using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

class Block
{
    public bool isEmpty = true;
}

public class LevelManager : MonoBehaviour {

    

    private int gridSize = 6;
    Block[,] carGrid;
    public int moves = 0;


	// Use this for initialization
	void Awake () {
        carGrid = new Block[gridSize, gridSize];
        CreateBlocks(carGrid);
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void CreateBlocks(Block[,] carGrid) {
        for (int i = 0; i < gridSize; i++)
        {
            for (int j = 0; j < gridSize; j++)
            {
                carGrid[i,j] = new Block();
            }
        }
    }

    public bool IsBlockEmpty(Vector2 pos) {
        if (pos.x < 0 || pos.x >= gridSize || pos.y < 0 || pos.y >= gridSize) { return false; }
        return carGrid[(int)pos.x, (int)pos.y].isEmpty;
    }

    public void ChangeBlock(Vector2 pos, bool isEmpty) {
        if (pos.x < 0 || pos.x >= gridSize || pos.y < 0 || pos.y >= gridSize) { return; }
        carGrid[(int)pos.x,(int)pos.y].isEmpty = isEmpty;
        //PrintGrid();
    }

    public void IncreaseMoves() {
        moves++;
        print("moves: "+moves);
    }
    void PrintGrid() {
        for (int i = 0; i < gridSize; i++)
        {
            string line = "";
            for (int j = 0; j < gridSize; j++)
            {
                line += carGrid[i, j].isEmpty +" ";
            }
            print(line);

        }
    }

    public void NextLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
}
