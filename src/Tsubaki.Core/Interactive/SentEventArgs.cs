// Author: Viyrex(aka Yuyu)
// Contact: mailto:viyrex.aka.yuyu@gmail.com
// Github: https://github.com/0x0001F36D


namespace Tsubaki.Core.Interactive
{
    using System;
    public sealed class SentEventArgs : EventArgs
    {
        public SentEventArgs(IMessagePayload messagePayload)
        {
            this.MessagePayload = messagePayload;
        }

        public IMessagePayload MessagePayload { get; }
    }
}