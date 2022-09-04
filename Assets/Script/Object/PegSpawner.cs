using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

[System.Serializable]
public class PegInfo
{
	public int MobLevel = 1;
	public int Count = 3;
}

public class PegSpawner : MonoBehaviour
{
	#region Pool
	public int DefaultPoolCapacity = 20;
	public int MaxPoolSize = 50;
	public bool CollectionCheck = true;

	ObjectPool<Peg> pool;
	public IObjectPool<Peg> Pool
	{
		get
		{
			if (pool == null)
				pool = new ObjectPool<Peg>(onCreateFunc, onGet, onRelease, onDestroy, CollectionCheck, DefaultPoolCapacity, MaxPoolSize);
			return pool;
		}
	}

	// ��ü ����
	Peg onCreateFunc()
	{
		Peg inst = Instantiate<Peg>(prefabpeg[Random.Range(0, prefabpeg.Length)], Vector3.zero, Quaternion.identity);
		return inst;
	}

	// T2 ��ü Ȱ��ȭ
	void onGet(Peg peg)
	{
		peg.gameObject.SetActive(true);
	}

	// T2 ��ü ��Ȱ��ȭ
	void onRelease(Peg peg)
	{
		peg.gameObject.SetActive(false);
	}

	// T2 ��ü ����
	void onDestroy(Peg peg)
	{
		Object.Destroy(peg.gameObject);
	}
	#endregion
	
	//����
	[SerializeField] float width = 1f; // X �� �������� ũ��
	[SerializeField] float height = 1f; // Z �� �������� ũ��

	//Peg
	[SerializeField] PegInfo[] PegInfos = null;
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
	public void Start()
	{
		StartCoroutine("setPeg");

	}
	
	
	IEnumerator setPeg()
	{
		for(int i = 0; i < Random.Range(20, 50); i++)
        {
			Peg instpeg = Pool.Get();
			instpeg.transform.position = RandomPos();
			instpeg.transform.rotation = Quaternion.identity;
			instpeg.callbackDestroy = PegDestroy;
		}
		

		yield return null;
	}

	void PegDestroy(Peg peg)
	{
		Pool.Release(peg);

	}
	Vector2 RandomPos()
	{
		Vector2 size = transform.lossyScale;
		size.x *= width;
		size.y *= height;
		// ���(��ġ�̵�, ȸ��, ������)�� �̿��ؼ� ������ġ�� ��Ȯ�� ���� ����Ѵ�.
		Matrix4x4 rMat = Matrix4x4.TRS(transform.position, transform.rotation, size);

		Vector2 randomPos = rMat.MultiplyPoint(new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f)));
		return randomPos;
	}
#if UNITY_EDITOR
	// ����� �׸� �� ȣ��Ǵ� �Լ�
	private void OnDrawGizmos()
	{
		drawCube(Color.yellow);
	}

	// ����� ������ �Ǿ��� �� ȣ��Ǵ� �Լ�
	void OnDrawGizmosSelected()
	{
		drawCube(Color.green);
	}

	//
	// ������ �������� ť�� 1�� �׸���
	void drawCube(Color drawColor)
	{
		Gizmos.color = drawColor;
		Vector3 size = transform.lossyScale;
		size.x *= width;
		size.z *= height;

		// ��ġ�� ȸ���� �������� ����� ����� ���ؼ�
		// Gizmos �� �����ϸ� ���� �׸��� Cube�� ����� ����(��ġ�̵�, ȸ��, ������)�� �޴´�.
		Matrix4x4 rMat = Matrix4x4.TRS(transform.position, transform.rotation, size);
		Gizmos.matrix = rMat;
		Gizmos.DrawCube(Vector3.zero, Vector3.one);
	}
#endif
}
