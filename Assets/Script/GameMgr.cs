using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMgr : GameDataMgr
{
	#region Singleton
	private static GameMgr instance = null;
    public static GameMgr Inst
	{
		get
		{
            if(instance == null)
			{
                instance = FindObjectOfType<GameMgr>();
                if (instance == null)
                    instance = new GameObject("GameMgr").AddComponent<GameMgr>();
			}
            return instance;
		}
	}
	#endregion

	[SerializeField] Player player;
	[SerializeField] Monster monster;
	[SerializeField] cameramove cam;
	[SerializeField] Speglin speglin;

	int directionnum; //0 - down 1-right 2-left

	void Awake()
    {
		instance = this;
		LoadGameData(); //Load orbdata
	}
	
	// Start is called before the first frame update
	void Start()
    {
		UIManager.Inst.DestroymainUI();
		monster.SetUI(false);
		directionnum = 0;
	}
	public void startbutton() // 시작버튼 누르면 호출
	{
		speglin.gameObject.SetActive(false);
		cam.MoveMap(directionnum);
		UIManager.Inst.DestroystartUI();
	}

	public void StageStart() //mpeglin moved
    {
		if(monster.IsDeath) UIManager.Inst.GameOver(true);
        else
        {
			cam.MoveMain();
			UIManager.Inst.SetmainUI();
			monster.SetUI(true);
			OrbPool.Inst.SetOrb();
		}
		
		
	}

	public void Playerattack(int AttackPower)
	{
		player.Attack(AttackPower);
	}

	public void Playerdamage(int AttackPower) //(mon ->player)
	{
		player.Damage(AttackPower);
		if (player.IsDeath == false && monster.IsDeath == false)
		{
			OrbPool.Inst.SetOrb();
		}
	}
	public void Monsterdamage(int attackpower) //player attacked (player ->monster)
	{
		monster.Damage(attackpower);
	}
	public void MonsterBdamage(int attackpower) //player attacked (player ->monster)
	{
		monster.BDamage(attackpower);
	}
	public void MonsterDie() 
	{
		Debug.Log("몬스터 죽음");
		player.Mapmove();
	}
	public void Direction(int dir) //peglin move dir check
	{
		UIManager.Inst.DestroymainUI();
		directionnum = dir;
		cam.MoveMap(directionnum);
	}
	
}
