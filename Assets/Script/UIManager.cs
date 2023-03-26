using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

    [SerializeField] TextMeshProUGUI gameOver;

    [SerializeField] GameObject StartUI;
    [SerializeField] GameObject MainUI;


    // Start is called before the first frame update
    void Start()
    {
        Player myPlayer = FindObjectOfType<Player>();
        myPlayer.CallbackChangedHP = onChangedHP;
        Monster myMonster = FindObjectOfType<Monster>();
        myMonster.CallbackChangedHP = onChangedMonHP;
        gameOver.gameObject.SetActive(false);
    }

    public void SetOrbUI(List<Orb> orbs) //orb 리스트 받아와.
    {

    }
    public void DestroystartUI()
    {
        StartUI.SetActive(false);
    }
    public void SetmainUI()
    {
        MainUI.SetActive(true);
    }

    public void DestroymainUI()
    {
        MainUI.SetActive(false);
    }
    // Player HP
    void onChangedHP(int curHP, int maxHP)
    {
        PlayerHP.value = (float)curHP / maxHP;
    }

    //Monster HP
    void onChangedMonHP(int curHP, int maxHP)
    {
        PMonHP.value = (float)curHP / maxHP;
    }

    public void GameOver(bool win)
    {
        gameOver.gameObject.SetActive(true);

        if (win == false) gameOver.text = "You Lose";
        else gameOver.text = "You Win";
    }
}
