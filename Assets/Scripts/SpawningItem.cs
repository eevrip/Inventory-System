using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SpawningItem : MonoBehaviour
{
    public GameObject SpawnItem(GameObject obj, Vector3 pos, Quaternion rot, Transform trf)
    {

        if (obj!= null)
        {
            Debug.Log("Spawn Item");
            GameObject temp;
            temp = Instantiate(obj, pos, rot, trf);
           
            temp.GetComponent<Rigidbody>().isKinematic = false;
            return temp;
        }
        return null;    
      
    }
}
