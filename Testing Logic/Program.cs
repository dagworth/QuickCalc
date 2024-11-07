using System;
using QuickCalc.Models;

namespace Hi
{
    static class Testing
    {
        static Dictionary<string, string> cases = new Dictionary<string, string>{
            ["-(2(3+2))(-12-2)^2"] = "-1960",
            ["-*(2(d+2))(-vv-2--*)^2"] = "error",
            ["x=4\r\nxxx"] = "64",
            ["x=4\r\n(2)x3x(2)"] = "192",
            ["0"] = "0",
            [""] = "",
            ["x=10^100\r\nx/2"] = "5E+99",
            ["(2)x3x(2)"] = "error",
            ["x=4\r\n8(-x*.124)"] = "-3.968",
            ["x=-2\r\n-x+8"] = "10",
            ["-(50+-2)"] = "-48",
            ["-(50%2)"] = "0",
            ["-(50%3)"] = "-2",
            ["-(50%(4%3))"] = "0",
            ["-9"] = "-9",
            ["-0"] = "0",
            ["-(9-9)"] = "0",
            ["50%-2"] = "0",
            ["----1"] = "1",
            ["2----4"] = "6",
            ["2--*4"] = "error",
            ["-(50+---2)"] = "-48",
            ["-(50+--+2)"] = "error",
            ["-(50+-*+2)"] = "error",
            [".2"] = "0.2",
            [".+2"] = "error",
            ["2.2+2.3"] = "4.5",
            ["2.2.+.9"] = "error",
            [".4*-.4"] = "-0.16",
            ["."] = "error",
            [".."] = "error",
            ["2..2"] = "error",
            ["2++2"] = "error",
            ["x=5\r\n12(x.5)^2"] = "75",
            [".(8)"] = "error",
            ["..4"] = "error",
            [".4.4"] = "error",
            ["4.4.4"] = "error",
        };

        static void Main(string[] args)
        {
            RunTests();
            while (true) {
                Console.WriteLine(Calculation.Solve(Console.ReadLine()));
            }
        }

        static string TestWriteOut(string equation) {
            for(int i = 0; i < equation.Length; i++){
                Calculation.Solve(equation.Substring(0,i+1));
            }
            string[] a = Calculation.Solve(equation).Split("\r\n");
            return a[a.Length-2];
        }

        static void RunTests()
        {
            int count = 0;
            foreach (KeyValuePair<string,string> a in cases)
            {
                string equation = a.Key;
                string expected = a.Value;
        
                try {
                    string result = TestWriteOut(equation);
                    if (!(result.Equals(expected) || (expected.Equals("error") && result.StartsWith("error")))){
                        Console.WriteLine("(Wrong answer)" + equation.Replace("\r\n", "  ") + "  EXPECTED: " + expected + "  RESULT: " + result);
                        count++;
                    }
                } catch(Exception e) {
                    Console.WriteLine("(TestWriteOut): " + equation.Replace("\r\n", "  "));
                    count++;
                }
            }

            if(count == 0){
                Console.WriteLine("everything passed!");
            }
        }
    }
}