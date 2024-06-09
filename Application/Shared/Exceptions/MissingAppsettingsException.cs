using System.Diagnostics.CodeAnalysis;

namespace Application.Shared.Exceptions;

[ExcludeFromCodeCoverage]
public class MissingAppsettingsException(string[] messages, Exception? innerException)
    : Exception(string.Join(" ", messages), innerException)
{
    public MissingAppsettingsException(string[] messages) : this(messages, null)
    {
    }
}