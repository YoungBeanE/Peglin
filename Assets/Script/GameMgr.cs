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
	Monster[] monster;
	cameramove camera;

	int waveNum;

	private void Awake()
    {
		GameDataMgr.Inst.LoadGameData(); //data lord
		player = FindObjectOfType<Player>();
		monster = FindObjectsOfType<Monster>();
		camera = FindObjectOfType<cameramove>();
	}
	
	// Start is called before the first frame update
	void Start()
    {
		camera.gamstart();
		waveNum = 1;
    }
	public void startbutton() // ���۹�ư ������
	{
		nextState(STATE.Map);
		UIManager.Inst.DestroystartUI();
		
	}
	
	public void main()
    {
		nextState(STATE.Orbcheck);
	}
	public void Pattack()
	{
		nextState(STATE.PlayerAttack);
	}

	public enum STATE
	{
		None,
		Map,   // �ʵ� �̵�
		Orbcheck, // orb - peg damage check
		PlayerAttack,
		MonsterAttack,
		Reward, //monster die
		Death, //player die

		MAX
	}

	Coroutine prevCoroutine = null;
	STATE curState = STATE.None;

	private void nextState(STATE newState) // �������� �ڷ�ƾ ��ȯ
	{
		//if (IsDeath) return; // ������ ����
		if (newState == curState) return; //���� ���Ƶ� ����

		// ���� �ڷ�ƾ ����
		if (prevCoroutine != null)
			StopCoroutine(prevCoroutine);

		// ���ο� ���·� ����
		curState = newState;
		prevCoroutine = StartCoroutine(newState.ToString() + "_State");
	}
	IEnumerator Map_State()
	{
		camera.mapmove();
		yield return null;
	}
	IEnumerator Orbcheck_State()
	{
		camera.mainmove();
		OrbPool.Inst.SetOrb();
		yield return null;
	}
	/*
	IEnumerator PlayerAttack_State()
	{
		player.
		yield return new WaitForSeconds(1f);

	}*/

	IEnumerator MonsterAttack_State()
	{

		yield return new WaitForSeconds(2f);

	}


	IEnumerator Reward_State()
	{

		// ���� ���׾�����
		nextState(STATE.Orbcheck);
		yield return null;
	}

	IEnumerator Death_State()
	{

		yield return null;

	}
	
	
}
