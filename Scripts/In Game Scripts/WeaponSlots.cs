using UnityEngine;
using System.Collections;

public class WeaponSlots : MonoBehaviour {
	
	public bool secondary;
	public int turretNumber;
	private Turret T;

	void Start()
	{
		Erase();
	}
	
	
	public void Reset()
	{
		
		T = GameObject.Find("Turret_" + turretNumber.ToString()).GetComponentInChildren<Turret>();
		
		if (!secondary)
		{	
			GameObject instance = (GameObject)Instantiate(T.bullet);
			this.transform.renderer.material = instance.renderer.material;
			Destroy(instance);
		}
		else
		{
			GameObject instance = (GameObject)Instantiate(T.secondaryBullet);
			this.transform.renderer.material = instance.renderer.material;
			Destroy(instance);
		}
	}
	
	public void Erase()
	{
		this.renderer.material = (Material)Resources.Load("Clear");
	}

}
