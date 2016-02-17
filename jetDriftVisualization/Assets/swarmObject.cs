using UnityEngine;
using System.Collections;

public class swarmObject : MonoBehaviour {

    public Vector3 targetGridSquare = new Vector3();

	// Use this for initialization
	void Awake ()
    {
        gameObject.GetComponent<Renderer>().material = new Material(Shader.Find("Unlit/Color"));
        //gameObject.GetComponent<Renderer>().material.SetFloat("_Mode", 3);
        //gameObject.GetComponent<Renderer>().material.SetFloat("_Glossiness", 0);

    }

    // Update is called once per frame
    void FixedUpdate ()
    {
        //attempts to make alpha color -> see through texture
        //gameObject.GetComponent<Renderer>().material.SetFloat("_Mode", 3);
        //gameObject.GetComponent<Renderer>().material.SetFloat("_Glossiness", 0);



        //This line makes it random
        //this.gameObject.GetComponent<Rigidbody>().AddForce((targetGridSquare - this.gameObject.transform.position)+9*Random.insideUnitSphere);

        //This line is precise
        this.gameObject.GetComponent<Rigidbody>().AddForce((targetGridSquare - this.gameObject.transform.position));

        //this line distorts the thing as a whole with a sin
        this.gameObject.GetComponent<Rigidbody>().AddForce(5*new Vector3(0, 0, Mathf.Sin(Time.time + targetGridSquare.magnitude / 5*Mathf.Sin(Time.time/10))));


        //This line rotates them
        this.gameObject.GetComponent<Rigidbody>().AddTorque(5 * new Vector3(0, 0, Mathf.Sin(Time.time + targetGridSquare.magnitude / 10 * Mathf.Sin(Time.time))));
    }
}
