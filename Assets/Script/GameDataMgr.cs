using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct PlayerData
{
	public int Level;
	public int Exp;
	public int AttackPower;
	public int MaxHP;
}

public struct MonsterData
{
	public int Level;
	public string Name;
	public int AttackPower;
	public int MaxHP;
}

public struct OrbData
{
	public int Level;
	public string Name;
	public int Damage;
	public int CriDamage;
	public int HealPower;
	public int AttackPower;
	public string Info;
}

// 게임데이터를 로드, 검색
public class GameDataMgr
{
	// 싱글턴
	#region 싱글턴
	static GameDataMgr inst = null;
	private GameDataMgr() { }
	public static GameDataMgr Inst
	{
		get
		{
			if (inst == null)
			{
				inst = new GameDataMgr();
			}
			return inst;
		}
	}
	#endregion //

	List<PlayerData> listPlayerData = new List<PlayerData>();
	List<MonsterData> listMonsterData = new List<MonsterData>();
	List<OrbData> listOrbData = new List<OrbData>();

	// 게임데이터 로드
	public void LoadGameData() //플레이어 awake에서 호출
	{
		loadPlayerData();
		//loadMonsterData();
		loadOrbData();
	}

	// orb 데이터
	void loadOrbData()
	{
		TextAsset ta = Resources.Load<TextAsset>("OrbData");

		string[] lines = ta.text.Split("\r\n");
		for (int i = 1; i < lines.Length-1; ++i)
		{
			// 데이터 1줄을 컴마로 구분한다.
			string[] columes = lines[i].Split(',');

			OrbData orbData = new OrbData();
			orbData.Level = int.Parse(columes[0]); // 레벨
			orbData.Name = columes[1];  // 이름
			orbData.Damage = int.Parse(columes[2]);  // 일반데미지
			orbData.CriDamage = int.Parse(columes[3]);  // 크리데미지
			orbData.HealPower = int.Parse(columes[4]);  // 힐
			orbData.AttackPower = int.Parse(columes[5]);  // 공격
			orbData.Info = columes[6];  // orb정보
			listOrbData.Add(orbData);
		}

	}

	// 플레이어 데이터
	void loadPlayerData()
	{
		TextAsset ta = Resources.Load<TextAsset>("PlayerData");

		string[] lines = ta.text.Split("\r\n");
		// 헤더를 제외
		for (int i = 1; i < lines.Length - 1; ++i)
		{
			// 데이터 1줄을 컴마로 구분
			string[] columes = lines[i].Split(',');

			PlayerData playerData = new PlayerData();
			playerData.Level = int.Parse(columes[0]);   // 레벨
			playerData.Exp = int.Parse(columes[1]);     // 경험치 누적값
			playerData.AttackPower = int.Parse(columes[2]);  // 공격력
			playerData.MaxHP = int.Parse(columes[3]);  // max hp
			listPlayerData.Add(playerData);
		}
	}


	// 몬스터 데이터
	void loadMonsterData()
	{
		TextAsset ta = Resources.Load<TextAsset>("MonsterData");

		string[] lines = ta.text.Split("\r\n");
		for (int i = 1; i < lines.Length - 1; ++i)
		{
			// 데이터 1줄을 컴마로 구분한다.
			string[] columes = lines[i].Split(',');

			MonsterData mobData = new MonsterData();
			mobData.Level = int.Parse(columes[0]); // 레벨
			mobData.Name = columes[1].Trim('\"');  // 이름  : "ddd" -> ddd
			mobData.AttackPower = int.Parse(columes[2]);  // 공격력
			mobData.MaxHP = int.Parse(columes[3]);  // max hp

			listMonsterData.Add(mobData);
		}
	}



	// 데이터 검색
	public MonsterData FindMonsterDataBy(int level)
	{
		MonsterData mobData = listMonsterData.Find(mData => mData.Level == level);
		return mobData;
	}

	public PlayerData FindPlayerDataBy(int level)
	{
		PlayerData playerData = listPlayerData.Find(pData => pData.Level == level);
		return playerData;
	}

	public OrbData FindOrbDataBy(int level)
	{
		OrbData orbData = listOrbData.Find(orbData => orbData.Level == level);
		return orbData;
	}

	// 경험치를 기준으로 플레이어 데이터 구하기
	public PlayerData FindPlayerDataByExp(int exp)
	{
		for (int i = 0; i < listPlayerData.Count; ++i)
		{
			if (listPlayerData[i].Exp >= exp)
			{
				return listPlayerData[i];
			}
		}
		return listPlayerData[listPlayerData.Count - 1];
	}

}
	