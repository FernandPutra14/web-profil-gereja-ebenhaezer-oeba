using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PKMGerejaEbenhaezer.Domain.ValueObjects;

namespace PKMGerejaEbenhaezer.DataAccess.ValueConverters;

public class NoWaValueConverter : ValueConverter<NoWa, string>
{
    public NoWaValueConverter() 
        : base(noWa => noWa.Value, s => NoWa.Create(s).IsSuccess ? NoWa.Create(s).Value : null, null)
    {
    }
}
