using System;
using System.Collections.Generic;

namespace Woodman.Extensions.Internal
{
    internal class KeyComparer<TSource, TKey> : EqualityComparer<TSource>
    {
        private readonly Func<TSource, TKey> _keySelector;

        public KeyComparer(Func<TSource, TKey> keySelector)
        {
            _keySelector = keySelector;
        }

        public override bool Equals(TSource x, TSource y)
        {
            return x == null || y == null
                ? x == null && y == null
                : _keySelector(x).Equals(_keySelector(y));
        }

        public override int GetHashCode(TSource obj)
        {
            return obj == null ? base.GetHashCode() : _keySelector(obj).GetHashCode();
        }
    }
}
