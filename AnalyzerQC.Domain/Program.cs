// See https://aka.ms/new-console-template for more information
namespace AnalyzerQC;
using System;
public class Program
{
    public static void Main(String[] args)
    {
        string name = "Id";
        Console.WriteLine(name.ToSnakeCase());
    }
}