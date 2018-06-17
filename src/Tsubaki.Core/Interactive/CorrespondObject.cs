
namespace Tsubaki.Core.Interactive
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public sealed class CorrespondObject : IMessagePayload
    {
        public CorrespondObject(string content)
        {
            if (string.IsNullOrWhiteSpace(content))
                throw new ArgumentException("message", nameof(content));
            this.Content = content;
        }
        public string Content { get;  }

        public MessagePayloadKinds PayloadKind => MessagePayloadKinds.Correspond;
    }
}
