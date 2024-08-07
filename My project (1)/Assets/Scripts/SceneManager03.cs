using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager03 : MonoBehaviour
{

    [SerializeField]
    string targetSceneName = "Result";

    void Update()
    {
        if (GameVariableStatic.finishedBattle)
        {
            LoadScene();
        }
    }

    void LoadScene()
    {
        SceneManager.LoadScene(targetSceneName);
    }

}