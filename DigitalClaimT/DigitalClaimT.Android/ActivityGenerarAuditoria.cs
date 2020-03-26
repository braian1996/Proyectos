using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Graphics;
using Android.Icu.Text;
using Android.OS;
using Android.Provider;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Support.V4.Content;
using Android.Views;
using Android.Widget;
using Java.Sql;
using Newtonsoft.Json;
using static Android.Graphics.Bitmap;

namespace DigitalClaimT.Droid
{
    [Activity(Label = "")]
    public class ActivityGenerarAuditoria : Activity
    {
        Button btnSubirFoto;
        Button btnEnviar;
        ImageView imgFoto;
        Bitmap bitmap;
        EditText edtObserva;
        TextView tvFechaCierre;
        string stIDReclamo;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            
            // Create your application here
            SetContentView(Resource.Layout.GenerarAuditoria);

            btnSubirFoto = FindViewById<Button>(Resource.Id.btnSubirFoto);
            btnEnviar = FindViewById<Button>(Resource.Id.btnEnviar);
            imgFoto = FindViewById<ImageView>(Resource.Id.imgFotoAuditoria);
            edtObserva = FindViewById<EditText>(Resource.Id.edtObservaciones);

            btnSubirFoto.Click += BtnSubirFoto_Click;
            btnEnviar.Click += BtnEnviar_Click;

            stIDReclamo = Intent.GetStringExtra("idRec");

            tvFechaCierre = FindViewById<TextView>(Resource.Id.textViewFechaMuestraCierre);
            DateTime Hoy = DateTime.Today;
            tvFechaCierre.Text = Hoy.ToString("dd/MM/yyyy");
            //ActivityCompat.RequestPermissions(this, new String[] { Manifest.Permission.AccessFineLocation }, 1);
        }

        private void Calendario_DateChange(object sender, CalendarView.DateChangeEventArgs e)
        {
            try
            {
                var newdatetime = new DateTime(e.Year, e.Month + 1, e.DayOfMonth);
                string df = newdatetime.ToString("dd/MM/yyyy");
            }
            catch (Exception ex)
            {

              
            }
        }




        //void ShowDatePickerDialog()
        //{
        //    try
        //    {
        //        DateTime date = DateTime.Today;
        //        var dialog = new DatePickerFragment(this, DateTime.Now, null);
        //        dialog.Show(FragmentManager, null);


        //    }
        //    catch (Exception ex)
        //    {

        //    }

        //}

        //public void OnDateSet(DatePicker view, int year, int monthOfYear, int dayOfMonth)
        //{
        //    try
        //    {
        //        var newDate = new DateTime(year, monthOfYear + 1, dayOfMonth);
        //        UpdateDisplay(newDate);
        //    }
        //    catch (Exception ex)
        //    {


        //    }

        //}

        //void UpdateDisplay(DateTime selectedDate)
        //{
        //    try
        //    {
        //        edtFechaResolucion.Text = selectedDate.ToString("dd/MM/yyyy");
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    // selectedDate.GetDateTimeFormats('d');
        //    //  dateDisplay.Text = selectedDate.GetDateTimeFormats('d').ToString();


        //}
        public string PostImage()
        {
            HttpClient httpClient = new HttpClient();
            MultipartFormDataContent form = new MultipartFormDataContent();

            byte[] imagebytearraystring = getBytesFromBitmap(bitmap);
            form.Add(new ByteArrayContent(imagebytearraystring, 0, imagebytearraystring.Count()), "~\\Imagen_Auditoria", string.Concat(DateTime.Now.Ticks.ToString(), ".jpg"));
            HttpResponseMessage response = httpClient.PostAsync("http://DCWebApi.somee.com/api/ReclamoController/UploadImageAuditoria", form).Result;
            if (response.IsSuccessStatusCode)
            {
                httpClient.Dispose();
                return response.Content.ReadAsStringAsync().Result.Substring(1, response.Content.ReadAsStringAsync().Result.Length - 2);
            }
            httpClient.Dispose();
            return string.Empty;
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
            try
            {
                Auditoria obj = new Auditoria();

                obj.aud_fechaAlta = tvFechaCierre.Text;
                obj.aud_observaciones = edtObserva.Text;
                obj.aud_IDReclamo = Convert.ToInt64( stIDReclamo);

                if (bitmap != null)
                {
                    obj.aud_foto = string.Concat("Imagen_Auditoria/", PostImage());
                }
                else
                {
                    obj.aud_foto = null;
                }


                string ValorAudit= JsonConvert.SerializeObject(obj);

                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string urlCalificarReclamo = "http://DCWebApi.somee.com/api/ReclamoController/RegistrarAuditoria?stObj=" + ValorAudit;
                HttpResponseMessage response = client.GetAsync(urlCalificarReclamo).Result;
                if (response.IsSuccessStatusCode)
                {
                    string resuAud = JsonConvert.DeserializeObject(response.Content.ReadAsStringAsync().Result).ToString();
                    if (resuAud == "1")
                    {
                        AlertDialog.Builder builder = new AlertDialog.Builder(this);
                        builder.SetTitle("Registrar Auditoria");
                        builder.SetIcon(Resource.Drawable.check);
                        builder.SetMessage("La auditoria se registro con exito");
                        builder.SetPositiveButton("ok", btnok);

                        AlertDialog alertdialog = builder.Create();
                        alertdialog.Show();
                    }
                }
            }
            catch (Exception ex)
            {

              
            }
        }

        private void btnok(object sender, DialogClickEventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {

               
            }
        }

        int REQUEST_LOCATION = 101;
        private void BtnSubirFoto_Click(object sender, EventArgs e)
        {
            try
            {

                if (ContextCompat.CheckSelfPermission(this, Manifest.Permission.Camera) == (int)Permission.Granted)
                {
                    // We have permission, go ahead and use the camera.
                    Intent inte = new Intent(MediaStore.ActionImageCapture);
                    StartActivityForResult(inte, 0);
                    //InitializeLocationManager();

                }
                else
                {
                    //// Camera permission is not granted. If necessary display rationale & request.

                    ActivityCompat.RequestPermissions(this, new String[] { Manifest.Permission.Camera }, REQUEST_LOCATION);
                    //    //ActivityCompat.RequestPermissions(this, new String[] { Manifest.Permission.AccessFineLocation }, REQUEST_LOCATION);
                    Intent inte = new Intent(MediaStore.ActionImageCapture);
                    StartActivityForResult(inte, 0);

                }
                
            }
            catch (Exception ex)
            {
                //Toast.MakeText(ApplicationContext, "Error al procesar la foto", ToastLength.Long).Show();
            }
        }
        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            try
            {
                base.OnActivityResult(requestCode, resultCode, data);
                bitmap = (Bitmap)data.Extras.Get("data");
                imgFoto.SetImageBitmap(bitmap);
            }
            catch (Exception ex)
            {
                //Toast.MakeText(ApplicationContext, "Error al procesar la foto", ToastLength.Long).Show();
            }

        }
    }
}