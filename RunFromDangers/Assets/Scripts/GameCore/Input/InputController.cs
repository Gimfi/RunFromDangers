using GameCore.Update;

namespace GameCore.Input
{
    public class InputController
    {
        public float Horizontal { get; private set; }
        public float Vertical { get; private set; }

        private GameUpdate _gameUpdate;

        public void Initialization(GameUpdate gameUpdate)
        {
            _gameUpdate = gameUpdate;

#if UNITY_EDITOR
            _gameUpdate.UpdateAction += Update;
#endif
        }

#if UNITY_EDITOR
        private void Update(float deltaTime)
        {
            PCInput();
        }

        private void PCInput()
        {
            Horizontal = UnityEngine.Input.GetAxis("Horizontal");
            Vertical = UnityEngine.Input.GetAxis("Vertical");
        }
#endif

        public void DeviceInput(float horizontal, float vertical)
        {
            Horizontal = horizontal;
            Vertical = vertical;
        }
    }
}