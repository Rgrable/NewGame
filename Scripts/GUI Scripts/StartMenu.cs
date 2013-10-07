using UnityEngine;
using System.Collections;

public class StartMenu : MonoBehaviour {
	
	public TextMesh T_PlayerName,T_WagerAmount, T_CoinAmount, T_WinLoss, T_Level;
	public int coins; 
	public GameObject LevelBar;
	
	
	void Start()
	{
		coins = GlobalVars.coinAmount;
		GlobalVars.HardReset();
		GlobalVars.WagerRound = 0;
		GlobalVars.RoundNumber = 1;
		GlobalVars._TempWins = 0;
		GlobalVars._TempLoss = 0;
	}
	
	void Update()
	{
		CheckInput();
		SetStrings();
		CheckLevel();
	}
	
	private void SetStrings()
	{
		T_WagerAmount.text = GlobalVars.WagerRound.ToString();
		T_CoinAmount.text = "Coins: " + coins.ToString();
		T_WinLoss.text = GlobalVars._playerScore_Wins.ToString() + "/" + GlobalVars._playerScore_Losses.ToString();
		T_Level.text = "Level: " + GlobalVars.playerLevel.ToString();
		T_PlayerName.text = GlobalVars.playerName;
	}
	
	private void CheckLevel()
	{
		LevelBar.transform.localScale = new Vector3(0.9f,(GlobalVars.playerLevelBar / GlobalVars.playerProgressNeeded) * 11.5f,0.9f);
	}
	
	private void CheckInput()
	{
		if (Input.GetMouseButtonDown(0))
		{
	
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			
			if (Physics.Raycast(ray, out hit))
			{
				if (hit.transform.name == "PlayGame")
				{
					Application.LoadLevel("Game");
				}
				
				if (hit.transform.name == "Wager Begin")
				{
					GlobalVars.coinAmount -= GlobalVars.WagerRound;
					Application.LoadLevel("Wager Match");
				}
				
				if (hit.transform.name == "Wager Left")
				{
					if (GlobalVars.WagerRound <= 0)
					{
						GlobalVars.WagerRound = GlobalVars.coinAmount;
						coins = 0;
					}
					else
					{
						GlobalVars.WagerRound--;
						coins++;
					}
				}
				if (hit.transform.name == "Wager Right")
				{
					if (GlobalVars.WagerRound >= GlobalVars.coinAmount)
					{
						GlobalVars.WagerRound = 0;
						coins = GlobalVars.coinAmount;
					}
					else
					{
						GlobalVars.WagerRound++;
						coins--;
					}
				}
			}
		}
	}
	
}
