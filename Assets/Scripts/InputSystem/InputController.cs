
using static InputS.PlayerMovement;
using static InputS.UIControl;

namespace InputS
{
    public class InputController
    {
        private static PlayerMovement _inputPlayer = new PlayerMovement();
        private static UIControl _inputUI = new UIControl();

        public static void EnablePlayerInput() => _inputPlayer.Enable();
        public static void DisablePlayerInput() => _inputPlayer.Disable();

        public static void EnableUIInput() => _inputUI.Enable();
        public static void DisableUIInput() => _inputUI.Disable();

        public static void SubscribeOnPlayerInput(IPlayerWASDActions playerMainActions)
        {
            _inputPlayer.PlayerWASD.AddCallbacks(playerMainActions);
        }

        public static void SubscribeOnUIInput(IUIActions uiMainActions)
        {
            _inputUI.UI.AddCallbacks(uiMainActions);
        }

        public static void UnsubscribeOnPlayerInput(IPlayerWASDActions playerMainActions)
        {
            _inputPlayer.PlayerWASD.RemoveCallbacks(playerMainActions);
        }

        public static void UnsubscribeOnUIInput(IUIActions uiMainActions)
        {
            _inputUI.UI.RemoveCallbacks(uiMainActions);
        }
    }
}