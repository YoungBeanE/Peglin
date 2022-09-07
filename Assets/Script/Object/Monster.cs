using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void OnChangedMonHP(int curHP, int maxHP);
public class Monster : MonoBehaviour
{
	public OnChangedMonHP CallbackChangedHP = null;    // HP�� ����Ǹ� ȣ��

	[SerializeField] ResourceDataObj ResDataObj = null; // ���ҽ� ������

	int level;
	int maxHP;  // �ִ� HP
	int curHP;  // ���� HP
	int attackPower; // ���ݷ�
	int damnum = 0;
	public bool IsDeath { get { return (curHP <= 0); } } // ���� ���ó��
	
	Animator myAnimator = null;
	
	private void Awake()
	{
		ResDataObj = Resources.Load<ResourceDataObj>("MyResourceDataObj");
		myAnimator = GetComponent<Animator>();
	}

	private void Start()
	{
		level = 1;
		maxHP = 100;
		curHP = 100;
		attackPower = 10;
		
		myAnimator.SetBool("Move", false);
	}
	public void MonsterGo()
    {
		StartCoroutine("MOVE");
    }

	IEnumerator MOVE()
	{
		myAnimator.SetBool("Move", true);
		int move = 0;
		while (true)
        {
			move++;

			transform.Translate(Vector3.left * 5f * Time.deltaTime, Space.Self); // ���� �̵�
			yield return new WaitForSeconds(0.5f);
			if(move > 10)
            {
				myAnimator.SetTrigger("Attack");
				DamageTextMgr.Inst.AttackText(attackPower, transform.position, Vector3.left);
				GameMgr.Inst.Playerdamage(attackPower);
				yield break;
			}
		}
	}

	public void Damage(int AttackPower)
	{
		if (IsDeath) return;
		damnum++;
		myAnimator.SetTrigger("Damage");
		curHP -= AttackPower;
		if (CallbackChangedHP != null)
			CallbackChangedHP(curHP, maxHP);
		if (curHP <= 0)
		{
			curHP = 0;
			myAnimator.SetTrigger("Die");
			GameMgr.Inst.MonsterDie();
		}
		if(damnum >= 2)
        {
			GameMgr.Inst.Monsterattack();
			damnum = 0;
		}
		
	}


	
}
