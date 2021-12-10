using System.Runtime.CompilerServices;

namespace GihanSoft.AppBase.Exceptions;

public static class StringArgExceptionHelper
{
    public static void ThrowIfNullOrEmpty(
        string? argument,
        [CallerArgumentExpression("argument")] string? paramName = null)
    {
        if (string.IsNullOrEmpty(argument))
        {
            throw new ArgumentException($"'{paramName}' cannot be null or empty.", nameof(paramName));
        }
    }

    public static void ThrowIfNullOrWhiteSpace(
        string? argument,
        [CallerArgumentExpression("argument")] string? paramName = null)
    {
        if (string.IsNullOrWhiteSpace(argument))
        {
            throw new ArgumentException($"'{paramName}' cannot be null or whitespace.", nameof(paramName));
        }
    }
}