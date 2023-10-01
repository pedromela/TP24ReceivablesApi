using System.Globalization;

namespace TP24LendingApi.Services
{
    public class CurrencyConverterService : ICurrencyConverterService
    {
        public CurrencyConverterService() { }

        public double Convert(double value, string currencyFrom, string currencyTo)
        {
            return GetRate(currencyFrom, currencyTo) * value;
        }

        public double GetRate(string currencyFrom, string currencyTo)
        {
            // This implementation would require an external API so for simplification this solution uses a fixed rate for all currencies
            return 1;
        }

        public static List<string?> GetAllCurrencyCodes()
        {
            return CultureInfo
                .GetCultures(CultureTypes.AllCultures)
                .Where(c => !c.IsNeutralCulture)
                .Select(culture =>
                {
                    try
                    {
                        var region = new RegionInfo(culture.Name);
                        return region?.ISOCurrencySymbol;
                    }
                    catch
                    {
                        return null;
                    }
                }).ToList();
        }

    }
}
