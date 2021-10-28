using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{

    [SerializeField] private DoorOpenAnim door;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            door.openDoor();
        }
    }
}
