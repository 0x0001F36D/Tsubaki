
namespace Tsubaki.Layer.Core
{
    using System;

    internal sealed class UtteranceIncomingProcessor : IIncomingProcessor<SaidEventArgs>, IIncomingProcessor
    {
        private readonly Modules _modules;

        internal UtteranceIncomingProcessor(Modules modules)
        {
            this._modules = modules;
        }

        void IIncomingProcessor<SaidEventArgs>.Process(object sender, SaidEventArgs e)
        {

        }

        void IIncomingProcessor.Process(object sender, EventArgs e)
        {
            if (e is SaidEventArgs s)
                ((IIncomingProcessor<SaidEventArgs>)this).Process(sender,s);
        }
    }



}
