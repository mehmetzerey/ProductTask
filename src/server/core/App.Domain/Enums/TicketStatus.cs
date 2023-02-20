namespace App.Domain.Enums;

public class TicketStatus : Enumeration
{
    public static TicketStatus IslemYapildi = new TicketStatus(1, nameof(IslemYapildi).ToLowerInvariant());
    public static TicketStatus IslemYapilmadi = new TicketStatus(2, nameof(IslemYapilmadi).ToLowerInvariant());
    public static TicketStatus Silinmis = new TicketStatus(3, nameof(Silinmis).ToLowerInvariant());
    public TicketStatus(int id, string name) : base(id, name)
    {
    }

    public static IEnumerable<TicketStatus> List() =>
        new[] { IslemYapildi, IslemYapilmadi, Silinmis };

    public static TicketStatus FromName(string name)
    {
        var state = List()
            .SingleOrDefault(s => string.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

        if (state == null)
        {
            throw new Exception($"Possible values for RoleEnum: {string.Join(",", List().Select(s => s.Name))}");
        }

        return state;
    }

    public static TicketStatus From(int id)
    {
        var state = List().SingleOrDefault(s => s.Id == id);

        if (state == null)
        {
            throw new Exception($"Possible values for RoleEnum: {string.Join(",", List().Select(s => s.Name))}");
        }

        return state;
    }
}
