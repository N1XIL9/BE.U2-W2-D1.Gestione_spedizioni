using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BE.U2_W2_D1.Gestione_spedizioni.Models
{
    public class Clienti
    {
        [Display(Name ="ID")]
        public int IdCliente { get; set; }

        public string Cognome { get; set; }

        public string Nome { get; set; }

        [Display(Name = "Nominativo")]

        public string RagioneSociale { get; set; }


        [Display(Name = "Codice Fiscale")]

        public string CF { get; set; }

        [Display(Name = "P.IVA")]

        public string PIVA { get; set; }

        public string Indirizzo { get; set;}

        public string Telefono { get; set; }

        [Display(Name = "E-mail")]

        public string Email { get; set; }




    }
}