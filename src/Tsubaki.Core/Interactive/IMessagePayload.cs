
namespace Tsubaki.Core.Interactive
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IMessagePayload
    {
        MessagePayloadKinds PayloadKind { get; }
    }
}
