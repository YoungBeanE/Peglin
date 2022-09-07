using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageTextMgr : MonoBehaviour
{
	static DamageTextMgr instance;
	public static DamageTextMgr Inst
	{
		get
		{
			if(instance == null)
			{
				// Hiearchy check
				instance = FindObjectOfType<DamageTextMgr>();
				// instance
				if(instance == null)
					instance = new GameObject("DamageTextMgr").AddComponent<DamageTextMgr>();
			}
			return instance;
		}
	}
	//----------------------------------------------------------------------

	[SerializeField] ResourceDataObj resourceDataObj = null;
	[SerializeField] DamageText prefabDamageText = null; // DamageText prefab
	[SerializeField] AttackText prefabAttackText = null; // AttackText prefab


	Queue<DamageText> damagetexts = new Queue<DamageText>();
	Queue<AttackText> attacktexts = new Queue<AttackText>();

	Canvas canvas = null;
	private void Awake()
	{
		resourceDataObj = Resources.Load<ResourceDataObj>("MyResourceDataObj");
		prefabDamageText = resourceDataObj.damagetext;
		prefabAttackText = resourceDataObj.attacktext;
		canvas = FindObjectOfType<Canvas>();
	}

	
	public void DamageText(int damageValue, Vector2 outputPos, Vector2 offsetPos) //peg damage text  - call Orb
	{
		DamageText instTxt;
		Vector3 textPos = Camera.main.WorldToScreenPoint(outputPos + offsetPos);
		if (damagetexts.Count == 0)
        {
			instTxt = Instantiate(prefabDamageText, textPos, Quaternion.identity, canvas.transform);
			instTxt.Damage = damageValue.ToString();
			instTxt.gameObject.SetActive(true);
		}
        else
        {
			instTxt = damagetexts.Dequeue();
			instTxt.transform.position = textPos;
			instTxt.Damage = damageValue.ToString();
			instTxt.gameObject.SetActive(true);
		}
	}

	public void AttackText(int attackValue, Vector2 outputPos, Vector2 offsetPos) //player, Monster attack text  - call Player, Monster
	{
		AttackText instTxt;
		Vector3 textPos = Camera.main.WorldToScreenPoint(outputPos + offsetPos);
		if (attacktexts.Count == 0)
		{
			instTxt = Instantiate(prefabAttackText, textPos, Quaternion.identity, canvas.transform);
			instTxt.Attack = attackValue.ToString();
			instTxt.gameObject.SetActive(true);
		}
		else
		{
			instTxt = attacktexts.Dequeue();
			instTxt.transform.position = textPos;
			instTxt.Attack = attackValue.ToString();
			instTxt.gameObject.SetActive(true);
		}
	}

	public void DestroyText(DamageText text) //peg damage text
    {
		text.gameObject.SetActive(false);
		damagetexts.Enqueue(text);

    }

	public void DestroyAttackText(AttackText text) //Player, Monster attack text
	{
		text.gameObject.SetActive(false);
		attacktexts.Enqueue(text);

	}
}
