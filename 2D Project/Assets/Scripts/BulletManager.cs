using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : Singleton<BulletManager> {

    public GameObject bulletPrefap;
    public GameObject fastBulletPrefap;
    public GameObject seekerBulletPrefap;
    public GameObject slimePrefap;

    public Transform playerTr;

    public float minSpeed = 5.0f;
    public float maxSpeed = 5.0f;

    public float generateSpeed = 0.1f;
    public float fastGenSpeed = 0.1f;
    public float seekerGenSpeed = 0.1f;
    public float slimeGenSpeed = 0.1f;

    List<GameObject> bulletPool;
    List<GameObject> slimePool;
    List<GameObject> fastBulletPool;
    List<GameObject> seekerBulletPool;

    int fastBulletGenCnt = 0;

	// Use this for initialization
	void Start () {
        bulletPool = new List<GameObject>();
        slimePool = new List<GameObject>();
        fastBulletPool = new List<GameObject>();
        seekerBulletPool = new List<GameObject>();

        for (int i = 0; i < 2000; i++)
        {
            GameObject bulletObj = Instantiate(bulletPrefap, gameObject.transform);
            bulletObj.SetActive(false);
            bulletPool.Add(bulletObj);
        }

        for (int i = 0; i < 200; i++)
        {
            GameObject slimeObj = Instantiate(slimePrefap, gameObject.transform);
            slimeObj.SetActive(false);
            slimePool.Add(slimeObj);

            GameObject fastbulletObj = Instantiate(fastBulletPrefap, gameObject.transform);
            fastbulletObj.SetActive(false);
            fastBulletPool.Add(fastbulletObj);

            GameObject seekerBulletObj = Instantiate(seekerBulletPrefap, gameObject.transform);
            seekerBulletObj.SetActive(false);
            seekerBulletPool.Add(seekerBulletObj);
        }

        StartCoroutine(GenerateBullet());
        StartCoroutine(GenerateFastBullet());
        StartCoroutine(GenerateSeekerBullet());
        StartCoroutine(GenerateSlimeBullet());
	}
	
    IEnumerator GenerateBullet()
    {
        while(true)
        {
            foreach(GameObject bullet in bulletPool)
            {
                if(!bullet.activeSelf)
                {
                    bullet.SetActive(true);

                    float xPos = 0.0f, yPos = 0.0f;

                    while (Mathf.Abs(xPos) < 9.0f || Mathf.Abs(yPos) < 5.0f)
                    {
                        xPos = Random.Range(-12.0f, 12.0f);
                        yPos = Random.Range(-8.0f, 8.0f);
                    }

                    bullet.transform.position = new Vector3(xPos, yPos, 0);

                    bullet.GetComponent<BulletCtrl>().moveDir = new Vector3(playerTr.position.x + Random.Range(-2, 2) - bullet.transform.position.x, playerTr.position.y + Random.Range(-2, 2) - bullet.transform.position.y).normalized;

                    yield return new WaitForSeconds(generateSpeed);
                }
            }
        }
    }

    IEnumerator GenerateFastBullet()
    {
        while (true)
        {
            for (int i = 0; i <= fastBulletGenCnt; i++)
            {
                foreach (GameObject bullet in fastBulletPool)
                {
                    if (!bullet.activeSelf)
                    {
                        bullet.SetActive(true);

                        float xPos = 0.0f, yPos = 0.0f;

                        while (Mathf.Abs(xPos) < 9.0f || Mathf.Abs(yPos) < 5.0f)
                        {
                            xPos = Random.Range(-12.0f, 12.0f);
                            yPos = Random.Range(-8.0f, 8.0f);
                        }

                        bullet.transform.position = new Vector3(xPos, yPos, 0);

                        bullet.GetComponent<BulletCtrl>().moveDir = new Vector3(playerTr.position.x + Random.Range(-2, 2) - bullet.transform.position.x, playerTr.position.y + Random.Range(-2, 2) - bullet.transform.position.y).normalized;

                        break;
                    }
                }
            }
            fastBulletGenCnt++;
            yield return new WaitForSeconds(fastGenSpeed);
        }
    }

    IEnumerator GenerateSeekerBullet()
    {
        int genBulletCnt = 0;
      
        while (true)
        {
            for (int i = 0; i <= genBulletCnt; i++)
            {
                foreach (GameObject bullet in seekerBulletPool)
                {
                    if (!bullet.activeSelf)
                    {
                        bullet.SetActive(true);

                        float xPos = 0.0f, yPos = 0.0f;

                        while (Mathf.Abs(xPos) < 9.0f || Mathf.Abs(yPos) < 5.0f)
                        {
                            xPos = Random.Range(-12.0f, 12.0f);
                            yPos = Random.Range(-8.0f, 8.0f);
                        }

                        bullet.transform.position = new Vector3(xPos, yPos, 0);

                        bullet.GetComponent<BulletCtrl>().moveDir = new Vector3(playerTr.position.x + Random.Range(-2, 2) - bullet.transform.position.x, playerTr.position.y + Random.Range(-2, 2) - bullet.transform.position.y).normalized;

                        break;
                    }
                }
            }
            //genBulletCnt++;
            yield return new WaitForSeconds(seekerGenSpeed);
        }
    }

    IEnumerator GenerateSlimeBullet()
    {
        while (true)
        {
            foreach (GameObject bullet in slimePool)
            {
                if (!bullet.activeSelf)
                {
                    yield return new WaitForSeconds(slimeGenSpeed);

                    bullet.SetActive(true);

                    float xPos = 0.0f, yPos = 0.0f;

                    while (Mathf.Abs(xPos) < 9.0f || Mathf.Abs(yPos) < 5.0f)
                    {
                        xPos = Random.Range(-12.0f, 12.0f);
                        yPos = Random.Range(-8.0f, 8.0f);
                    }

                    bullet.transform.position = new Vector3(xPos, yPos, 0);

                    bullet.GetComponent<BulletCtrl>().moveDir = new Vector3(playerTr.position.x + Random.Range(-0.2f, 0.2f) - bullet.transform.position.x, playerTr.position.y + Random.Range(-0.2f, 0.2f) - bullet.transform.position.y).normalized;
                }
            }
        }
    }

    public void Restart()
    {
        fastBulletGenCnt = 0;

        foreach (GameObject bullet in bulletPool)
        {
            bullet.SetActive(false);
        }

        foreach (GameObject bullet in fastBulletPool)
        {
            bullet.SetActive(false);
        }

        foreach (GameObject bullet in seekerBulletPool)
        {
            bullet.SetActive(false);
        }

        foreach (GameObject bullet in slimePool)
        {
            bullet.SetActive(false);
        }

        StopAllCoroutines();

        StartCoroutine(GenerateBullet());
        StartCoroutine(GenerateFastBullet());
        StartCoroutine(GenerateSeekerBullet());
        StartCoroutine(GenerateSlimeBullet());
    }
}
