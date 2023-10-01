using System.Globalization;

namespace TP24LendingApi.Services
{
    public interface ICurrencyConverterService
    {
        double Convert(double value, string currencyFrom, string currencyTo);

        double GetRate(string currencyFrom, string currencyTo);
    }
}
