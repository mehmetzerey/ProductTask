namespace App.Domain.Enums;

public class RoleEnum : Enumeration
{
    public static RoleEnum Member = new RoleEnum(2, nameof(Member).ToLowerInvariant());
    public static RoleEnum Admin = new RoleEnum(1, nameof(Admin).ToLowerInvariant());
    public RoleEnum(int id, string name) : base(id, name)
    {
    }

    public static IEnumerable<RoleEnum> List() =>
        new[] { Member, Admin };

    public static RoleEnum FromName(string name)
    {
        var state = List()
            .SingleOrDefault(s => string.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

        if (state == null)
        {
            throw new Exception($"Possible values for RoleEnum: {string.Join(",", List().Select(s => s.Name))}");
        }

        return state;
    }

    public static RoleEnum From(int id)
    {
        var state = List().SingleOrDefault(s => s.Id == id);

        if (state == null)
        {
            throw new Exception($"Possible values for RoleEnum: {string.Join(",", List().Select(s => s.Name))}");
        }

        return state;
    }
}
