using UnityEngine;
using System.Collections;

public class ModelList : List {
	
	void Start()
	{
		Init();
	}
	
	public override void Init ()
	{
		for(int child = 0; child <= transform.GetChildCount(); child++)
		{
			ObjectList.Add(this.transform.GetChild(child).transform.gameObject);
		}
	}
}
