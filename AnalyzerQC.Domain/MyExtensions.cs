using System.Text;
using System.Text.RegularExpressions;

namespace AnalyzerQC;

public static class MyExtensions
{
    public static string ToSnakeCase(this string name)
    {
        if (string.IsNullOrEmpty(name)) return name;
        StringBuilder parts =new StringBuilder();
        string word = name.Substring(0,1).ToLower();
        for (int i = 1; i < name.Length; i++)
        {
            if (!char.IsUpper(name[i]))
            {
                word+=name[i];
            }
            else
            {
                parts.Append(word).Append("_");
                word = name[i].ToString().ToLower();
            }
        }
        parts.Append(word);
        return parts.ToString();
    }

    
}