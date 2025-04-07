using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopUpMessagesManager : MonoBehaviour
{
    #region Singleton
    public static PopUpMessagesManager instance;

    private Animator anim;
    [SerializeField] private List<PopUpMessage> messages = new List<PopUpMessage>();

    [SerializeField] private PopUpMessagePool messagePool;
    private Dictionary<string, int> messageCounts = new Dictionary<string, int>(); //How many times the same message is displayed
    private Queue<string> pendingMessages = new Queue<string>();

    private TextMeshProUGUI messageToDisplay;
    private Coroutine delayToolTip;
    public Coroutine DelayToolTip => delayToolTip;
    private GameObject currentMessage;
    private Coroutine currentCoroutine;
    void Awake()
    {

        instance = this;
    }
    #endregion
    // Start is called before the first frame update
    public void ShowMessage(string messageText)
    {
        PopUpMessage currPopUp = null;
        currentMessage = null;
        string msg = messageText;
        // Check if the message is already in the dictionary
        if (messageCounts.ContainsKey(messageText))
        {
            messageCounts[messageText]++;
            currPopUp = messages.Find(x => x.msgText == messageText);
            messageText = $"{messageText} (x{messageCounts[messageText]})"; // Update the message with the count


            currentMessage = currPopUp.msgGO;

            StopCoroutine(currPopUp.coroutine);
        }
        else
        {

            

            currPopUp = new PopUpMessage();

            currPopUp.msgGO = messagePool.GetMessage(this.transform);
            if (currPopUp.msgGO != null)
            {
                messageCounts.Add(messageText, 1);
                currPopUp.msgText = messageText;

                messages.Add(currPopUp);
                currentMessage = currPopUp.msgGO;
            }
            else
            {
                PendingMessage(messageText);
            }

        }

        // Get a message object from the pool

        if (currentMessage != null && currPopUp!=null)
        {
           
            currentMessage.GetComponentInChildren<TextMeshProUGUI>().text = messageText;//plusMinus + 
                                                                                        //  currentCoroutine = StartCoroutine(HideMessageAfterTime(currPopUp, 2f, msg));
            currPopUp.coroutine = StartCoroutine(AnimationFadeOut(currPopUp, msg));
        }

    }

    public void PendingMessage(string messageText)
    {
        pendingMessages.Enqueue(messageText);
    }
    public void ShowPendingMessages()
    { 
        string pendingMsg = "";
        if (pendingMessages.Count > 0) {  
            pendingMsg = pendingMessages.Dequeue();
            ShowMessage(pendingMsg);
        }
           

    }
    public void ShowPopUpMessage(string message)
    {
        if(pendingMessages.Count == 0)
        {
            ShowMessage(message);
        }
        else
        {
            pendingMessages.Enqueue(message);
            string msgToshow = pendingMessages.Dequeue();
            ShowMessage(msgToshow);
        }
    }

   
   

    private IEnumerator AnimationFadeOut(PopUpMessage message, string messageText)
    {
       
        yield return new WaitForSeconds(2f);
       
        message.msgGO.GetComponent<Animator>().SetTrigger("fadeOut");
        messageCounts.Remove(messageText);
        
        StartCoroutine(HideMessageAfterTime(message, 1f, messageText));
    }
    private IEnumerator HideMessageAfterTime(PopUpMessage message, float time, string messageText)
    {
       
        yield return new WaitForSeconds(2f);
       
        messagePool.ReturnMessage(message.msgGO); // Return the message to the pool
       
        messages.Remove(message);

        //show any pending messages
        ShowPendingMessages();
    }

    public class PopUpMessage
    {
        public GameObject msgGO;
        public string msgText;
        public Coroutine coroutine;
    }
}
