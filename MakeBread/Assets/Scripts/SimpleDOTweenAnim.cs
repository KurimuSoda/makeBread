using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SimpleDOTweenAnim : MonoBehaviour
{
    //[SerializeField] private Transform _animObjTrans;
    [SerializeField] private RectTransform _animObjTrans;
    [SerializeField] private float _upwardValue = 3.0f;
    [SerializeField] private float _upwardTime = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        UpDownAnim();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void UpDownAnim()
    {
        _animObjTrans.transform.DOMoveY(_upwardValue, _upwardTime)
            .SetRelative(true)
            .SetLoops(-1, LoopType.Yoyo);
    }
}
