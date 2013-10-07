using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public delegate void OpenList(object obj);

public class List : MonoBehaviour {
	
	public List<object> ObjectList = new List<object>();
	
	
	public virtual void Init()
	{
		
	}
	
	public virtual void Populate(object obj)
	{
		ObjectList.Add(obj);
	}
	
	public virtual void DePopulate(object obj)
	{
		ObjectList.Remove(obj);
		Destroy((obj as GameObject));
		
	}
	
	void Update()
	{
		//Debug.Log(ObjectList.Count);
		if (ObjectList.Count >= 1)
		{
			foreach ( object OBJ in ObjectList)
			{
				(OBJ as GameObject).transform.localPosition = new Vector3(ObjectList.IndexOf(OBJ) * 30,(OBJ as GameObject).transform.localPosition.y,0);
			}
		}
	}
}
