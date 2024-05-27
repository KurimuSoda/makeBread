using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickRotateController : MonoBehaviour
{
    private Transform _stickTransform;
    private float _stickRot = 0.0f;
     

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        _stickTransform = this.gameObject.transform;
        _stickRot = Input.GetAxis("Horizontal") * -1;
        _stickTransform.Rotate(0.0f, 0.0f, _stickRot);
    }

    private void FixedUpdate()
    {
        
    }
}
