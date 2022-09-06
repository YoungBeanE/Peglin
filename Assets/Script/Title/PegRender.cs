using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PegRender : MonoBehaviour
{
	[SerializeField] float width = 1f; // X �� �������� ũ��
	[SerializeField] float height = 1f; // Z �� �������� ũ��

	//Peg
	[SerializeField] ResourceDataObj ResDataObj = null; // ���ҽ� ������
	[SerializeField] Peg[] prefabpeg = null;

	private void Awake()
	{
		ResDataObj = Resources.Load<ResourceDataObj>("MyResourceDataObj");
		prefabpeg = new Peg[ResDataObj.peg.Length];
		//���ҽ������Ϳ��� peg ��������
		for (int i = 0; i < prefabpeg.Length; i++)
		{
			prefabpeg[i] = ResDataObj.peg[i]; // origin peg, critical peg, reflesh peg, bomb peg
		}
	}

	private void OnEnable()
	{
		for (int i = 0; i < Random.Range(15, 25); i++)
		{
			Peg instpeg = Instantiate(prefabpeg[Random.Range(0, prefabpeg.Length)], RandomPos(), Quaternion.identity, this.transform);
		}
	}
	Vector2 RandomPos() //�̺κ� ������ �ϴµ� ���߿� ���ľߵ�
	{
		Vector2 size = transform.lossyScale;
		size.x *= width;
		size.y *= height;
		// ���(��ġ�̵�, ȸ��, ������)�� �̿��ؼ� ������ġ�� ��Ȯ�� ���� ����Ѵ�.
		Matrix4x4 rMat = Matrix4x4.TRS(transform.position, transform.rotation, size);

		Vector2 randomPos = rMat.MultiplyPoint(new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f)));
		return randomPos;
	}
}
