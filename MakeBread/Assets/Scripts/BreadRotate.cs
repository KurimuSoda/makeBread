using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BreadRotate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            SceneManager.LoadScene("OvenFire");
        }
    }
    private void FixedUpdate()
    {
        this.gameObject.transform.Rotate(0.0f, 0.0f, 1.0f);
    }
}
