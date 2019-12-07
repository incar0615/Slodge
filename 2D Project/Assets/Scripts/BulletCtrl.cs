using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCtrl : MonoBehaviour {

    public Vector3 moveDir;

    protected float speed;

    public float minSpeed;
    public float maxSpeed;


	// Use this for initialization
	void Start () 
    {
     
	}


	private void OnEnable()
	{
        speed = Random.Range(minSpeed, maxSpeed);
	}

	// Update is called once per frame
	void Update () 
    {
        if (transform.position.sqrMagnitude > 101 * 101) 
        {
            gameObject.SetActive(false);
        }
        
        transform.Translate(moveDir * Time.deltaTime * speed);
	}

    void Init(Vector3 _moveDir, float _speed)
    {
        moveDir = _moveDir;
        speed = _speed;
    }

}
