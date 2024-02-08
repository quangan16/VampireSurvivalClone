using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    public TMP_Text timeDisplayTxt;
    public Image rangeWeaponCooldownDisplay;
    public Image meleeWeaponCooldownDisplay;
    public Slider expBarDisplay;
    public GameObject defeatUI;
    public Button playAgain;
    public TMP_Text surviveTimeTxt;
    public TMP_Text bestTimeTxt;
    public Button backToMenu;
    public TMP_Text enemyKilledTxt;
   

    public void Start()
    {
        Init();
    }

    public void Init()
    {
        defeatUI.SetActive(false);
        playAgain.onClick.AddListener(PlayAgain);
        UpdateKilledEnemy();
    }

    public void Update()
    {
       
    }

    public void FixedUpdate()
    {
        UpdateWeaponCooldown();
        UpdateTimeDisplay();
    }

    public void UpdateWeaponCooldown()
    {

        RangedWeapon rangedWeapon = GameManager.Instance.player.rangedWeapon;
        MeleeWeapon meleeWeapon = GameManager.Instance.player.meleeWeapon;
        float rangeWeaponCooldown = rangedWeapon.WeaponCooldown / rangedWeapon.AttackRate;
        float meleeWeaponCooldown = meleeWeapon.WeaponCooldown  / meleeWeapon.AttackRate;
        rangeWeaponCooldownDisplay.fillAmount = rangeWeaponCooldown;
        meleeWeaponCooldownDisplay.fillAmount = meleeWeaponCooldown;

    }

    public void UpdateExpBar()
    {
        expBarDisplay.value = (float)GameManager.Instance.player.experience / GameManager.Instance.player.maxLevelExperience;
    }

    public void UpdateTimeDisplay()
    {
        float levelTimeElapsed = GameManager.Instance.levelTimeElapsed;
        timeDisplayTxt.text = FormatFloatTimeToString(levelTimeElapsed);
    }

    public string FormatFloatTimeToString(float time)
    {
        return $"{(int)(time / 60):00}:{(int)(time % 60):00}";
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void ShowFinal()
    {
        GameManager.Instance.UpdateTimeRecord();
        float levelTimeElapsed = GameManager.Instance.levelTimeElapsed;
        surviveTimeTxt.text = FormatFloatTimeToString(levelTimeElapsed);
        bestTimeTxt.text = FormatFloatTimeToString(DataManager.LoadBestTime());
        defeatUI.SetActive(true);
    }

    public void UpdateKilledEnemy()
    {
        enemyKilledTxt.text = GameManager.Instance.enemyKilled.ToString();
    }
}
