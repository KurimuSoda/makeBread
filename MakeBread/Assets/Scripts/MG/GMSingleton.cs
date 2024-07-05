using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GMSingleton : MonoBehaviour
{
    public static GMSingleton instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            gameObject.SetActive(false);
            //Destroy(gameObject);
        }
    }
    
}
