using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class UIManager : MonoBehaviour
{
    #region Singleton
    public static UIManager instance;
    void Awake()
    {
        instance = this;
    }
    #endregion
    [SerializeField] private GameObject player;
    private PlayerMovement playerMvm;
    private Camera cam;
    private MouseLook mouseCursor;
    [SerializeField] private BackpackUI backpackUI;
    [SerializeField] private StorageUI storageUI;
    [SerializeField] private CraftingUI craftingUI;
    public BackpackUI Backpack_UI => backpackUI;
      public StorageUI Storage_UI => storageUI;
    public CraftingUI Crafting_UI => craftingUI;

    private bool isStorageOpen = false;
    public bool IsStorageOpen { get { return isStorageOpen; } }
    private bool wasStorageClosed = false; //was storage closed before

    private bool isUIEnabled = false;
    public bool IsUIEnabled { get { return isUIEnabled; } }

    private bool isCraftingTableOpen = false;


    public void Start()
    {

       
        
        playerMvm = player.GetComponent<PlayerMovement>();
        cam = Camera.main; //Camera
        mouseCursor = cam.GetComponent<MouseLook>(); //MouseLook script of the camera

    }
    void Update()
    {
        isStorageOpen = storageUI.IsUIShown;
        isCraftingTableOpen = craftingUI.IsUIShown;
        if (!isStorageOpen && !isCraftingTableOpen) //Storage is not enabled
        {
            if (Input.GetButtonDown("Tab")) //If button tab is pressed then the UI for the inventory appears
            {
                if (wasStorageClosed)
                {
                    backpackUI.InventoryMode(true);
                    wasStorageClosed = false;
                }
                if (!backpackUI.IsUIShown)
                {
                    isUIEnabled = true;
                    MouseMovementEnabled();
                    backpackUI.ShowInventory();
                    
                }
                else
                {   isUIEnabled = false;
                    MouseMovementDisabled();
                    backpackUI.CloseInventory();
                    ToolTipManager.instance.HideToolTip();
                }
            }
        }
        if(isStorageOpen) //Storage is enabled
        {
            isUIEnabled = true;
            
            if (Input.GetKeyDown(KeyCode.E))//Input.GetButtonDown("Esc"))
            {
                
                isUIEnabled = false;
               // MouseMovementDisabled();
                storageUI.CloseInventory();
                backpackUI.CloseInventory();
                ToolTipManager.instance.HideToolTip();
                wasStorageClosed = true;



            }
        }

      if (isCraftingTableOpen)
        {
            isUIEnabled = true;

            if (Input.GetKeyDown(KeyCode.E))//Input.GetButtonDown("Esc"))
            {

                isUIEnabled = false;
               
                ToolTipManager.instance.HideToolTip();
                craftingUI.CloseCraftingMenu();



            }
        }
        

    }


    public void MouseMovementEnabled()
    {
       
        playerMvm.ResetHorizontalSpeed();
        playerMvm.enabled = false;
        ShowCursor.instance.CursorEnabled();
        Cursor.lockState = CursorLockMode.Confined; //Mouse Cursor appears and cannot escape the screen boundaries Confined wasnt working properly
        mouseCursor.enabled = false;
    }
    public void MouseMovementDisabled()
    {
        

        playerMvm.enabled = true;
        ShowCursor.instance.CursorDisabled();
        Cursor.lockState = CursorLockMode.Locked; //Mouse Cursor is locked to the center of the screen
        mouseCursor.enabled = true;
    }
}
