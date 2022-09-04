using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void OnChangedMonHP(int curHP, int maxHP);
public class Monster : MonoBehaviour
{
	[Header("[���� �⺻�Ӽ���]")]
	[SerializeField] int maxHP = 100;   // ���� ���� �� �ִ� �ִ� HP
	[SerializeField] int attackPower; // ���ݷ�
	[Header("[����Ʈ ����]")]
	[SerializeField] ResourceDataObj ResDataObj = null; // ���ҽ� ������
	[SerializeField] Transform transEff = null; // ����Ʈ�� ��µ� ��ġ

	int curHP = 0;  // ���� HP
	bool IsDeath { get { return (curHP <= 0); } } // ���� ���ó��
	bool isAttackProcess = false; // ������
	public OnChangedHP CallbackChangedHP = null;    // HP�� ����Ǹ� ȣ��
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
		curHP = maxHP; // �����Ǹ� HP �� ����
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
		NONE,   // �ƹ��͵� �ƴ� ����
		IDLE,
		MOVE,
		ATTACK,
		DAMAGE,
		DEATH,

		MAX
	}


	// �������̿� ���� �ڷ�ƾ�� ��ȯ�ϴ� �Լ�
	Coroutine prevCoroutine = null;
	STATE curState = STATE.NONE;
	void nextState(STATE newState)
	{
		if (IsDeath) return; // ������ ����

		if (newState == curState)
			return;
		// ���� �ڷ�ƾ ����
		if (prevCoroutine != null)
			StopCoroutine(prevCoroutine);

		// ���ο� ���·� ����
		curState = newState;
		prevCoroutine = StartCoroutine(newState.ToString() + "_State");
	}

	// ��� ����
	IEnumerator IDLE_State()
	{
		myAnimator.SetBool("Move", false);

		//����Ȱ��ȭ��Ű��, ������ ��� �� ������ ����

		yield return null;

	}

	// �̵� ����
	IEnumerator MOVE_State()
	{
		// �̵� �ִϸ��̼� ���
		myAnimator.SetBool("Move", true);

		transform.Translate(Vector3.forward * 5f * Time.deltaTime, Space.Self); // �÷��̾� �߾��̵� ������


		nextState(STATE.IDLE); //�߾��̵��ϸ� �ٽ� ���
		yield return null;

	}

	// ���� ����
	IEnumerator ATTACK_State()
	{
		myAnimator.SetTrigger("Attack"); //���� �ִϸ��̼� ���
										 //���� �����Ŀ��� ���� ����

		yield return new WaitForSeconds(2f);

	}


	// �ǰ� ����
	IEnumerator DAMAGE_State()
	{

		// �ǰ� ����Ʈ ���
		GameObject instObj = Instantiate(ResDataObj.EffHit, transform.position, Quaternion.identity);
		Destroy(instObj, 2f); // 2�� �ڿ� �����ȴ�.

		// �ǰ� �ִϸ��̼� ���
		myAnimator.SetTrigger("Damage");

		// ���� ���׾�����
		nextState(STATE.IDLE);
		yield return null;
	}

	//
	// ���� ���¸� ó���ϴ� �ڷ�ƾ
	IEnumerator DEATH_State()
	{

		// �׾��ٸ� �״¾ִϸ��̼� ȣ��
		myAnimator.SetTrigger("Die");
		yield return null;

	}


	void TransferDamge(int AttackPower)
	{
		//Debug.Log("## ���� �޾Ҵ� : " + damageValue);
		if (IsDeath) return;

		// ������ �������� ���� HP �� ����Ǿ���.
		curHP -= AttackPower;
		// ����� HP ������ �ʿ��� ���� ���� delegate �Լ� ȣ��
		if (CallbackChangedHP != null)
			CallbackChangedHP(curHP, maxHP);

		// ������ �ؽ�Ʈ ���
		DamageTextMgr.Inst.AddText(AttackPower, transform.position, Vector3.up * 1.5f);

		if (curHP <= 0)
		{
			// ������ �ش�.
			//Player.SendMessage("Reward", 10, SendMessageOptions.DontRequireReceiver);


			curHP = 0;
			nextState(STATE.DEATH);
		}
		// �ǰ�
		else
		{
			nextState(STATE.DAMAGE);
		}

	}


	// ���ݾִϸ��̼� �߿� �����̺�Ʈ�� �߻��ϴ� ������ ȣ��
	void OnAttackEvent()
	{
		
		//AttackPower = attackPower;

	}

	// ���ݾִϸ��̼� ����� �� ȣ��Ǵ� �Լ�
	void OnAnimEndEvent()
	{
		isAttackProcess = false;
	}
}
