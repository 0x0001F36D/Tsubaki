// Author: Viyrex(aka Yuyu)
// Contact: mailto:viyrex.aka.yuyu@gmail.com
// Github: https://github.com/0x0001F36D

namespace Tsubaki.Layer.Core
{
#if DEBUG

    using System;

    public interface IMessageInterfaceDebugSink
    {
        #region Events

        event EventHandler<OrderedEventArgs> OrderedCallback;

        event EventHandler<SaidEventArgs> SaidCallback;

        #endregion Events

        #region Methods

        event EventHandler<Correspond> OnReceivedCorrespond;

        void OnReceived(object sender, Correspond e);

        #endregion Methods
    }

    partial class MessageInterface : IMessageInterfaceDebugSink
    {
        #region Events

        public event EventHandler<Correspond> OnReceivedCorrespond;

        event EventHandler<OrderedEventArgs> IMessageInterfaceDebugSink.OrderedCallback
        {
            add => this._ordered += value;
            remove => this._ordered -= value;
        }

        event EventHandler<SaidEventArgs> IMessageInterfaceDebugSink.SaidCallback
        {
            add => this._said += value;
            remove => this._said -= value;
        }

        #endregion Events

        #region Methods

        void IMessageInterfaceDebugSink.OnReceived(object sender, Correspond e)
        {
            this.OnReceived(sender, e);
            this.OnReceivedCorrespond?.Invoke(sender, e);
        }

        #endregion Methods
    }

#endif
}