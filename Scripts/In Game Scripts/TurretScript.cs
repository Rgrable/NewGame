using UnityEngine;
using System.Collections;

public class TurretScript : MonoBehaviour {
    
    #region GameObjects
    private GameObject Turret_Head;
    protected Mesh TurretMesh;
	private GameObject Turret_Barrel;
	public GameObject bullet;
	public GameObject secondaryBullet;
	private GameObject explosion;
	public GameObject healthBar;
	public GameObject armorBar;
    #endregion
    
    #region private variables
    private float turHealth,turMaxHealth,turArmor,turMaxArmor;
	private float coolDown = 0.5f;
    private float ComputerTime;
	private float animSpeed;
	private float randTime_CoolDown = 0.5f;
    #endregion

    #region Audio Files
	private AudioClip death;
	private AudioClip take_Damage;
	private AudioClip bullet_Fired;
	private AudioClip armor_Damage;
    #endregion
	public bool isPlayer;
	protected GameScript _GM;
	protected WeaponSlots defaultSlot, secondarySlot;
    public int turretNumber;
	public bool startGame;
   
    
	public TurretScript()
    {
        
    }
    
    protected virtual void Init(float health,float armor, string bullet_name, string explosion_name, string turName = null)
    {
    	Turret_Head = this.gameObject; 
		_GM = Camera.main.GetComponent<GameScript>();
		
        #region Resource Load
        // Resource Load
		bullet = (GameObject)Resources.Load(bullet_name);

		
		explosion = (GameObject)Resources.Load(explosion_name);
        Turret_Barrel = Turret_Head.transform.GetChild(0).gameObject;
        death = (AudioClip)Resources.Load(GlobalVars._deathSound);
        take_Damage = (AudioClip)Resources.Load(GlobalVars._hitSound);
		armor_Damage = (AudioClip)Resources.Load(GlobalVars._armorSound);
		bullet_Fired = (AudioClip)Resources.Load(bullet_name + "_Fired");
		
      	TurretMesh = (Mesh)Resources.Load(turName,typeof(Mesh));
        //
        #endregion
        
      	Turret_Head.GetComponent<MeshFilter>().mesh = TurretMesh;
		
		if (turretNumber != -999)
		{
			Turret_Head.renderer.material = (Material)Resources.Load(GlobalVars._playerColor[turretNumber]);
			secondaryBullet = (GameObject)Resources.Load(GlobalVars.GetSecondaryBullet(turretNumber));
			ChangeTurret(GlobalVars.GetPlayerTurrets(turretNumber));
			
			defaultSlot = GameObject.Find("Default_" + (turretNumber + 1) .ToString()).GetComponent<WeaponSlots>();
			secondarySlot = GameObject.Find("Secondary_" + (turretNumber + 1).ToString()).GetComponent<WeaponSlots>();
			defaultSlot.Reset();
			secondarySlot.Reset();
			
		}
		else
		{
			int random = Random.Range(0,4);
			switch (random)
			{
			case 0:
				secondaryBullet = (GameObject)Resources.Load("DefaultBullet");
				break;
			case 1:
				secondaryBullet = (GameObject)Resources.Load("StrongBullet");
				break;
			case 2:
				secondaryBullet = (GameObject)Resources.Load("StrongerBullet");
				break;
			case 3:
				secondaryBullet = (GameObject)Resources.Load("SniperBullet");
				break;
			}
			
		}
		
    	turHealth = health;
		turMaxHealth = health;
        turArmor = armor;
        turMaxArmor = armor + 50;
        
        animSpeed = Random.Range(1,3);
		
		armorBar.renderer.material.color = Color.blue;
		
		
        
    } 
	
	public void ChangeTurret(string turret_name)
	{
		TurretMesh = (Mesh)Resources.Load(turret_name,typeof(Mesh));
		Turret_Head.GetComponent<MeshFilter>().mesh = TurretMesh;
	}
	
	public void ChangeTurretColor(Material turret_color)
	{
		Turret_Head.renderer.material = turret_color;
		//Debug.Log(turret_color.ToString());
		GlobalVars.SetMaterial(turretNumber,turret_color.ToString().Replace(" (Instance) (UnityEngine.Material)", ""));
	}
	
    public void ChangeBullet(string bullet_name)
    {
        secondaryBullet = (GameObject)Resources.Load(bullet_name);
		GlobalVars.SetSecondaryBullet(turretNumber,bullet_name);
		bullet_Fired = (AudioClip)Resources.Load(bullet_name + "_Fired");
		secondarySlot.Reset();
    }
    
    public IEnumerator IncreaseArmor(float increase, float increaseSpeed)
    {
       float newArmor = turArmor + increase;
		
        while (turArmor < newArmor)
		{
			if (turArmor >= turMaxArmor)
	        {
	            turArmor = turMaxArmor;
				break;
	        }
			else
			{
				turArmor += 1;
			}
			yield return new WaitForSeconds(increaseSpeed);
		}
        
    }
    
    public IEnumerator IncreaseHealth(float increase, float increaseSpeed)
    {
		float newHealth = turHealth + increase;
        while (turHealth < newHealth)
		{
			if (turHealth >= turMaxHealth)
	        {
	            turHealth = turMaxHealth;
				break;
	        }
			else
			{
				turHealth += 1;
			}
			yield return new WaitForSeconds(increaseSpeed);
		}
        
    }
	
	 void Update() {
		if (startGame)
		{
			if (coolDown <= 0)
			RotateTurret();
			else 
			{
				animSpeed = Random.Range(1,3);
				ComputerTime = Random.Range(5.0f,10.0f);
			}
			
		    Compute();
			CoolDownWeap();
			CheckHealth();
		}
		else
		{
			turHealth = turMaxHealth;
			turArmor = 10;
			Turret_Head.animation.Rewind("Rotator");
		}
	 }
	void OnTriggerEnter(Collider hit)
	{
		if (hit.gameObject.tag == "Bullet")
		{
            Bullet bulletDamage = hit.gameObject.GetComponent<Bullet>();
			Ricochet bulletMultiplier = hit.gameObject.GetComponent<Ricochet>();
            
            if (turArmor >= 1)
            {
                turArmor -= (bulletDamage._damageDealt + bulletMultiplier.damageMultiplier);
				 AudioSource.PlayClipAtPoint(armor_Damage,transform.position);
            }
            else
            {
                turHealth -= (bulletDamage._damageDealt + bulletMultiplier.damageMultiplier);
                AudioSource.PlayClipAtPoint(take_Damage,transform.position);
            }
			Destroy(hit.gameObject);
		}
	}
	protected void RotateTurret()
	{
		Turret_Head.animation.Play("Rotator");
		Turret_Head.animation["Rotator"].speed = animSpeed;
	}
	protected void Compute()
	{
    	if (ComputerTime <= 0)
    	{
    		FireWeapon();
            
    	}
    	else 
        {
    		ComputerTime -= Time.deltaTime;
    	}
	}
	protected void FireWeapon()
	{
		if (coolDown <= 0)
		{
			animSpeed = 0;
			Turret_Head.animation["Rotator"].speed = animSpeed;
			int random = Random.Range(0,10);
			GameObject newBullet = (GameObject)Instantiate((random != 0) ? bullet : secondaryBullet);
			AudioSource.PlayClipAtPoint(bullet_Fired,transform.position);
            Bullet newSpeed = newBullet.GetComponent<Bullet>();
			newBullet.transform.position = Turret_Barrel.transform.position;
			newBullet.rigidbody.AddForce(Turret_Barrel.transform.forward * newSpeed._speed * 1000);
			randTime_CoolDown = Random.Range(3.0f,10.0f);
			coolDown = randTime_CoolDown;
			
		}
	}
	protected void CoolDownWeap()
	{
		coolDown -= Time.deltaTime;
	}
	private void CheckHealth()
	{
        healthBar.renderer.material.color = Color.Lerp(Color.red,Color.green,(turHealth / turMaxHealth));
        
		if (turArmor <= 0)
		{
			armorBar.transform.localScale = new Vector3(0.2f,0.0f,0.2f);
		}
		else
		{
			armorBar.transform.localScale = new Vector3(0.2f,(turArmor / turMaxArmor) * 3,0.2f);
		}
		if (turHealth <= 0)
		{
			healthBar.transform.localScale = new Vector3(0.2f,0.0f,0.2f);
			_GM.CheckResults(isPlayer,this as Turret);
			AudioSource.PlayClipAtPoint(death,transform.position);
			Instantiate(explosion,transform.position,new Quaternion(90,0,0,-90));
			
		}
		else
		{
			healthBar.transform.localScale = new Vector3(0.2f,(turHealth / turMaxHealth) * 3,0.2f);
		}
	}
}
