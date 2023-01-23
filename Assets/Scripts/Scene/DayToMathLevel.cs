using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DayToMathLevel : MonoBehaviour
{
    [SerializeField] int sceneBuildIndex;
    
    public void DayToMathQA()
    {
        SceneManager.LoadScene(sceneBuildIndex);
    }
}
