using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    #region �̱���
    static UIManager inst = null;
    public static UIManager Inst
    {
        get
        {
            if (inst == null)
            {
                inst = new UIManager();
            }
            return inst;
        }
    }
    #endregion 
    [SerializeField] Slider sliHP;

    [SerializeField] Text txtWave;

	private void Awake()
	{
        txtWave.gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        Player myPlayer = FindObjectOfType<Player>();
        myPlayer.CallbackChangedHP = onChangedHP;

    }

    public void SetOrbUI(List<Orb> orbs) //orb ����Ʈ �޾ƿ�.
    {

    }
    // �÷��̾��� HP�� ����� ���� ȣ��ȴ�.
    void onChangedHP(int curHP, int maxHP)
    {
        sliHP.value = (float)curHP / maxHP;
    }

    // ���̺� UI ����ϴ� �ڷ�ƾ
    public IEnumerator processWaveUI(int waveNum)
	{
        // "WAVE 1" .. 
        txtWave.text = "WAVE " + waveNum;
        // �ؽ�Ʈ�� ���
        txtWave.gameObject.SetActive(true);

        // 2�� ���� ���̰�
        yield return new WaitForSeconds(2f);
        // �Ⱥ��̰�
		txtWave.gameObject.SetActive(false);
	}
}
