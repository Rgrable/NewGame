using UnityEngine;
using System.Collections;

public class TurretRemover : MonoBehaviour {

	void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			
			if (Physics.Raycast(ray, out hit))
			{
				if (hit.transform == this.transform)
				{
					Destroy(GameObject.Find("Turret_" + int.Parse(hit.transform.name.Replace("Position_","")).ToString()).gameObject);
					ModelList _list = GameObject.Find("ModelList").GetComponent<ModelList>();
					GameObject newDisplay = (GameObject)Instantiate(GameObject.Find("TurretDisplay_" + int.Parse(hit.transform.name.Replace("Position_",""))));
					newDisplay.tag = "Model";
					newDisplay.transform.parent = _list.transform;
					newDisplay.transform.name = newDisplay.GetComponent<MeshFilter>().mesh.name.Replace("Turret Instance","");
					_list.Populate(newDisplay);
					Debug.Log(_list.ObjectList.Count);
					Destroy(GameObject.Find("TurretDisplay_" + int.Parse(hit.transform.name.Replace("Position_","")).ToString()).gameObject);
					
					WeaponSlots defaultSlots = GameObject.Find("Default_" + int.Parse(hit.transform.name.Replace("Position_","")).ToString()).GetComponent<WeaponSlots>();
					WeaponSlots secondarySlots = GameObject.Find("Secondary_" + int.Parse(hit.transform.name.Replace("Position_","")).ToString()).GetComponent<WeaponSlots>();
					
					defaultSlots.Erase();
					secondarySlots.Erase();
					
					GlobalVars.SetPosition(int.Parse(hit.transform.name.Replace("Position_","")),"NONE");
					GlobalVars._playerTurrets[int.Parse(hit.transform.name.Replace("Position_","")) - 1] = null;
				}
			}
		}
	}
}
