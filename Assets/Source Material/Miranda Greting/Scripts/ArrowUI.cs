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

    //add this script to Player GameObject, add Canvas > ArrowAmmo > ArrowAmmoText to text variable slot
    public void UpdateAmmo(int arrowAmmo, int arrowAmmoMax)
    {
        arrowAmtText.text = arrowAmmo + "/" + arrowAmmoMax;

    }

    //  add   gameObject.GetComponent<ArrowUI>().UpdateAmmo(arrowAmmo, arrowAmmoMax); in CharacterController wherever ammo changes

}
