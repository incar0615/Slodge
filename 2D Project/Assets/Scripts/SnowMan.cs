using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowMan : MonoBehaviour {
    
    public Sprite snowManSpr;

	// Use this for initialization
	void Start () {
        SpriteRenderer snowManSprRenderer = GetComponent<SpriteRenderer>();
        snowManSprRenderer.sprite = snowManSpr;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
