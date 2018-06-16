// Author: Viyrex(aka Yuyu)
// Contact: mailto:viyrex.aka.yuyu@gmail.com
// Github: https://github.com/0x0001F36D

namespace Tsubaki.Layer.Core.Tests
{
    using System;
    using NUnit.Framework;

    using Tsubaki.Layer.Core;
    using Models;
    using System.Diagnostics;

    public class UnitTest
    {
        #region Methods

        private static string[] messages = { "Test" };

        [Conditional("DEBUG")]
        [Test]
        [TestCaseSource(nameof(messages))]
        public void 訊息發送及接收交互測試(string message)
        {
            IMessageHubDebugSink hub = MessageHub.Instance;

            var interface1 = hub.GetMessageInterface<TestMessageInterface>();

            var interface2 = hub.GetMessageInterface<TestMessageInterface2>();
            (interface2 as IMessageInterfaceDebugSink).OnReceivedCorrespond += (sender, e) => Assert.AreEqual(e.Message, message);

            interface1.TestSay(message);
        }

        [Conditional("DEBUG")]
        [Test]
        [TestCaseSource(nameof(messages))]
        public void 訊息發送及接收自我測試(string message)
        {
            var entry = new TestMessageInterface();
            (entry as IMessageInterfaceDebugSink).OnReceivedCorrespond += (sender, e) => Assert.AreEqual(e, message);
            entry.TestSay(message);
        }

        [Conditional("DEBUG")]
        [Test]
        public void 嘗試取得訊息介面實體()
        {
            IMessageHubDebugSink hub = MessageHub.Instance;

            var @interface = hub.GetMessageInterface<TestMessageInterface>();
            Assert.IsInstanceOf<TestMessageInterface>(@interface);
        }

        [Conditional("DEBUG")]
        [Test]
        [TestCaseSource(nameof(messages))]
        public void 驗證訊息埠是否接收到訊息(string message)
        {
            IMessageHubDebugSink hub = MessageHub.Instance;
            hub.OnReceiveCorrespond += (sender, e) => Assert.AreEqual(e.Message, message);

            var @interface = hub.GetMessageInterface<TestMessageInterface>();
            @interface.TestSay(message);
        }

        #endregion Methods
    }
}

namespace Tsubaki.Layer.Core.Tests.Models
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Windows.Forms;
    using Core;

    public class TestMessageInterface : MessageInterface
    {
        #region Methods

        public void TestSay(string message) => base.Say(message);

        protected override void OnReceived(object sender, Correspond e)
        {
        }

        #endregion Methods
    }

    public class TestMessageInterface2 : MessageInterface
    {
        #region Methods

        public TestMessageInterface2()
        {
        }

        public void TestSay(string message) => base.Say(message);

        protected override void OnReceived(object sender, Correspond e)
        {
        }

        #endregion Methods
    }
}