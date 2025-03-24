using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpMessagePool : MonoBehaviour
{
    [SerializeField] private GameObject messagePrefab;
    [SerializeField] private int poolSize = 5;
    
    private Queue<GameObject> messagePool = new Queue<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject message = Instantiate(messagePrefab, this.transform);
            ReturnMessage(message);
        }

    }
    public GameObject GetMessage(Transform currParent)
    {
        if (messagePool.Count > 0)
        {
            GameObject msg = messagePool.Dequeue();
            msg.transform.SetParent(currParent);
            msg.SetActive(true);
            return msg;
        }
        return null;
    }

    public void ReturnMessage(GameObject message)
    {         
            message.SetActive(false);
            message.transform.SetParent(this.transform);
            messagePool.Enqueue(message);        
    }


    
}
