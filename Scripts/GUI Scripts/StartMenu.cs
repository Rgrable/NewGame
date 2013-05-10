using UnityEngine;
using System.Collections;

public class StartMenu : MonoBehaviour {
	public GUIStyle Menu_Tex = new GUIStyle();
	public GUIStyle Text_Buttons = new GUIStyle();
	public GUIStyle HS_Style = new GUIStyle();
	public static bool VS_MODE;
	public static bool TARGET_MODE;
	private Vector2 pivot;
	private bool growing_T;

	void OnGUI()
	{
		GUI.Label(new Rect(0,0,Screen.width,Screen.height),"",Menu_Tex);
		
		GUI.BeginGroup(new Rect(0,0,Screen.width,Screen.height));
		GUIUtility.RotateAroundPivot(15,pivot);
		GUI.Label(new Rect(800,100,700,300),"YOU'VE DESTROYED \n" + ScoreKeeper.OverallScore.ToString() + " Enemies!!",HS_Style);
		GUIUtility.RotateAroundPivot(-15,pivot);
		GUI.EndGroup();
		
		
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
