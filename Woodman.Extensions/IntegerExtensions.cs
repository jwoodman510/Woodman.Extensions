namespace System
{
    public enum ToArrayOptions
    {
        UseDefaultValue,
        UseIndex
    }

    public static class IntegerExtensions
    {
        public static int[] ToArray(this int? count, ToArrayOptions toArrayOptions = ToArrayOptions.UseDefaultValue)
        {
            return count?.ToArray(toArrayOptions);
        }

        public static int[] ToArray(this int count, ToArrayOptions toArrayOptions = ToArrayOptions.UseDefaultValue)
        {
            return count.ToArray(index => toArrayOptions == ToArrayOptions.UseIndex ? index : 0);
        }

        public static T[] ToArray<T>(this int? count, Func<int, T> indexedValueSelector = null)
        {
            return count?.ToArray(indexedValueSelector);
        }

        public static T[] ToArray<T>(this int count, Func<int, T> indexedValueSelector = null)
        {
            if(count < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(count), count, $"{nameof(count)} must be greater than -1.");
            }

            var valArray = new T[count];

            for(var i = 0; i < count; i++)
            {
                valArray[i] = indexedValueSelector == null ? default(T) : indexedValueSelector(i);
            }

            return valArray;
        }
    }
}
