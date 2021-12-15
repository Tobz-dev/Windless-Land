using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchFireScript : MonoBehaviour
{
    [SerializeField]
    private GameObject fireNormal;
    [SerializeField]
    private GameObject fireReduced;

    public void ChangeToNormal()
    {
        fireNormal.SetActive(true);
        fireReduced.SetActive(false);
    }

    public void ChangeToReduced()
    {
        fireNormal.SetActive(false);
        fireReduced.SetActive(true);
    }

    public void ChangeToMinimal()
    {
        fireNormal.SetActive(false);
        fireReduced.SetActive(false);
    }
}
