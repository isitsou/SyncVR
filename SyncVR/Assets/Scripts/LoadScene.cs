using UnityEngine;

public class LoadScene : MonoBehaviour
{
    public void LoadSceneByIndex(int i)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(i);
    }
    
}
