﻿using UnityEngine;

namespace RTEditor
{
    /// <summary>
    /// Static class that contains useful functions for handling user input.
    /// </summary>
    public static class InputHelper
    {
        #region Public Static Functions
        /// <summary>
        /// Returns true if the either one (left or right) of the CTRL or COMMAND keys is pressed.
        /// </summary>
        public static bool IsAnyCtrlOrCommandKeyPressed()
        {
            return Input.GetKey(KeyCode.LeftControl) ||
                   Input.GetKey(KeyCode.RightControl) ||
                   Input.GetKey(KeyCode.LeftCommand) ||
                   Input.GetKey(KeyCode.RightCommand);
        }

        /// <summary>
        /// Returns true if either one (left or right) of the SHIFT keys is pressed.
        /// </summary>
        public static bool IsAnyShiftKeyPressed()
        {
            return Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
        }

        /// <summary>
        /// Checks if the left mouse button was pressed during the current frame update.
        /// </summary>
        /// <remarks>
        /// You should call this method from the 'Update' method of a Monobehaviour.
        /// </remarks>
        public static bool WasLeftMouseButtonPressedInCurrentFrame()
        {
            return Input.GetMouseButtonDown((int)MouseButton.Left);
        }

        /// <summary>
        /// Checks if the left mouse button was released during the current frame update.
        /// </summary>
        /// <remarks>
        /// You should call this method from the 'Update' method of a Monobehaviour.
        /// </remarks>
        public static bool WasLeftMouseButtonReleasedInCurrentFrame()
        {
            return Input.GetMouseButtonUp((int)MouseButton.Left);
        }

        /// <summary>
        /// Checks if the right mouse button was pressed during the current frame update.
        /// </summary>
        /// <remarks>
        /// You should call this method from the 'Update' method of a Monobehaviour.
        /// </remarks>
        public static bool WasRightMouseButtonPressedInCurrentFrame()
        {
            return Input.GetMouseButtonDown((int)MouseButton.Right);
        }

        /// <summary>
        /// Checks if the right mouse button was released during the current frame update.
        /// </summary>
        /// <remarks>
        /// You should call this method from the 'Update' method of a Monobehaviour.
        /// </remarks>
        public static bool WasRightMouseButtonReleasedInCurrentFrame()
        {
            return Input.GetMouseButtonUp((int)MouseButton.Right);
        }

        /// <summary>
        /// Checks if the middle mouse button was pressed during the current frame update.
        /// </summary>
        /// <remarks>
        /// You should call this method from the 'Update' method of a Monobehaviour.
        /// </remarks>
        public static bool WasMiddleMouseButtonPressedInCurrentFrame()
        {
            return Input.GetMouseButtonDown((int)MouseButton.Middle);
        }

        /// <summary>
        /// Checks if the middle mouse button was released during the current frame update.
        /// </summary>
        /// <remarks>
        /// You should call this method from the 'Update' method of a Monobehaviour.
        /// </remarks>
        public static bool WasMiddleMouseButtonReleasedInCurrentFrame()
        {
            return Input.GetMouseButtonUp((int)MouseButton.Middle);
        }
        #endregion

        #region Functions added by me

        /// <summary>
        /// Checks if none of the  modifier keys (shift, alt, ctrl, cmd) were pressed. 
        /// </summary>
        /// <returns><c>true</c>, if no modifier was pressed, <c>false</c> otherwise.</returns>
        public static bool IsNoModifierPressed()
        {
            return IsNoShiftKeyPressed()
                && IsNoAltKeyPressed()
                && IsNoCommandKeyPressed()
                && IsNoControlKeyPressed();
        }

        private static bool IsNoShiftKeyPressed()
        {
            return !(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift));
        }

        private static bool IsNoAltKeyPressed()
        {
            return !(Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt));
        }

        private static bool IsNoCommandKeyPressed()
        {
            return !(Input.GetKey(KeyCode.LeftCommand) || Input.GetKey(KeyCode.RightCommand));
        }

        private static bool IsNoControlKeyPressed()
        {
            return !(Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl));
        }

        #endregion
    }
}
