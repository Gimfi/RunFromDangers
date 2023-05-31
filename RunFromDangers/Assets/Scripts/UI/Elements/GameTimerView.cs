using GameCore;
using Gameplay.Levels;
using TMPro;
using UnityEngine;

namespace UI.Elements
{
    public class GameTimerView : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _timerView;
        [SerializeField] 
        private LevelController _levelController;

        private void Start()
        {
            Core.GameUpdate.UpdateAction += CustomUpdate;
        }

        private void OnDestroy()
        {
            Core.GameUpdate.UpdateAction -= CustomUpdate;
        }

        private void CustomUpdate(float deltaTime)
        {
            _timerView.text = $"Time: {_levelController.GameTimeSec:0} sec.";
        }
    }
}