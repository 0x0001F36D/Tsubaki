
namespace Tsubaki.Core.Interactive
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Internal;
    public class TsubakiMessageHub : INotifyPropertyChanged
    {
        private void OnReceived(object sender, SentEventArgs e)
        {
            if (this.Status == MessageHubStatus.NonAvailable)
                return;

            if(this.Status == MessageHubStatus.CallbackOnly)
            {
                this._send?.Invoke(sender, new ReceivedEventArgs(e.MessagePayload));
                return;
            }

            if(this.Status == MessageHubStatus.Available)
            {
                throw new NotImplementedException();
            }
        }



        private event EventHandler<ReceivedEventArgs> _send;

        public void Register(MessageInterface messageInterface)
        {
            ((IInternalMessageInterface)messageInterface).InternalSend += this.OnReceived;
            this._send += ((IInternalMessageInterface)messageInterface).OnInternalReceived;
        }
        public void Unregister(MessageInterface messageInterface)
        {
            ((IInternalMessageInterface)messageInterface).InternalSend -= this.OnReceived;
            this._send -= ((IInternalMessageInterface)messageInterface).OnInternalReceived;
        }

        public MessageHubStatus Status { get; private set; }

        public void Switch(MessageHubStatus status)
        {
            if (Enum.IsDefined(typeof(MessageHubStatus), status) && status != this.Status)
            {
                this.Status = status;
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.Status)));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
