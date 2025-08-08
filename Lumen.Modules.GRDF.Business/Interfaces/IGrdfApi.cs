namespace Lumen.Modules.GRDF.Business.Interfaces {
	public interface IGrdfApi {
		Task QueryConsumptionData(string cookie, string pce);
	}
}
