using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    #region 싱글턴
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

    public void SetOrbUI(List<Orb> orbs) //orb 리스트 받아와.
    {

    }
    // 플레이어의 HP가 변경될 때만 호출된다.
    void onChangedHP(int curHP, int maxHP)
    {
        sliHP.value = (float)curHP / maxHP;
    }

    // 웨이브 UI 출력하는 코루틴
    public IEnumerator processWaveUI(int waveNum)
	{
        // "WAVE 1" .. 
        txtWave.text = "WAVE " + waveNum;
        // 텍스트를 출력
        txtWave.gameObject.SetActive(true);

        // 2초 동안 보이게
        yield return new WaitForSeconds(2f);
        // 안보이게
		txtWave.gameObject.SetActive(false);
	}
}
