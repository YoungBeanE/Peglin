using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMgr : MonoBehaviour
{
	//=============================================================================
	// �̱���
	#region �̱���
	static GameMgr instance = null;
    public static GameMgr Inst
	{
		get
		{
            if(instance == null)
			{
				// ���� ã�� ����
                instance = FindObjectOfType<GameMgr>();
				// ���ٸ� ���� ����
                if (instance == null)
                    instance = new GameObject("GameMgr").AddComponent<GameMgr>();
			}
            return instance;
		}
	}
	#endregion // �̱���
	//=============================================================================

	[SerializeField] UIManager uiManager = null;


	// Start is called before the first frame update
	void Start()
    {
		StartCoroutine(processGameMgr());
    }


	// 
	// ���� ��ü ������ �����Ѵ�.
	IEnumerator processGameMgr()
	{
		int waveNum = 1;
		while (true)
		{
			// 1. UIManager ���� ���̺� UI�� ����ϵ��� ��û�Ѵ�. (�׸��� ���~)
			Debug.Log("## Wave UI ���� ����");
			yield return StartCoroutine(uiManager.processWaveUI(waveNum));

			// 2. ���������� ��ü�鿡�� ���� ���̺��� ���͸� �����ϵ��� ��û (�׸��� ���~)
			Debug.Log("## �����ʵ鿡�� ���� ��û");
			//SpawnArea_Ver2[] spawner = FindObjectsOfType<SpawnArea_Ver2>();
			//foreach(SpawnArea_Ver2 spawnArea in spawner)
			{
				//spawnArea.Go(waveNum);
			}


			// ���� �� ���� �ִٸ� 
			//while (MonsterListMgr.Inst.GetAllCount() > 0)
				yield return null; // ���� �����ӱ��� ��ٸ��� ��

			// ���� ���̺�
			++waveNum;
		}

	}

}
