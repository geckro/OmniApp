using System.Diagnostics;

namespace CSharpReferences.Core.PerformanceTests;

public class TenMillion
{
    private const int TenMillionCount = 10000000;

    private readonly float[] _floats = new float[TenMillionCount];
    private readonly float[] _floats2 = new float[TenMillionCount];
    private readonly double[] _doubles = new double[TenMillionCount];
    private readonly double[] _doubles2 = new double[TenMillionCount];
    private readonly decimal[] _decimals = new decimal[TenMillionCount];
    private readonly decimal[] _decimals2 = new decimal[TenMillionCount];

    private readonly short[] _shorts = new short[TenMillionCount];
    private readonly short[] _shorts2 = new short[TenMillionCount];
    private readonly int[] _ints = new int[TenMillionCount];
    private readonly int[] _ints2 = new int[TenMillionCount];
    private readonly long[] _longs = new long[TenMillionCount];
    private readonly long[] _longs2 = new long[TenMillionCount];

    private readonly Random _random = new();

    public void InitializeTenMillion()
    {
        for (int i = 0; i < TenMillionCount; i++)
        {
            _floats[i] = (float)_random.NextDouble();
            _floats2[i] = (float)_random.NextDouble();
            _doubles[i] = _random.NextDouble();
            _doubles2[i] = _random.NextDouble();
            _decimals[i] = (decimal)_random.NextDouble();
            _decimals2[i] = (decimal)_random.NextDouble();
        }
    }

    private static void TestPerformance(string testName, Action action)
    {
        Stopwatch stopwatch = Stopwatch.StartNew();
        action();
        stopwatch.Stop();
        Console.WriteLine($"{testName}: Elapsed time = {stopwatch.ElapsedMilliseconds} ms");
    }

    public void TenMilBillionTest()
    {
        TestPerformance("Float addition", () =>
        {
            float sum = _floats.Sum();
        });
        TestPerformance("Double addition", () =>
        {
            double sum = _doubles.Sum();
        });
        TestPerformance("Decimal addition", () =>
        {
            decimal sum = _decimals.Sum();
        });
        TestPerformance("Float multiplication", () =>
        {
            float[] result = new float[TenMillionCount];
            for (int i = 0; i < TenMillionCount; i++)
            {
                result[i] = _floats[i] * _floats2[i];
            }
        });
        TestPerformance("Double multiplication", () =>
        {
            double[] result = new double[TenMillionCount];
            for (int i = 0; i < TenMillionCount; i++)
            {
                result[i] = _doubles[i] * _doubles2[i];
            }
        });
        TestPerformance("Decimal multiplication", () =>
        {
            decimal[] result = new decimal[TenMillionCount];
            for (int i = 0; i < TenMillionCount; i++)
            {
                result[i] = _decimals[i] * _decimals2[i];
            }
        });
    }
}
