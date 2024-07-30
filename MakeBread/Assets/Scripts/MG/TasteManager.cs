using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TasteMG
{

    public class TasteManager
    {
        private int _mainTaste = 0;
        /// <summary>選択した材料の味を選択した順番に格納する</summary>
        private int[] _tasteArray = new int[4];
        private string[] _tasteStringArray = new string[5] { "甘い", "辛い", "酸っぱい", "しょっぱい", "苦い" };
        private int[] _tastCountReArray = new int[4] { 0, 0, 0, 0 };

        public bool isBiter = false;


        
        public void PutInTastArray(int inputNumber, int data_taste)
        {
            _tasteArray[inputNumber] = data_taste;

        }

        /// <summary>
        /// 最終的な味を返す。0甘い, 1辛い, 2酸っぱい, 3しょっぱい, 4苦い
        /// </summary>
        /// <returns>int型</returns>
        public int ReturnLastItemTaste()
        {
            int lastTaste = _mainTaste;
            if (isBiter)
            {
                lastTaste = 4;
            }

            return lastTaste;
        }

        /// <summary>
        /// メインの味を決定する。受け取ったimputCountの値で最後のアイテムの味を_mainTasteに記録する
        /// </summary>
        /// <param name="inputCount">選んだアイテムの数</param>
        public void PushEnter(int inputCount)
        {
            _mainTaste = _tasteArray[inputCount];
            //Debug.Log("main taste is ---> " + _mainTaste + " : " + _tasteStringArray[_mainTaste - 1]);
            _tastCountReArray = TasteCheck();   //味の個数を配列で受け取る
        }

        /// <summary>
        /// 各味の個数を配列に格納する。[swe, spi, sour, salt]
        /// </summary>
        /// <returns>int配列[swe, spi, sour, salt]</returns>
        private int[] TasteCheck()
        {
            int sweet = 0, spice = 1, sour = 2, salty = 3;
            int[] tasteCheckArray = new int[4] { 0, 0, 0, 0 };

            for (int i = 0; i < 4; i++)
            {
                if (_tasteArray[i] == 1)
                {
                    tasteCheckArray[sweet]++;
                }
                else if (_tasteArray[i] == 2)
                {
                    tasteCheckArray[spice]++;
                }
                else if (_tasteArray[i] == 3)
                {
                    tasteCheckArray[sour]++;
                }
                else if (_tasteArray[i] == 4)
                {
                    tasteCheckArray[salty]++;
                }
                else { }


            }

            return tasteCheckArray;
        }
    }
}