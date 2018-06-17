
namespace Tsubaki.Layer.Core
{
    using System;

    internal interface IIncomingProcessor<TEventArgs> where TEventArgs : EventArgs
    {
        void Process(object sender, TEventArgs e);
    }
    internal interface IIncomingProcessor
    {
        void Process(object sender, EventArgs e);
    }



}
