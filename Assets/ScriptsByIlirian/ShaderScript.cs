using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderScript : MonoBehaviour
{

    [SerializeField] private Material outlineMaterial;
    [SerializeField] private float outlineScaleFactor;
    [SerializeField] private Color outlineColor;
    private Renderer myRenderer;

    void Start()
    {
        myRenderer = CreateOutline(outlineMaterial, outlineScaleFactor, outlineColor);
        myRenderer.enabled = true;
    }

    Renderer CreateOutline(Material outlinemat, float scalefactor, Color color)
    {
        GameObject outlineobject = Instantiate(this.gameObject, transform.position, transform.rotation, transform);
        outlineobject.transform.localScale = new Vector3(1, 1, 1);
        Renderer rend = outlineobject.GetComponent<Renderer>();

        rend.material = outlinemat;
        rend.material.SetColor("_Color", color);
        rend.material.SetFloat("_Scale", scalefactor);
        rend.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;

        outlineobject.GetComponent<ShaderScript>().enabled = false;
        outlineobject.GetComponent<Collider>().enabled = false;

        rend.enabled = false;

        return rend;


    }


}
