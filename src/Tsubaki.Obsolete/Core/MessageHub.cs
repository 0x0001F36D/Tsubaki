// Author: Viyrex(aka Yuyu)
// Contact: mailto:viyrex.aka.yuyu@gmail.com
// Github: https://github.com/0x0001F36D
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsubaki.Layer.Core
{
    using Viyrex.RuntimeServices.Callable;

    /// <summary>
    /// 匯集來自 <see cref="MessageInterface"/> 的訊息，並將已處理的訊息分派至對應的 <see cref="MessageInterface"/> 類別
    /// </summary>
    public sealed partial class MessageHub
    {
        #region Constructors

        private MessageHub()
        {
            var entries = Constraint<MessageInterface>.Collector.Fuzzy().NewAll();
            this._entries = new List<IInternalMessageInterface>();

            foreach (IInternalMessageInterface entry in entries)
            {
                this._internalConfluence += entry.OnReceived;
                entry.OrderedCallback += ((IIncomingProcessor<OrderedEventArgs>)ProxyIncomingProcessor.Instance).Process;
                entry.SaidCallback += ((IIncomingProcessor<SaidEventArgs>)ProxyIncomingProcessor.Instance).Process;
                this._entries.Add(entry);
            }
        }

        #endregion Constructors

        #region Fields

        private static volatile MessageHub s_instance;

        private static object s_locker = new object();

        private readonly List<IInternalMessageInterface> _entries;

        #endregion Fields

        #region Events

        private event EventHandler<Correspond> _internalConfluence;

        #endregion Events

        #region Properties

        public static MessageHub Instance
        {
            get
            {
                if (s_instance == null)
                    lock (s_locker)
                        if (s_instance == null)
                            s_instance = new MessageHub();
                return s_instance;
            }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// 廣播回應訊息
        /// </summary>
        /// <param name="correspond"></param>
        internal void BroadcastCorrespond(Correspond correspond)
        {
            this._internalConfluence.Invoke(this, correspond);
        }

        #endregion Methods
    }
}