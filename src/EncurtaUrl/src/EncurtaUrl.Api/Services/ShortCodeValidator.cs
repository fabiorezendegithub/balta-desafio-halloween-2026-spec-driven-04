namespace EncurtaUrl.Api.Services;

public interface IShortCodeValidator
{
    ShortCodeValidationResult Validate(string code);
}

public sealed record ShortCodeValidationResult(bool IsValid, string? ErrorMessage)
{
    public static ShortCodeValidationResult Valid { get; } = new(true, null);
}

public sealed class ShortCodeValidator : IShortCodeValidator
{
    private const int MinimumLength = 3;
    private const int MaximumLength = 32;

    public ShortCodeValidationResult Validate(string code)
    {
        if (string.IsNullOrWhiteSpace(code))
        {
            return new(false, "Informe um código personalizado com pelo menos 3 caracteres.");
        }

        if (code.Length is < MinimumLength or > MaximumLength)
        {
            return new(false, "O código personalizado deve ter entre 3 e 32 caracteres.");
        }

        if (code.Any(character => !IsAllowed(character)))
        {
            return new(false, "O código personalizado deve conter somente letras, números, hífen ou underscore ASCII.");
        }

        if (code.Equals("urls", StringComparison.OrdinalIgnoreCase)
            || code.Equals("openapi", StringComparison.OrdinalIgnoreCase))
        {
            return new(false, "Esse código personalizado é reservado e não está disponível.");
        }

        return ShortCodeValidationResult.Valid;
    }

    private static bool IsAllowed(char character)
        => character is >= 'A' and <= 'Z'
            or >= 'a' and <= 'z'
            or >= '0' and <= '9'
            or '-'
            or '_';
}
