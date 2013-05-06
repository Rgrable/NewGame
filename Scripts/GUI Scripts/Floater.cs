using UnityEngine;
using System.Collections;

public class Floater : MonoBehaviour { // Items in the Level that just Spin in mid air.... nothing special
	private int x_Spin;
	private int y_Spin;
	private int z_Spin;

	void Start()
	{
		x_Spin = Random.Range(-1,2);
		y_Spin = Random.Range(-1,2);
		z_Spin = Random.Range(-1,2);
	}
	void Update () {
		
		transform.Rotate(x_Spin,y_Spin,z_Spin);
	}
}
