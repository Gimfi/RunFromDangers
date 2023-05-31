using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameCore.Update
{
    public class GameUpdate
    {
        public event Action<float> UpdateAction;

        private GameUpdaterView _view;

        public void Initialization()
        {
            SceneManager.sceneLoaded += CreateGameUpdater;
        }

        private void CreateGameUpdater(Scene scene, LoadSceneMode loadSceneMode)
        {
            if (_view == null)
            {
                GameObject gameObject = new GameObject("GameUpdaterView");
                _view = gameObject.AddComponent<GameUpdaterView>();
                _view.UpdateAaction += (deltaTime) =>
                {
                    UpdateAction?.Invoke(deltaTime);
                };
            }
        }
    }
}