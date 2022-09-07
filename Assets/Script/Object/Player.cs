using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void OnChangedHP(int curHP, int maxHP); // HP����

public class Player : MonoBehaviour
{
	public OnChangedHP CallbackChangedHP = null;    // HP�� ����Ǹ� ȣ��

	
    [SerializeField] ResourceDataObj ResDataObj = null; // ���ҽ� ������
	[SerializeField] Transform transEff = null; // ����Ʈ�� ��µ� ��ġ

	Animator myAnimator = null;
	Orb bomborb = null;

	int maxHP = 100;   // �ִ� HP
	int curHP = 0;  // ���� HP
	int bombnum = 0;
	public bool IsDeath { get { return (curHP <= 0); } } // �÷��̾� ���ó��
	public int Level { get; set; } = 1; // ����
	public int Exp { get; set; } = 0;   // ����ġ
	public int AttackPower { get; set; } // ���ݷ�

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

	//�̵�
	IEnumerator MOVE()
	{
		myAnimator.SetBool("Move", true);
		transform.Translate(Vector3.right * 5f * Time.deltaTime); // �÷��̾� �߾��̵�  Space.Self

		yield return new WaitUntil(() => transform.position.x < 5);

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
