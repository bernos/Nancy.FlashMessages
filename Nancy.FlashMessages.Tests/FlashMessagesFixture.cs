using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Nancy.FlashMessages.Tests
{
    public class FlashMessagesFixture
    {
        [Fact]
        public void Should_Throw_For_Null_Session()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new FlashMessages(null, new FlashMessagesConfiguration());
            });
        }

        [Fact]
        public void Should_Throw_For_Null_Configuration()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new FlashMessages(new Session.Session(), null);
            });
        }

        [Fact]
        public void AddMessage_Should_Persist_Message()
        {
            var messages = new FlashMessages(new Session.Session(), new FlashMessagesConfiguration());

            messages.AddMessage(FlashMessages.Danger, "Hello world");

            var retrievedMessages = messages.PopMessages(FlashMessages.Danger);

            Assert.Equal("Hello world", retrievedMessages.First());
        }

        [Fact]
        public void Should_Not_Remove_Messages_When_Peeking()
        {
            var messages = new FlashMessages(new Session.Session(), new FlashMessagesConfiguration());

            messages.AddMessage(FlashMessages.Danger, "Hello world");

            var retrievedMessages = messages.PeekMessages(FlashMessages.Danger);

            Assert.Equal("Hello world", messages.PeekMessages(FlashMessages.Danger).First());
        }

        [Fact]
        public void Should_Remove_Messages_When_Popping()
        {
            var messages = new FlashMessages(new Session.Session(), new FlashMessagesConfiguration());

            messages.AddMessage(FlashMessages.Danger, "Hello world");

            var retrievedMessages = messages.PopMessages(FlashMessages.Danger);

            Assert.Null(messages.PopMessages(FlashMessages.Danger));
        }
    }
}
