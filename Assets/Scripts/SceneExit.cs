using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneExit : MonoBehaviour
{
    public void LoadGame()
    {
        SceneManager.LoadScene("IntroScene");
    }
}