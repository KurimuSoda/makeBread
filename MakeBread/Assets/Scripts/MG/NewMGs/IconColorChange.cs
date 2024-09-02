using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconColorChange : MonoBehaviour
{
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


    public void SourIconOnry()
    {
        GrayTOWhite(_sourIcon, _sourIconBG);
        WhiteTOGray(_sweetIcon, _sweetIconBG);
        WhiteTOGray(_spicyIcon, _spicyIconBG);
        WhiteTOGray(_saltIcon, _saltIconBG);

    }

    public void SweetIconOnry()
    {
        GrayTOWhite(_sweetIcon, _sweetIconBG);
        WhiteTOGray(_sourIcon, _sourIconBG);
        WhiteTOGray(_spicyIcon, _spicyIconBG);
        WhiteTOGray(_saltIcon, _saltIconBG);

    }

    public void SpicyIconOnry()
    {
        GrayTOWhite(_spicyIcon, _spicyIconBG);
        WhiteTOGray(_sweetIcon, _sweetIconBG);
        WhiteTOGray(_sourIcon, _sourIconBG);
        WhiteTOGray(_saltIcon, _saltIconBG);

    }

    public void SaltIconOnry()
    {
        GrayTOWhite(_saltIcon, _saltIconBG);
        WhiteTOGray(_sweetIcon, _sweetIconBG);
        WhiteTOGray(_spicyIcon, _spicyIconBG);
        WhiteTOGray(_sourIcon, _sourIconBG);

    }

    private void WhiteTOGray(Image image, Image imageBG)
    {
        image.color = _grayRGBA;
        imageBG.color = _grayRGBA;
    }

    private void GrayTOWhite(Image image, Image imageBG)
    {
        image.color = _whiteRGBA;
        imageBG.color = _whiteRGBA;
    }
}
