// Author: Viyrex(aka Yuyu)
// Contact: mailto:viyrex.aka.yuyu@gmail.com
// Github: https://github.com/0x0001F36D

namespace Tsubaki.Core.Interactive
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Interactive;
    using Internal;

    public class MessageInterface : MarshalByRefObject, IInternalMessageInterface
    {

        event EventHandler<SentEventArgs> IInternalMessageInterface.InternalSend
        {
            add => this._send += value;
            remove => this._send -= value;
        }

        private event EventHandler<SentEventArgs> _send;

        public event EventHandler<ReceivedEventArgs> Received;

        void IInternalMessageInterface.OnInternalReceived(object sender,ReceivedEventArgs e)
        {
            this.Received?.Invoke(sender, e);
            this.OnReceived(sender, e);
        }

        protected virtual void OnReceived(object sender, ReceivedEventArgs e)
        {

        }

        public void Echo(string message)
        {
            var correspond = new CorrespondObject(message);
            var e = new ReceivedEventArgs(correspond);
            this.Received?.Invoke(this, e);
        }

        public void Say(string sentence)
        {
            var utterance = new UtteranceObject(sentence);
            var e = new SentEventArgs(utterance);
            this._send?.Invoke(this, e);
        }

        public void Order(string module, params string [] moduleArgs)
        {
            var command = new CommandObject(module, moduleArgs: moduleArgs ?? Array.Empty<string>());
            var e = new SentEventArgs(command);
            this._send?.Invoke(this, e);
        }
    }
}
