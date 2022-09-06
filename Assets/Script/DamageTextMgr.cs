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
				// ���̶�Ű(Hiearchy)�� �����ϴ� ���� �ִ��� ���� ã�ƺ���
				instance = FindObjectOfType<DamageTextMgr>();
				// ������ ���� �ٽ� �����
				if(instance == null)
					instance = new GameObject("DamageTextMgr").AddComponent<DamageTextMgr>();
			}
			return instance;
		}
	}
	//----------------------------------------------------------------------

	[SerializeField] ResourceDataObj resourceDataObj = null;
	[SerializeField] DamageText prefabDamageText = null; // �������ؽ�Ʈ ������
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
			Debug.Log("Ȯ��" + damageValue);
			
			
			instTxt.gameObject.SetActive(true);
			instTxt.Damage = damageValue.ToString();
			Debug.Log("����" + damageValue);
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
