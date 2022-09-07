using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMgr : MonoBehaviour
{
	#region �̱���
	static GameMgr instance = null;
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
	#endregion // �̱���

	Player player;
	Monster monster;
	cameramove camera;

	int directionnum; //0 - down 1-right 2-left
	void Awake()
    {
		GameDataMgr.Inst.LoadGameData(); //orbdata lord
		player = FindObjectOfType<Player>();
		monster = FindObjectOfType<Monster>();
		camera = FindObjectOfType<cameramove>();

	}
	
	// Start is called before the first frame update
	void Start()
    {
		UIManager.Inst.DestroymainUI();
		camera.gamstart();
		directionnum = 0;
	}
	public void startbutton() // ���۹�ư ������ ȣ��
	{
		camera.mapmove(directionnum);
		UIManager.Inst.DestroystartUI();
	}
	public void main() //mpeglin moved
    {
		camera.mainmove();
		UIManager.Inst.SetmonhpUI();
		UIManager.Inst.SetmainUI();
		OrbPool.Inst.SetOrb();
	}
	public void Playerattack(int AttackPower) //orb damage checked (orb ->player)
	{
		player.AttackGo(AttackPower);
	}
	public void Monsterattack() //player attacked (player ->monster)
	{
		monster.MonsterGo();
	}
	public void Playerdamage(int AttackPower) //(mon ->player)
	{
		player.Damage(AttackPower);
		if (player.IsDeath == false)
		{
			OrbPool.Inst.SetOrb();
		}
	}
	public void Monsterdamage(int attackpower) //player attacked (player ->monster)
	{
		monster.Damage(attackpower);
	}
	public void MonsterDie() 
	{
		player.Mapmove();
	}
	
	public void Direction(int dir) //peglin move dir check
	{
		directionnum = dir;
		camera.mapmove(directionnum);
	}
	
}
