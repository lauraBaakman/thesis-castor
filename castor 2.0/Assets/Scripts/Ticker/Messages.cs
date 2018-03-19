using UnityEngine;
using System;

namespace Ticker
{
    public interface IToTickerMessage
    {
        Message ToTickerMessage();
    }

    /// <summary>
    /// Message, a message send to the ticker.
    /// </summary>
    public abstract class Message : IComparable<Message>
    {
        /// <summary>
        /// The text of the message.
        /// </summary>
        public string Text;

        /// <summary>
        /// The color of the message.
        /// </summary>
        public Color Color;

        DateTime timeStamp;

        public enum PriorityLevel { help = 0, info = 1, warning = 2, error = 3 }

        /// <summary>
        ///  Indicates the priority of the message, if the priority of the message that is being displayed is higher than that of the new message, the new message is ignored.
        /// </summary>
        public PriorityLevel Priority;

        /// <summary>
        /// How long the message should be show in seconds, default is 3s.
        /// </summary>
        public float DecayInS;

        private static float DefaultDecayInS = 3.0f;

        protected Message(PriorityLevel priority)
        {
            DecayInS = DefaultDecayInS;
            Priority = priority;

            timeStamp = DateTime.UtcNow;
        }

        protected Message(string text, Color color, PriorityLevel priority)
            : this(priority)
        {
            Text = text;
            Color = color;
        }

        protected Message(string text, Color color, PriorityLevel priority, float decayInS)
            : this(text, color, priority)
        {
            DecayInS = decayInS;
        }

        protected string BuildMessage(string type, string text)
        {
            string baseFormat = "<b>{0}</b>: {1}";
            return string.Format(baseFormat, type, text);
        }

        public int CompareTo(Message other)
        {
            int priorityComparison = this.Priority.CompareTo(other.Priority);
            if (priorityComparison != 0) return priorityComparison;

            //Older messages go before newer messages
            return -1 * this.timeStamp.CompareTo(other.timeStamp);
        }

        /// <summary>
        /// Info message, i.e. message with information for the user that the user does not need to act on.
        /// </summary>
        public class InfoMessage : Message
        {
            private static readonly Color DefaultColor = new Color(
                19f / 256.0f, 36f / 256.0f, 21f / 256.0f
            );

            /// <summary>
            /// Initializes a new instance of the <see cref="T:Ticker.Message.InfoMessage"/> class.
            /// </summary>
            /// <param name="text">Text of the message.</param>
            public InfoMessage(string text)
                : base(
                    text: text, color: DefaultColor,
                    decayInS: 5.0f, priority: PriorityLevel.info)
            { }

            public override string ToString()
            {
                return string.Format("[InfoMessage: {0}]", Text);
            }
        }

        /// <summary>
        /// Help message, i.e. message with help information for the user.
        /// </summary>
        public class HelpMessage : Message
        {
            private static readonly Color DefaultColor = new Color(
                6.0f / 256.0f, 31.0f / 256.0f, 49.0f / 256.0f
            );

            /// <summary>
            /// Initializes a new instance of the <see cref="T:Ticker.Message.HelpMessage"/> class.
            /// </summary>
            /// <param name="text">Text of the message.</param>
            /// <param name="keyboard">Keyboard shortcut of the operation.</param>
            public HelpMessage(string text, string keyboard)
                : base(PriorityLevel.help)
            {
                Text = BuildMessage(
                    type: "help",
                    text: BuildMessageText(text, keyboard)
                );
                Color = DefaultColor;
            }

            private string BuildMessageText(string text, string keyboard)
            {
                if (keyboard.Length == 0) return text;
                return string.Format("({0}) {1}", keyboard, text);
            }

            public override string ToString()
            {
                return string.Format("[HelpMessage: {0}]", Text);
            }
        }

        /// <summary>
        /// Warning message, i.e. message warning the user that sometehing might be going wrong.
        /// </summary>
        public class WarningMessage : Message
        {
            private static readonly Color DefaultColor = new Color(
                208.0f / 256.0f, 103f / 256.0f, 29.0f / 256.0f
            );

            public WarningMessage(string text)
                : base(text, DefaultColor, PriorityLevel.warning)
            { }

            public override string ToString()
            {
                return string.Format("[WarningMessage: {0}]", Text);
            }
        }

        /// <summary>
        /// Error message, i.e. message notifiying the user of an error that occured.
        /// </summary>
        public class ErrorMessage : Message
        {

            new private static float DefaultDecayInS = 10.0f;

            private static readonly Color DefaultColor = new Color(
                179.0f / 256.0f, 28.0f / 256.0f, 30f / 256.0f
            );

            /// <summary>
            /// Initializes a new instance of the <see cref="T:Ticker.Message.ErrorMessage"/> class.
            /// </summary>
            /// <param name="text">Text of the message</param>
            public ErrorMessage(string text) : base(text, DefaultColor, PriorityLevel.error, DefaultDecayInS) { }

            public override string ToString()
            {
                return string.Format("[ErrorMessage: {0}]", Text);
            }
        }

    }
}