using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public delegate void OnAttackEvent();
public delegate void OnAnimEndEvent();
public delegate void OnSkillAttackEvent();

public class AnimationEventReceiver : MonoBehaviour
{
	public OnAttackEvent callbackAttackEvent = null;
	public OnAnimEndEvent callbackAnimEndEvent = null;
	public OnSkillAttackEvent callbackSkillAttackEvent = null;
	//
	// �����̺�Ʈ�� �߻����� �� ȣ��Ǵ� �Լ�
	public void AttackEvent()
	{
		//Debug.Log("## Attack Event");
		if (callbackAttackEvent != null)
			callbackAttackEvent();
	}

	//
	// �ִϸ��̼��� ����� �� ȣ��Ǵ� �̺�Ʈ
	public void AnimEndEvent()
	{
		if (callbackAnimEndEvent != null)
			callbackAnimEndEvent();
	}


	public void SkillAttackEvent()
	{
		//Debug.Log("## Skill Attack Event");
		if (callbackSkillAttackEvent != null)
			callbackSkillAttackEvent();

	}
}
