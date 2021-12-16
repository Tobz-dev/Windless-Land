using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class SkinnedMeshToMesh : MonoBehaviour
{
    [SerializeField]
    private SkinnedMeshRenderer skinnedMesh;
    [SerializeField]
    private VisualEffect VFXGraph;

    private float activationTime = 2f;
    private bool isActive;

    private void Update()
    {
        if (isActive)
        {
            StartTimer();

            Mesh newMesh = new Mesh();
            skinnedMesh.BakeMesh(newMesh);
            Vector3[] vertices = newMesh.vertices;
            Mesh newMesh2 = new Mesh();
            newMesh2.vertices = vertices;

            VFXGraph.SetMesh("PlayerMesh", newMesh2);
            Debug.Log("Changed mesh");
        }
    }
    
    private void StartTimer()
    {
        activationTime -= Time.deltaTime;

        if(activationTime <= 0)
        {
            isActive = false;
            activationTime = 2f;
        }
    }


    public void SetSkinnedMeshAsMesh()
    {
        isActive = true;
    }
}
