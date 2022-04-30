using System;
using NSTester;
using NSReader;
using NSWriter;

namespace NSMainProgramm
{
    class Program
    {
        static int Solution(string[] args)
        {
            return 0;
        }

        static void Main(string[] args)
        {
            TestUnit[] tests = {
                new TestUnit(
                    x =>
                    {
                        return 0;
                    },
                    "", "",
                    ""),
            };
            #if RUN
                // Solution(null);
                Test.ExecuteSmallTests(tests);
                return;
            #else
                Test.RunSmallTests(tests);
            #endif
        }
    }
}