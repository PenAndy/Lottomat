using System;
using TrendChartSDK.TrendChartManager;

namespace TrendChartSDK
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        static void Main()
        {
            Console.WriteLine("***********************应用程序的主入口点***********************");

            TrendChartMain _main = new TrendChartMain();
            _main.GetTrendChartList();

            Console.ReadKey();
        }
    }
}
