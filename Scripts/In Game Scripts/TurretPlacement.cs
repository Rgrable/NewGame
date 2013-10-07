using UnityEngine;
using System.Collections;

public class TurretPlacement : MonoBehaviour {
	
	
	private Vector3 screenPoint;
	private Vector3 offset;
	private Vector3 curPoint;
	private Vector3 curPosition;
	
	private GameObject selectedOBJ;
	private Transform list;
	
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
					list = this.transform.parent;
					hit.transform.parent = null;
					
					selectedOBJ = (GameObject)Instantiate(hit.transform.gameObject);
					selectedOBJ.collider.enabled = false;
					selectedOBJ.name = this.name;
					selectedOBJ.transform.localScale = new Vector3(this.transform.localScale.x * 1.8f,this.transform.localScale.y * 1.8f,this.transform.localScale.z * 1.8f);
					screenPoint = Camera.main.WorldToScreenPoint(selectedOBJ.transform.localPosition);
					offset = selectedOBJ.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y, screenPoint.z));
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
				if (hit.transform.name.Contains("Position_") && GlobalVars._turretPos[int.Parse(hit.transform.name.Replace("Position_","")) - 1] == "NONE")
				{
					
					GlobalVars.SetPosition(int.Parse(hit.transform.name.Replace("Position_","")),selectedOBJ.name);
					GlobalVars.SetTurret(int.Parse(hit.transform.name.Replace("Position_","")) - 1,selectedOBJ.name);
					GameScript _GM = Camera.main.GetComponent<GameScript>();
					_GM.SetBoard(true,hit.transform.gameObject,selectedOBJ);
					
					ModelList _list = GameObject.Find("ModelList").GetComponent<ModelList>();
					_list.DePopulate(this.gameObject);
						
					Destroy(selectedOBJ);
				}
				else
				{
					this.transform.parent = list;
					this.transform.renderer.enabled = true;
					Destroy(selectedOBJ);
				}
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
		selectedOBJ.transform.position = curPosition;
	
	}
}
