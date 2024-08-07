using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager02 : MonoBehaviour
{

    [SerializeField]
    string targetSceneName = "Title";

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            LoadScene();
        }
    }

    void LoadScene()
    {
        SceneManager.LoadScene(targetSceneName);
    }

}