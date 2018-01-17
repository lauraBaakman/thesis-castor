using UnityEngine;

namespace Ticker
{
    public abstract class Message
    {
        public string Text;
        public Color Color;
        public float DecayInS;

        private static float DefaultDecayInS = 3.0f;

        protected Message(){
            DecayInS = DefaultDecayInS;
        }

        protected string BuildMessage(string type, string text)
        {
            string baseFormat = "<b>{0}</b>: {1}";
            return string.Format(baseFormat, type, text);
        }

        public class InfoMessage : Message
        {
            private static readonly Color DefaultColor = new Color(
                0.6352941176f, 0.9019607843f, 0.3843137255f
            );

            public InfoMessage(string text, string keyboard) : base()
            {
                Text = BuildMessage(
                    type: "info",
                    text: BuildMessageText(text, keyboard)
                );
                Color = DefaultColor;
            }

            private string BuildMessageText(string text, string keyboard)
            {
                return string.Format("({0}) {1}", keyboard, text);
            }
        }

        public class HelpMessage : Message
        {
            private static readonly Color DefaultColor = new Color(
                0.3098039216f, 0.3647058824f, 0.8470588235f
            );

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
                return string.Format("({0}) {1}", keyboard, text);
            }
        }

        public class WarningMessage : Message
        {
            private static readonly Color DefaultColor = new Color(
                0.9882352941f, 0.8862745098f, 0.2392156863f
            );

            public WarningMessage(string text)
            {
                Text = BuildMessage(
                    type: "warning",
                    text: text
                );
                Color = DefaultColor;
            }
        }

        public class ErrorMessage : Message
        {

            private static readonly Color DefaultColor = new Color(
                0.9607843137f, 0.4156862745f, 0.5058823529f
            );

            public ErrorMessage(string text)
            {
                Text = BuildMessage(
                    type: "error",
                    text: text
                );
                Color = DefaultColor;
            }
        }

    }
}