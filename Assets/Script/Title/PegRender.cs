using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PegRender : MonoBehaviour
{
	[SerializeField] float width = 1f; // X 축 기준으로 크기
	[SerializeField] float height = 1f; // Z 축 기준으로 크기

	//Peg
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

	private void OnEnable()
	{
		for (int i = 0; i < Random.Range(15, 25); i++)
		{
			Peg instpeg = Instantiate(prefabpeg[Random.Range(0, prefabpeg.Length)], RandomPos(), Quaternion.identity, this.transform);
		}
	}
	Vector2 RandomPos() //이부분 동작은 하는데 나중에 고쳐야됨
	{
		Vector2 size = transform.lossyScale;
		size.x *= width;
		size.y *= height;
		// 행렬(위치이동, 회전, 스케일)을 이용해서 랜덤위치의 정확한 값을 계산한다.
		Matrix4x4 rMat = Matrix4x4.TRS(transform.position, transform.rotation, size);

		Vector2 randomPos = rMat.MultiplyPoint(new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f)));
		return randomPos;
	}
}
