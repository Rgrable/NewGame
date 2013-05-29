using UnityEngine;
using System.Collections;

public class Player_1_Turret : TurretScript {
	
	void Start () {
		
		turHealth = 50;
		Turret_Head = this.gameObject;
		bullet = (GameObject)Resources.Load("Bullet_Standard");
		explosion = (GameObject)Resources.Load("Explosion_1");
		rotValue_Min = 1.0f;
		rotValue_Max = 3.1f;
		player = true;
	}
	
}
