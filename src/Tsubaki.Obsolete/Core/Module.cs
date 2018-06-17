
namespace Tsubaki.Layer.Core
{
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;
    using System.Security.Permissions;
    using System.Collections.Specialized;
    using System.Threading;

    public abstract class Module
    {
        protected Module()
        {
            this.Name = this.GetType().Name;
        }

        public virtual string Name { get; }

        public override string ToString() => $"[{Name}]";

        public abstract void Execute(ModuleArguments moduleArgs);
    }

    
}
