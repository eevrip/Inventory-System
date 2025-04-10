
using UnityEngine;

public interface IInteractable 
{
  
  
   public ItemObject Item { get; set; }
   public void Interact();
  //  public void EndInteraction();
 
    public string Message {  get; set; }
    public bool IsInteractable { get; set; }
}
