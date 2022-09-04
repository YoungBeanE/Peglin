using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void OnChangedHP(int curHP, int maxHP); // HP����

public class Player : MonoBehaviour
{
	public OnChangedHP CallbackChangedHP = null;    // HP�� ����Ǹ� ȣ��

	[Header("[����Ʈ ����]")]
    [SerializeField] ResourceDataObj ResDataObj = null; // ���ҽ� ������
	[SerializeField] Transform transEff = null; // ����Ʈ�� ��µ� ��ġ

	int maxHP = 100;   // �ִ� HP
	int curHP = 0;  // ���� HP
	public bool IsDeath { get { return (curHP <= 0); } } // �÷��̾� ���ó��
	public int Level { get; set; } = 1; // ����
	public int Exp { get; set; } = 0;   // ����ġ
	public int AttackPower { get; set; } = 0; // ���ݷ�

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
        curHP = maxHP; // �÷��̾ �����Ǹ� HP �� ����
		GameDataMgr.Inst.LoadGameData();
		nextState(STATE.IDLE);
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
	private void nextState(STATE newState)
	{
		if (IsDeath) return; // ������ ����
		if (newState == curState) return; //���� ���Ƶ� ����

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

		//Orb������û
		OrbPool.Inst.SetOrb();
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
	

    // ���͸� ���̸� ������ �޴´�.
    void Reward(int rewardValue)
	{
        Exp += rewardValue; // ����ġ ȹ��
        // ȹ��� ����ġ�� �������� ������ �ö󰬴ٸ� 
        // �ٸ� �ɷ�ġ�� �ݿ��� �ش�.
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
			curHP = 0;
			nextState(STATE.DEATH);
		}
		// �ǰ�
		else
		{
			nextState(STATE.DAMAGE);
		}

	}
	

    // �����̺�Ʈ�� �߻��ϴ� ������ ȣ��
    void OnAttackEvent()
	{
		//mobs[i].SendMessage("TransferDamge", AttackPower, SendMessageOptions.DontRequireReceiver);
	}

	// ���ݾִϸ��̼� ����� �� ȣ��Ǵ� �Լ�
	void OnAnimEndEvent()
	{
        //isAttackProcess = false;
    }



}
