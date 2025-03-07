using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class RecipeSystem : MonoBehaviour
{
    public static RecipeSystem instance;

 

    [SerializeField] private List<RecipeBundle> recipeBundles = new List<RecipeBundle>();
    public List<RecipeBundle> RecipeBundles { get {  return recipeBundles; } }

    #region Singleton
    void Awake()
    {
        
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of recipeSystem found!");
            return;
        }
        instance = this;

       
    }
    #endregion

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
