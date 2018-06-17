
namespace Tsubaki.Layer.Core
{
    using System;

    internal class SampleModule : Module
    {
        public SampleModule()
        {
        }
        
        public override void Execute(ModuleArguments arguments)
        {
            if (arguments.Retrieve("display", out var v))
            {
                Console.WriteLine(v);
            }
            else
                throw new NotSupportedException();
        }
        // public override string Name => "Test";
    }

    
}
