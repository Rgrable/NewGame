using UnityEngine;
using System.Collections;

public class Turret : TurretScript {
	
	
	void Start()
	{
		Init(50,10,"DefaultBullet","Explosion_1",GlobalVars.GetPlayerTurrets(turretNumber));
	}
	public void Create()
	{
		Init(50,10,"DefaultBullet","Explosion_1");
	}
}
