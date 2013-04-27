using UnityEngine;
using System.Collections;

public class Enemy_Turret : MonoBehaviour {
	protected GameObject Turret_Head;
	public Transform Turret_Barrel;
	protected float turHealth = 50;
	protected float coolDown;
	public Object bullet;
	public float randTime_CoolDown;
	private float ComputerTime;
	
	// Creates a vector to position the health and cooldown bars for each turret
	protected Vector3 TurPos;
	
	public float rotValue; // the rotational value at which the turrets spin, 1.0 to 5.0
	
	void Start () {
		
		Turret_Head = this.gameObject;
		rotValue = Random.Range(1.0f,5.1f);
		bullet = Resources.Load("Bullet_Standard",typeof(GameObject));
		ComputerTime = Random.Range(10.0f,15.0f);
	}
	
	void Update () {
		
		
		if (coolDown <= 0)
			RotateTurret();
		else 
		{
			ComputerTime = Random.Range(5.0f,10.0f);
			rotValue = Random.Range(1.0f,5.1f);
		}
		Compute();
		CoolDownWeap();
		TurPos = Camera.current.WorldToScreenPoint(transform.position);
		CheckHealth();
	}
	void OnGUI()
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
	private void Compute()
	{
		if (ComputerTime <= 0)
		{
			FireWeapon();
		}
		else 
			ComputerTime -= Time.deltaTime;
		
	}
	private void FireWeapon()
	{
		if (coolDown <= 0)
		{
			Debug.Log("FIRED");
			GameObject newBullet = (GameObject)Instantiate(bullet);
			newBullet.transform.position = Turret_Barrel.transform.position;
			newBullet.rigidbody.AddForce(Turret_Barrel.transform.forward * 5000.0f);
			randTime_CoolDown = Random.Range(10.0f,15.0f);
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
