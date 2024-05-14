using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingTable : MonoBehaviour, IInteractable
{
    [SerializeField]
    private ItemObject item;
    public ItemObject Item { get { return item; } set { item = value; } }
    public void Interact()
    {
       //open ui for crafting table
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
