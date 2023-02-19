namespace App.Domain.Enums;

public class Gender : Enumeration
{
    public static Gender Woman = new Gender(1, nameof(Woman).ToLowerInvariant());
    public static Gender Man = new Gender(2, nameof(Man).ToLowerInvariant());
    public static Gender Other = new Gender(3, nameof(Other).ToLowerInvariant());
    public Gender(int id, string name) : base(id, name)
    {
    }

    public static IEnumerable<Gender> List() =>
        new[] { Woman, Man, Other };

    public static Gender FromName(string name)
    {
        var state = List()
            .SingleOrDefault(s => string.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

        if (state == null)
        {
            throw new Exception($"Possible values for RoleEnum: {string.Join(",", List().Select(s => s.Name))}");
        }

        return state;
    }

    public static Gender From(int id)
    {
        var state = List().SingleOrDefault(s => s.Id == id);

        if (state == null)
        {
            throw new Exception($"Possible values for RoleEnum: {string.Join(",", List().Select(s => s.Name))}");
        }

        return state;
    }
}