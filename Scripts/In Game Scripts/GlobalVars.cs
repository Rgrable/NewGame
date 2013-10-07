using UnityEngine;
using System.Collections;
 

public static class GlobalVars {

    public static string _deathSound = PlayerPrefs.GetString("DeathSound","_DefaultDeath");
    public static string _hitSound = PlayerPrefs.GetString("HitSound","_DefaultHit");
    public static string _armorSound = PlayerPrefs.GetString("ArmorSound","_DefaultArmor");
	public static string[] _secondaryWeapons = new string[4];
    
	public static string playerName = PlayerPrefs.GetString("PlayerName","");
	public static string _enteredName = PlayerPrefs.GetString("NameSet");
    public static string[] _playerTurrets = new string[4];
	public static string[] _playerColor = new string[4];
	public static string[] _turretPos = new string[4];
	
	public static int _playerScore_Wins = PlayerPrefs.GetInt("PlayerWins",0),
							 _playerScore_Losses = PlayerPrefs.GetInt("PlayerLoss",0);
	
	public static int RoundNumber = 1;
	public static int WagerRound;
	public static int _TempWins, _TempLoss;
	public static int coinAmount = PlayerPrefs.GetInt("PlayerCoins",500);
	
	public static float playerLevelBar = PlayerPrefs.GetFloat("PlayerLevelBar",0);
	public static float playerProgressNeeded = PlayerPrefs.GetFloat("ProgressNeeded",50);
	public static int playerLevel = PlayerPrefs.GetInt("PlayerLevel",1);
	
	public static string GameMode = "Wager Match";
	
	
	public static int SoundFocus = 1;
	
    public enum ETurret
    {
      TURRET_1,
      TURRET_2,
      TURRET_3,
      TURRET_4,
      NULL
    };
    
    public static void HasPlayerLeveled(float progress)
	{
		if (playerLevelBar >= playerProgressNeeded)
		{
			playerProgressNeeded += (playerProgressNeeded / 2);
			playerLevelBar = 0;
			playerLevel++;
		}
		else
		{
			playerLevelBar += progress;
		}
	}
	
	public static bool isEndGame()
	{
		if (RoundNumber == WagerRound)
		{
			return true;
		}
		else
		{
			RoundNumber++;
			return false;
		}
	}
	
    public static string GetPlayerTurrets(int turretNumber)
    {
		if (turretNumber == -999)
		{
			int random = Random.Range(1,4);
			
			switch (random)
			{
			case 1:
				return "Default";
			case 2:
				return "Bug";
			case 3:
				return "Piercer";
			}
			
		}
		_playerTurrets[turretNumber] = PlayerPrefs.GetString("TurretType_" + turretNumber.ToString(),null);
		SetTurret(turretNumber,_playerTurrets[turretNumber].ToString());
		GetMaterial(turretNumber,"Green");
     	return   _playerTurrets[turretNumber];
    }
    
    public static string GetSecondaryBullet(int turretNumber)
	{
		//_secondaryWeapons[turretNumber] = PlayerPrefs.GetString("SecondaryWeapon_" + turretNumber.ToString(),"DefaultBullet");
		_secondaryWeapons[turretNumber] = "DefaultBullet";
		return _secondaryWeapons[turretNumber];
	}
	
	public static void SetSecondaryBullet(int turretNumber, string bullet_name)
	{
		_secondaryWeapons[turretNumber] = bullet_name;
		//PlayerPrefs.SetString("SecondaryWeapon_" + turretNumber.ToString(),_secondaryWeapons[turretNumber]);
	}
    
    public static void SavePreferences()
    {
//        for (int i = 0; i < (int)ETurret.NULL; i++)
//        {
//           PlayerPrefs.SetString("TurretType_" + i.ToString(), _playerTurrets[i]);
//			PlayerPrefs.SetString("TurretMaterial" + i.ToString(), _playerColor[i]);
//			PlayerPrefs.SetString("TurretPosition_" + i.ToString(), _turretPos[i]);
//			
//			Debug.Log(_playerTurrets[i]);
//        }
//		
//		PlayerPrefs.SetInt("PlayerCoins",coinAmount);
//		PlayerPrefs.SetInt("PlayerWins",_playerScore_Wins);
//		PlayerPrefs.SetInt("PlayerLoss",_playerScore_Losses);
//		PlayerPrefs.SetFloat("PlayerLevelBar",playerLevelBar);
//		PlayerPrefs.SetInt("PlayerLevel",playerLevel);
//		PlayerPrefs.SetFloat("ProgressNeeded",playerProgressNeeded);
		
    }
	
	public static void HardReset()
	{
	 	coinAmount = 500;
		_playerScore_Losses = 0;
		_playerScore_Wins = 0;
		playerLevelBar = 0;
		playerLevel = 1;
		playerProgressNeeded = 50;
		
		 for (int i = 0; i < (int)ETurret.NULL; i++)
        {
            _playerTurrets[i] = null;
			_playerColor[i] = "Green";
			_turretPos[i] = "NONE";
			_secondaryWeapons[i] = "DefaultBullet";
        }
		
		SavePreferences();
	}
	
	public static void SetTurret(int position, string name)
	{
		_playerTurrets[position] = name;
		PlayerPrefs.SetString("TurretType_" + (position).ToString(),_playerTurrets[(position)]);
	}
	
	public static void SetPosition(int position,string name)
	{
		_turretPos[(position - 1)] = name;
		//PlayerPrefs.SetString("TurretPosition_" + (position - 1).ToString(),_turretPos[(position - 1)]);
	}
	
	public static void GetPosition()
	{
		for (int i = 0; i < _turretPos.Length; i++)
		{
			_turretPos[i] = PlayerPrefs.GetString("TurretPosition_" + i.ToString(),"NONE");
			Debug.Log(_turretPos[i]);
		}
	}
	
	public static void SetMaterial(int index, string ColorName)
	{
		//Debug.Log(ColorName);
		PlayerPrefs.SetString("TurretMaterial" + index.ToString(),ColorName);
	}
	
	private static void GetMaterial(int index, string defaultName)
	{
		_playerColor[index] = PlayerPrefs.GetString("TurretMaterial" + index.ToString(),defaultName);
	}
}