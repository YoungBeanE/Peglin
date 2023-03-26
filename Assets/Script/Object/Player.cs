using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void OnChangedHP(int curHP, int maxHP); // HP����

public class Player : MonoBehaviour
{
	public OnChangedHP CallbackChangedHP = null;    // HP�� ����Ǹ� ȣ��

    [SerializeField] ResourceDataObj ResDataObj = null; // ���ҽ� ������
	[SerializeField] GameObject bomborb = null;
	Animator myAnimator = null;
	

	int maxHP = 100;   // �ִ� HP
	int curHP = 0;  // ���� HP
	int bombnum = 0;
	public bool IsDeath { get { return (curHP <= 0); } } // �÷��̾� ���ó��
	public int AttackPower { get; set; } // ���ݷ�

	private void Awake()
	{
		ResDataObj = Resources.Load<ResourceDataObj>("MyResourceDataObj");
		bomborb = ResDataObj.Bomb;
		myAnimator = GetComponent<Animator>();
    }

	private void Start()
	{
        curHP = maxHP; // HP
		myAnimator.SetBool("Move", false);
	}
	IEnumerator Move()
	{
		myAnimator.SetBool("Move", true);
        while (transform.position.x < 0f)
        {
			transform.Translate(Vector3.right * 5f * Time.deltaTime);
			yield return null;
		}
		GameMgr.Inst.Direction(1);
	}
	public void Mapmove()
    {
		StartCoroutine(nameof(Move));
    }

	public void Damage(int AttackPower)
	{
		if (IsDeath) return;
		myAnimator.SetTrigger("Damage");
	
		curHP -= AttackPower;
		if (CallbackChangedHP != null)
			CallbackChangedHP(curHP, maxHP);

		if (curHP <= 0)
		{
			curHP = 0;
			myAnimator.SetTrigger("Die");
			UIManager.Inst.GameOver(false);
		}
	}

	public void Attack(int attackPower)
	{
		if (bombnum == 0)
		{
			AttackGo(attackPower);
		}
        else
        {
			myAnimator.SetTrigger("Attack");
			for (int i = 0; i < bombnum; i++)
			{
				Instantiate(bomborb, transform.position, Quaternion.identity);
			}
			bombnum = 0;
			AttackGo(attackPower);
		}
		
	
	}
	public void AttackGo(int attackPower)
    {
		myAnimator.SetTrigger("Attack");
		AttackPower = attackPower;
		DamageTextMgr.Inst.AttackText(AttackPower, transform.position, Vector3.right * 0.1f);
		GameMgr.Inst.Monsterdamage(AttackPower);
	}

	public void GetBomb()
    {
		bombnum++;
	}
	

}
