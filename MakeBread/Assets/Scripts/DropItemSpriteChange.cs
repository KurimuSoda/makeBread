using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItemSpriteChange : MonoBehaviour
{
    private SpriteRenderer _spriteRender;
    public string itemID;

    // Start is called before the first frame update
    void Start()
    {
        _spriteRender = GetComponent<SpriteRenderer>();
        _spriteRender.sprite = Resources.Load<Sprite>("Images/" + itemID);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
