using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{ 
    // PlayStart ��ư�� Ŭ���ϸ� PlayStartScene �̸��� ������ �Ѿ��.
   public void FirstChSceneButton()
    {
        SceneManager.LoadScene("FirstStage");
    }
}
