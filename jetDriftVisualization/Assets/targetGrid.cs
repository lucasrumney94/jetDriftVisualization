using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class targetGrid : MonoBehaviour {

    public float spacing = 1.0f;
    
    public List<Vector3> positions = new List<Vector3>();
    public int height = 20;
    public int width = 20;
    public float magnitude = 50.0f;
    public float frequency = .2f;

	// Use this for initialization
	void Awake ()
    {
        for (int j = 0; j < height; j++)
        {
            for (int i = 0; i < width; i++)
            {
                positions.Add(new Vector3(gameObject.transform.position.x + spacing * i, gameObject.transform.position.y + spacing * j, gameObject.transform.position.z));
            }
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        //Debug.Log("running targrid update");
        for (int j = 0; j < height; j++)
        {
            for (int i = 0; i < width; i++)
            {
                positions[(j*width)+i]=(new Vector3(gameObject.transform.position.x + spacing * i, gameObject.transform.position.y + spacing * j, gameObject.transform.position.z));
            }
        }

        //this.gameObject.transform.position = (magnitude*new Vector3(5*Mathf.Cos(frequency*2*Mathf.PI*Time.time), Mathf.Sin(frequency * 2 * Mathf.PI * Time.time),Random.Range(-.1f,.1f)));

    }
}
