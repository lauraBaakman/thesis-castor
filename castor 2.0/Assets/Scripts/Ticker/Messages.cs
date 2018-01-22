using UnityEngine;

namespace Ticker
{
    /// <summary>
    /// Message, a message send to the ticker.
    /// </summary>
    public abstract class Message
    {
        /// <summary>
        /// The text of the message.
        /// </summary>
        public string Text;

        /// <summary>
        /// The color of the message.
        /// </summary>
        public Color Color;

        /// <summary>
        /// How long the message should be show in seconds, default is 3s.
        /// </summary>
        public float DecayInS = DefaultDecayInS;

        private static float DefaultDecayInS = 3.0f;

        protected Message() { }

        protected Message(string text, Color color)
        {
            Text = text;
            Color = color;
        }

        protected Message(string text, Color color, float decayInS){
            Text = text;
            Color = color;
            DecayInS = decayInS;
        }

        protected string BuildMessage(string type, string text)
        {
            string baseFormat = "<b>{0}</b>: {1}";
            return string.Format(baseFormat, type, text);
        }

        /// <summary>
        /// Info message, i.e. message with information for the user that the user does not need to act on.
        /// </summary>
        public class InfoMessage : Message
        {
            private static readonly Color DefaultColor = new Color(
                0.6352941176f, 0.9019607843f, 0.3843137255f
            );

            /// <summary>
            /// Initializes a new instance of the <see cref="T:Ticker.Message.InfoMessage"/> class.
            /// </summary>
            /// <param name="text">Text of the message.</param>
            public InfoMessage(string text) : base(text, DefaultColor, 5.0f) { }
        }

        /// <summary>
        /// Help message, i.e. message with help information for the user.
        /// </summary>
        public class HelpMessage : Message
        {
            private static readonly Color DefaultColor = new Color(
                0.3098039216f, 0.3647058824f, 0.8470588235f
            );

            /// <summary>
            /// Initializes a new instance of the <see cref="T:Ticker.Message.HelpMessage"/> class.
            /// </summary>
            /// <param name="text">Text of the message.</param>
            /// <param name="keyboard">Keyboard shortcut of the operation.</param>
            public HelpMessage(string text, string keyboard)
            {
                Text = BuildMessage(
                    type: "help",
                    text: BuildMessageText(text, keyboard)
                );
                Color = DefaultColor;
            }

            private string BuildMessageText(string text, string keyboard)
            {
                if(keyboard.Length == 0) return text;
                return string.Format("({0}) {1}", keyboard, text);
            }
        }

        /// <summary>
        /// Warning message, i.e. message warning the user that sometehing might be going wrong.
        /// </summary>
        public class WarningMessage : Message
        {
            private static readonly Color DefaultColor = new Color(
                0.9882352941f, 0.8862745098f, 0.2392156863f
            );

            public WarningMessage(string text) : base(text, DefaultColor) { }
        }

        /// <summary>
        /// Error message, i.e. message notifiying the user of an error that occured.
        /// </summary>
        public class ErrorMessage : Message
        {

            private static readonly Color DefaultColor = new Color(
                0.9607843137f, 0.4156862745f, 0.5058823529f
            );

            /// <summary>
            /// Initializes a new instance of the <see cref="T:Ticker.Message.ErrorMessage"/> class.
            /// </summary>
            /// <param name="text">Text of the message</param>
            public ErrorMessage(string text) : base(text, DefaultColor) { }
        }

    }
}