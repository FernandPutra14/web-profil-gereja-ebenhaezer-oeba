namespace PKMGerejaEbenhaezer.DataAccess;

public static class IntExtension
{
    private static readonly Dictionary<int, string> IntToRomanDict = new Dictionary<int, string>
    {
        { 1, "I" }, { 4, "IV" }, { 5, "V" }, { 9, "IX" }, { 10, "X" }, 
        { 50, "L" }, { 40, "XL" }, { 90, "XC" }, { 100, "C" }, { 500, "D" }, { 1000, "M" }
    };

    public static string ToRomanNumeral(this int value)
    {
        if (value <= 0) throw new ArgumentOutOfRangeException(nameof(value), "Must be positive");

        if (IntToRomanDict.TryGetValue(value, out var result)) return result;

        string romanNumeral = string.Empty;

        while (value > 0)
        {
            int key = IntToRomanDict.Keys.Reverse().First(x => x <= value);
            int repeat = value / key;
            value %= key;

            for (int i = 0; i < repeat; i++)
                romanNumeral += IntToRomanDict[key];
        }

        return romanNumeral;
    }
}
