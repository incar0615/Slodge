using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slimeColiderScript : MonoBehaviour {

    public int hp = 5;
	// Use this for initialization
	
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Bullet"))
        {
            other.gameObject.SetActive(false);

            if (hp > 0)
            {
                hp -= 1;
                UIManager.instance.ChangeHp(hp);
            } 
        }
        else if(other.CompareTag("SlimeBullet"))
        {
            other.gameObject.SetActive(false);

            if(hp > 0 && hp < 5)
            {
                hp++;
                UIManager.instance.ChangeHp(hp);
            }
        }
    }
}
