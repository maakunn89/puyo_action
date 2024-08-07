using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager01 : MonoBehaviour
{

    [SerializeField]
    string targetSceneName = "Battle";

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            GameVariableStatic.finishedBattle = false;
            LoadScene();
        }
    }

    void LoadScene()
    {
        SceneManager.LoadScene(targetSceneName);
    }

}