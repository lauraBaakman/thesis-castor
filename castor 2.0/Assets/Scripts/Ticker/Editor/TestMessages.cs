using NUnit.Framework;
using Ticker;

namespace Tests.Ticker
{
    [TestFixture]
    public class MessageTests
    {
        [Test]
        public void CompareTo_SmallerThan()
        {
            Message thisMessage = new Message.InfoMessage("");
            Message otherMessage = new Message.ErrorMessage("");

            int expected = -1;
            int actual = thisMessage.CompareTo(otherMessage);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CompareTo_Equal()
        {
            Message thisMessage = new Message.ErrorMessage("");
            Message otherMessage = new Message.ErrorMessage("");

            int expected = 0;
            int actual = thisMessage.CompareTo(otherMessage);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CompareTo_GreaterThan()
        {
            Message thisMessage = new Message.ErrorMessage("");
            Message otherMessage = new Message.InfoMessage("");

            int expected = +1;
            int actual = thisMessage.CompareTo(otherMessage);

            Assert.AreEqual(expected, actual);
        }
    }
}