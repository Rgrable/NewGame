using UnityEngine;
using System.Collections;

public class Abilities : MonoBehaviour {
	
	private Vector3 screenPoint;
	private Vector3 offset;
	private Vector3 curPoint;
	private Vector3 curPosition;
	
	private GameObject selectedOBJ;
	private Transform list;
	
	private AudioClip _loaded;
	
	void Start()
	{
		_loaded = (AudioClip)Resources.Load("Loaded");
	}
	
	protected virtual void CheckInput()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit))
			{
				if (hit.transform.gameObject == this.gameObject)
				{
					if (this.transform.parent != null)
					{
						list = this.transform.parent;
						hit.transform.parent = null;
					}
					
					
					selectedOBJ = (GameObject)Instantiate(hit.transform.gameObject);
					selectedOBJ.collider.enabled = false;
					selectedOBJ.name = this.name;
					selectedOBJ.transform.localScale = new Vector3(this.transform.localScale.x * 1.8f,this.transform.localScale.y * 1.8f,this.transform.localScale.z * 1.8f);
					screenPoint = Camera.main.WorldToScreenPoint(selectedOBJ.transform.localPosition);
					offset = selectedOBJ.transform.localPosition - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y, screenPoint.z));
					this.transform.renderer.enabled = false;
				}
			}	
		}
		if (Input.GetMouseButtonUp(0))
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit))
			{
				if (hit.transform.name.Contains("Position_"))
				{
					Turret getAttribute = GameObject.Find("Turret_" + int.Parse(hit.transform.name.Replace("Position_",""))).GetComponentInChildren<Turret>();
					
					if (selectedOBJ.tag == "Bullet")
					{
						
//						BulletList _list  = GameObject.Find("BulletList").GetComponent<BulletList>();
//						_list.DePopulate(this.gameObject);
						
						this.transform.parent = list;
						this.transform.renderer.enabled = true;
						
						getAttribute.ChangeBullet(selectedOBJ.name);
						AudioSource.PlayClipAtPoint(_loaded,transform.position);
					}
					
					if (selectedOBJ.tag == "Model")
					{
						this.transform.parent = list;
						this.transform.renderer.enabled = true;
						
						getAttribute.ChangeTurret(selectedOBJ.name);
						getAttribute.ChangeTurretColor(selectedOBJ.renderer.material);
					}
                   
                    else if (selectedOBJ.tag == "HealthGlobe")
                    {
                       StartCoroutine (getAttribute.IncreaseHealth(selectedOBJ.GetComponent<Health>()._healthAmount, selectedOBJ.GetComponent<Health>()._healSpeed));
                    }
                    
                    else if (selectedOBJ.tag == "ArmorGlobe")
                    {
                        StartCoroutine (getAttribute.IncreaseArmor(selectedOBJ.GetComponent<Armor>()._armorAmount, selectedOBJ.GetComponent<Armor>()._healSpeed));
                    }
				}
				
				else
				{
					this.transform.parent = list;
					this.transform.renderer.enabled = true;
					Destroy(selectedOBJ);
				}
				
				Destroy(selectedOBJ);
			}
			else
			{
				this.transform.parent = list;
				this.transform.renderer.enabled = true;
				Destroy(selectedOBJ);
			}
		}
	}
	
	 void OnMouseDrag()
	{
		curPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
		curPosition = Camera.main.ScreenToWorldPoint(curPoint) + offset;
		selectedOBJ.transform.localPosition = curPosition;
	
	}
	
	public void AssignAbility(Turret _turret)
	{
		
	}
}
