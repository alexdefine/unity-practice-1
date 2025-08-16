using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boot : MonoBehaviour
{
    void Start()
    {
        DataPersistanceManager.Instance.Load();
        SceneManager.LoadScene(1);
    }
}
