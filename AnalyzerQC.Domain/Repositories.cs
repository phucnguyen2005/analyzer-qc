namespace AnalyzerQC;

public static class Repositories
{

    public static readonly List<Model> Models = [
        new Model(1234, "12345678", "abcd12345"),
        new Model(1235, "efgh5678", "efgh12345"),
    ];

    public static readonly List<Site> Sites =
    [
        new Site("efgh1234", "01234567", "hanoi", "11:30", true),
        new Site("abcdefgh", "87654321", "ninh binh", "1:00", true),
    ];

    public static readonly List<Analyzer> Analyzers =
    [
        new Analyzer(Models[0], Sites[0], "87654321", true),
        new Analyzer(Models[1], Sites[1], "abcdefgh", true),
    ];
}