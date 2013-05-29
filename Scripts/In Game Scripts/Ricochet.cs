using UnityEngine;
using System.Collections;

public class Ricochet : MonoBehaviour {
	public AudioClip bounce;
	
	void Update () {
		
		Destroy(this.gameObject,30.0f);
	}
	
	void OnTriggerEnter(Collider wall)
	{
		Vector3 newVelocity = transform.rigidbody.velocity;
		
		if (wall.transform.name == "SideWall")
		{
			newVelocity.x *= -0.9f;
			transform.rigidbody.velocity = newVelocity;
			transform.rigidbody.mass -= 0.1f;
			AudioSource.PlayClipAtPoint(bounce,transform.position);
		}
		else if (wall.transform.name == "BackWall")
		{
			newVelocity.z *= -0.9f;
			transform.rigidbody.mass -= 0.1f;
			transform.rigidbody.velocity = newVelocity;
			AudioSource.PlayClipAtPoint(bounce,transform.position);
		}
	}
	
	void OnBecameInvisible()
	{
		Destroy(this.gameObject);
	}
}
