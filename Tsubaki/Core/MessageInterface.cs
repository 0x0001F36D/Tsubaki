// Author: Viyrex(aka Yuyu)
// Contact: mailto:viyrex.aka.yuyu@gmail.com
// Github: https://github.com/0x0001F36D

namespace Tsubaki.Layer.Core
{
    using System;
    using System.Collections.Generic;

    public interface IOutgoingPayload
    {
    }

    public struct Command : IOutgoingPayload
    {
        #region Constructors

        public Command(string setalliteName, ModuleArguments arguments)
        {
            if (string.IsNullOrWhiteSpace(setalliteName))
                throw new ArgumentException("message", nameof(setalliteName));
            if (arguments == null)
                throw new ArgumentNullException(nameof(arguments));
            this.ModuleName = setalliteName;
            this.Arguments = arguments;
        }

        #endregion Constructors

        #region Properties

        public ModuleArguments Arguments { get; }

        public string ModuleName { get; }

        #endregion Properties
    }

    public struct NaturalLanguege : IOutgoingPayload
    {
        #region Constructors

        public NaturalLanguege(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
                throw new ArgumentException("message", nameof(message));
            this.Utterance = message;
        }

        #endregion Constructors

        #region Properties

        public string Utterance { get; }

        #endregion Properties
    }

    public class Correspond
    {
        #region Properties

        public string Message { get; set; }

        #endregion Properties
    }

    /// <summary>
    /// 提供與 Tsubaki 交互訊息的一般觀點。 呼叫 <see cref="Say(string)"/> 與 <seealso cref="Order(string,
    /// IDictionary{string, string})"/> 可分別將自然語言及指令發送至 <see cref="MessageHub"/> 之消息處理機制進行處理， 實作此類別之
    /// <see cref="OnReceived(object, Correspond)"/> 即可接收來自消息處理機制的訊息，這是 <see langword="abstract"/> 類別。
    /// </summary>
    public abstract partial class MessageInterface : IInternalMessageInterface
    {
        #region Events

        #region Events

        event EventHandler<OrderedEventArgs> IInternalMessageInterface.OrderedCallback
        {
            add => this._ordered += value;
            remove => this._ordered -= value;
        }

        event EventHandler<SaidEventArgs> IInternalMessageInterface.SaidCallback
        {
            add => this._said += value;
            remove => this._said -= value;
        }

        #endregion Events

        private event EventHandler<OrderedEventArgs> _ordered;

        private event EventHandler<SaidEventArgs> _said;

        #endregion Events

        #region Methods

        #region Methods

        void IInternalMessageInterface.OnReceived(object sender, Correspond e)
            => this.OnReceived(sender, e);

        #endregion Methods

        /// <summary>
        /// 當接收到訊息時引發
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected abstract void OnReceived(object sender, Correspond e);

        /// <summary>
        /// 引發 <seealso cref="IInternalMessageInterface.OrderedCallback"/> 事件
        /// </summary>
        /// <param name="satelliteName"></param>
        /// <param name="arguments"></param>
        protected virtual void Order(string satelliteName, IDictionary<string, string> arguments)
            => this._ordered?.Invoke(this, new OrderedEventArgs(new Command(satelliteName, new ModuleArguments(arguments))));

        /// <summary>
        /// 引發 <seealso cref="IInternalMessageInterface.SaidCallback"/> 事件
        /// </summary>
        /// <param name="utterance"></param>
        protected virtual void Say(string utterance)
            => this._said?.Invoke(this, new SaidEventArgs(new NaturalLanguege(utterance)));

        #endregion Methods
    }

    public sealed class OrderedEventArgs : EventArgs
    {
        #region Constructors

        public OrderedEventArgs(Command command)
        {
            this.Command = command;
        }

        #endregion Constructors

        #region Properties

        public Command Command { get; }

        #endregion Properties
    }

    public sealed class SaidEventArgs : EventArgs
    {
        #region Constructors

        public SaidEventArgs(NaturalLanguege naturalLanguege)
        {
            this.NaturalLanguege = naturalLanguege;
        }

        #endregion Constructors

        #region Properties

        public NaturalLanguege NaturalLanguege { get; }

        #endregion Properties
    }
}