using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidePuzzleScripts : MonoBehaviour {

    public GameObject tile;

    public int sizeX;
    public int sizeY;

	// Use this for initialization
	void Start () {
        MakePuzzle();
	}
	
	// Update is called once per frame
	void MakePuzzle() {
        for (int y = 0; y < sizeY; y++)
        {
            for (int x = 0; x < sizeX; x++)
            {
                GameObject obj = Instantiate(tile, gameObject.transform);

                //obj.transform.localPosition.Set(x * 0.64f, y * 0.64f, 0);
                obj.transform.position = new Vector3(x * 0.64f, y * 0.64f, 0);
            }
        }
	}
}
