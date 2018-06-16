// Author: Viyrex(aka Yuyu)
// Contact: mailto:viyrex.aka.yuyu@gmail.com
// Github: https://github.com/0x0001F36D
using System;
using System.Text;
using System.Threading.Tasks;

namespace Tsubaki.Layer.Core
{
    internal interface IInternalMessageInterface
    {
        #region Events

        event EventHandler<OrderedEventArgs> OrderedCallback;

        event EventHandler<SaidEventArgs> SaidCallback;

        #endregion Events

        #region Methods

        void OnReceived(object sender, Correspond e);

        #endregion Methods
    }
}