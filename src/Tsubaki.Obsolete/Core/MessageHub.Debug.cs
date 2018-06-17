// Author: Viyrex(aka Yuyu)
// Contact: mailto:viyrex.aka.yuyu@gmail.com
// Github: https://github.com/0x0001F36D
using System;
using System.Linq;

namespace Tsubaki.Layer.Core
{
#if DEBUG

    public interface IMessageHubDebugSink
    {
        #region Events

        /// <summary>
        /// *Debug-Only* 當接收到 <see cref="Correspond"/> 時引發
        /// </summary>
        event EventHandler<Correspond> OnReceiveCorrespond;

        #endregion Events

        #region Methods

        /// <summary>
        /// *Debug-Only* 取得訊息介面
        /// </summary>
        /// <typeparam name="TInterface"></typeparam>
        /// <returns></returns>
        TInterface GetMessageInterface<TInterface>() where TInterface : MessageInterface;

        #endregion Methods
    }

    partial class MessageHub : IMessageHubDebugSink
    {
        #region Methods

        public TInterface GetMessageInterface<TInterface>() where TInterface : MessageInterface
        {
            var entry = this._entries.OfType<TInterface>().ToArray();
            switch (entry.Length)
            {
                case 1:
                    return entry[0];

                default:
                    return default;
            }
        }

        #endregion Methods

        #region Events

        public event EventHandler<Correspond> OnReceiveCorrespond
        {
            add => _internalConfluence += value;
            remove => _internalConfluence -= value;
        }

        #endregion Events
    }

#endif
}