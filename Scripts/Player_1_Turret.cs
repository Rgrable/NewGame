using UnityEngine;
using System.Collections;

public class Player_1_Turret : MonoBehaviour {
	protected GameObject Turret_Head;
	public Transform Turret_Barrel;
	protected float turHealth = 50;
	protected float coolDown;
	public Object bullet;
	public GUIStyle TURRET_FIRE;
	public float randTime_CoolDown;
	
	// Creates a vector to position the health and cooldown bars for each turret
	protected Vector3 TurPos;
	
	public float rotValue; // the rotational value at which the turrets spin, 1.0 to 3.0
	
	void Start () {
		
		Turret_Head = this.gameObject;
		rotValue = Random.Range(1.0f,2.1f);
		bullet = Resources.Load("Bullet_Standard",typeof(GameObject));
		
	}
	
	void Update () {
		
		
		if (coolDown <= 0)
			RotateTurret();
		else 
			rotValue = Random.Range(1.0f,3.1f);
		
		CoolDownWeap();
		TurPos = Camera.current.WorldToScreenPoint(transform.position);
		CheckHealth();
	
	}
	void OnGUI()
	{
		
		if (coolDown <= 0)
		{
			GUI.Label(new Rect(TurPos.x - 100,670,500,200),"READY");
		}
		
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
		
		GUI.BeginGroup(new Rect(0,0,Screen.width,Screen.height),"");
			GUI.color = Color.white;
			if (GUI.Button(new Rect(TurPos.x - 35,575,100,100),"",TURRET_FIRE))
				{
					FireWeapon();
				}
		GUI.EndGroup();
	}
	void OnTriggerEnter(Collider hit)
	{
		if (hit.gameObject.name == "Bullet_Standard(Clone)")
		{
			turHealth -= 10;
			Destroy(hit.gameObject);
		}
	}
	private void RotateTurret()
	{
		Turret_Head.transform.Rotate(0.0f,rotValue,0.0f);
		if ((Turret_Head.transform.localRotation.eulerAngles.y >= 90 && Turret_Head.transform.localRotation.eulerAngles.y <= 270)
			|| (Turret_Head.transform.localRotation.eulerAngles.y <= 270 && Turret_Head.transform.localRotation.eulerAngles.y >= 90)) // determines the range of motion each turret has.
		{
			rotValue *= -1;
		}

	}
	
	private void FireWeapon()
	{
		if (coolDown <= 0)
		{
			Debug.Log("FIRED");
			GameObject newBullet = (GameObject)Instantiate(bullet);
			newBullet.transform.position = Turret_Barrel.transform.position;
			newBullet.rigidbody.AddForce(Turret_Barrel.transform.forward * 5000.0f);
			randTime_CoolDown = Random.Range(5.0f,10.0f);
			coolDown = randTime_CoolDown;
		}
	}
	
	private void CoolDownWeap()
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
			Destroy(this.gameObject);
		}
	}
}
