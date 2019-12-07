using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager> {

    public Text timeText;
    public Text hpText;
    public Button restartBtn;

    public slimeColiderScript slimeColiderScrpt;
    public SkinningScript slimeCtrl;
    float time = 0.0f;

	// Use this for initialization
	void Start () {
        StartCoroutine(UpdateTime());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator UpdateTime()
    {
        while(true)
        {
            if(slimeColiderScrpt.hp != 0)
            {
                timeText.text = ((int)time).ToString();
                time += 0.05f;
            }
            yield return new WaitForSeconds(0.05f);
        }
    }

    public void ChangeHp(int amount)
    {
        hpText.text = "HP : " + amount.ToString();

        if(amount == 0 )
        {
            GameOver();
        }
    }

    void GameOver()
    {
        restartBtn.gameObject.SetActive(true);
    }

    public void Restart()
    {
        restartBtn.gameObject.SetActive(false);

        slimeColiderScrpt.hp = 5;
        ChangeHp(slimeColiderScrpt.hp);
        slimeCtrl.Restart();
        BulletManager.instance.Restart();
        time = 0;

    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
