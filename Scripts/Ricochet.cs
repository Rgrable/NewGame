using UnityEngine;
using System.Collections;

public class Ricochet : MonoBehaviour {

	void Update () {
		
		Destroy(this.gameObject,30.0f);
		CheckBounds(GameObject.Find("Main Camera"));
	
	}
	
	protected virtual void CheckBounds(GameObject camera)
	{
		Vector3 newVelocity = transform.rigidbody.velocity;
		if (transform.position.x <= -215)
		{
			newVelocity.x *= -1.1f;
			transform.rigidbody.velocity = newVelocity;
		}
		else if (transform.position.x >= 215)
		{
			newVelocity.x *= -1.1f;
			transform.rigidbody.velocity = newVelocity;
		}
		else if (transform.position.z >= camera.transform.position.z + 114)
		{
			newVelocity.z *= -1.1f;
			transform.rigidbody.velocity = newVelocity;
		}
		else if (transform.position.z <= camera.transform.position.z - 114)
		{
			newVelocity.z *= -1.1f;
			transform.rigidbody.velocity = newVelocity;
		}
	}
	
	void OnBecameInvisible()
	{
		Destroy(this.gameObject);
	}
}
