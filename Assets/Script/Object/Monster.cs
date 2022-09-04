using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void OnChangedMonHP(int curHP, int maxHP);
public class Monster : MonoBehaviour
{
	[Header("[몬스터 기본속성들]")]
	[SerializeField] int maxHP = 100;   // 내가 가질 수 있는 최대 HP
	[SerializeField] int attackPower; // 공격력
	[Header("[이펙트 내용]")]
	[SerializeField] ResourceDataObj ResDataObj = null; // 리소스 데이터
	[SerializeField] Transform transEff = null; // 이펙트가 출력될 위치

	int curHP = 0;  // 현재 HP
	bool IsDeath { get { return (curHP <= 0); } } // 몬스터 사망처리
	bool isAttackProcess = false; // 공격중
	public OnChangedHP CallbackChangedHP = null;    // HP가 변경되면 호출
	public int level;
	
	Animator myAnimator = null;
	AnimationEventReceiver myEventReceiver = null;

	private void Awake()
	{
		myAnimator = GetComponent<Animator>();
		myEventReceiver = GetComponent<AnimationEventReceiver>();
		myEventReceiver.callbackAttackEvent = OnAttackEvent;
		myEventReceiver.callbackAnimEndEvent = OnAnimEndEvent;
	}

	private void Start()
	{
		curHP = maxHP; // 생성되면 HP 를 갱신
		nextState(STATE.IDLE);
	}

	public void SetData(MonsterData mobData)
	{
		gameObject.name = mobData.Name;
		attackPower = mobData.AttackPower;
		maxHP = mobData.MaxHP;
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
	void nextState(STATE newState)
	{
		if (IsDeath) return; // 죽으면 리턴

		if (newState == curState)
			return;
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

		//구슬활성화시키고, 데미지 계산 다 끝나면 어택

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
			// 보상을 준다.
			//Player.SendMessage("Reward", 10, SendMessageOptions.DontRequireReceiver);


			curHP = 0;
			nextState(STATE.DEATH);
		}
		// 피격
		else
		{
			nextState(STATE.DAMAGE);
		}

	}


	// 공격애니메이션 중에 공격이벤트가 발생하는 시점에 호출
	void OnAttackEvent()
	{
		
		//AttackPower = attackPower;

	}

	// 공격애니메이션 종료될 때 호출되는 함수
	void OnAnimEndEvent()
	{
		isAttackProcess = false;
	}
}
