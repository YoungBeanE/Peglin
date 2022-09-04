using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void OnChangedHP(int curHP, int maxHP); // HP관리

public class Player : MonoBehaviour
{
	public OnChangedHP CallbackChangedHP = null;    // HP가 변경되면 호출

	[Header("[이펙트 내용]")]
    [SerializeField] ResourceDataObj ResDataObj = null; // 리소스 데이터
	[SerializeField] Transform transEff = null; // 이펙트가 출력될 위치

	int maxHP = 100;   // 최대 HP
	int curHP = 0;  // 현재 HP
	public bool IsDeath { get { return (curHP <= 0); } } // 플레이어 사망처리
	public int Level { get; set; } = 1; // 레벨
	public int Exp { get; set; } = 0;   // 경험치
	public int AttackPower { get; set; } = 0; // 공격력

	//Transform transCam = null;
	
	Animator myAnimator = null;
    AnimationEventReceiver myEventReceiver = null;

	Orb curorb = null;
    private void Awake()
	{
		//transCam  = FindObjectOfType<Camera>().transform;
        myAnimator = GetComponent<Animator>();
        myEventReceiver = GetComponent<AnimationEventReceiver>();
        //myEventReceiver.callbackAttackEvent = OnAttackEvent;
        //myEventReceiver.callbackAnimEndEvent = OnAnimEndEvent;
    }

	private void Start()
	{
        curHP = maxHP; // 플레이어가 생성되면 HP 를 갱신
		GameDataMgr.Inst.LoadGameData();
		nextState(STATE.IDLE);
	}

	
	public enum STATE
	{
		NONE,   // 아무것도 아닌 상태
		IDLE,
		MOVE,
		ATTACK,
		DAMAGE,
		DEATH,

		MAX
	}


	// 상태전이에 따른 코루틴을 전환하는 함수
	Coroutine prevCoroutine = null;
	STATE curState = STATE.NONE;
	private void nextState(STATE newState)
	{
		if (IsDeath) return; // 죽으면 리턴
		if (newState == curState) return; //상태 같아도 리턴

		// 기존 코루틴 종료
		if (prevCoroutine != null)
			StopCoroutine(prevCoroutine);

		// 새로운 상태로 변경
		curState = newState;
		prevCoroutine = StartCoroutine(newState.ToString() + "_State");
	}

	// 대기 상태
	IEnumerator IDLE_State()
	{
		myAnimator.SetBool("Move", false);

		//Orb생성요청
		OrbPool.Inst.SetOrb();
		yield return null;
		
	}

	// 이동 상태
	IEnumerator MOVE_State()
	{
		// 이동 애니메이션 출력
		myAnimator.SetBool("Move", true);

		transform.Translate(Vector3.forward * 5f * Time.deltaTime, Space.Self); // 플레이어 중앙이동 느리게

		
		nextState(STATE.IDLE); //중앙이동하면 다시 대기
		yield return null;

	}

	// 공격 상태
	IEnumerator ATTACK_State()
	{
		myAnimator.SetTrigger("Attack"); //공격 애니메이션 출력
		//계산된 어택파워로 몬스터 공격

		yield return new WaitForSeconds(2f);

	}


	// 피격 상태
	IEnumerator DAMAGE_State()
	{
		
		// 피격 이펙트 출력
		GameObject instObj = Instantiate(ResDataObj.EffHit, transform.position, Quaternion.identity);
		Destroy(instObj, 2f); // 2초 뒤에 삭제된다.

		// 피격 애니메이션 출력
		myAnimator.SetTrigger("Damage");

		// 만약 안죽었으면
		nextState(STATE.IDLE);
		yield return null;
	}

	//
	// 죽음 상태를 처리하는 코루틴
	IEnumerator DEATH_State()
	{

		// 죽었다면 죽는애니메이션 호출
		myAnimator.SetTrigger("Die");
		yield return null;

	}
	

    // 몬스터를 죽이면 보상을 받는다.
    void Reward(int rewardValue)
	{
        Exp += rewardValue; // 경험치 획득
        // 획득된 경험치를 바탕으로 레벨이 올라갔다면 
        // 다른 능력치도 반영해 준다.
        PlayerData playerData = GameDataMgr.Inst.FindPlayerDataByExp(Exp);
        if(playerData.Level != Level)
		{
			//AttackPower = playerData.AttackPower;
            maxHP = playerData.MaxHP;
            curHP = maxHP;
            Level = playerData.Level;
        }
	}

	void TransferDamge(int AttackPower)
	{
		//Debug.Log("## 공격 받았다 : " + damageValue);
		if (IsDeath) return;

		// 데미지 영향으로 나의 HP 가 변경되었다.
		curHP -= AttackPower;
		// 변경된 HP 정보가 필요한 곳을 위해 delegate 함수 호출
		if (CallbackChangedHP != null)
			CallbackChangedHP(curHP, maxHP);

		// 데미지 텍스트 출력
		DamageTextMgr.Inst.AddText(AttackPower, transform.position, Vector3.up * 1.5f);

		if (curHP <= 0)
		{
			curHP = 0;
			nextState(STATE.DEATH);
		}
		// 피격
		else
		{
			nextState(STATE.DAMAGE);
		}

	}
	

    // 공격이벤트가 발생하는 시점에 호출
    void OnAttackEvent()
	{
		//mobs[i].SendMessage("TransferDamge", AttackPower, SendMessageOptions.DontRequireReceiver);
	}

	// 공격애니메이션 종료될 때 호출되는 함수
	void OnAnimEndEvent()
	{
        //isAttackProcess = false;
    }



}
