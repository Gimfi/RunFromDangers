using System.Threading.Tasks;
using GameCore.Input;
using GameCore.Update;

namespace GameCore
{
    public static class Core
    {
        public static InputController InputController;
        public static GameUpdate GameUpdate;

        public static async Task Initialization()
        {
            CreateObjects();
            InitializationObjects();

            //Can add some loading logic
            await Task.Delay(1000);
        }

        private static void CreateObjects()
        {
            InputController = new InputController();
            GameUpdate = new GameUpdate();
        }

        private static void InitializationObjects()
        {
            GameUpdate.Initialization();
            InputController.Initialization(GameUpdate);
        }
    }
}