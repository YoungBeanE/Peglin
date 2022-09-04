using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class OrbPool : MonoBehaviour
{
	#region �̱���
	static OrbPool instance = null;
	private OrbPool() { }
	public static OrbPool Inst
	{
		get
		{
			if (instance == null)
			{
				// ���̷�Ű�� �����ϴ� ���ӿ�����Ʈ��� ���� ã�ƺ���.
				instance = FindObjectOfType<OrbPool>();
				if (instance == null)
					instance = new GameObject("OrbPool").AddComponent<OrbPool>();
			}
			return instance;
		}
	}
	#endregion // Singleton

	[SerializeField] ResourceDataObj ResDataObj = null; // ���ҽ� ������
	[SerializeField] Orb[] prefaborb = null;
	[SerializeField] List<Orb> Orbpool = new List<Orb>();

	Vector2 Orbpos = new Vector2(0.5f, 0.88f); // �߻���ġ
	int shootOrb = 0; // �߻����
	private void Awake()
	{
		ResDataObj = Resources.Load<ResourceDataObj>("MyResourceDataObj");
		prefaborb = new Orb[ResDataObj.orb.Length];
		//���ҽ������Ϳ��� orb ��������
		for(int i = 0; i < prefaborb.Length; i++)
        {
			prefaborb[i] = ResDataObj.orb[i]; // orb, critical orb, heal orb, bomb orb
		}
	
	}
    private void Start() // �ϴ� orb ����.�÷��̾ ���� orb ����ŭ.
    {
		for(int i = 0; i < ResDataObj.PlayerOrb; i++)
        {
			Orb instOrb = null;
			int R = Random.Range(0, 2); // �Ϲ�, ũ�� �߿� �������� ����.
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
			UIManager.Inst.SetOrbUI(ShuffleList(Orbpool)); // List �ٽü����ؼ� UI�� �Ѱ�
			shootOrb = 0;
		}
		yield break;
	}

	public void SetOrb() //�÷��̾� ��� ���¿� ���ÿ�û
	{
		StartCoroutine(ShootOrb());
	}

	List<Orb> ShuffleList(List<Orb> orbpool) // ����Ʈ ���� ����
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

	public void DestroyOrb(Orb orb) //orb�� �������� ���ݷ� �Ѱ��� ������ ��Ȱ��ȭ ȣ��
	{
		orb.gameObject.SetActive(false);
	}

}
