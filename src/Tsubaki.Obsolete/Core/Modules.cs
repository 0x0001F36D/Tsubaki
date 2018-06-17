
namespace Tsubaki.Layer.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Viyrex.RuntimeServices.Callable;
    using System.Collections;
    using Viyrex.RuntimeServices.Callable.Models;

    public sealed class Modules : IEnumerable<Module>
    {
        public static Modules Global
        {
            get
            {
                if (s_instance == null)
                    lock (s_locker)
                        if (s_instance == null)
                            s_instance = new Modules();
                return s_instance;
            }
        }

        private static volatile Modules s_instance;
        private static object s_locker = new object();

        private readonly List<LazyObject<Module>> _modules;

        private Modules()
        {
            this._modules = Constraint<Module>.Collector.Fuzzy().LazyAll().ToList();
        }

        IEnumerator<Module> IEnumerable<Module>.GetEnumerator()
        {
            foreach (var item in _modules)
            {
                yield return item.Instance;
            }

        }
        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable<Module>)this).GetEnumerator();

        public Module this[string moduleName]
        {
            get
            {
                foreach (Module mod in this._modules)
                {
                    if (string.Equals(mod.Name, moduleName, StringComparison.CurrentCultureIgnoreCase))
                        return mod;
                }
                throw new MissingModuleException($"Module name: {moduleName}");
            }
        }
    }

    [Serializable]
    public class MissingModuleException : Exception
    {
        public MissingModuleException() { }
        public MissingModuleException(string message) : base(message) { }
        public MissingModuleException(string message, Exception inner) : base(message, inner) { }
        protected MissingModuleException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }


}
