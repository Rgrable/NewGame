using UnityEngine;
using System.Collections;

public class Enemy_Turret : TurretScript {
	
	void Start () {
		
		turHealth = 50;
		Turret_Head = this.gameObject;
		bullet = Resources.Load("Bullet_Standard",typeof(GameObject));
		rotValue_Min = 1.0f;
		rotValue_Max = 5.1f;
		ComputerTime = Random.Range(10.0f,15.0f);
	}
}
