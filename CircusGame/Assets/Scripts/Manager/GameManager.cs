using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : SingletonBase<GameManager>
{
    public int CurrentStage = 1;

    public TMP_Text tmp_text;


    public void Awake()
    {
        base.Awake();
    }
    // Start is called before the first frame update
    void Start()
    {
        tmp_text.text = "Stage - " + CurrentStage;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void tmpTextChange()
    {
        tmp_text.text = "Stage - " + CurrentStage;
    }
}
