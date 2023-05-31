using Gameplay.Levels;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Game
{
    public class GameMenu : MonoBehaviour
    {
        [SerializeField] private LevelController _levelController;
        [SerializeField] private GameObject _gameMenuGameObject;
        [SerializeField] private GameObject _optionGameObject;
        [SerializeField] private GameObject _joystickGameObject;
        [SerializeField] private Button _playAgaineButton;
        [SerializeField] private Button _optionButton;

        public void Start()
        {
            _optionButton.onClick.AddListener(() =>
            {
                _optionGameObject.SetActive(true);
                gameObject.SetActive(false);
            });

            _playAgaineButton.onClick.AddListener(() =>
            {
                _gameMenuGameObject.SetActive(false);
                _joystickGameObject.SetActive(true);
                _levelController.StartGame();
            });

            AddListeners();
        }

        private void AddListeners()
        {
            _levelController.OnGameEnded += GameEnded;
        }

        private void OnDestroy()
        {
            RemoveListeners();
        }

        private void RemoveListeners()
        {
            _levelController.OnGameEnded -= GameEnded;
        }

        private void GameEnded()
        {
            _gameMenuGameObject.SetActive(true);
            _joystickGameObject.SetActive(false);
        }
    }
}