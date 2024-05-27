using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemImageController : MonoBehaviour
{
    /// <summary>
    /// 何番目のアイテムか
    /// </summary>
    public int num = 1;
    /// <summary>
    /// アイテムのID（画像検索用）
    /// </summary>
    public string itemname_ID;
    private Image _itemImage;
    private Sprite _itemSprite;

    // Start is called before the first frame update
    void Start()
    {
        _itemImage = this.gameObject.GetComponent<Image>();
        _itemSprite = Resources.Load<Sprite>("Images/" + itemname_ID);
        if(_itemSprite == true)
        {
            _itemImage.sprite = _itemSprite;
        }
        else
        {
            return;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        
    }
}
