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

    private RectTransform _rectTransform;

    // Start is called before the first frame update
    void Start()
    {
        _itemImage = this.gameObject.GetComponent<Image>();
        _itemSprite = Resources.Load<Sprite>("Images/" + itemname_ID);
        _rectTransform = gameObject.GetComponent<RectTransform>();
        

        if(_itemSprite == true)
        {
            _itemImage.sprite = _itemSprite;
        }
        else { }

        switch (num)
        {
            case 1:
                _rectTransform.anchoredPosition = new Vector2(-720.0f, -270.0f);
                return;
            case 2:
                _rectTransform.anchoredPosition = new Vector2(-240.0f, -270.0f);
                return;
            case 3:
                _rectTransform.anchoredPosition = new Vector2(240.0f, -270.0f);
                return;
            case 4:
                _rectTransform.anchoredPosition = new Vector2(720.0f, -270.0f);
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
