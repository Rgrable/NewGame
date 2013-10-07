using UnityEngine;
using System.Collections;

public class NameMenu : MonoBehaviour {

	// Use this for initialization
	
	public TextMesh _name = new TextMesh();
	
	void Start () {
		
		if (GlobalVars._enteredName == "TRUE")
		{
			Application.LoadLevel("StartMenu");
		}
		
	
	}
	
	// Update is called once per frame
	void Update () {
		
		foreach (char c in Input.inputString)
		{
			
			if (c == "\b"[0])
			{
				if (_name.text.Length != 0)
				{
					_name.text = _name.text.Substring(0,_name.text.Length -1);
				}
			}
			else if ((c== "\n"[0] || c == "\r"[0]) && _name.text.Length > 1)
			{
				GlobalVars.playerName = _name.text;
				PlayerPrefs.SetString("PlayerName",GlobalVars.playerName);
				PlayerPrefs.SetString("NameSet","TRUE");
				Application.LoadLevel("StartMenu");
			}
			else
			{
				if (_name.text.Length < 9)
				{
					_name.text += c;
				}
			}
		}
		
		if (Input.GetMouseButtonDown(0))
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			
			if (Physics.Raycast(ray, out hit))
			{
				if (hit.transform.name == "Name")
				{
					
				}
			}
		}
		
	
	}
}
