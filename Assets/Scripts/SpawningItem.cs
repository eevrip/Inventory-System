using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningItem : MonoBehaviour
{
    public void SpawnItem(GameObject obj, Vector3 pos, Quaternion rot, Transform trf)
    {

        if (obj!= null)
        {
            Debug.Log("Spawn Item");
            GameObject temp;
            temp = Instantiate(obj, pos, rot, trf);
            temp.GetComponent<Rigidbody>().isKinematic = false;
        }


    }
}
