using UnityEngine;
using System.Collections;

public class WeaponPack : MonoBehaviour {
	int amount;
	int currentWeapon;
	GameObject [] weapons;
	
	
	
	// Use this for initialization
	void Awake () {
		weapons = Resources.LoadAll<GameObject>("Prefabs/Weapons/");
		
		print(weapons.LongLength);
		amount = weapons.Length;
		currentWeapon = 0;
		print (amount);
		print (weapons);
		
		
		
	}
	public GameObject NextWeapon(Quaternion rotation){
		currentWeapon += 1;
		if (currentWeapon >= amount){
			currentWeapon = 0;
		}
		Destroy(gameObject.GetComponent<Player>().weapon);
		GameObject nextWeapon = Instantiate(weapons[currentWeapon],transform.position,rotation) as GameObject;
		nextWeapon.transform.SetParent(transform.GetChild(0));
		return nextWeapon;
	}
	
	
	// Update is called once per frame
	void Update () {
	
	}
}
