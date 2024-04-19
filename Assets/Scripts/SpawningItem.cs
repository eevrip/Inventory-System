using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningItem : MonoBehaviour
{
    public void SpawnItem(Object obj, Vector3 pos, Quaternion rot, Transform trf)
    {

        if (obj!= null)
        {
            Debug.Log("Spawn Item");
            Instantiate(obj, pos, rot, trf);

        }
    }
}
