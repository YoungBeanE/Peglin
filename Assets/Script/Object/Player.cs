using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void OnChangedHP(int curHP, int maxHP); // HP관리

public class Player : MonoBehaviour
{
	public OnChangedHP CallbackChangedHP = null;    // HP가 변경되면 호출

	
    [SerializeField] ResourceDataObj ResDataObj = null; // 리소스 데이터
	[SerializeField] Transform transEff = null; // 이펙트가 출력될 위치

	Animator myAnimator = null;
	Orb bomborb = null;

	int maxHP = 100;   // 최대 HP
	int curHP = 0;  // 현재 HP
	int bombnum = 0;
	public bool IsDeath { get { return (curHP <= 0); } } // 플레이어 사망처리
	public int Level { get; set; } = 1; // 레벨
	public int Exp { get; set; } = 0;   // 경험치
	public int AttackPower { get; set; } // 공격력

	/*
	 * 
	*/
	private void Awake()
	{
		ResDataObj = Resources.Load<ResourceDataObj>("MyResourceDataObj");
		bomborb = ResDataObj.orb[3];

		myAnimator = GetComponent<Animator>();
    }

	private void Start()
	{
        curHP = maxHP; // HP
		
	}

	void OrbCheck()
    {
		myAnimator.SetBool("Move", false);
	}
	void PlayerAttack()
	{
		myAnimator.SetTrigger("Attack");
		//mobs[i].SendMessage("TransferDamge", AttackPower, SendMessageOptions.DontRequireReceiver);
		
	}
	void Reward()
	{

	}
	void Death()
	{

	}

	//이동
	IEnumerator MOVE()
	{
		myAnimator.SetBool("Move", true);
		transform.Translate(Vector3.right * 5f * Time.deltaTime); // 플레이어 중앙이동  Space.Self

		yield return new WaitUntil(() => transform.position.x < 5);

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
		DamageTextMgr.Inst.AttackText(AttackPower, transform.position, Vector3.up * 1.5f);

		if (curHP <= 0)
		{
			curHP = 0;
			
		}
		

	}
	
	public void ReadyAttack(int attackPower)
    {
		AttackPower = attackPower;
		PlayerAttack();


	}
	public void GetBomb()
    {
		bombnum++;
		Debug.Log(bombnum);
	}



}
