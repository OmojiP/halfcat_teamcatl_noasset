using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeScene : MonoBehaviour
{
    [SerializeField] private string _sceneName;

    public void Change()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(_sceneName);
    }
}