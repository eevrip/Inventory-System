using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingTable : MonoBehaviour, IInteractable
{
    [SerializeField]
    private ItemObject item;
    public ItemObject Item { get { return item; } set { item = value; } }
    [SerializeField] private string message;
    [SerializeField] private bool isInteractable = true;
    public string Message
    {
        get { return message; }
        set
        {
            message = value;
        }
    }
    public bool IsInteractable
    {
        get { return isInteractable; }
        set
        {
            isInteractable = value;
        }
    }
    private CraftingUI craftingUI;
    public void Interact()
    {
        //open ui for crafting table
        if (craftingUI)
        {
            OpenCraftingUI();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        craftingUI = UIManager.instance.Crafting_UI;
        message = "Open Crafting Table";
    }

    // Update is called once per frame
    public void OpenCraftingUI()
    {
        craftingUI.OpenCraftingMenu();

    }
    
}
