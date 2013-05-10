using UnityEngine;
using System.Collections;

public class TurretScript : MonoBehaviour {
	protected GameObject Turret_Head;
	public Transform Turret_Barrel;
	protected float turHealth;
	protected float coolDown = 0.5f;
	public Object bullet;
	public GUIStyle TURRET_FIRE;
	protected float randTime_CoolDown = 0.5f;
	// Audio
	public AudioClip death;
	public AudioClip fired;
	public AudioClip take_Damage;
	//
	protected bool player;
	protected bool player_2;
	
	protected float ComputerTime;
	
	protected Vector3 TurPos;// Creates a vector to position the health and cooldown bars for each turret
	protected float rotValue; // the rotational value at which the turrets spin, 1.0 to 3.0
	protected float rotValue_Min;
	protected float rotValue_Max;
	

	
	 void Update() {

		if (coolDown <= 0)
			RotateTurret();
		else 
		{
			rotValue = Random.Range(rotValue_Min,rotValue_Max);
			if (!player)
				ComputerTime = Random.Range(5.0f,10.0f);
		}
		if (!player)
			Compute();
		
		CoolDownWeap();
		TurPos = Camera.current.WorldToScreenPoint(transform.position);
		CheckHealth();
	
	}
	void OnGUI()
	{
		if (player)
		{
			// Health Bar
			GUI.BeginGroup(new Rect(0,0,Screen.width,Screen.height),"");
				GUI.color = Color.red;
				GUI.HorizontalScrollbar(new Rect(TurPos.x - 25,670,50,25),0,turHealth,0,50);
			GUI.EndGroup();
			
			// CoolDown Bar
			GUI.BeginGroup(new Rect(0,0,Screen.width,Screen.height),"");
				GUI.color = Color.green;
				GUI.VerticalScrollbar(new Rect(TurPos.x - 75,620,25,50),0,coolDown,randTime_CoolDown,0);
			GUI.EndGroup();
		}
		else if (!player || (player && player_2))
		{
			// Health Bar
			GUI.BeginGroup(new Rect(0,0,Screen.width,Screen.height),"");
				GUI.color = Color.red;
				GUI.HorizontalScrollbar(new Rect(TurPos.x - 25,45,50,25),0,turHealth,0,50);
			GUI.EndGroup();
		
			// CoolDown Bar
			GUI.BeginGroup(new Rect(0,0,Screen.width,Screen.height),"");
				GUI.color = Color.green;
				GUI.VerticalScrollbar(new Rect(TurPos.x - 75,60,25,50),0,coolDown,0,randTime_CoolDown);
			GUI.EndGroup();
		}
		GUI.BeginGroup(new Rect(0,0,Screen.width,Screen.height),"");
			GUI.color = Color.white;
			if (player)
			{
				if (GUI.Button(new Rect(TurPos.x - 35,575,100,100),"",TURRET_FIRE))
					{
						FireWeapon();
					}
			}
		GUI.EndGroup();
	}
	void OnTriggerEnter(Collider hit)
	{
		if (hit.gameObject.tag == "Bullet")
		{
			turHealth -= 10;
			Destroy(hit.gameObject);
			AudioSource.PlayClipAtPoint(take_Damage,transform.position);
		}
	}
	protected virtual void RotateTurret()
	{
		Turret_Head.transform.Rotate(0.0f,rotValue,0.0f);
		if ((Turret_Head.transform.localRotation.eulerAngles.y >= 90 && Turret_Head.transform.localRotation.eulerAngles.y <= 270)
			|| (Turret_Head.transform.localRotation.eulerAngles.y <= 270 && Turret_Head.transform.localRotation.eulerAngles.y >= 90)) // determines the range of motion each turret has.
		{
			rotValue *= -1;
		}
	}
	protected virtual void Compute()
	{
		if (!player)
		{
			if (ComputerTime <= 0)
			{
				FireWeapon();
			}
			else 
				ComputerTime -= Time.deltaTime;
		}
		
	}
	protected virtual void FireWeapon()
	{
		if (coolDown <= 0)
		{
			AudioSource.PlayClipAtPoint(fired,transform.position);
			GameObject newBullet = (GameObject)Instantiate(bullet);
			newBullet.transform.position = Turret_Barrel.transform.position;
			newBullet.rigidbody.AddForce(Turret_Barrel.transform.forward * 5000.0f);
			if (player)
				randTime_CoolDown = Random.Range(3.0f,5.0f);
			else
				randTime_CoolDown = Random.Range(5.0f,10.0f);
			
			coolDown = randTime_CoolDown;
		} 
	}
	protected virtual void CoolDownWeap()
	{
		if (coolDown <= 0)
			return;
		
		else
			coolDown -= Time.deltaTime;
	}
	private void CheckHealth()
	{
		if (turHealth <= 0)
		{
			if (player)
				ScoreKeeper.player_Death++;
			else
				ScoreKeeper.enemy_Death++;
				
			Destroy(this.gameObject);
			AudioSource.PlayClipAtPoint(death,transform.position);
			
		}
	}
}
