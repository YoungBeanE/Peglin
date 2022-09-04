using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageTextMgr : MonoBehaviour
{
	//----------------------------------------------------------------------
	// �̱��� ��ü�� ������.
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

	Canvas canvas = null;
	private void Awake()
	{
		canvas = FindObjectOfType<Canvas>();
	}







	[SerializeField] Text prefabDamageText = null; // �������ؽ�Ʈ ������


	// ���������ڸ� ����ϵ��� ��û�� ������
	// �������� �����ؼ� 1�� ��ü�� �����մϴ�.
	public void AddText(float damageValue, Vector3 outputPos, Vector3 offsetPos)
	{
		// �Էµ� outputPos�� 3D ���� ���� ��ǥ�̹Ƿ�
		// �� ���� 2D UI ���� ���� ��ǥ�� ��ȯ�� �ʿ��ϴ�.

		// 1. ī�޶� �������� ȭ�� ��ǥ��� ����
		Vector3 screenPos = Camera.main.WorldToScreenPoint(outputPos + offsetPos);

		Text instTxt = Instantiate(prefabDamageText, screenPos, Quaternion.identity, canvas.transform);
		instTxt.text = ((int)damageValue).ToString();
	}
    
}
