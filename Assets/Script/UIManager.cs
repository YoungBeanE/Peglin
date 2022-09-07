using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    #region 싱글턴
    static UIManager instance = null;
    public static UIManager Inst
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<UIManager>();
                if (instance == null)
                    instance = new GameObject("UIManager").AddComponent<UIManager>();
            }
            return instance;
        }
    }
    #endregion 
    [SerializeField] Slider PlayerHP;
    [SerializeField] Slider PMonHP;
    [SerializeField] Slider SMonHP;

    [SerializeField] GameObject StartUI;



    private void Awake()
	{
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
    public void DestroystartUI()
    {
        StartUI.SetActive(false);
    }
    // Player HP
    void onChangedHP(int curHP, int maxHP)
    {
        PlayerHP.value = (float)curHP / maxHP;
    }

}
