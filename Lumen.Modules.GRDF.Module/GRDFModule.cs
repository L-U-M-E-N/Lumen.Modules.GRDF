using Lumen.Modules.Sdk;
using Lumen.Modules.GRDF.Data;

using Microsoft.EntityFrameworkCore;
using Lumen.Modules.GRDF.Business.Interfaces;
using Lumen.Modules.GRDF.Business.Implementations;

namespace Lumen.Modules.GRDF.Module {
	public class GRDFModule(IEnumerable<ConfigEntry> configEntries, ILogger<LumenModuleBase> logger, IServiceProvider provider) : LumenModuleBase(configEntries, logger, provider) {
		public const string PCE_NUMBER = nameof(PCE_NUMBER);
		public static string PCENumber = null!;
		private string GetPCENumberFromConfig() {
			var configEntry = configEntries.FirstOrDefault(x => x.ConfigKey == PCE_NUMBER);
			if (configEntry is null || configEntry.ConfigValue is null) {
				logger.LogError($"[{nameof(PCE_NUMBER)}] Config key \"{PCE_NUMBER}\" is missing!");
			}

			return configEntry.ConfigValue;
		}

		public override Task InitAsync(LumenModuleRunsOnFlag currentEnv) {
			PCENumber = GetPCENumberFromConfig();

			return Task.CompletedTask;
		}

		public override async Task RunAsync(LumenModuleRunsOnFlag currentEnv, DateTime date) {
			try {
				logger.LogTrace($"[{nameof(GRDFModule)}] Running tasks ...");
				throw new NotImplementedException();
			} catch (Exception ex) {
				logger.LogError(ex, $"[{nameof(GRDFModule)}] Error when running tasks.");
			}
		}

		public override bool ShouldRunNow(LumenModuleRunsOnFlag currentEnv, DateTime date) {
			return false;
		}

		public override Task ShutdownAsync() {
			// Nothing to do
			return Task.CompletedTask;
		}

		public static new void SetupServices(LumenModuleRunsOnFlag currentEnv, IServiceCollection serviceCollection, string? postgresConnectionString) {
			if (currentEnv == LumenModuleRunsOnFlag.API) {
				serviceCollection.AddDbContext<GRDFContext>(o => o.UseNpgsql(postgresConnectionString, x => x.MigrationsHistoryTable("__EFMigrationsHistory", GRDFContext.SCHEMA_NAME)));
				serviceCollection.AddTransient<IGrdfApi, GrdfApi>();
				serviceCollection.AddHttpClient();
			}
		}

		public override Type GetDatabaseContextType() {
			return typeof(GRDFContext);
		}
	}
}
