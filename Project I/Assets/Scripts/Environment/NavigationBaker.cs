using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.AI.Navigation;

public class NavigationBaker : MonoBehaviour
{

    public NavMeshSurface[] surfaces;

    // Use this for initialization
    void Start()
    {

        for (int i = 0; i < surfaces.Length; i++)
        {
            Debug.Log("bake ne`");
            surfaces[i].BuildNavMesh();
        }
        Debug.Log("bake xong");
    }


}