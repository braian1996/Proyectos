using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Java.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using static Android.Graphics.Bitmap;


namespace DigitalClaimT.Droid
{
    [Activity(Label = "Reclamo")]
    public class ActivityConfNuevoReclamo : Activity
    {

        Spinner spnBarrio;
        Spinner spnCalle;
        ImageView imgFoto;
        EditText edtNro;
        Button btnEnviar;
        TextView tvReclamante;
        TextView tvFecha;
        TextView tvAreaServicio;
        TextView tvTipoReclamo;
        TextView tvDireccion;
        TextView tvBarrio;
        TextView tvObser;
        string stidCalle;
        string idTR;
        string stidBarrio;
        string nombreBarrio;
        string nombreCalle;
        string stTipoReclamoNombre = "";
        string stNombreCompleto;
        string stEmail;
        string stAltura;
        string stNomUsuario;
        string idRol;
        Bitmap bitmapFoto;
        CheckBox chkEnviarReclamo;
        //private ProgressDialog progressDialog;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            try
            {
                // Create your application here
                SetContentView(Resource.Layout.ConfirmarNuevoReclamo);

                spnBarrio = FindViewById<Spinner>(Resource.Id.spBarrio);
                spnCalle = FindViewById<Spinner>(Resource.Id.spCalle);
                edtNro = FindViewById<EditText>(Resource.Id.editTextAltura);

                //progressDialog = new ProgressDialog(this);

                tvReclamante = FindViewById<TextView>(Resource.Id.textViewReclamante);
                tvFecha = FindViewById<TextView>(Resource.Id.textViewFecha);
                tvAreaServicio = FindViewById<TextView>(Resource.Id.textViewAreaServicio);
                tvTipoReclamo = FindViewById<TextView>(Resource.Id.textViewTipoReclamo);
                tvDireccion = FindViewById<TextView>(Resource.Id.textViewDireccion);
                tvBarrio = FindViewById<TextView>(Resource.Id.textViewBarrio);
                tvObser = FindViewById<TextView>(Resource.Id.textViewObserv);
                chkEnviarReclamo = FindViewById<CheckBox>(Resource.Id.checkBoxEnviarReclamo);
                chkEnviarReclamo.CheckedChange += ChkEnviarReclamo_CheckedChange;

                imgFoto = FindViewById<ImageView>(Resource.Id.ivCamaraFoto);
                btnEnviar = FindViewById<Button>(Resource.Id.btnEnviar);



                string stAreaServicioNombre = Intent.GetStringExtra("AreaServicioNombre");
                stTipoReclamoNombre = Intent.GetStringExtra("TipoReclamoNombre");
                idTR = Intent.GetStringExtra("TipoReclamoID");
                bitmapFoto = (Bitmap)Intent.GetParcelableExtra("foto");


                        //using (MemoryStream memory = new MemoryStream())
                        //{
                        //    bitmapFoto.Compress(Bitmap.CompressFormat.Jpeg, 70, memory);
                        //    var ddd = memory.ToArray();
                        //}

                


                string stObservaciones = Intent.GetStringExtra("observaciones");
                string stLatitud = Intent.GetStringExtra("idLatitud");
                string stLongitud = Intent.GetStringExtra("idLongitud");


                stidCalle = Intent.GetStringExtra("idCalle");
                stidBarrio = Intent.GetStringExtra("idBarrio");
                nombreBarrio = Intent.GetStringExtra("nombreBarrio");
                nombreCalle = Intent.GetStringExtra("nombreCalle");
                //stObser = Intent.GetStringExtra("obser");
                stAltura = Intent.GetStringExtra("altura");

                stNombreCompleto = Intent.GetStringExtra("NombreCompleto");
                stEmail = Intent.GetStringExtra("email");
                stNomUsuario = Intent.GetStringExtra("usuarioNombre");
                idRol = Intent.GetStringExtra("idRol");

                DateTime FechaDT = DateTime.Now;
                tvReclamante.Text = stNombreCompleto;
                tvTipoReclamo.Text = stTipoReclamoNombre;
                tvFecha.Text = FechaDT.ToString("dd/MM/yyyy");
                tvBarrio.Text = nombreBarrio;
                tvDireccion.Text = nombreCalle + " " + stAltura;
                tvAreaServicio.Text = stAreaServicioNombre;
                tvObser.Text = stObservaciones;


                imgFoto.SetImageBitmap(bitmapFoto);
                tvObser.Text = stObservaciones;


                btnEnviar.Click += BtnEnviar_Click;

            }
            catch (Exception ex)
            {
                Toast.MakeText(ApplicationContext, "Error al cargar datos", ToastLength.Long).Show();
            }
        }

        private void ChkEnviarReclamo_CheckedChange(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            if (chkEnviarReclamo.Checked == true)
            {
                btnEnviar.Enabled = true;
            }
            else
            {
                btnEnviar.Enabled = false;
            }
        }

        public byte[] getBytesFromBitmap(Bitmap bitmap)
        {
            byte[] bitmapData;
            MemoryStream m = new MemoryStream();
            bitmap.Compress(CompressFormat.Jpeg, 95, m);
            return bitmapData = m.ToArray();
        }

        private void BtnEnviar_Click(object sender, EventArgs e)
        {
            //progressDialog.SetMessage("El reclamo se esta registrando");
            ////muestras el ProgressDialog
            //progressDialog.Indeterminate = true;
            //progressDialog.SetCancelable(false);
            //progressDialog.Show();

            try
            {

                string stIdUsuario = Intent.GetStringExtra("idUsuario");

                //clsValidarReclamo objValidarReclamo = new clsValidarReclamo();
                //objValidarReclamo.rec_altura = stAltura;
                //objValidarReclamo.rec_IDCalle = stidCalle;
                //objValidarReclamo.rec_IDBarrio = stidBarrio;
                //objValidarReclamo.rec_IDTipoReclamo = idTR;
                //var jsValidarReclamo = JsonConvert.SerializeObject(objValidarReclamo);

                HttpClient client = new HttpClient();
                //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //string urlValidarReclamo = "http://DCWebApi.somee.com/api/ReclamoController/ValidarReclamo?stObj=" + jsValidarReclamo;
                //HttpResponseMessage responseValidarReclamo = client.GetAsync(urlValidarReclamo).Result;
                //string ResultadoValidarReclamo = JsonConvert.DeserializeObject(responseValidarReclamo.Content.ReadAsStringAsync().Result).ToString();

                //if (ResultadoValidarReclamo == "null")
                //{

                    clsEnviarRecla obj = new clsEnviarRecla();

                    obj.rec_fechaAlta = DateTime.Now.ToString("dd/MM/yyyy");
                    obj.rec_altura = Convert.ToInt32(stAltura);
                    obj.rec_observaciones = tvObser.Text;
                    obj.cal_IDCalle = Convert.ToInt32(stidCalle);
                    obj.bar_IDBarrio = Convert.ToInt32(stidBarrio);
                    obj.rec_IDCanal = 4;
                    obj.rec_IDUsuario =Convert.ToInt64(stIdUsuario);
                    obj.his_horaIngreso = DateTime.Now.ToString("hh:mm");
                    obj.usu_boExiste = true;
                    obj.rec_IDTipoReclamo = Convert.ToInt64(idTR);
                    obj.tipRec_nombre = stTipoReclamoNombre;
                    obj.usu_nombre = stNombreCompleto;
                    obj.usu_email = stEmail;

                    if (bitmapFoto != null)
                    {
                        obj.rec_Foto = string.Concat("Imagen_Reclamo/", PostImage());
                    }
                    else
                    {
                        obj.rec_Foto = null;
                    }


                    string ValorReclamo = JsonConvert.SerializeObject(obj);

                    //var httpContentReclamo = new StringContent(ValorReclamo);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string urlNuevoReclamo = "http://DCWebApi.somee.com/api/ReclamoController/RegistrarReclamo?stObj=" + ValorReclamo;
                    HttpResponseMessage responseNuevoReclamo = client.GetAsync(urlNuevoReclamo).Result;
                    if (responseNuevoReclamo.IsSuccessStatusCode)
                    {
                        string codNuevoReclamo = JsonConvert.DeserializeObject(responseNuevoReclamo.Content.ReadAsStringAsync().Result).ToString();

                        //JObject o = JObject.Parse(ResultadoNuevoReclamo.ToString());

                        //string StcodRec = o["rec_codigo"].ToString();

                        Intent secondActivityIntentParcelable = new Intent(this, typeof(ActivityMenu));
                        secondActivityIntentParcelable.PutExtra("usuarioNombre", stNomUsuario);
                        secondActivityIntentParcelable.PutExtra("usuarioId", stIdUsuario);
                        secondActivityIntentParcelable.PutExtra("usuarioIdRol", idRol);
                        secondActivityIntentParcelable.PutExtra("NombreCompleto", stNombreCompleto);
                        secondActivityIntentParcelable.PutExtra("email", stEmail);
                        secondActivityIntentParcelable.PutExtra("CodigoReclamo", codNuevoReclamo);

                        StartActivity(secondActivityIntentParcelable);
                        Finish();
                    //new Handler().PostDelayed(() =>
                    //{
                    //    progressDialog.Dismiss();
                    //}, 3000);

                    tvReclamante.Text = "";
                        tvTipoReclamo.Text = "";
                        tvFecha.Text = "";
                        tvBarrio.Text = "";
                        tvDireccion.Text = "";
                        tvAreaServicio.Text = "";
                        tvObser.Text = "";

                        
                }
                }
            //}
            catch (Exception ex)
            {

            }
        }

        public string PostImage()
        {
            HttpClient httpClient = new HttpClient();
            MultipartFormDataContent form = new MultipartFormDataContent();

            byte[] imagebytearraystring = getBytesFromBitmap(bitmapFoto);
            form.Add(new ByteArrayContent(imagebytearraystring, 0, imagebytearraystring.Count()), "IMAGENES_RECLAMO", string.Concat(DateTime.Now.Ticks.ToString(), ".jpg"));
            HttpResponseMessage response = httpClient.PostAsync("http://DCWebApi.somee.com/api/ReclamoController/UploadImageReclamo", form).Result;
            if (response.IsSuccessStatusCode)
            {
                httpClient.Dispose();
                return response.Content.ReadAsStringAsync().Result.Substring(1, response.Content.ReadAsStringAsync().Result.Length - 2);
            }
            httpClient.Dispose();
            return string.Empty;
        }
 

        private void SpnAS_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {

        }


    }
}