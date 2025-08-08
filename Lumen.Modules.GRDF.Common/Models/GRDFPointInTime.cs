namespace Lumen.Modules.GRDF.Common.Models {
	public class GRDFPointInTime {
		public DateTime DateDebut { get; set; }
		public DateTime DateFin { get; set; }
		public DateOnly JourneeGaziere { get; set; }
		public int IndexDebut { get; set; }
		public int IndexFin { get; set; }
		public float VolumeBrutConsomme { get; set; }
		public float EnergieConsomme { get; set; }
		public int VolumeConverti { get; set; }
		public float OutsideTemperature { get; set; }
	}
}
