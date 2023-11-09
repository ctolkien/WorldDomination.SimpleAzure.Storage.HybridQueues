namespace WorldDomination.SimpleAzure.Storage.HybridQueues;

internal static class Helpers
{
    // REF: https://stackoverflow.com/a/38998370/30674
    /// <summary>
    /// This checks to see if the <see cref="Type"/> is a Simple Type (<see cref="Type.IsPrimitive"/> | <see cref="string"/> | <see cref="decimal"/>).
    /// </summary>
    /// <param name="type">The Type to check.</param>
    /// <remarks>To see the full list of .NET Primitive types: <seealso href="https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/built-in-types-table"/></remarks>
    /// <returns><see langword="true"/> if this is a Simple Type or <see langword="false"/> if it is not.</returns>
    internal static bool IsASimpleType(this Type type) =>
        type.IsPrimitive ||
        type == typeof(string) ||
        type == typeof(decimal);
}
