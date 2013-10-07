using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public delegate void ActionScreen(bool isActive);

public class GameScript : MonoBehaviour  {
    
//    public TextMesh _scoreBoard = new TextMesh();
//    public TextMesh _timeBoard = new TextMesh();
//    
    
    public TextMesh winLoss = new TextMesh();
	public TextMesh T_O_winLoss, T_Level, T_PlayerName;
	public GameObject levelBar;
	
	public List<Turret> _EnemyList = new List<Turret>().ToList();
	public List<Turret> _PlayerList = new List<Turret>().ToList();
	
	public Transform[] breakPoints = new Transform[3];
	public enum _breaks
	{
		PLAYERMODE,
		BATTLEMODE,
		INVENTORYMODE
	};
	
	public Player _player;
 
   void Start()
	{
		//Debug.Log(GlobalVars._TempWins);
		
		if (GlobalVars.GameMode != "Wager Match")
		{
			SetBoard(false);
		}
		else
		{
			SetWagerBoard();
		}
	}
    
	public void SetWagerBoard()
	{
		for (int i = 0; i < GlobalVars._turretPos.Length; i++)
		{
			GameObject newTurret = (GameObject)Instantiate(Resources.Load("Turret"));
			newTurret.transform.position = GameObject.Find("Position_" + (i + 1).ToString()).transform.position;
			newTurret.transform.name = "Turret_" + (i + 1).ToString();
			
			Turret _turret = newTurret.GetComponentInChildren<Turret>();
			_PlayerList[i] = _turret;
			_turret.turretNumber = i;
			_turret.Create();
			
			GameObject newEnemy = (GameObject)Instantiate((GameObject)Resources.Load("Turret_Enemy"));
				newEnemy.transform.position = GameObject.Find("Position_" +(i + 5).ToString()).transform.position;
				
				
			Turret _Eturret = newEnemy.GetComponentInChildren<Turret>();
			_EnemyList[i] = _Eturret;
			_Eturret.turretNumber = -999;
			_Eturret.Create();
			
			_PlayerList[i].startGame = true;
			_EnemyList[i].startGame = true;
		}
	}
	
	public void SetBoard(bool additive, GameObject hit = null, GameObject selectedTurret = null)
	{
		GlobalVars.GetPosition();
		if (!additive)
		{
			for (int i = 0; i < GlobalVars._turretPos.Length; i++)
			{
				
				if (GlobalVars._turretPos[i] != "NONE")
				{
					GameObject newTurret = (GameObject)Instantiate(Resources.Load("Turret"));
					newTurret.transform.position = GameObject.Find("Position_" + (i + 1).ToString()).transform.position;
					newTurret.transform.name = "Turret_" + (i + 1).ToString();
					
					Turret _turret = newTurret.GetComponentInChildren<Turret>();
					_PlayerList[i] = _turret;
					_turret.turretNumber = i;
					_turret.Create();
					
					GameObject pos = GameObject.Find("Position_" + (i + 1));
					GameObject newDisplay = (GameObject)Instantiate((GameObject)Resources.Load(GlobalVars.GetPlayerTurrets(_turret.turretNumber) + "Display"));
					newDisplay.name = "TurretDisplay_" + (i + 1).ToString();
					newDisplay.transform.position = new Vector3(pos.transform.position.x,-275,pos.transform.position.z);
					
				}
			}
		}
		else
		{
			GameObject newTurret = (GameObject)Instantiate((GameObject)Resources.Load("Turret"));
			newTurret.transform.position = hit.transform.position;
			newTurret.transform.name = "Turret_" + (int.Parse(hit.transform.name.Replace("Position_",""))).ToString();
			Turret gameTurret = newTurret.transform.GetComponentInChildren<Turret>();
			gameTurret.turretNumber = (int.Parse(hit.transform.name.Replace("Position_","")) - 1);
			gameTurret.Create();
			gameTurret.ChangeTurret(selectedTurret.name);	
			gameTurret.ChangeTurretColor(selectedTurret.renderer.material);
			
			_PlayerList[(int.Parse(hit.transform.name.Replace("Position_","")) - 1)] = gameTurret;
			
			GameObject pos = hit;
			GameObject newDisplay = (GameObject)Instantiate((GameObject)Resources.Load(GlobalVars.GetPlayerTurrets(gameTurret.turretNumber) + "Display"));
			newDisplay.name = "TurretDisplay_" + (int.Parse(hit.transform.name.Replace("Position_",""))).ToString();
			newDisplay.transform.position = new Vector3(pos.transform.position.x,-275,pos.transform.position.z);
			
		}
		
	}
	
	public void StartMatch(int enemyCount)
	{
		this.transform.position = breakPoints[(int)_breaks.BATTLEMODE].position;
		_player._moveable = false;
		
		for (int i = 0; i < enemyCount; i++)
		{
			if (_EnemyList[i] == null)
			{
				GameObject newEnemy = (GameObject)Instantiate((GameObject)Resources.Load("Turret_Enemy"));
				newEnemy.transform.position = GameObject.Find("Position_" + (Random.Range(5,9)).ToString()).transform.position;
				
				
				Turret _turret = newEnemy.GetComponentInChildren<Turret>();
				_EnemyList[i] = _turret;
				_turret.turretNumber = -999;
				_turret.Create();
				
			}
		}
		
		for (int i = 0; i < _PlayerList.Count; i++)
			{
				if (_PlayerList[i] != null)
				{
					_PlayerList[i].startGame = true;
				}
			}
		for (int i = 0; i < _EnemyList.Count; i++)
		{
			if (_EnemyList[i] != null)
			{
				_EnemyList[i].startGame = true;
			}
		}
	}
	
	public void Inventory(bool isActive)
	{
		_player._moveable = false;
		this.transform.position = breakPoints[(int)_breaks.INVENTORYMODE].position;
	}
	
	public void PlayerScreen(bool isActive)
	{
		this.transform.position = breakPoints[(int)_breaks.PLAYERMODE].position;
		_player._moveable = true;
		for (int i = 0; i < _PlayerList.Count; i++)
			{
				if (_PlayerList[i] != null)
				{
					_PlayerList[i].startGame = false;
					
				}
				else
				{
					Destroy(GameObject.Find("TurretDisplay_" + (i + 1).ToString()));
						
					WeaponSlots defaultSlot = GameObject.Find("Default_" + (i + 1).ToString()).GetComponent<WeaponSlots>();
					WeaponSlots secondarySlot = GameObject.Find("Secondary_" + (i + 1).ToString()).GetComponent<WeaponSlots>();
					
					GlobalVars._turretPos[i] = "NONE";
				
					defaultSlot.Erase();
					secondarySlot.Erase();
				}
			}
		for (int i = 0; i < _EnemyList.Count; i++)
			{
				if (_EnemyList[i] != null)
				{
					Destroy(_EnemyList[i].transform.parent.gameObject);
				}
			}
	}
	
    void Update()
    {
       CheckInput();
		
		winLoss.text = GlobalVars._TempWins.ToString() + "/" + GlobalVars._TempLoss.ToString();
		levelBar.transform.localScale = new Vector3(0.9f,(GlobalVars.playerLevelBar / GlobalVars.playerProgressNeeded) * 45,0.9f);
		T_O_winLoss.text = GlobalVars._playerScore_Wins.ToString() + "/" + GlobalVars._playerScore_Losses.ToString();
		T_Level.text = "Level: " + GlobalVars.playerLevel.ToString();
		T_PlayerName.text = GlobalVars.playerName;
		
		AudioListener.volume = GlobalVars.SoundFocus;
    }
    
	public void CheckResults(bool isPlayer,Turret T)
	{
		if (GlobalVars.GameMode != "Wager Match")
		{
			if (!isPlayer)
			{

				Destroy(T.transform.parent.gameObject);
				_EnemyList.Insert(_EnemyList.IndexOf(T),null);
				_EnemyList.Remove(T);
				GlobalVars.HasPlayerLeveled(Random.Range(1,15));
	
				if (_EnemyList[0] == null &&_EnemyList[1] == null &&_EnemyList[2] == null &&_EnemyList[3] == null)
				{
					GlobalVars._playerScore_Wins++;
					GlobalVars._TempWins++;
					EndGame(false);
				}
			}
			else
			{
	
				Destroy(T.transform.parent.gameObject);
				_PlayerList.Insert(_PlayerList.IndexOf(T),null);
				_PlayerList.Remove(T);
				
				if (_PlayerList[0] == null &&_PlayerList[1] == null &&_PlayerList[2] == null &&_PlayerList[3] == null)
				{
					GlobalVars._playerScore_Losses++;
					GlobalVars._TempLoss++;
					EndGame(false);
				}
			}
		}
		
		else
		{
			if (!isPlayer)
			{
				_EnemyList.Remove(T);
				Destroy(T.transform.parent.gameObject);
				GlobalVars.HasPlayerLeveled(Random.Range(1,15));
				
				if (_EnemyList.Count == 0)
				{
					GlobalVars._playerScore_Wins++;
					GlobalVars._TempWins++;
					EndGame(true);
				}	
			}
			else
			{
				_PlayerList.Remove(T);
				Destroy(T.transform.parent.gameObject);
				
				if (_PlayerList.Count == 0)
				{
					GlobalVars._playerScore_Losses++;
					GlobalVars._TempLoss++;
					EndGame(true);
				}
			}
			
		}
		
		
	}
	
	private void EndGame(bool isWager)
	{
		
		if (isWager)
		{
			float reward = ((float)GlobalVars._TempWins / (float)GlobalVars.WagerRound) * (GlobalVars.WagerRound * 50);
			GlobalVars.coinAmount += (int)reward;
			if (GlobalVars.isEndGame())
			{
				Application.LoadLevel("StartMenu");
			}
			else
			{
				Application.LoadLevel(Application.loadedLevel);
			}
		}
		else
		{
			PlayerScreen(true);
		}
		
	}
	
	public void CheckInput()
	{
		 if (Input.GetMouseButtonDown(0))
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit))
			{
				if (hit.transform.name == "1")
				{
					Time.timeScale = 1;
				}
				if (hit.transform.name == "2")
				{
					Time.timeScale = 2;
				}
				if (hit.transform.name == "3")
				{
					Time.timeScale = 3;
				}
				if (hit.transform.name == "4")
				{
					Time.timeScale = 4;
				}
				if (hit.transform.name == "5")
				{
					Time.timeScale = 5;
				}
				
				if (hit.transform.name == "II")
				{
					Inventory(true);
				}
				if (hit.transform.name == "Back")
				{
					PlayerScreen(true);
				}
			}
			
		}
	}
	
	
	void OnDestroy()
	{
		GlobalVars.SavePreferences();
	}
	
	void OnApplicationQuit()
	{
		GlobalVars.SavePreferences();
	}
	
	void OnApplicationFocus(bool focus)
	{
		
		if (focus)
		{
			GlobalVars.SoundFocus = 1;
		}
		else
		{
			GlobalVars.SoundFocus = 0;
		}
	}
}