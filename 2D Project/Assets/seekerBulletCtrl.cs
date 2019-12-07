using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class seekerBulletCtrl : BulletCtrl {

    public GameObject moveTarget;

    private float modifyValue;

    public Vector3 originDir;
	// Use this for initialization
	void Start () {
        originDir = base.moveDir;
        StartCoroutine(Guided());
	}

    private void OnEnable()
    {
        originDir = base.moveDir.normalized;

        moveTarget = BulletManager.instance.playerTr.gameObject;
        base.speed = Random.Range(base.minSpeed, base.maxSpeed);
        StartCoroutine(Guided());
    }

	// Update is called once per frame
	void Update () 
    {
        if (!moveTarget) return;

        if (transform.position.sqrMagnitude > 101 * 101)
        {
            gameObject.SetActive(false);
        }
        transform.Translate(moveDir.normalized * Time.deltaTime * 3.0f);

	}

    IEnumerator Guided()
    {
        while(true)
        {
            yield return new WaitForSeconds(0.05f);

            Vector3 destDir = (moveTarget.transform.position - transform.position).normalized;

            float dot = Vector3.Dot(moveDir, destDir);

            if (dot > 0)
            {
                moveDir = destDir.normalized * 0.05f + moveDir.normalized * 0.95f;
            }
        }
    }
}
