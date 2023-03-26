using System.Collections.Generic;
using UnityEngine;

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

public class GameDataMgr : MonoBehaviour
{
	List<OrbData> listOrbData = null;

	protected void LoadGameData()
	{
		listOrbData = new List<OrbData>();
		loadOrbData();
	}

	// orb Data Parsing
	void loadOrbData()
	{
		TextAsset ta = Resources.Load<TextAsset>("OrbData");

		string[] lines = ta.text.Split("\r\n");
		for (int i = 1; i < lines.Length-1; ++i)
		{
			// ������ 1�� �ĸ��� ����
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

	public OrbData FindOrbDataBy(int level)
	{
		return listOrbData.Find(oData => oData.Level == level);
	}
}
	