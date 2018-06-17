// Author: Viyrex(aka Yuyu)
// Contact: mailto:viyrex.aka.yuyu@gmail.com
// Github: https://github.com/0x0001F36D

namespace Tsubaki.Layer.Core
{
    using System.Collections.Generic;
    using System;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Collections;

    [Serializable]
    public sealed class ModuleArguments : INotifyPropertyChanged, IDictionary<string, string>
    {
        #region Constructors

        public ModuleArguments()
        {
            this._map = new Dictionary<string, string>();
            this.Unlock();
        }

        public ModuleArguments(IDictionary<string, string> map)
        {
            this._map = map ?? new Dictionary<string, string>();
            this.IsLock = true;
        }

        #endregion Constructors

        #region Classes

        [Serializable]
        public class ValueAccessException : Exception
        {
            #region Constructors

            public ValueAccessException(string message) : base(message)
            {
            }

            protected ValueAccessException(SerializationInfo info, StreamingContext context) : base(info, context)
            {
            }

            #endregion Constructors
        }

        #endregion Classes

        #region Fields

        private readonly IDictionary<string, string> _map;

        #endregion Fields

        #region Events

        event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged
        {
            add => this._internalPC += value;
            remove => this._internalPC -= value;
        }

        private event PropertyChangedEventHandler _internalPC;

        #endregion Events

        #region Properties

        int ICollection<KeyValuePair<string, string>>.Count => this._map.Count;

        public bool IsLock { get; private set; }

        bool ICollection<KeyValuePair<string, string>>.IsReadOnly => ((IDictionary<string, string>)this._map).IsReadOnly;

        ICollection<string> IDictionary<string, string>.Keys => ((IDictionary<string, string>)this._map).Keys;

        ICollection<string> IDictionary<string, string>.Values => ((IDictionary<string, string>)this._map).Values;

        #endregion Properties

        #region Indexers

        public string this[uint index]
        {
            get
            {
                if (!this.IsLock)
                    goto NotLocked;
                if (index >= this._map.Count)
                    goto Error;

                return ((IList<KeyValuePair<string, string>>)this._map).ElementAt((int)index).Value;

                Error:
                throw new ArgumentOutOfRangeException("index");

                NotLocked:
                throw new ValueAccessException("請先將呼叫 Lock() 函式上鎖");
            }
        }

        public string this[string key]
        {
            get
            {
                if (!this.IsLock)
                    goto NotLocked;
                if (string.IsNullOrWhiteSpace(key))
                    goto Error;

                foreach (var kv in this._map)
                {
                    if (string.Equals(key, kv.Key, StringComparison.CurrentCultureIgnoreCase))
                        return kv.Value;
                }

                Error:
                throw new KeyNotFoundException("key");

                NotLocked:
                throw new ValueAccessException("請先將呼叫 Lock() 函式上鎖");
            }

            set
            {
                if (this.IsLock)
                    throw new ValueAccessException("請先將呼叫 Unlock() 函式解鎖");

                if (!this.IsLock)
                {
                    if (!this.Retrieve(key, out var _))
                    {
                        this._map[key] = value;
                        this._internalPC?.Invoke(this, new PropertyChangedEventArgs(key));
                    }
                }
            }
        }

        #endregion Indexers

        #region Methods

        void IDictionary<string, string>.Add(string key, string value) => this._map.Add(key, value);

        void ICollection<KeyValuePair<string, string>>.Add(KeyValuePair<string, string> item) => ((IDictionary<string, string>)this._map).Add(item);

        void ICollection<KeyValuePair<string, string>>.Clear() => this._map.Clear();

        bool ICollection<KeyValuePair<string, string>>.Contains(KeyValuePair<string, string> item) => ((IDictionary<string, string>)this._map).Contains(item);

        bool IDictionary<string, string>.ContainsKey(string key) => this._map.ContainsKey(key);

        void ICollection<KeyValuePair<string, string>>.CopyTo(KeyValuePair<string, string>[] array, int arrayIndex) => ((IDictionary<string, string>)this._map).CopyTo(array, arrayIndex);

        IEnumerator<KeyValuePair<string, string>> IEnumerable<KeyValuePair<string, string>>.GetEnumerator() => ((IDictionary<string, string>)this._map).GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => ((IDictionary<string, string>)this._map).GetEnumerator();

        public void Lock()
        {
            this.IsLock = true;
        }

        bool IDictionary<string, string>.Remove(string key) => this._map.Remove(key);

        bool ICollection<KeyValuePair<string, string>>.Remove(KeyValuePair<string, string> item) => ((IDictionary<string, string>)this._map).Remove(item);

        public bool Retrieve(string key, out string value)
        {
            value = default;
            if (!this.IsLock)
            {
                foreach (var kv in this._map)
                {
                    if (string.Equals(key, kv.Key, StringComparison.CurrentCultureIgnoreCase))
                    {
                        value = kv.Value;
                        return true;
                    }
                }
            }
            return false;
        }

        bool IDictionary<string, string>.TryGetValue(string key, out string value) => this._map.TryGetValue(key, out value);

        public void Unlock()
        {
            this.IsLock = false;
        }

        #endregion Methods
    }
}