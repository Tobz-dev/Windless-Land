using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogScript : MonoBehaviour
{
    [SerializeField]
    private GameObject fogNormal;
    [SerializeField]
    private GameObject fogReduced;

    public void ChangeToNormal()
    {
        fogNormal.SetActive(true);
        fogReduced.SetActive(false); 
    }

    public void ChangeToReduced()
    {
        fogNormal.SetActive(false);
        fogReduced.SetActive(true);
    }

    public void ChangeToMinimal()
    {
        fogNormal.SetActive(false);
        fogReduced.SetActive(false);
    }
}
