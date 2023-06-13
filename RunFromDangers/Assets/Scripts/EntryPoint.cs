using System.Threading.Tasks;
using GameCore;
using GlobalConstants;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EntryPoint : MonoBehaviour
{
    private async void Start()
    {
        Task initialization = Core.Initialization();
        await initialization;

        SceneManager.LoadScene(ScenesNamesConstants.GAME_SCENE);
    }
}
