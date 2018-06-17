// Author: Viyrex(aka Yuyu)
// Contact: mailto:viyrex.aka.yuyu@gmail.com
// Github: https://github.com/0x0001F36D

namespace Tsubaki.Core.Interactive
{
    using System;
    using Interactive;

    public sealed class ReceivedEventArgs : EventArgs
    {

        internal ReceivedEventArgs(IMessagePayload payload)
        {
            this.Payload = payload;
        }

        public IMessagePayload Payload { get; }

    }
}