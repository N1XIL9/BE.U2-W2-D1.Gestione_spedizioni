using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace BE.U2_W2_D1.Gestione_spedizioni.Models
{
    public class Spedizioni
    {
        [Display(Name = "ID")]
        public int IdSpedizione { get; set; }

        public int IdCliente { get; set; }

        public string Mittente { get; set; }

        [Display(Name = "Data di spedizione")]

        public DateTime DataSpedizione { get; set; }

        public string Peso { get; set; }

        public string Destinatario { get; set; }

        [Display(Name = "Indirizzo del destinatario")]

        public string IndirizzoDestinatario { get; set; }

        [Display(Name = "Città del destinatario")]

        public string CittaDestinatario { get; set; }

        [Display(Name = "Costi Spedizione")]

        public string CostiSpedizione { get; set; }


        [Display(Name = "Data di consegna")]

        public DateTime DataConsegna { get; set; }

        [Display(Name = "Stato consegna")]

        public string Aggiornamento { get; set; }
    }
}