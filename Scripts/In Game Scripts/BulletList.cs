using UnityEngine;
using System.Collections;

public class BulletList : List {
	
	
	void Start()
	{
		Init ();
	}
	
	public override void DePopulate (object obj)
	{
		base.DePopulate (obj);
	}

	public override void Init ()
	{
		for(int child = 0; child <= transform.GetChildCount(); child++)
		{
			ObjectList.Add(this.transform.GetChild(child).transform.gameObject);
		}
	}

	public override void Populate (object obj)
	{
		base.Populate (obj);
	}
	
}
