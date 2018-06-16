// Author: Viyrex(aka Yuyu)
// Contact: mailto:viyrex.aka.yuyu@gmail.com
// Github: https://github.com/0x0001F36D
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

using Tsubaki.Layer.Presentation;
using Tsubaki.Layer.Presentation.Internal;
using Viyrex.RuntimeServices.Callable;

namespace Tsubaki.ConsoleDebugger
{
    using Mock.Layer.Presentation;

    internal class Debugger
    {
        #region Fields

        private static readonly Constraint<Entry> _constraint = Constraint<Entry>.Collector;

        #endregion Fields

        #region Methods

        private static void Main(string[] args)
        {
            var myclass = Modules.Global["SampleModule"];
            var argus = new ModuleArguments
            {
                ["Display"] = "Hello"
            };
            myclass.Execute(argus);

            Console.WriteLine(myclass);
            Console.ReadKey();
        }

        #endregion Methods
    }
}