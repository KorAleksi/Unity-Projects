using UnityEngine;
using System.Collections;


public class VehicleBehaviour : MonoBehaviour {

    public LevelManager levelManager;

    int length;
    int xPos;
    int yPos;
    public float rotation;

    private Vector3 screenPoint;
    private Vector3 offset;

    Vector2 moveDir;
    
    int clampMin;
    int clampMax;

    public Vector3 lastPosition;

    private bool isPlayer = false;


    void Start () {
        xPos = (int)transform.position.x;
        yPos = (int)transform.position.y;
        lastPosition = transform.position;
        levelManager = Transform.FindObjectOfType<LevelManager>();
        string tag = transform.tag;
        if (tag.Equals("Truck"))
        {
            length = 3;
        }
        else {
            length = 2;
            if (tag.Equals("PlayerCar")) { isPlayer = true; }
        }
        moveDir = SetMoveDirection();
        SendNewPosition();     
	}

    void OnMouseDown()
    {
        lastPosition = transform.position;
        GetClamps();
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
        
    }

    void OnMouseDrag()
    {
        Vector3 cursorPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(cursorPoint) + offset;

        float newX = moveDir.x == 0 ? transform.position.x : Mathf.Clamp(Mathf.Round(cursorPosition.x), clampMin, clampMax);
        float newY = moveDir.y == 0 ? transform.position.y : Mathf.Clamp(Mathf.Round(cursorPosition.y), clampMin, clampMax);
        transform.position = new Vector3(newX, newY,transform.position.z);
    }

    void OnMouseUp() {
        if (lastPosition != transform.position) {
            levelManager.IncreaseMoves();
            SendNewPosition();
            if (isPlayer && transform.position == new Vector3(4,3,transform.position.z)) {
                levelManager.NextLevel();
            }
        }
    }

    Vector2 SetMoveDirection() {
        int rotation = Mathf.RoundToInt(transform.rotation.eulerAngles.z);
        Vector2 dir;
        if (rotation == 0) { dir = Vector2.up; }
        else if (rotation == 180) { dir = Vector2.down; }
        else if (rotation == 90) {
            dir = Vector2.left;
        }
        else {
            dir =Vector2.right;
        }
        return dir;
    }

    void SendNewPosition() {
        for (int i = 0; i < length; i++) {
            levelManager.ChangeBlock(lastPosition + i*(Vector3)moveDir,true);
        }
        for (int i = 0; i < length; i++)
        {
            levelManager.ChangeBlock(transform.position + i * (Vector3)moveDir, false);
        }
        lastPosition = transform.position;
    }

    void GetClamps() {
        
        int upStart, lowStart;
        if (moveDir.x == 0) {
            clampMin = (int)lastPosition.y;
            clampMax = (int)lastPosition.y;
            if (moveDir.y == 1) {
                lowStart = (int)lastPosition.y-1;
                upStart = (int)lastPosition.y + length;
            }
            else{
                lowStart = (int)lastPosition.y - length;
                upStart = (int)lastPosition.y+1;
            }
            while (levelManager.IsBlockEmpty(new Vector2 (transform.position.x,lowStart))) {
                lowStart--;
                clampMin--;
            }
            while (levelManager.IsBlockEmpty(new Vector2(transform.position.x, upStart)))
            {
                upStart++;
                clampMax++;
            }
        }
        else {
            clampMin = (int)lastPosition.x;
            clampMax = (int)lastPosition.x;
            if (moveDir.x == 1)
            {
                lowStart = (int)lastPosition.x - 1;
                upStart = (int)lastPosition.x + length;
            }
            else {
                lowStart = (int)lastPosition.x - length;
                upStart = (int)lastPosition.x+1;
            }
            while (levelManager.IsBlockEmpty(new Vector2(lowStart,transform.position.y)))
            {
                lowStart--;
                clampMin--;
            }
            while (levelManager.IsBlockEmpty(new Vector2(upStart,transform.position.y)))
            {
                upStart++;
                clampMax++;
            }
        }
        print(clampMin + "," + clampMax);
    }

}

