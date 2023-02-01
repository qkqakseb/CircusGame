using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{ 
    // PlayStart 버튼을 클릭하면 PlayStartScene 이름의 씬으로 넘어간다.
   public void FirstChSceneButton()
    {
        SceneManager.LoadScene("FirstStage");
    }
}
