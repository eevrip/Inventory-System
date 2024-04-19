using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolManager : MonoBehaviour
{
    #region Singleton
    public static ToolManager instance;
    void Awake() {
        instance = this;
    }
    #endregion

    ToolObject[] currTool; //current equipment

    PlayerInventory inventory;
    public delegate void toolUpdate(ToolObject item);
    public toolUpdate onToolUpdateCallback;
    public int[] currToolStats = new int[2];

    void Start() {
        inventory = PlayerInventory.instance;
        int numSlots = System.Enum.GetNames(typeof(ToolKind)).Length;
        currTool = new ToolObject[numSlots]; //create array with the same dimensions as the number of the kinds of equipment
        
    }

    public void AddStats(ToolObject obj)
    {
        currToolStats[0] = obj.damage;
        //currToolStats[1] = obj.defence;
        if(onToolUpdateCallback!=null)
            onToolUpdateCallback.Invoke(obj);
    }

    public void RemoveStats(ToolObject obj)
    {
        currToolStats[0] = 0;
       // currToolStats[1] = 0;
        if (onToolUpdateCallback != null)
            onToolUpdateCallback.Invoke(obj);
    }

    
}
