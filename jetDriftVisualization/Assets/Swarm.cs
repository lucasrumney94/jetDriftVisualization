using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Swarm : MonoBehaviour {


    public GameObject swarmObjectPrefab;
    public GameObject targetGridPrefab;

    public Texture2D sourceTex;
    public Rect sourceRect;
    public int width = 128;
    public int height = 128;
    public float randInitPosMag = 20.0f;
    public float swarmGridSpacing;

    public List<GameObject> swarmList = new List<GameObject>();
    //public List<Vector3> desiredPositions = new List<Vector3>();



    private int numberOfSwarmObjects = 256;

    private Vector3 randomInitialPosition;

    private GameObject targetGrid;
	// Use this for initialization
	void Start ()
    {
        //Get image height and width for this
        numberOfSwarmObjects = width * height;


        targetGrid = GameObject.Instantiate(targetGridPrefab, Vector3.zero, Quaternion.identity) as GameObject;
        targetGrid.transform.parent = this.gameObject.transform;
        targetGrid.GetComponent<targetGrid>().spacing = swarmGridSpacing;
        targetGrid.GetComponent<targetGrid>().height = height;
        targetGrid.GetComponent<targetGrid>().width = width;

        for (int j = 0; j < height; j++)
        {
            for (int i = 0; i < width; i++)
            {
                targetGrid.GetComponent<targetGrid>().positions.Add(new Vector3(gameObject.transform.position.x + targetGrid.GetComponent<targetGrid>().spacing * i, gameObject.transform.position.y + targetGrid.GetComponent<targetGrid>().spacing * j, targetGrid.transform.position.z));
            }
        }
        for (int i = 0; i < numberOfSwarmObjects; i++)
        {
            randomInitialPosition = randInitPosMag * Random.insideUnitSphere;
            GameObject temp = GameObject.Instantiate(swarmObjectPrefab, randomInitialPosition, Quaternion.identity) as GameObject;
            temp.transform.parent = this.gameObject.transform;
            temp.GetComponent<swarmObject>().targetGridSquare = targetGrid.GetComponent<targetGrid>().positions[i];
            swarmList.Add(temp);
        }

        imgToColor();
    }
	
	// Update is called once per frame
	void Update ()
    {
        for (int i = 0; i < numberOfSwarmObjects; i++)
        {
            swarmList[i].GetComponent<swarmObject>().targetGridSquare = targetGrid.GetComponent<targetGrid>().positions[i];
        }
    }

    void imgToColor()
    {
        int x = Mathf.FloorToInt(sourceRect.x);
        int y = Mathf.FloorToInt(sourceRect.x);
        int mywidth = Mathf.FloorToInt(sourceRect.width);
        int myheight = Mathf.FloorToInt(sourceRect.height);
        Color[] pix = sourceTex.GetPixels(x, y, mywidth, myheight);
        //Texture2D destTex = new Texture2D(width, height);
        //destTex.SetPixels(pix);
        //destTex.Apply();
        //GetComponent<Renderer>().material.mainTexture = destTex;

        for (int i = 0; i < pix.Length; i++)
        {
            swarmList[i].GetComponent<MeshRenderer>().sharedMaterial.SetColor("_Color", pix[i]);
            
            //Debug.Log(pix[i]);
        }
    }
}
