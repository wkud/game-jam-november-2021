using UnityEngine.SceneManagement;

public static class SceneManagerExtension
{
    public static SceneId GetCurrentScene()
    {
        return (SceneId)SceneManager.GetActiveScene().buildIndex;
    }
}