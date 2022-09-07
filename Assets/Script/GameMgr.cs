using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMgr : MonoBehaviour
{
	#region 싱글턴
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
	#endregion // 싱글턴

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
	public void startbutton() // 시작버튼 누르면
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
		Map,   // 필드 이동
		Orbcheck, // orb - peg damage check
		PlayerAttack,
		MonsterAttack,
		Reward, //monster die
		Death, //player die

		MAX
	}

	Coroutine prevCoroutine = null;
	STATE curState = STATE.None;

	private void nextState(STATE newState) // 상태전이 코루틴 전환
	{
		//if (IsDeath) return; // 죽으면 리턴
		if (newState == curState) return; //상태 같아도 리턴

		// 기존 코루틴 종료
		if (prevCoroutine != null)
			StopCoroutine(prevCoroutine);

		// 새로운 상태로 변경
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

		// 만약 안죽었으면
		nextState(STATE.Orbcheck);
		yield return null;
	}

	IEnumerator Death_State()
	{

		yield return null;

	}
	
	
}
