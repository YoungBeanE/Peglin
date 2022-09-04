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

	// 객체 생성
	Peg onCreateFunc()
	{
		Peg inst = Instantiate<Peg>(prefabpeg[Random.Range(0, prefabpeg.Length)], Vector3.zero, Quaternion.identity);
		return inst;
	}

	// T2 객체 활성화
	void onGet(Peg peg)
	{
		peg.gameObject.SetActive(true);
	}

	// T2 객체 비활성화
	void onRelease(Peg peg)
	{
		peg.gameObject.SetActive(false);
	}

	// T2 객체 삭제
	void onDestroy(Peg peg)
	{
		Object.Destroy(peg.gameObject);
	}
	#endregion
	
	//영역
	[SerializeField] float width = 1f; // X 축 기준으로 크기
	[SerializeField] float height = 1f; // Z 축 기준으로 크기

	//Peg
	[SerializeField] PegInfo[] PegInfos = null;
	[SerializeField] ResourceDataObj ResDataObj = null; // 리소스 데이터
	[SerializeField] Peg[] prefabpeg = null;
	
	private void Awake()
	{
		ResDataObj = Resources.Load<ResourceDataObj>("MyResourceDataObj");
		prefabpeg = new Peg[ResDataObj.peg.Length];
		//리소스데이터에서 peg 가져오기
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
		// 행렬(위치이동, 회전, 스케일)을 이용해서 랜덤위치의 정확한 값을 계산한다.
		Matrix4x4 rMat = Matrix4x4.TRS(transform.position, transform.rotation, size);

		Vector2 randomPos = rMat.MultiplyPoint(new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f)));
		return randomPos;
	}
#if UNITY_EDITOR
	// 기즈모를 그릴 때 호출되는 함수
	private void OnDrawGizmos()
	{
		drawCube(Color.yellow);
	}

	// 기즈모가 선택이 되었을 때 호출되는 함수
	void OnDrawGizmosSelected()
	{
		drawCube(Color.green);
	}

	//
	// 지정된 색상으로 큐브 1개 그리기
	void drawCube(Color drawColor)
	{
		Gizmos.color = drawColor;
		Vector3 size = transform.lossyScale;
		size.x *= width;
		size.z *= height;

		// 위치와 회전과 스케일이 적용된 행렬을 구해서
		// Gizmos 에 적용하면 이후 그리는 Cube는 행렬의 영향(위치이동, 회전, 스케일)을 받는다.
		Matrix4x4 rMat = Matrix4x4.TRS(transform.position, transform.rotation, size);
		Gizmos.matrix = rMat;
		Gizmos.DrawCube(Vector3.zero, Vector3.one);
	}
#endif
}
