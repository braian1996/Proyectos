using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.Content;
using Android.Support.V4.Graphics.Drawable;
using Android.Views;
using Android.Widget;
using Java.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DigitalClaimT.Droid
{
    [Activity(Label = "")]
    public class ActivityDetalleReclamo : Activity
    {
        RatingBar rbAD;
        AutoCompleteTextView edtComentario;
        TextView tvFecha;
        TextView tvComentarioMuestra;
        Button btnCalificarReclamo;
        ListView listviewHistorialReclamo;
        ImageView imgFotoRec;
        string idReclamo;
        string estado;
        string stFechaR;
        string stRating;
        string stComentario;
        string stfotoAudi;
        bool btnCalificarRating;

        protected override void OnCreate(Bundle savedInstanceState)
        {

            try
            {

                base.OnCreate(savedInstanceState);
                SetContentView(Resource.Layout.DetalleReclamo);

                listviewHistorialReclamo = FindViewById<ListView>(Resource.Id.listViewHistorialReclamo);

                btnCalificarReclamo = FindViewById<Button>(Resource.Id.btnCalificarReclamo);
                imgFotoRec = FindViewById<ImageView>(Resource.Id.imageViewReclamo);
               ImageView imgFotoAuditoria = FindViewById<ImageView>(Resource.Id.imageViewAuditoria);
                TextView tvFEchaAud = FindViewById<TextView>(Resource.Id.tvFechaAuditoria);
                TextView tvObserAud = FindViewById<TextView>(Resource.Id.tvObservacionAuditoria);

                TextView tvantes = FindViewById<TextView>(Resource.Id.tvAntesRec);
                TextView tvdespues = FindViewById<TextView>(Resource.Id.tvDespuesAu);

                btnCalificarReclamo.Click += BtnCalificarReclamo_Click;
               

                string stIdRec = Intent.GetStringExtra("codrec");
                string stRutaFoto = Intent.GetStringExtra("ruta");
                
                if (stRutaFoto != null)
                {
                    string stRuta = "http://www.dcwebapi.somee.com/" + stRutaFoto;
                    URL newurl = new URL(stRuta);
                    imgFotoRec.SetImageBitmap(GetImageBitmapFromUrl(newurl.ToString()));
                }

                clsReclamo objIdReclamo = new clsReclamo();
                objIdReclamo.rec_IDReclamo = stIdRec;
                string stIdRecSerializa = JsonConvert.SerializeObject(objIdReclamo);

                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string urlSelectAuditoria = "http://DCWebApi.somee.com/api/ReclamoController/SelectAuditoria?stObj=" + stIdRecSerializa;
                HttpResponseMessage responseSelectAuditoria = client.GetAsync(urlSelectAuditoria).Result;
                if (responseSelectAuditoria.IsSuccessStatusCode)
                {
                    string ResultadoAuditoria = JsonConvert.DeserializeObject(responseSelectAuditoria.Content.ReadAsStringAsync().Result).ToString();
                    
                    if (ResultadoAuditoria != "null")
                    {
                        var detalleAuditoria = JsonConvert.DeserializeObject(ResultadoAuditoria.ToString());

                        JObject o = JObject.Parse(detalleAuditoria.ToString());

                        string idAuditoria = o["aud_IDAuditoria"].ToString();
                        string FechaAlta = o["aud_fechaAlta"].ToString();
                        string stObserva = o["aud_observaciones"].ToString();
                        string stIdR = o["aud_IDReclamo"].ToString();
                        stfotoAudi = o["aud_foto"].ToString();

                         tvantes.Visibility = ViewStates.Visible;
                         tvdespues.Visibility = ViewStates.Visible;

                        if (stObserva != "")
                        {
                            tvObserAud.Text = "Observación: " + stObserva;
                        }
                        tvFEchaAud.Text = "Fecha: " + FechaAlta;

                        if (stfotoAudi != "" || stfotoAudi != null)
                        {
                            string stRutaAu = "http://www.dcwebapi.somee.com/" + stfotoAudi;
                            URL newurla = new URL(stRutaAu);
                            imgFotoAuditoria.SetImageBitmap(GetImageBitmapFromUrl(newurla.ToString()));
                        }
                    }
                }

            


                clsReclamo objCodRec = new clsReclamo();
                objCodRec.rec_IDReclamo = stIdRec;
                string stCodRecSerializa = JsonConvert.SerializeObject(objCodRec);
                List<clsHistorialReclamo> lstHistorialReclamo = new List<clsHistorialReclamo>();
                
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string urlDetalleReclamo = "http://DCWebApi.somee.com/api/ReclamoController/SelectHistorial?stObj=" + stCodRecSerializa;
                HttpResponseMessage response = client.GetAsync(urlDetalleReclamo).Result;
                if (response.IsSuccessStatusCode)
                {
                    var ResultadoDetalleReclamo = JsonConvert.DeserializeObject(response.Content.ReadAsStringAsync().Result);
                    var detalleReclamo = JsonConvert.DeserializeObject(ResultadoDetalleReclamo.ToString());
                    foreach (var element in (JArray)detalleReclamo)
                    {
                        clsHistorialReclamo objHisRec = new clsHistorialReclamo();
                        objHisRec.his_fechaIngreso = ((JObject)element).SelectToken("$.his_fechaIngreso").ToString();
                        objHisRec.his_horaIngreso = ((JObject)element).SelectToken("$.his_horaIngreso").ToString();
                        objHisRec.his_observaciones =  ((JObject)element).SelectToken("$.his_observaciones").ToString();
                        objHisRec.estRec_nombre = ((JObject)element).SelectToken("$.estRec_nombre").ToString();
                        objHisRec.his_IDHistorial = ((JObject)element).SelectToken("$.his_IDHistorial").ToString();
                        objHisRec.his_IDReclamo = ((JObject)element).SelectToken("$.his_IDReclamo").ToString();

                        idReclamo = objHisRec.his_IDHistorial;
                        estado = ((JObject)element).SelectToken("$.estRec_nombre").ToString();

                        lstHistorialReclamo.Add(objHisRec);
                       
                    }

                }
                clsListaDetalleReclamoAdaptador cls = new clsListaDetalleReclamoAdaptador(this, lstHistorialReclamo);
                listviewHistorialReclamo.Adapter = cls;

               
                if (estado == "Cumplido")
                {
                    btnCalificarReclamo.Enabled = true;

                    clsReclamo objRec = new clsReclamo();
                    objRec.rec_IDReclamo = stIdRec;
                    string stValIDRec = JsonConvert.SerializeObject(objRec);

                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string urlrating = "http://DCWebApi.somee.com/api/ReclamoController/SelectRating?stObj=" + stValIDRec;
                    HttpResponseMessage responseRating = client.GetAsync(urlrating).Result;
                    if (responseRating.IsSuccessStatusCode)
                    {
                        var resultadoRating = JsonConvert.DeserializeObject(responseRating.Content.ReadAsStringAsync().Result);
                        //var detalleReclamo = JsonConvert.DeserializeObject(responseRating.ToString());

                        if (resultadoRating.ToString() != "null") 
                        {
                            JObject o = JObject.Parse(resultadoRating.ToString());
                            //clsRating ObjRating = new clsRating();
                            string stIdReclamo = o["rec_IDReclamo"].ToString();
                            stRating = o["rat_rating"].ToString();
                            stComentario = o["rat_comentario"].ToString();
                            stFechaR = o["rat_fechaAlta"].ToString();

                            btnCalificarReclamo.Text = "VER CALIFICACIÓN";
                            btnCalificarRating = false;
                        }
                        else
                        {
                            btnCalificarReclamo.Text = "CALIFICAR RECLAMO";
                            btnCalificarRating = true;
                        }
                   
                    }
                    else
                    {

                    }
                }
                else
                {
                    btnCalificarReclamo.Enabled = false;
                    btnCalificarReclamo.Text = "CALIFICAR RECLAMO";
                    btnCalificarRating = true;
                }
                
            }
            catch (Exception ex)
            {


            }
        }

        private Bitmap GetImageBitmapFromUrl(string url)
        {
            try
            {
                Bitmap imageBitmap = null;
                using (var webClient = new WebClient())
                {
                    var imageBytes = webClient.DownloadData(url);
                    if (imageBytes != null && imageBytes.Length > 0)
                    {
                        imageBitmap = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
                    }
                }
                return imageBitmap;
            }
            catch (Exception ex)
            {

                return null;
            }

        }
        private void BtnCalificarReclamo_Click(object sender, EventArgs e)
        {
            try
            {
                AlertDialog.Builder builder = new AlertDialog.Builder(this);
                if (btnCalificarRating == true)
                {
                    
                    builder.SetTitle("Calificar Reclamo");
                    LayoutInflater inflater = LayoutInflater;
                    View v = inflater.Inflate(Resource.Layout.alertdialog, null);
                    builder.SetView(v);
                    builder.SetPositiveButton("Enviar", btnEnviarCalificacion);

                    edtComentario = (AutoCompleteTextView)v.FindViewById<AutoCompleteTextView>(Resource.Id.autoComptvComentario);
                    rbAD = (RatingBar)v.FindViewById<RatingBar>(Resource.Id.rating);
                    rbAD.RatingBarChange += RbAD_RatingBarChange;

                    //AlertDialog alertdialog = builder.Create();
                    //alertdialog.Show();
                }
                else
                {
                    
                    //AlertDialog.Builder builder = new AlertDialog.Builder(this);
                    builder.SetTitle("Calificación de Reclamo");
                    LayoutInflater inflater = LayoutInflater;
                    View v = inflater.Inflate(Resource.Layout.alertdialog, null);
                    builder.SetView(v);
                    builder.SetPositiveButton("Ok",btnOkRating);

                    edtComentario = (AutoCompleteTextView)v.FindViewById<AutoCompleteTextView>(Resource.Id.autoComptvComentario);
                    tvFecha = v.FindViewById<TextView>(Resource.Id.textViewFecha);
                    tvComentarioMuestra = v.FindViewById<TextView>(Resource.Id.textViewComentarioMuestra);
                    rbAD = (RatingBar)v.FindViewById<RatingBar>(Resource.Id.rating);

                    tvFecha.Visibility = ViewStates.Visible;
                    tvComentarioMuestra.Visibility = ViewStates.Visible;
                    edtComentario.Visibility = ViewStates.Invisible;

                    tvFecha.Text = "Fecha: " + stFechaR;
                    tvComentarioMuestra.Text = stComentario;
                    rbAD.Enabled = false;
                    rbAD.Rating = float.Parse(stRating);

                   
                }
                AlertDialog alertdialog = builder.Create();
                alertdialog.Show();

            }
            catch (Exception ex)
            {


            }
        }

        private void btnOkRating(object sender, DialogClickEventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {

                
            }
        }

        private void RbAD_RatingBarChange(object sender, RatingBar.RatingBarChangeEventArgs e)
        {
            try
            {
                Toast.MakeText(this, "Su calificación: " + rbAD.Rating.ToString(), ToastLength.Long).Show();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private void btnEnviarCalificacion(object sender, DialogClickEventArgs e)
        {
            try
            {

                clsDetalleReclamo objCalifReclamo = new clsDetalleReclamo();
                objCalifReclamo.rat_IDReclamo = idReclamo;
                objCalifReclamo.rat_comentario = edtComentario.Text;
                objCalifReclamo.rat_rating = rbAD.Rating.ToString();
                objCalifReclamo.rat_fechaAlta = DateTime.Now.ToString("dd/MM/yyyy");
                string stValorCalifReclamo = JsonConvert.SerializeObject(objCalifReclamo);

                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string urlCalificarReclamo = "http://DCWebApi.somee.com/api/ReclamoController/RegistrarRating?stObj=" + stValorCalifReclamo;
                HttpResponseMessage response = client.GetAsync(urlCalificarReclamo).Result;
                if (response.IsSuccessStatusCode)
                {
                    string ResultadoRating = JsonConvert.DeserializeObject(response.Content.ReadAsStringAsync().Result).ToString();
                    if (ResultadoRating == "1")
                    {
                        AlertDialog.Builder builderr = new AlertDialog.Builder(this);
                        builderr.SetTitle("Rating Registrado");
                        builderr.SetIcon(Resource.Drawable.check);
                        builderr.SetMessage("Rating registrado con exito");
                        builderr.SetPositiveButton("OK", BTNok);
                        AlertDialog alertdialogg = builderr.Create();
                        alertdialogg.Show();
                    }
                }
            }
            catch (Exception ex)
            {

                
            }
        }

        private void BTNok(object sender, DialogClickEventArgs e)
        {
            try
            {

            }
            catch (Exception)
            {

                
            }
        }
    }
}