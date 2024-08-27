using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SceneMG.support {

    public enum SceneNames
    {
        TitleScene,
        CookingPotBT,
        OvenFire,
        ResultScene,
        FermentScene
    }

    public class SceneNameMG
    {
        /// <summary>
        /// ビルドセッティングに登録中のシーン --> "TitleScene", "CookingPotBT", "OvenFire", "ResultScene", "FermentScene"
        /// </summary>
        public string[] gameSceneNames = { "TitleScene", "CookingPotBT", "OvenFire", "ResultScene", "FermentScene" };

        
    }
}
