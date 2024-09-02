using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResultSceneMG : MonoBehaviour
{
    private GameMG_new _gameMG;
    public static bool IsShaked = false;

    [SerializeField] private TextMeshProUGUI _ovenScore;
    [SerializeField] private TextMeshProUGUI _fermentScore;

    // Start is called before the first frame update
    void Start()
    {
        _gameMG = GameObject.FindWithTag("GameManager").GetComponent<GameMG_new>();
        _ovenScore.text = _gameMG.SendBakeStatus();
        _fermentScore.text = _gameMG.score_Ferment;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return) || IsShaked == true)
        {
            IsShaked = false;
            StartCoroutine(_gameMG.LoadSceneAsync("TitleScene"));
        }
    }
}
