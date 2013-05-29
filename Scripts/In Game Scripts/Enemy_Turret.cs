using UnityEngine;
using System.Collections;

public class Enemy_Turret : TurretScript {
	
	void Start () {
		
		turHealth = 5;
		Turret_Head = this.gameObject;
		bullet = (GameObject)Resources.Load("Bullet_Standard");
		explosion = (GameObject)Resources.Load("Explosion_1");
		rotValue_Min = 1.0f;
		rotValue_Max = 5.1f;
		ComputerTime = Random.Range(10.0f,15.0f);
	}
}
