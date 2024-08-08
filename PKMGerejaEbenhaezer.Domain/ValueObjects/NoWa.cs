using PKMGerejaEbenhaezer.Domain.DomainErrors;
using PKMGerejaEbenhaezer.Domain.Shared;
using PKMGerejaEbenhaezer.Domain.ValueObjects.Commons;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace PKMGerejaEbenhaezer.Domain.ValueObjects;

public class NoWa : ValueObject
{
    public const int ValidLength = 12;
    public const string ValidRegexPattern = @"^08[0-9]+$";

    public string Value { get; }

    private NoWa(string noWa)
    {
        Value = noWa;
    }

    public static Result<NoWa> Create(string noWa)
    {
        if (string.IsNullOrEmpty(noWa))
            return NoWaErrors.Empty;

        if (!Regex.IsMatch(noWa, ValidRegexPattern))
            return NoWaErrors.NotValid;

        if (noWa.Length != ValidLength)
            return NoWaErrors.InvalidLength(ValidLength);

        return new NoWa(noWa);
    }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}
