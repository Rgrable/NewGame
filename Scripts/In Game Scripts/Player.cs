using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	
	public GameObject checkPoint;
	private bool moving;
	
	private const int LOCK_SPACE = 2;
	public float speed = 3.0f;
	
	public bool _moveable = true;
	private GameScript _GM;
	
	// Use this for initialization
	void Start () {
		
		_GM = Camera.main.GetComponent<GameScript>();
	
	}
	
	// Update is called once per frame
	void Update () {
		if (_moveable)
		{
			if (Input.GetMouseButtonUp(0))
			{
				CheckInput();
			}
			
			if (moving)
			{
				Movement();
			}
		}
		
	}
	
	void CheckInput()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		
		if (Physics.Raycast(ray,out hit))
		{
			checkPoint.transform.position = new Vector3(ray.origin.x, this.transform.position.y,ray.origin.z);
			moving = true;
		}	
	}
	
	void OnTriggerEnter(Collider hit)
	{
		if (hit.transform.name.Contains("Enemy"))
		{
			_GM.StartMatch(Random.Range(1,5));
		}
	}
	
	private void Movement()
	{
		if (this.transform.position.x < checkPoint.transform.position.x - LOCK_SPACE)
		{
			this.transform.position += transform.right * speed;
		}
		else if (this.transform.position.x > checkPoint.transform.position.x + LOCK_SPACE)
		{
			this.transform.position -= transform.right * speed;
			
		}
		else if (this.transform.position.z > checkPoint.transform.position.z + LOCK_SPACE)
		{
			this.transform.position -= transform.forward * speed;
		}
		else if (this.transform.position.z < checkPoint.transform.position.z - LOCK_SPACE)
		{
			this.transform.position += transform.forward * speed;
		}
		else
		{
			this.transform.position = checkPoint.transform.position;
			moving = false;
		}
	}
}
