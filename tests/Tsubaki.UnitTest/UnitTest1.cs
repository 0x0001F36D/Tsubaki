using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tsubaki.UnitTest
{

    using NUnit.Framework;
    using Core.Interactive;
    using Core.Interactive;
    [TestFixture]
    public class UnitTest
    {

        public static string[] TestMessage = { "Test-Message" };

        [Test]
        [TestCaseSource(nameof(TestMessage))]
        public void EchoTest(string message)
        {
            var mi = new MessageInterface(name: "Test");
            mi.Received += (sender, e) =>
            {
                Assert.AreSame(sender, mi);
                Assert.AreEqual(message, ((CorrespondObject)e.Payload).Content);
            };

            mi.Echo(message);
        }


        [Test]
        [TestCaseSource(nameof(TestMessage))]
        public void InteractiveTest(string message)
        {
            var hub = new TsubakiMessageHub();
            var mi_1 = new MessageInterface();
            var mi_2 = new MessageInterface();

            mi_2.Received += (sender, e) =>
            {
                switch (e.Payload.PayloadKind)
                {
                    case MessagePayloadKinds.Command:
                        Assert.AreEqual((e.Payload as CommandObject).Module, message);
                        break;

                    case MessagePayloadKinds.Utterance:
                        Assert.AreEqual((e.Payload as UtteranceObject).Sentence, message);
                        break;

                    case MessagePayloadKinds.Correspond:
                        Assert.AreSame(sender, mi_1);
                        Assert.AreEqual((e.Payload as CorrespondObject).Content, message);
                        break;

                    default:
                        break;
                }
            };

            hub.Register(mi_1);
            hub.Register(mi_2);

            hub.Switch(MessageHubStatus.CallbackOnly);

            mi_1.Say(message);
            mi_1.Order(message);



        }


    }
}
