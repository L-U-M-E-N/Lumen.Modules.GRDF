using Lumen.Modules.GRDF.Business.APIDto;
using Lumen.Modules.GRDF.Business.Interfaces;
using Lumen.Modules.GRDF.Common.Models;
using Lumen.Modules.GRDF.Data;

using System.Text.Json;

namespace Lumen.Modules.GRDF.Business.Implementations {
    public class GrdfApi(GRDFContext context, IHttpClientFactory httpClientFactory) : IGrdfApi {
        public async Task<APIResultEntry> GetDataFromAPI(string cookie, DateOnly begin, DateOnly end, string pce) {
            var client = httpClientFactory.CreateClient();
            var request = new HttpRequestMessage {
                Method = HttpMethod.Get,
                RequestUri = new Uri(
                    $"https://monespace.grdf.fr/api/e-conso/pce/consommation/informatives?dateDebut={begin:yyyy-MM-dd}&dateFin={end:yyyy-MM-dd}&pceList%5B%5D={pce}&pceList%255B%255D={pce}"),
                Headers =
                {
                    { "Accept", "application/json" },
                    { "Cookie", cookie },
                },
            };
            using var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<Dictionary<string, APIResultEntry>>(body)![pce];
        }

        public async Task<Dictionary<string, float>> GetMeteoFromAPI(string cookie, DateOnly begin, DateOnly end, string pce) {
            var nbJours = (end.ToDateTime(TimeOnly.MinValue) - begin.ToDateTime(TimeOnly.MinValue)).TotalDays + 1;
            var client = httpClientFactory.CreateClient();
            var request = new HttpRequestMessage {
                Method = HttpMethod.Get,
                RequestUri = new Uri(
                    $"https://monespace.grdf.fr/api/e-conso/pce/{pce}/meteo?dateFinPeriode={end:yyyy-MM-dd}&nbJours={nbJours}"),
                Headers =
                {
                    { "Accept", "application/json" },
                    { "Cookie", cookie },
                },
            };
            using var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var body = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<Dictionary<string, float>>(body)!;
        }

        public async Task QueryConsumptionData(string cookie, string pce) {
            var maxSubmittedEntry = context.GRDF.Select(d => d.JourneeGaziere).Max();
            if (maxSubmittedEntry == null) {
                throw new Exception("Please create the first entry in database manually");
            }

            var min = (DateOnly)maxSubmittedEntry;
            var max = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(-1));

            var data = await GetDataFromAPI(cookie, min, max, pce);
            var alreadySubmitted = context.GRDF.Where(x => x.JourneeGaziere >= maxSubmittedEntry);
            context.GRDF.RemoveRange(alreadySubmitted);

            var meteo = await GetMeteoFromAPI(cookie, min, max, pce);

            context.GRDF.AddRange(data.releves.Select((r) => {
                return new GRDFPointInTime {
                    DateDebut = r.dateDebutReleve.ToUniversalTime(),
                    DateFin = r.dateFinReleve.ToUniversalTime(),
                    JourneeGaziere = DateOnly.ParseExact(r.journeeGaziere, "yyyy-MM-dd"),
                    IndexDebut = r.indexDebut,
                    IndexFin = r.indexFin,
                    VolumeBrutConsomme = r.volumeBrutConsomme,
                    EnergieConsomme = r.energieConsomme,
                    VolumeConverti = r.volumeConverti,
                    OutsideTemperature = meteo[r.journeeGaziere],
                };
            }));

            await context.SaveChangesAsync();
        }
    }
}
