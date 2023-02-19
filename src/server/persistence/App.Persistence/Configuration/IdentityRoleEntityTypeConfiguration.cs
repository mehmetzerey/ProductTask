namespace App.Persistence.Configuration;

class IdentityRoleEntityTypeConfiguration : IEntityTypeConfiguration<ApplicationRole>
{
    public void Configure(EntityTypeBuilder<ApplicationRole> roleConfiguration)
    {
        roleConfiguration.HasData(RoleEnum.List().ToArray());

    }
}
