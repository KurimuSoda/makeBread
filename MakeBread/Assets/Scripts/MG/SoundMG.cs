using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SceneMG.support;

public class SoundMG : MonoBehaviour
{
    [SerializeField] private AudioSource _mainBGM;
    [SerializeField] private AudioSource _selectItemBGM;
    private SceneNameMG _sceneNameMG = new SceneNameMG();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BGMInit()
    {
        _mainBGM.Play();
        _selectItemBGM.Play();
        _selectItemBGM.Pause();
    }

    /// <summary>
    /// Sceneの名前を受け取ってBGMを再生する。
    /// </summary>
    /// <param name="sceneName">Sceneの名前</param>
    public void ChangeBGM(string sceneName)
    {
        if(sceneName == _sceneNameMG.gameSceneNames[0] || sceneName == _sceneNameMG.gameSceneNames[3])
        {
            _selectItemBGM.Pause();
            _mainBGM.UnPause();
            //_mainBGM.Play();
        }
        else if(sceneName == _sceneNameMG.gameSceneNames[1] || sceneName == _sceneNameMG.gameSceneNames[2])
        {
            _mainBGM.Pause();
            _selectItemBGM.UnPause();
            //_selectItemBGM.Play();
        }
    }
}
