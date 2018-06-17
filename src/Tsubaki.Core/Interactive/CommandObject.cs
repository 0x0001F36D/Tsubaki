namespace Tsubaki.Core.Interactive
{
    public sealed class CommandObject : IMessagePayload
    {
        public CommandObject(string module, string[] moduleArgs)
        {
            this.Module = module;
            this.ModuleArgs = moduleArgs;
        }

        public MessagePayloadKinds PayloadKind => MessagePayloadKinds.Command;

        public string Module { get;  }
        public string[] ModuleArgs { get;  }
    }
}
