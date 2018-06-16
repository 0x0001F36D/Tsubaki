using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Tsubaki.Layer.Presentation;

namespace Tsubaki.Mock.Layer.Presentation
{
    public sealed class MockEntry : Entry
    {
        public new void Say(string message)
            => base.Say(message);

        protected override void OnReceived(object sender, Correspond e)
            => Console.WriteLine(e.Message);
    }
}