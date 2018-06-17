using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tsubaki.Core.Interactive
{
    public sealed class UtteranceObject : IMessagePayload
    {
        public MessagePayloadKinds PayloadKind => MessagePayloadKinds.Utterance;

        public string Sentence { get; }

        public UtteranceObject(string sentence)
        {
            this.Sentence = sentence;
        }
    }
}
