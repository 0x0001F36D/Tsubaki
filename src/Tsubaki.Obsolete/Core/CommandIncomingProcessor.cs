
namespace Tsubaki.Layer.Core
{
    using System;

    internal class CommandIncomingProcessor : IIncomingProcessor<OrderedEventArgs>, IIncomingProcessor
    {
        private readonly Modules _modules;

        internal CommandIncomingProcessor(Modules modules)
        {
            this._modules = modules;
        }

        void IIncomingProcessor<OrderedEventArgs>.Process(object sender, OrderedEventArgs e)
        {
            if(this._modules[e.Command.ModuleName] is Module module)
            {
                module.Execute(e.Command.Arguments);
            }
        }

        void IIncomingProcessor.Process(object sender, EventArgs e)
        {
            if (e is OrderedEventArgs o)
                ((IIncomingProcessor<OrderedEventArgs>)this).Process(sender, o);
        }
    }



}
