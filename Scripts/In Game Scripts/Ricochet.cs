using UnityEngine;
using System.Collections;

public class Ricochet : MonoBehaviour {
	public AudioClip bounce;
	public int damageMultiplier;
	
	void Update () {
		
		Destroy(this.gameObject,30.0f);
	}
	
	void OnTriggerEnter(Collider wall)
	{
		Vector3 newVelocity = transform.rigidbody.velocity;
		
		if (wall.transform.gameObject.name.Contains("_SideWall"))
		{
			newVelocity.x *= -1.01f;
			transform.rigidbody.velocity = newVelocity;
			AudioSource.PlayClipAtPoint(bounce,transform.position);
			damageMultiplier++;
		}
		else if (wall.transform.gameObject.name.Contains("_BackWall"))
		{
			newVelocity.z *= -1.01f; 
			transform.rigidbody.velocity = newVelocity;
			AudioSource.PlayClipAtPoint(bounce,transform.position);
            damageMultiplier++;
		}
	}
	
	void OnBecameInvisible()
	{
		Destroy(this.gameObject);
	}
}
