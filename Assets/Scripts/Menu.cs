using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void playGame() {
        SceneManager.LoadScene("Main");
    }

    public void exitGame() {
        Application.Quit();
    }
}
