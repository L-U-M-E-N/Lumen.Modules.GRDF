using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lumen.Modules.GRDF.Business.APIDto {
	public class APIResultEntry {
		public string idPce { get; set; }
		public Releve[] releves { get; set; }
		// public object frequence { get; set; }
	}

	public class Releve {
		public DateTime dateDebutReleve { get; set; }
		public DateTime dateFinReleve { get; set; }
		//public object horodatageConsoH { get; set; }
		public string journeeGaziere { get; set; }
		public int indexDebut { get; set; }
		public int indexFin { get; set; }
		public float volumeBrutConsomme { get; set; }
		public float energieConsomme { get; set; }
		//public object pcs { get; set; }
		public int volumeConverti { get; set; }
		//public object pta { get; set; }
		public string natureReleve { get; set; }
		public string qualificationReleve { get; set; }
		//public object status { get; set; }
		public float coeffConversion { get; set; }
		//public object frequenceReleve { get; set; }
		//public object temperature { get; set; }
	}
}
