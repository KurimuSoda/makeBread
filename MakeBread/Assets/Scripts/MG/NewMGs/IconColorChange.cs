using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 素材選択の時に、選択した素材の味に対応したアイコンのみ色付きで表示する。ItemSelectMGから呼び出して使う
/// </summary>
public class IconColorChange : MonoBehaviour
{
    //BGはアイコンに縁取りをつけるために設置しているIconBG
    //各味に対応するアイコンをセットする
    [SerializeField] private Image _sourIcon;
    [SerializeField] private Image _sourIconBG;
    [SerializeField] private Image _sweetIcon;
    [SerializeField] private Image _sweetIconBG;
    [SerializeField] private Image _spicyIcon;
    [SerializeField] private Image _spicyIconBG;
    [SerializeField] private Image _saltIcon;
    [SerializeField] private Image _saltIconBG;

    private Color32 _grayRGBA = new Color32(205, 190, 205, 255);
    private Color32 _whiteRGBA = new Color32(255, 255, 255, 255);


    /// <summary>
    /// taste:Sour
    /// </summary>
    public void SourIconOnry()
    {
        GrayTOWhite(_sourIcon, _sourIconBG);
        WhiteTOGray(_sweetIcon, _sweetIconBG);
        WhiteTOGray(_spicyIcon, _spicyIconBG);
        WhiteTOGray(_saltIcon, _saltIconBG);

    }

    /// <summary>
    /// taste:Sweet
    /// </summary>
    public void SweetIconOnry()
    {
        GrayTOWhite(_sweetIcon, _sweetIconBG);
        WhiteTOGray(_sourIcon, _sourIconBG);
        WhiteTOGray(_spicyIcon, _spicyIconBG);
        WhiteTOGray(_saltIcon, _saltIconBG);

    }

    /// <summary>
    /// taste:Spicy
    /// </summary>
    public void SpicyIconOnry()
    {
        GrayTOWhite(_spicyIcon, _spicyIconBG);
        WhiteTOGray(_sweetIcon, _sweetIconBG);
        WhiteTOGray(_sourIcon, _sourIconBG);
        WhiteTOGray(_saltIcon, _saltIconBG);

    }

    /// <summary>
    /// taste:Salt
    /// </summary>
    public void SaltIconOnry()
    {
        GrayTOWhite(_saltIcon, _saltIconBG);
        WhiteTOGray(_sweetIcon, _sweetIconBG);
        WhiteTOGray(_spicyIcon, _spicyIconBG);
        WhiteTOGray(_sourIcon, _sourIconBG);

    }

    /// <summary>
    /// アイコン画像のcolorをグレーに変える
    /// </summary>
    /// <param name="image"></param>
    /// <param name="imageBG"></param>
    private void WhiteTOGray(Image image, Image imageBG)
    {
        image.color = _grayRGBA;
        imageBG.color = _grayRGBA;
    }

    /// <summary>
    /// アイコン画像のcolorを白に戻す
    /// </summary>
    /// <param name="image"></param>
    /// <param name="imageBG"></param>
    private void GrayTOWhite(Image image, Image imageBG)
    {
        image.color = _whiteRGBA;
        imageBG.color = _whiteRGBA;
    }
}
