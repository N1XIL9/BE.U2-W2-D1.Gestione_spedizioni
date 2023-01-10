using BE.U2_W2_D1.Gestione_spedizioni.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls.WebParts;

namespace BE.U2_W2_D1.Gestione_spedizioni.Controllers
{
    public class GestioneController : Controller
    {
        // GET: Gestione
        public ActionResult Privati()
        {
            List<Clienti> listaClienti = new List<Clienti>();

            SqlConnection sql = Connessioni.GetConnection();
            sql.Open();

            SqlCommand com = Connessioni.GetCommand("SELECT * FROM CLIENTI WHERE TipoCliente = 'Privato' ", sql);
            SqlDataReader reader  = com.ExecuteReader();

            while (reader.Read())
            {
                Clienti c = new Clienti();
                
                
                    c.IdCliente = Convert.ToInt32(reader["IdCliente"]);
                    c.Cognome = reader["Cognome"].ToString();
                    c.Nome = reader["Nome"].ToString();
                    c.CF = reader["CodiceFiscale"].ToString();
                    c.Indirizzo = reader["Residenza_SedeLegale"].ToString();
                    c.Telefono = reader["Telefono"].ToString();
                    c.Email = reader["email"].ToString();
                    listaClienti.Add(c);    

                
            }sql.Close();

            return View(listaClienti);
        }


        public ActionResult Aziende()
        {
            List<Clienti> listaClienti = new List<Clienti>();

            SqlConnection sql = Connessioni.GetConnection();
            sql.Open();

            SqlCommand com = Connessioni.GetCommand("SELECT * FROM CLIENTI WHERE TipoCliente = 'Azienda' ", sql);
            SqlDataReader reader = com.ExecuteReader();

            while (reader.Read())
            {
                Clienti c = new Clienti();


                c.IdCliente = Convert.ToInt32(reader["IdCliente"]);
                c.RagioneSociale = reader["RagioneSociale"].ToString();
                c.PIVA = reader["P_IVA"].ToString();
                c.Indirizzo = reader["Residenza_SedeLegale"].ToString();
                c.Telefono = reader["Telefono"].ToString();
                c.Email = reader["email"].ToString();
                listaClienti.Add(c);


            }
            sql.Close();

            return View(listaClienti);
        }

        public ActionResult Spedizioni()
        {
            List<Spedizioni> listaSpedizioni = new List<Spedizioni>();

            SqlConnection sql = Connessioni.GetConnection();
            sql.Open();

            SqlCommand com = Connessioni.GetCommand("SELECT * FROM SPEDIZIONE INNER JOIN" +
                " CLIENTI ON SPEDIZIONE.IdCliente = CLIENTI.IdCliente " +
                "INNER JOIN AGGIORNAMENTO ON SPEDIZIONE.IdSpedizione = AGGIORNAMENTO.IdSpedizione", sql);
            SqlDataReader reader = com.ExecuteReader();

            while (reader.Read())
            {
                Spedizioni s = new Spedizioni();

                s.Mittente = reader["Cognome"].ToString() + reader["Nome"].ToString();
                s.IdSpedizione = Convert.ToInt32(reader["IdSpedizione"]);
                s.DataSpedizione = Convert.ToDateTime(reader["Data_Spedizione"]);
                s.Peso = reader["Peso"].ToString();
                s.Destinatario = reader["Destinatario"].ToString();
                s.IndirizzoDestinatario = reader["Ind_Destinatario"].ToString();
                s.CittaDestinatario = reader["Citta_Destinatario"].ToString();
                s.CostiSpedizione = reader["Costo_Spedizione"].ToString();
                s.DataConsegna = Convert.ToDateTime(reader["Data_Consegna"]);
                
                listaSpedizioni.Add(s);


            }
            sql.Close();

            return View(listaSpedizioni);
        }
    }
}