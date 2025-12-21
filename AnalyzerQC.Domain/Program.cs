// See https://aka.ms/new-console-template for more information
namespace AnalyzerQC;
using System;
public class Program
{
    static void Main(string[] args)
    {
        Site site = new Site( "nguyen bach phuc", "12345678", "ha noi", "12:30", true);
        Console.WriteLine(site.SiteName+" "+site.SiteCode+" "+site.Address);
        site.Update("bach phuc", "Ninh Binh", "10:30");
        Console.WriteLine(site.SiteName+" "+site.SiteCode+" "+site.Address);
    }
}