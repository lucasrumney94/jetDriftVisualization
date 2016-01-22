using UnityEngine;
using System.Collections;

public class imgRead : MonoBehaviour {


    // Use this for initialization
    // Load a .jpg or .png file by adding .bytes extensions to the file
    // and dragging it on the imageAsset variable.
    public Texture2D sourceTex;
    public Rect sourceRect;
    void Start()
    {
        int x = Mathf.FloorToInt(sourceRect.x);
        int y = Mathf.FloorToInt(sourceRect.x);
        int width = Mathf.FloorToInt(sourceRect.width);
        int height = Mathf.FloorToInt(sourceRect.height);
        Color[] pix = sourceTex.GetPixels(x, y, width, height);
        Texture2D destTex = new Texture2D(width, height);
        destTex.SetPixels(pix);
        destTex.Apply();
        GetComponent<Renderer>().material.mainTexture = destTex;
        foreach (Color color in pix)
        {
           //Debug.Log(color);
        }

    }
    // Update is called once per frame
    void Update ()
    {
        



    }
}
