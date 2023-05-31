using GameCore;
using GlobalConstants;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EntryPoint : MonoBehaviour
{
    private async void Start()
    {
        await Core.Initialization();
        SceneManager.LoadScene(ScenesNamesConstants.GAME_SCENE);
    }
}
