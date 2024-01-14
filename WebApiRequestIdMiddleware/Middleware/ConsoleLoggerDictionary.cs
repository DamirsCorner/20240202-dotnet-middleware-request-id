namespace WebApiRequestIdMiddleware.Middleware;

public class ConsoleLoggerDictionary<TKey, TValue> : Dictionary<TKey, TValue>
    where TKey : notnull
{
    public override string ToString()
    {
        return string.Join(
            ", ",
            this.AsEnumerable().Select(keyValue => $"{keyValue.Key}: {keyValue.Value}")
        );
    }
}
