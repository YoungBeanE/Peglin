using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class OrbPool : MonoBehaviour
{
	#region 싱글톤
	static OrbPool instance = null;
	private OrbPool() { }
	public static OrbPool Inst
	{
		get
		{
			if (instance == null)
			{
				// 하이러키에 존재하는 게임오브젝트라면 먼저 찾아본다.
				instance = FindObjectOfType<OrbPool>();
				if (instance == null)
					instance = new GameObject("OrbPool").AddComponent<OrbPool>();
			}
			return instance;
		}
	}
	#endregion // Singleton

	[SerializeField] ResourceDataObj ResDataObj = null; // 리소스 데이터
	[SerializeField] Orb[] prefaborb = null;
	[SerializeField] List<Orb> Orbpool = new List<Orb>();

	Vector2 Orbpos = new Vector2(0.5f, 0.88f); // 발사위치
	int shootOrb = 0; // 발사순서
	private void Awake()
	{
		ResDataObj = Resources.Load<ResourceDataObj>("MyResourceDataObj");
		prefaborb = new Orb[ResDataObj.orb.Length];
		//리소스데이터에서 orb 가져오기
		for(int i = 0; i < prefaborb.Length; i++)
        {
			prefaborb[i] = ResDataObj.orb[i]; // orb, critical orb, heal orb, bomb orb
		}
	
	}
    private void Start() // 일단 orb 생성.플레이어가 가진 orb 수만큼.
    {
		for(int i = 0; i < ResDataObj.PlayerOrb; i++)
        {
			Orb instOrb = null;
			int R = Random.Range(0, 2); // 일반, 크리 중에 랜덤으로 생성.
			instOrb = Instantiate<Orb>(prefaborb[R], Orbpos, Quaternion.identity, this.transform);
			instOrb.SetData(GameDataMgr.Inst.FindOrbDataBy(R + 1));
			instOrb.gameObject.SetActive(false);
			Orbpool.Add(instOrb);
		}
		UIManager.Inst.SetOrbUI(Orbpool);
	}
    IEnumerator ShootOrb()
	{
		Orbpool[shootOrb].gameObject.SetActive(true);
		shootOrb++;
		if(shootOrb >= ResDataObj.PlayerOrb)
        {
			UIManager.Inst.SetOrbUI(ShuffleList(Orbpool)); // List 다시세팅해서 UI에 넘겨
			shootOrb = 0;
		}
		yield break;
	}

	public void SetOrb() //플레이어 대기 상태에 세팅요청
	{
		StartCoroutine(ShootOrb());
	}

	List<Orb> ShuffleList(List<Orb> orbpool) // 리스트 랜덤 섞기
	{
		for(int i = orbpool.Count -1; i > 0; i--)
        {
			int R = Random.Range(0, i);

			Orb orb = orbpool[i];
			orbpool[i] = orbpool[R];
			orbpool[R] = orb;
		}
		return orbpool;
	}

	public void DestroyOrb(Orb orb) //orb가 떨어지고 공격력 넘겨준 다음에 비활성화 호출
	{
		orb.gameObject.SetActive(false);
	}

}
