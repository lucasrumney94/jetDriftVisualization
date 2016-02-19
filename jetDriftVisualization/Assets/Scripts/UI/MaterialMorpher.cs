using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MaterialMorpher : MonoBehaviour
{
    public float period = 40f;

    public Material wallMaterial;

    public Text rgbDisplay;

    void Update()
    {
        ChangeMaterial();
        ChangeText();
    }

    private void ChangeMaterial()
    {
        float colorIntensity = (Time.time % period) / period;
        Color morphedWallColor = new Color(colorIntensity, colorIntensity, colorIntensity);
        wallMaterial.color = morphedWallColor;
    }

    private void ChangeText()
    {
        rgbDisplay.text = "R" + (wallMaterial.color.r * 255).ToString("F0") + " G" + (wallMaterial.color.g * 255).ToString("F0") + " B" + (wallMaterial.color.b * 255).ToString("F0");
    }
}
