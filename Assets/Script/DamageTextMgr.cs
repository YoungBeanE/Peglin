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
				// 하이라키(Hiearchy)에 존재하는 것이 있는지 먼저 찾아보고
				instance = FindObjectOfType<DamageTextMgr>();
				// 없으면 새로 다시 만들고
				if(instance == null)
					instance = new GameObject("DamageTextMgr").AddComponent<DamageTextMgr>();
			}
			return instance;
		}
	}
	//----------------------------------------------------------------------

	[SerializeField] ResourceDataObj resourceDataObj = null;
	[SerializeField] DamageText prefabDamageText = null; // 데미지텍스트 프리팹
	Queue<DamageText> texts = new Queue<DamageText>();
	Canvas canvas = null;
	private void Awake()
	{
		resourceDataObj = Resources.Load<ResourceDataObj>("MyResourceDataObj");
		prefabDamageText = resourceDataObj.damagetext;
		canvas = FindObjectOfType<Canvas>();
	}

	
	public void DamageText(int damageValue, Vector2 outputPos, Vector2 offsetPos)
	{
		DamageText instTxt;
		Vector3 textPos = Camera.main.WorldToScreenPoint(outputPos + offsetPos);
		if (texts.Count == 0)
        {
			instTxt = Instantiate(prefabDamageText, textPos, Quaternion.identity, canvas.transform);
			Debug.Log("확인" + damageValue);
			
			
			instTxt.gameObject.SetActive(true);
			instTxt.Damage = damageValue.ToString();
			Debug.Log("과연" + damageValue);
		}
        else
        {
			instTxt = texts.Dequeue();
			instTxt.transform.position = textPos;
			instTxt.Damage = damageValue.ToString();
			instTxt.gameObject.SetActive(true);
		}
	}

    public void DestroyText(DamageText text)
    {
		text.gameObject.SetActive(false);
		texts.Enqueue(text);

    }
}
