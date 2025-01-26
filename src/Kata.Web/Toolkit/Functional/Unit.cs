namespace Kata.Web.Toolkit.Functional;

public sealed record Unit
{
    private static readonly Unit _instance = new();
    private Unit() { }


    public static Unit U() => _instance;

    public static Unit U(Action a)
    {
        a();
        return _instance;
    }
}
