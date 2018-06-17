using System;

namespace Tsubaki.Core.Interactive.Internal
{
    internal interface IInternalMessageInterface
    {

        event EventHandler<SentEventArgs> InternalSend;

        void OnInternalReceived(object sender, ReceivedEventArgs e);
    }
}