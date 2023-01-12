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
    [Authorize]
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
                " CLIENTI ON SPEDIZIONE.IdCliente = CLIENTI.IdCliente ", sql);
            SqlDataReader reader = com.ExecuteReader();

            while (reader.Read())
            {
                Spedizioni s = new Spedizioni();

                s.Mittente = reader["Cognome"].ToString() + "" + reader["Nome"].ToString();
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

            reader.Close();
            sql.Close();

            SqlConnection con = Connessioni.GetConnection();
            con.Open();

            foreach (Spedizioni item in listaSpedizioni)
            {
                SqlCommand cnct = Connessioni.GetCommand(" SELECT TOP(1) * FROM AGGIORNAMENTO INNER JOIN STATO_SPEDIZIONE ON STATO_SPEDIZIONE.IdStato = Aggiornamento.IdStato WHERE IdSpedizione = @IdSpedizione order by DataAggiornamento Desc", con);
                cnct.Parameters.AddWithValue("IdSpedizione", item.IdSpedizione);

               SqlDataReader red = cnct.ExecuteReader();

                while (red.Read())
                {
                    item.Aggiornamento = red["Stato"].ToString() ;
                }
            }
            con.Close();
            return View(listaSpedizioni);
        }

        //MODIFICARE - SELECT
        public ActionResult ModificaCliente(int id)
        {
            SqlConnection sql = Connessioni.GetConnection();
            sql.Open();

            SqlCommand com = Connessioni.GetCommand("SELECT * from CLIENTI where IdCliente = @IdCliente", sql);
            com.Parameters.AddWithValue("IdCliente", id);
            SqlDataReader reader = com.ExecuteReader();

            Clienti c = new Clienti();

            while (reader.Read())
            {
                c.IdCliente = Convert.ToInt32(reader["IdCliente"]);
                c.Cognome = reader["Cognome"].ToString();
                c.Nome = reader["Nome"].ToString();
                c.CF = reader["CodiceFiscale"].ToString();
                c.Indirizzo = reader["Residenza_SedeLegale"].ToString();
                c.Telefono = reader["Telefono"].ToString();
                c.Email = reader["email"].ToString();

            }
            sql.Close();
            return View(c);
        }
        
        [HttpPost]
        public ActionResult ModificaCliente(Clienti custom)
        {
            SqlConnection sql = Connessioni.GetConnection();
            sql.Open();

            try
            {

                SqlCommand com = Connessioni.GetCommand("UPDATE CLIENTI set Cognome=@Cognome, Nome=@Nome, CodiceFiscale=@CodiceFiscale, Residenza_SedeLegale=@Indirizzo," +
                    " Telefono=@Telefono, email=@email where IdCliente = @IdCliente", sql);

                com.Parameters.AddWithValue("IdCliente", custom.IdCliente);
                com.Parameters.AddWithValue("Cognome", custom.Cognome);
                com.Parameters.AddWithValue("Nome", custom.Nome);
                com.Parameters.AddWithValue("CodiceFiscale", custom.CF);
                com.Parameters.AddWithValue("Indirizzo", custom.Indirizzo);
                com.Parameters.AddWithValue("Telefono", custom.Telefono);
                com.Parameters.AddWithValue("email", custom.Email);

                int row = com.ExecuteNonQuery();

                if (row > 0)
                {
                    ViewBag.confirm = "Scheda cliente modificata con successo";
                }

            }
            catch (Exception ex)
            {
                ViewBag.errore = ex.Message;
            }
            finally { sql.Close(); }

            return RedirectToAction("Privati");
        }

        public ActionResult CreatePrivate()
        {
            return View();
        }
    }
}