using UnityEngine;
using System.Collections;

public class ScoreKeeper : MonoBehaviour { // keeps track of player score along with the game over splash screens
	
	// VS mode victory conditions
	public static int player_Death;
	public static int enemy_Death;
	protected int VS_Score;
	private bool VS_GameOver;
	private bool VS_PlayerWin;
	//
	
	// Target practice victory conditions
	protected int target_Destroy;
	protected int target_Score;
	protected int target_Ammo;
	private bool target_GameOver;
	//
	
	// Misc
	public GUIStyle gameOver_Text = new GUIStyle();
	private float finishTime = 3.0f;
	//
	
	void Start()
	{ 
		gameOver_Text.font = (Font)Resources.Load("BAUS");
		gameOver_Text.fontSize = 150;
		
	}
	void Update () {
		
		Debug.Log(enemy_Death);
		if (StartMenu.VS_MODE)
		{
			victoryCon_VS();
		}
	}
	void OnGUI()
	{
		if (VS_GameOver)
		{
			GUI.Label(new Rect(250,300,800,150),"GAMEOVER",gameOver_Text);
			ReturntoStart();
		}
		if (VS_PlayerWin)
		{
			GUI.Label(new Rect(350,300,800,150),"WINNER",gameOver_Text);
			ReturntoStart();
		}
	}
	protected virtual void victoryCon_VS()
	{
		if (player_Death >= 4)
		{
			gameOver_Text.normal.textColor = new Color(256,0,0);
			VS_GameOver = true;
		}
		if (enemy_Death >= 4)
		{
			VS_PlayerWin = true;
			gameOver_Text.normal.textColor = new Color(0,256,0);
		}
		
	}
	private void ReturntoStart()
	{
		if (finishTime <= 0)
		{
			Application.LoadLevel("StartMenu");
			ResetGame();
		}
		else
			finishTime -= Time.deltaTime;
	}
	private void ResetGame()
	{
		player_Death = 0;
		enemy_Death = 0;
		VS_GameOver = false;
		VS_PlayerWin = false;
	}
}
