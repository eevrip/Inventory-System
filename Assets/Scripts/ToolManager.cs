using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEditor.Progress;

public class ToolManager : MonoBehaviour
{
    #region Singleton
    public static ToolManager instance;
    void Awake()
    {
        instance = this;
    }
    #endregion

    ToolObject[] currTool; //current equipment

    PlayerInventory inventory;
    public delegate void toolUpdate(ToolObject item);
    public toolUpdate onToolUpdateCallback;
    public int[] currToolStats = new int[2];
    [SerializeField] private GameObject toolGO;

    void Start()
    {
        inventory = PlayerInventory.instance;
        int numSlots = System.Enum.GetNames(typeof(ToolKind)).Length;
        currTool = new ToolObject[numSlots]; //create array with the same dimensions as the number of the kinds of equipment
        
    }

    public void AddStats(ToolObject obj)
    {
        toolGO.SetActive(true);
        currToolStats[0] = obj.damage;
        //currToolStats[1] = obj.defence;
        if (onToolUpdateCallback != null)
            onToolUpdateCallback.Invoke(obj);
    }

    public void RemoveStats(ToolObject obj)
    {
        toolGO.SetActive(false);
        currToolStats[0] = 0;
        // currToolStats[1] = 0;
        if (onToolUpdateCallback != null)
            onToolUpdateCallback.Invoke(obj);
    }

    /*public void SpawnTool(bool spawningState, ToolObject obj)
    {
        if(spawningState)
        {
            
            GameObject temp;
            temp = Instantiate(obj.prefab, toolPos.position, Quaternion.identity, toolPos);  //
            temp.GetComponent<Rigidbody>().isKinematic = true;
        }
        else
        {
            Destroy(toolPos.GetChild(0).gameObject);
        }
    }*/
}