using UnityEngine;
using System.Collections;

public class Ricochet : MonoBehaviour {
	private float hitTimer = 0.4f; // makes sure that the ball doesnt get stuck ricocheting
	private bool hitBool;
	public AudioClip bounce;
	
	void Update () {
		
		Destroy(this.gameObject,30.0f);
		CheckBounds(GameObject.Find("Main Camera"));
	
	}
	
	protected virtual void CheckBounds(GameObject camera)
	{
		Vector3 newVelocity = transform.rigidbody.velocity;
		if (!hitBool)
		{
			if (transform.position.x < -215)
			{
				newVelocity.x *= -1.0f;
				transform.rigidbody.velocity = newVelocity;
				hitBool = true;
				AudioSource.PlayClipAtPoint(bounce,transform.position);
			}
			else if (transform.position.x > 215)
			{
				newVelocity.x *= -1.0f;
				transform.rigidbody.velocity = newVelocity;
				hitBool = true;
				AudioSource.PlayClipAtPoint(bounce,transform.position);
			}
			else if (transform.position.z > camera.transform.position.z + 117)
			{
				newVelocity.z *= -1.0f;
				transform.rigidbody.velocity = newVelocity;
				hitBool = true;
				AudioSource.PlayClipAtPoint(bounce,transform.position);
			}
			else if (transform.position.z < camera.transform.position.z - 117)
			{
				newVelocity.z *= -1.0f;
				transform.rigidbody.velocity = newVelocity;
				hitBool = true;
				AudioSource.PlayClipAtPoint(bounce,transform.position);
			}
		}
		else
			Hit();
	}
	
	protected virtual void Hit()
	{
		if (hitTimer <= 0)
			hitBool = false;
		else
			hitTimer -= Time.deltaTime;
	}
	void OnBecameInvisible()
	{
		Destroy(this.gameObject);
	}
}
