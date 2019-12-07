using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fieldScript : MonoBehaviour {

	private void OnTriggerExit(Collider other)
	{
        if(other.CompareTag("Bullet") || other.CompareTag("SlimeBullet"))
        {
            other.gameObject.SetActive(false);
        }
	}
}
