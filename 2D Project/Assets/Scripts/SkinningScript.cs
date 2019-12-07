using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinningScript : MonoBehaviour {
    
    public int divSizeX = 2;
    public int divSizeY = 2;

    public float scale = 1;
    public float moveSpeed= 10;
    public float stretchSpeed = 2;
    public float shrinkSpeed = 4;
    public float remainSpeed = 4;

    public GameObject moveTarget;
    public GameObject bulletTarget;
    public MeshCollider slimeColider;

    public GameObject[] vtxObjects = new GameObject[0];
    public Vector3[] verticesPos;
    public Vector3[] originVerticesPos;

    public Texture2D texture;
    public Material material;

    List<GameObject> leftVertice; 
    List<GameObject> rightVertice; 
    List<GameObject> topVertice; 
    List<GameObject> btmVertice; 

    Mesh mesh;

    public float maxThreshold;
    public float minThreshold;

    public bool moveLeft;
    public bool moveRight;
    public bool moveTop;
    public bool moveBtm;


    // Use this for initialization
    public void Awake () {

        InitSpeeds();
        CreateVertexObjects();
        RefreshVertices();
        CreateMesh();

        minThreshold = 1.0f / (divSizeX);
    }

    public void Restart()
    {
        moveTarget.transform.position = Vector3.zero;
    }

    void InitSpeeds()
    {
        moveSpeed *= scale;
        stretchSpeed *= scale;
        shrinkSpeed *= scale;
        remainSpeed *= scale;
    }
    // Update is called once per frame
    void Update () {


        MoveInput();
        RefreshVertices();

        AdjustVtxPosition();
        AdjustVtxOrigin();
    }

    void CreateVertexObjects()
    {
        Vector2 texSize = new Vector2(texture.texelSize.x * texture.width, texture.texelSize.y * texture.height);

        //Vector2 texSize = new Vector2(126,126);

        int vtxCnt = ((int)divSizeX + 1) * ((int)divSizeY + 1);
               
        verticesPos = new Vector3[vtxCnt];
        originVerticesPos = new Vector3[vtxCnt];
        vtxObjects = new GameObject[vtxCnt];

        leftVertice = new List<GameObject>();
        rightVertice = new List<GameObject>();
        topVertice = new List<GameObject>();
        btmVertice = new List<GameObject>();

        float xInterval = texSize.x / divSizeX;
        float yInterval = texSize.y / divSizeY;


        for (int y = 0; y < divSizeY + 1; y++)
        {
            for (int x = 0; x < divSizeX + 1; x++)
            {
                GameObject obj = new GameObject("vertex" + ((y * (divSizeX+1)) + x ));

                if (x == 0) leftVertice.Add(obj);
                else if (x == divSizeX) rightVertice.Add(obj);

                if (y == 0) btmVertice.Add(obj);
                else if (y == divSizeY) topVertice.Add(obj);


                obj.transform.parent = transform;
                obj.transform.position = new Vector3(xInterval * x + gameObject.transform.position.x, yInterval * y + gameObject.transform.position.y, 0);

                vtxObjects[(int)(y * (divSizeX + 1)) + x] = obj;
                verticesPos[(int)(y * (divSizeX + 1)) + x] = obj.transform.position * scale;
                originVerticesPos[(int)(y * (divSizeX + 1)) + x] = obj.transform.position * scale;

            }
        }

        bulletTarget = vtxObjects[ ((int)divSizeX * (int)divSizeY) - 1];
        BulletManager.instance.playerTr = bulletTarget.transform;
    }

    void RefreshVertices()
    {
        for (int i = 0; i < verticesPos.Length; i++)
        {
            verticesPos[i] = (vtxObjects[i].transform.position - gameObject.transform.position);

        }

        if(mesh)
        {
            mesh.vertices = verticesPos;
            slimeColider.GetComponent<MeshCollider>().sharedMesh = mesh;
        }
            
    }

    void CreateMesh()
    {
        mesh = new Mesh();
        mesh.name = "customMesh";


        mesh.vertices = verticesPos;

        Vector2[] convertedPos = new Vector2[verticesPos.Length];

        for (int i = 0; i < convertedPos.Length; i++)
        {
            convertedPos[i] = new Vector2(verticesPos[i].x , verticesPos[i].y );
        }
        mesh.uv = convertedPos;

        int triVtxCnt = (int)divSizeX * (int)divSizeY * 2 * 3;

        int[] triangles = new int[triVtxCnt];

        Debug.Log(triVtxCnt + " " + mesh.triangles.Length);

        int rectNo = 0;
        for (int y = 0; y < (int)divSizeY; y++)
        {
            for (int x = 0; x < (int)divSizeX; x++)
            {
                triangles[rectNo * 6 + 0] = y * ((int)divSizeX + 1) + x;
                triangles[rectNo * 6 + 1] = (y + 1) * ((int)divSizeX + 1) + x;
                triangles[rectNo * 6 + 2] = y * ((int)divSizeX + 1) + (x + 1);
                          
                triangles[rectNo * 6 + 3] = y * ((int)divSizeX + 1) + (x + 1);
                triangles[rectNo * 6 + 4] = (y + 1) * ((int)divSizeX + 1) + x;
                triangles[rectNo * 6 + 5] = (y + 1) * ((int)divSizeX + 1) + (x + 1);

                rectNo++;
            }
        }

        mesh.triangles = triangles;
       

        mesh.RecalculateNormals();
        mesh.RecalculateBounds();

        if (GetComponent<MeshFilter>() == null)
        {
            gameObject.AddComponent<MeshFilter>();
        }
            
        if (GetComponent<MeshRenderer>() == null)
        {
            gameObject.AddComponent<MeshRenderer>();
        }

        GetComponent<MeshFilter>().mesh = mesh;
        slimeColider.sharedMesh = mesh;

        material.mainTexture = texture;


        GetComponent<MeshRenderer>().material = material;

    }

    void moveObj(GameObject obj, int direction, float speed)
    {
        switch(direction)
        {
            case 0: // left
                break;

            case 1: // right
                break;

            case 2: // top
                
                break;

            case 3: // bottom
                break;
        }

    }

    void MoveInput()
    {
        moveLeft = moveRight = moveTop = moveBtm = false;

        float localMoveSpeed = moveSpeed;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            localMoveSpeed = moveSpeed * 0.5f;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            moveLeft = true;

            moveTarget.transform.Translate(Vector3.left * Time.deltaTime * localMoveSpeed);

            foreach (GameObject obj in leftVertice)
            {
                obj.transform.Translate(Vector3.left * Time.deltaTime * localMoveSpeed);

            }
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            moveRight = true;

            moveTarget.transform.Translate(Vector3.right * Time.deltaTime * localMoveSpeed);

            foreach (GameObject obj in rightVertice)
            {
                obj.transform.Translate(Vector3.right * Time.deltaTime * localMoveSpeed);

            }
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            moveTop = true;

            moveTarget.transform.Translate(Vector3.up * Time.deltaTime * localMoveSpeed);

            foreach (GameObject obj in topVertice)
            {
                obj.transform.Translate(Vector3.up * Time.deltaTime * localMoveSpeed);

            }
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            moveBtm = true;
            moveTarget.transform.Translate(Vector3.down * Time.deltaTime * localMoveSpeed);

            foreach (GameObject obj in btmVertice)
            {
                obj.transform.Translate(Vector3.down * Time.deltaTime * localMoveSpeed);

            }
        }
    }

    void AdjustVtxPosition()
    {
        for (int i = 0; i < vtxObjects.Length; i++)
        {
            Vector3 moveDir = Vector3.zero;

            if (!topVertice.Contains(vtxObjects[i]) && !moveBtm)
            {
                if (Mathf.Abs(vtxObjects[i].transform.position.y - vtxObjects[i + (divSizeX + 1)].transform.position.y) > minThreshold)
                {
                    moveDir += Vector3.up * Vector3.Distance(vtxObjects[i].transform.position, vtxObjects[i + (divSizeX + 1)].transform.position) * shrinkSpeed;
                }
            }

            if (!btmVertice.Contains(vtxObjects[i]) && !moveTop)
            {
                if (Mathf.Abs(vtxObjects[i].transform.position.y - vtxObjects[i - (divSizeX + 1)].transform.position.y) > minThreshold)
                {
                    moveDir += Vector3.down * Vector3.Distance(vtxObjects[i].transform.position, vtxObjects[i - (divSizeX + 1)].transform.position) * shrinkSpeed;
                }
            }

            if (!leftVertice.Contains(vtxObjects[i]) && !moveRight)
            {
                if (Mathf.Abs(vtxObjects[i].transform.position.x - vtxObjects[i - 1].transform.position.x) > minThreshold)
                {
                    moveDir += Vector3.left * Vector3.Distance(vtxObjects[i].transform.position, vtxObjects[i - 1].transform.position) * shrinkSpeed;
                }
            }

            if (!rightVertice.Contains(vtxObjects[i]) && !moveLeft)
            {
                if (Mathf.Abs(vtxObjects[i].transform.position.x - vtxObjects[i + 1].transform.position.x) > minThreshold)
                {
                    moveDir += Vector3.right * Vector3.Distance(vtxObjects[i].transform.position, vtxObjects[i + 1].transform.position) * shrinkSpeed;
                }
            }

            vtxObjects[i].transform.Translate(moveDir * Time.deltaTime);
        }
    }

    void AdjustVtxOrigin()
    {
        float moveRange = 0.1f;

        //if (!moveBtm && !moveTop && !moveLeft && !moveRight) 
        //{
            
        //    return;
        //}

        for (int i = 0; i < vtxObjects.Length; i++)
        {
            Vector3 moveDir = originVerticesPos[i] + moveTarget.transform.position - vtxObjects[i].transform.position;

            if (moveDir.magnitude > 0.01f)
                vtxObjects[i].transform.Translate(moveDir * Time.deltaTime * remainSpeed  );
            else
                vtxObjects[i].transform.position = originVerticesPos[i] + moveTarget.transform.position;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        if (vtxObjects.Length == 0) return;

        for (int i = 0; i < vtxObjects.Length; i++)
        {
            Gizmos.DrawSphere(vtxObjects[i].transform.position, 0.02f);
        }
    }


}
