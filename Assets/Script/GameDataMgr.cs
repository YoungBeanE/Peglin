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

// ���ӵ����͸� �ε�, �˻�
public class GameDataMgr
{
	// �̱���
	#region �̱���
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

	// ���ӵ����� �ε�
	public void LoadGameData() //�÷��̾� awake���� ȣ��
	{
		loadPlayerData();
		//loadMonsterData();
		loadOrbData();
	}

	// orb ������
	void loadOrbData()
	{
		TextAsset ta = Resources.Load<TextAsset>("OrbData");

		string[] lines = ta.text.Split("\r\n");
		for (int i = 1; i < lines.Length-1; ++i)
		{
			// ������ 1���� �ĸ��� �����Ѵ�.
			string[] columes = lines[i].Split(',');

			OrbData orbData = new OrbData();
			orbData.Level = int.Parse(columes[0]); // ����
			orbData.Name = columes[1];  // �̸�
			orbData.Damage = int.Parse(columes[2]);  // �Ϲݵ�����
			orbData.CriDamage = int.Parse(columes[3]);  // ũ��������
			orbData.HealPower = int.Parse(columes[4]);  // ��
			orbData.AttackPower = int.Parse(columes[5]);  // ����
			orbData.Info = columes[6];  // orb����
			listOrbData.Add(orbData);
		}

	}

	// �÷��̾� ������
	void loadPlayerData()
	{
		TextAsset ta = Resources.Load<TextAsset>("PlayerData");

		string[] lines = ta.text.Split("\r\n");
		// ����� ����
		for (int i = 1; i < lines.Length - 1; ++i)
		{
			// ������ 1���� �ĸ��� ����
			string[] columes = lines[i].Split(',');

			PlayerData playerData = new PlayerData();
			playerData.Level = int.Parse(columes[0]);   // ����
			playerData.Exp = int.Parse(columes[1]);     // ����ġ ������
			playerData.AttackPower = int.Parse(columes[2]);  // ���ݷ�
			playerData.MaxHP = int.Parse(columes[3]);  // max hp
			listPlayerData.Add(playerData);
		}
	}


	// ���� ������
	void loadMonsterData()
	{
		TextAsset ta = Resources.Load<TextAsset>("MonsterData");

		string[] lines = ta.text.Split("\r\n");
		for (int i = 1; i < lines.Length - 1; ++i)
		{
			// ������ 1���� �ĸ��� �����Ѵ�.
			string[] columes = lines[i].Split(',');

			MonsterData mobData = new MonsterData();
			mobData.Level = int.Parse(columes[0]); // ����
			mobData.Name = columes[1].Trim('\"');  // �̸�  : "ddd" -> ddd
			mobData.AttackPower = int.Parse(columes[2]);  // ���ݷ�
			mobData.MaxHP = int.Parse(columes[3]);  // max hp

			listMonsterData.Add(mobData);
		}
	}



	// ������ �˻�
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

	// ����ġ�� �������� �÷��̾� ������ ���ϱ�
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
	