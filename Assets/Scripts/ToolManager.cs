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
    public delegate void toolUpdate(ToolObject item, bool state);
    public toolUpdate onToolUpdateCallback;
    public int[] currToolStats = new int[2];
    [SerializeField] private GameObject toolGO;


    void Start()
    {
        inventory = PlayerInventory.instance;
        int numSlots = System.Enum.GetNames(typeof(ToolKind)).Length;
        currTool = new ToolObject[numSlots]; //create array with the same dimensions as the number of the kinds of equipment
        onToolUpdateCallback += SpawnTool;

    }

    public void AddStats(ToolObject obj)
    {


        currToolStats[0] = obj.damage;
        //currToolStats[1] = obj.defence;

        if (onToolUpdateCallback != null)
        {
            onToolUpdateCallback.Invoke(obj, true); //Calls from PlayerStateManager

        }

    }

    public void RemoveStats(ToolObject obj)
    {

        currToolStats[0] = 0;
        // currToolStats[1] = 0;
        if (onToolUpdateCallback != null)
            onToolUpdateCallback.Invoke(obj, false);
    }

    public void SpawnTool(ToolObject obj, bool addingState)
    {
        if (addingState)
        {

            if (toolGO.transform.childCount > 0)
                Destroy(toolGO.transform.GetChild(0).gameObject);
            

            GameObject temp;
            temp = Instantiate(obj.prefab, toolGO.transform);
            temp.GetComponent<IInteractable>().IsInteractable = false;
            temp.GetComponent<Rigidbody>().isKinematic = true;
        }


        else
        {
            if (toolGO.transform.childCount > 0)
                Destroy(toolGO.transform.GetChild(0).gameObject);
            
        }

    }

}

