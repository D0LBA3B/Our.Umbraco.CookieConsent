using Microsoft.Extensions.Logging;
using NPoco;
using Umbraco.Cms.Infrastructure.Migrations;
using Umbraco.Cms.Infrastructure.Persistence.DatabaseAnnotations;

namespace Our.Umbraco.CookieConsent
{
    //doc: https://docs.umbraco.com/umbraco-cms/13.latest/extending/database
    public class CookieConsentSettingsTable : MigrationBase
    {
        public CookieConsentSettingsTable(IMigrationContext context) : base(context)
        {}

        protected override void Migrate()
        {
            Logger.LogDebug("Running migration {MigrationStep}", "CookieConsentSettingsTable ");

            if (TableExists("CookieConsentSettings") == false)
                Create.Table<CookieConsentSettingsSchema>().Do();
            else
                Logger.LogDebug("The database table {DbTable} already exists, skipping", "CookieConsentSettings");
        }
    }

    [TableName("CookieConsentSettings")]
    [PrimaryKey("Id", AutoIncrement = true)]
    [ExplicitColumns]
    public class CookieConsentSettingsSchema
    {
        [PrimaryKeyColumn(AutoIncrement = true, IdentitySeed = 1)]
        [Column("Id")]
        public int Id { get; set; }

        [Column("SettingsJson")]
        [SpecialDbType(SpecialDbTypes.NVARCHARMAX)]
        public string SettingsJson { get; set; }

        [Column("LastUpdated")]
        [Constraint(Default = "GETDATE()")]
        public DateTime LastUpdated { get; set; }
    }
}
