namespace DotFinglish
{
    public static class Utility
    {
        public static object Convert(this object value, Type t)
        {
            Type underlyingType = Nullable.GetUnderlyingType(t);

            if (underlyingType != null && value == null)
            {
                return null;
            }
            Type basetype = underlyingType == null ? t : underlyingType;
            return System.Convert.ChangeType(value, basetype);
        }

        public static T Convert<T>(this object value)
        {
            return (T)value.Convert(typeof(T));
        }

        public static IEnumerable<(T item, int index)> WithIndex<T>(this IEnumerable<T> self)
            => self.Select((item, index) => (item, index));

        public static IEnumerable<T> AsSingleton<T>(this T item) => new[] { item };

        public static IEnumerable<IEnumerable<T>> CartesianProduct<T>(this IEnumerable<IEnumerable<T>> sequences) =>
            sequences.Aggregate(
                Enumerable.Empty<T>().AsSingleton(),
                (accumulator, sequence) => accumulator.SelectMany(
                    accseq => sequence,
                    (accseq, item) => accseq.Append(item)));
    }
}
