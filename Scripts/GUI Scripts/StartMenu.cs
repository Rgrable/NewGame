using UnityEngine;
using System.Collections;

public class StartMenu : MonoBehaviour {
	public GUIStyle Menu_Tex = new GUIStyle();
	public GUIStyle Text_Buttons = new GUIStyle();
	public static bool VS_MODE;
	public static bool TARGET_MODE;

	void OnGUI()
	{
		GUI.Label(new Rect(0,0,Screen.width,Screen.height),"",Menu_Tex);
		
		if (GUI.Button(new Rect(Screen.width / 2 - 590,Screen.height / 2 - 100,320,150),"PLAY",Text_Buttons))
		{
			VS_MODE = true;
			Application.LoadLevel("TestBattle_Computer");
		}
		if (GUI.Button(new Rect(Screen.width / 2 - 590,Screen.height / 2 + 50,320,150),"QUIT",Text_Buttons))
		{
			Application.Quit();
		}
	}

}
