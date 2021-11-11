using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ArrowUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI arrowAmtText;
    private int maxArrows;
    private int arrows;

    public void UpdateAmmo(int arrowAmmo, int arrowAmmoMax)
    {
        arrowAmtText.text = arrowAmmo + "/" + arrowAmmoMax;

    }
}
