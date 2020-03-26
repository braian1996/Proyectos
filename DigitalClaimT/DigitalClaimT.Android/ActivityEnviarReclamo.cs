using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Provider;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.IO;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Plugin.Geolocator;
using Plugin.Media;
using Xamarin.Forms.Maps;
using Android.Locations;
using Android.Util;
using Plugin.Permissions;
using Android.Content.PM;
using Plugin.CurrentActivity;
using System.Threading.Tasks;
using System.Collections;
using static Android.Graphics.Bitmap;
using Android.Support.V4.Content;
using Android;
using Android.Support.V4.App;
using Android.Support.Design.Widget;
using Xamarin.Forms.PlatformConfiguration;
using System.Net.Http;
using System.Net.Http.Headers;

namespace DigitalClaimT.Droid
{
    [Activity(Label = "Reclamo")]
    public class ActivityEnviarReclamo : Activity, ILocationListener
    {
        Button btnSubirFoto;
        Button btnSiguiente;
        ImageView imgFoto;
        Spinner spnerAS;
        Spinner spnerTR;
        AutoCompleteTextView txtObser;
        string stIDAreaServicio = "";
        string stIDTipoReclamo = "";
        string stNombreTipoReclamo = "";
        string stNombreAreaServicio = "";
        Bitmap bitmap;
        Location currentLocation;
        //LocationManager locationManager;
        //string locationProvider;
        string stLatitud;
        string stLongitud;
        List<Int64> lstid = null;
        List<Int64> lstIdTR = null;
        List<string> lstSpinerNombreBarrio = null;
        Spinner spnBarrio;
        Spinner spnCalle;
        EditText edtNro;
        string idCalle;
        string stIdBarrio;
        string stCalleNombre;
        string stBarrioNombre;
        List<Int64> lstIdBarrio;
        List<Int64> lstIdCalle;
        //TextView tvValidaAreaServicio;
        //TextView tvValidaBarrio;
        //TextView tvValidaAltura;
        //TextView tvValidaReclamo;
        //TextView tvValidaCalle;
        //TextView tvCampoOblig;

        public string TAG
        {
            get;
            private set;
        }

   
     

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            try
            {

                CrossCurrentActivity.Current.Activity = this;
                // Create your application here
                SetContentView(Resource.Layout.lEnviarReclamo);



                btnSubirFoto = FindViewById<Button>(Resource.Id.btnSubirFoto);
                btnSiguiente = FindViewById<Button>(Resource.Id.btnSiguie);
                imgFoto = FindViewById<ImageView>(Resource.Id.ivCamara);
                spnerAS = FindViewById<Spinner>(Resource.Id.spinnerAreaServicio);
                spnerTR = FindViewById<Spinner>(Resource.Id.spinnerTipoReclamo);
                txtObser = FindViewById<AutoCompleteTextView>(Resource.Id.ACtvObservacio);

                //tvValidaAreaServicio = FindViewById<TextView>(Resource.Id.tvValidaAreaServicio);
                //tvValidaBarrio = FindViewById<TextView>(Resource.Id.tvValidaBarrio);
                //tvValidaAltura = FindViewById<TextView>(Resource.Id.tvValidaAltura);
                //tvValidaReclamo = FindViewById<TextView>(Resource.Id.tvValidaTipoReclamo);
                //tvValidaCalle = FindViewById<TextView>(Resource.Id.tvValidaCalle);
                //tvCampoOblig = FindViewById<TextView>(Resource.Id.tvMuestraMsjCampOblig);

                spnBarrio = FindViewById<Spinner>(Resource.Id.spBarrio);
                spnCalle = FindViewById<Spinner>(Resource.Id.spCalle);
                edtNro = FindViewById<EditText>(Resource.Id.editTextAltura);
                edtNro.BeforeTextChanged += EdtNro_BeforeTextChanged;


                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string url = "http://DCWebApi.somee.com/api/AreaServicioController/SelectAreaServicio";
                HttpResponseMessage response = client.GetAsync(url).Result;
                if (response.IsSuccessStatusCode)
                {
                    var ResultadoAreaServicio = JsonConvert.DeserializeObject(response.Content.ReadAsStringAsync().Result);

                    List<string> lstNombre = new List<string>();
                    lstid = new List<Int64>();
                    // string stConca;

                    //agrego "Seleccione Area de Servicio.."
                    clsLlenaCombo clscc = new clsLlenaCombo();
                    clscc.id = -1;
                    clscc.nombre = "Seleccione Area de Servicio...";
                    lstNombre.Add(clscc.nombre);
                    lstid.Add(clscc.id);

                 

                    var ListaAreaServicio = JsonConvert.DeserializeObject(ResultadoAreaServicio.ToString());
                    foreach (var element in (JArray)ListaAreaServicio)
                    {
                        clsLlenaCombo clsc = new clsLlenaCombo();
                        clsc.id = (Int64)((JObject)element).SelectToken("$.arServ_IDAreaServicio");
                        clsc.nombre = ((JObject)element).SelectToken("$.arServ_nombre").ToString();


                        lstNombre.Add(clsc.nombre);
                        lstid.Add(clsc.id);
                    }

                    var adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, lstNombre);
                    spnerAS.Adapter = adapter;

                    spnerAS.SetSelection(getIndexSpinner(spnerAS, "Seleccione Area de Servicio..."));
                }

           

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string urlBarrio = "http://DCWebApi.somee.com/api/BarrioController/SelectBarrio";
                HttpResponseMessage responseBarrio = client.GetAsync(urlBarrio).Result;
                if (responseBarrio.IsSuccessStatusCode)
                {
                    var ResultadoBarrio = JsonConvert.DeserializeObject(responseBarrio.Content.ReadAsStringAsync().Result);

                    lstSpinerNombreBarrio = new List<string>();
                    var ListaBarrio = JsonConvert.DeserializeObject(ResultadoBarrio.ToString());
                    lstIdBarrio = new List<long>();

                    //agrego "Seleccione Barrio.."
                    clsLlenaCombo clscb = new clsLlenaCombo();
                    clscb.id = -1;
                    clscb.nombre = "Seleccione Barrio...";
                    lstSpinerNombreBarrio.Add(clscb.nombre);
                    lstIdBarrio.Add(clscb.id);

                    foreach (var element in (JArray)ListaBarrio)
                    {
                        clsLlenaCombo clsc = new clsLlenaCombo();
                        clsc.id = (Int64)((JObject)element).SelectToken("$.bar_IDBarrio");
                        clsc.nombre = ((JObject)element).SelectToken("$.bar_nombre").ToString();

                        lstSpinerNombreBarrio.Add(clsc.nombre);
                        lstIdBarrio.Add(clsc.id);
                    }
                    var adapterBarrio = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, lstSpinerNombreBarrio);
                    spnBarrio.Adapter = adapterBarrio;

                    spnBarrio.SetSelection(getIndexSpinner(spnBarrio, "Seleccione Barrio..."));
                }

                btnSubirFoto.Click += BtnSubirFoto_Click;
                btnSiguiente.Click += BtnSiguiente_Click;
                spnerAS.ItemSelected += SpnerAS_ItemSelected;
                spnerTR.ItemSelected += SpnerTR_ItemSelected;

                spnBarrio.ItemSelected += SpnBarrio_ItemSelected;
                spnCalle.ItemSelected += SpnCalle_ItemSelected;
               

                ActivityCompat.RequestPermissions(this, new String[] { Manifest.Permission.AccessFineLocation }, 1);
               

            }
            catch(Exception ex)
            {
                Toast.MakeText(ApplicationContext, "Error al cargar datos", ToastLength.Long).Show();
            }
        }

        private void EdtNro_BeforeTextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            try
            {
                //if (tvValidaAltura.Visibility == ViewStates.Visible)
                //{
                //    tvValidaAltura.Visibility = ViewStates.Invisible;
                //    tvCampoOblig.Visibility = ViewStates.Invisible;
                //}
            }
            catch (Exception)
            {


            }
        }

  


        public static int getIndexSpinner(Spinner spinner, String myString)
        {
            int index = 0;

            for (int i = 0; i < spinner.Count; i++)
            {
                if (spinner.GetItemAtPosition(i).ToString().Equals(myString))
                {
                    index = i;
                }
            }
            return index;
        }
        private void SpnCalle_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            try
            {
                idCalle = lstIdCalle[e.Position].ToString();
                Spinner spinner = (Spinner)sender;
                stCalleNombre = string.Format("{0}", spinner.GetItemAtPosition(e.Position));

                //if (tvValidaCalle.Visibility == ViewStates.Visible && idCalle != "-2")
                //{
                //    tvValidaCalle.Visibility = ViewStates.Invisible;
                //    tvCampoOblig.Visibility = ViewStates.Invisible;
                //}
            }
            catch (Exception ex)
            {

            }
        }

        private void SpnBarrio_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            try
            {
                Spinner spinner = (Spinner)sender;
                stBarrioNombre = string.Format("{0}", spinner.GetItemAtPosition(e.Position));

                stIdBarrio = lstIdBarrio[e.Position].ToString();
                if (stIdBarrio != "0")
                {
                    List<string> lstSpinerNombreCalle = new List<string>();


                    clsCalle objCalle = new clsCalle();
                    objCalle.bar_IDBarrio = stIdBarrio;
                    string jsCalle = JsonConvert.SerializeObject(objCalle);

                    HttpClient client = new HttpClient();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string url = "http://DCWebApi.somee.com/api/CalleController/SelectCallePorBarrio?stObj=" + jsCalle;
                    HttpResponseMessage response = client.GetAsync(url).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        var ResultadoCalle = JsonConvert.DeserializeObject(response.Content.ReadAsStringAsync().Result);

                        lstIdCalle = new List<long>();

                        //agrego "Seleccione Calle.."
                        clsLlenaCombo clsca = new clsLlenaCombo();
                        clsca.id = -2;
                        clsca.nombre = "Seleccione Calle...";
                        lstSpinerNombreCalle.Add(clsca.nombre);
                        lstIdCalle.Add(clsca.id);

                        var listaCalle = JsonConvert.DeserializeObject(ResultadoCalle.ToString());
                        foreach (var element in (JArray)listaCalle)
                        {
                            clsLlenaCombo clsc = new clsLlenaCombo();
                            clsc.nombre = ((JObject)element).SelectToken("$.cal_nombre").ToString();
                            clsc.id = (Int64)((JObject)element).SelectToken("$.cal_IDCalle");
                            idCalle = clsc.id.ToString();


                            lstSpinerNombreCalle.Add(clsc.nombre);
                            lstIdCalle.Add(clsc.id);
                        }
                    }


                    var adapterCalle = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, lstSpinerNombreCalle);
                    spnCalle.Adapter = adapterCalle;
                }

                //if (tvValidaBarrio.Visibility == ViewStates.Visible && stIdBarrio != "-1")
                //{
                //    tvValidaBarrio.Visibility = ViewStates.Invisible;
                //    tvCampoOblig.Visibility = ViewStates.Invisible;
                //}
            }
            catch (Exception ex)
            {
                Toast.MakeText(ApplicationContext, "Error al encontrar las Calles", ToastLength.Long).Show();
            }
        }

        private void SpnerTR_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            stIDTipoReclamo = lstIdTR[e.Position].ToString();
            Spinner spinner = (Spinner)sender;
            stNombreTipoReclamo = string.Format("{0}", spinner.GetItemAtPosition(e.Position));


            //if (tvValidaReclamo.Visibility == ViewStates.Visible && stIDTipoReclamo != "-2")
            //{
            //    tvValidaReclamo.Visibility = ViewStates.Invisible;
            //    tvCampoOblig.Visibility = ViewStates.Invisible;
            //}
        }

        //string pathFoto ="";
        private void BtnSiguiente_Click(object sender, EventArgs e)
        {
            try
            {
                //ValidarCamposObligatorios();

              

                string stIdUsuario = Intent.GetStringExtra("idUsuario");
                string stNomCompleto = Intent.GetStringExtra("NombreCompleto");
                string stEmail = Intent.GetStringExtra("email");
                string stNomusuario = Intent.GetStringExtra("usuarioNombre");
                string stidRol = Intent.GetStringExtra("idRol");

                string valorObser = txtObser.Text;

                Intent secondActivityConfNuevoRecl = new Intent(this, typeof(ActivityConfNuevoReclamo));
                secondActivityConfNuevoRecl.PutExtra("AreaServicioID", stIDAreaServicio);
                secondActivityConfNuevoRecl.PutExtra("TipoReclamoID", stIDTipoReclamo);
                secondActivityConfNuevoRecl.PutExtra("TipoReclamoNombre", stNombreTipoReclamo);
                secondActivityConfNuevoRecl.PutExtra("AreaServicioNombre", stNombreAreaServicio);

                secondActivityConfNuevoRecl.PutExtra("idCalle", idCalle);
                secondActivityConfNuevoRecl.PutExtra("idBarrio", stIdBarrio);
                secondActivityConfNuevoRecl.PutExtra("nombreBarrio", stBarrioNombre);
                secondActivityConfNuevoRecl.PutExtra("nombreCalle", stCalleNombre);
                //secondActivityConfNuevoRecl.PutExtra("obser", txtObser.Text);
                secondActivityConfNuevoRecl.PutExtra("altura", edtNro.Text);

                secondActivityConfNuevoRecl.PutExtra("foto", bitmap);
                secondActivityConfNuevoRecl.PutExtra("observaciones", valorObser);
                secondActivityConfNuevoRecl.PutExtra("idUsuario", stIdUsuario);
                secondActivityConfNuevoRecl.PutExtra("idLatitud", stLatitud);
                secondActivityConfNuevoRecl.PutExtra("idLongitud", stLongitud);
                secondActivityConfNuevoRecl.PutExtra("NombreCompleto", stNomCompleto);
                secondActivityConfNuevoRecl.PutExtra("email", stEmail);
                secondActivityConfNuevoRecl.PutExtra("usuarioNombre", stNomusuario);
                secondActivityConfNuevoRecl.PutExtra("idRol", stidRol);

                if (stIDAreaServicio != "-1" && stIdBarrio != "-1" && edtNro.Text != "" && stIDTipoReclamo != "-2" && idCalle != "-2")
                {
                    StartActivity(secondActivityConfNuevoRecl);
                    //Finish();
                }
                else
                {
                    AlertDialog ad = new AlertDialog.Builder(this).Create();
                    ad.SetTitle("Error al ingreso");
                    ad.SetIcon(Resource.Drawable.cancel);
                    ad.SetMessage("Por favor verifique si se ingresaron los datos minimos y requeridos.");
                    ad.SetButton("Ok", (g, h) => { });
                    ad.Show();
                    //ValidarCamposObligatorios();
                }


            }
            catch
            {
                Toast.MakeText(ApplicationContext, "Error en datos de ingreso", ToastLength.Long).Show();
            }
          
        }
        //public void ValidarCamposObligatorios()
        //{
        //    if (stIDAreaServicio == "-1")
        //    {
        //        tvValidaAreaServicio.Visibility = ViewStates.Visible;
        //        //tvCampoOblig.Visibility = ViewStates.Visible;
        //    }
        //    else
        //    {
        //        tvValidaAreaServicio.Visibility = ViewStates.Invisible;
        //       // tvCampoOblig.Visibility = ViewStates.Invisible;
        //    }
        //    if (stIDTipoReclamo == "-2")
        //    {
        //        tvValidaReclamo.Visibility = ViewStates.Visible;
        //        //tvCampoOblig.Visibility = ViewStates.Visible;
        //    }
        //    else
        //    {
        //        tvValidaReclamo.Visibility = ViewStates.Invisible;
        //      // tvCampoOblig.Visibility = ViewStates.Invisible;

        //    }
        //    if (stIdBarrio == "-1")
        //    {
        //        tvValidaBarrio.Visibility = ViewStates.Visible;
        //       // tvCampoOblig.Visibility = ViewStates.Visible;
        //    }
        //    else
        //    {
        //        tvValidaBarrio.Visibility = ViewStates.Invisible;
        //        //tvCampoOblig.Visibility = ViewStates.Invisible;
        //    }
        //    if (idCalle == "-2")
        //    {
        //        tvValidaCalle.Visibility = ViewStates.Visible;
        //       // tvCampoOblig.Visibility = ViewStates.Visible;
        //    }
        //    else
        //    {
        //        tvValidaCalle.Visibility = ViewStates.Invisible;
        //        //tvCampoOblig.Visibility = ViewStates.Invisible;

        //    }
        //    if (edtNro.Text == "")
        //    {
        //        tvValidaAltura.Visibility = ViewStates.Visible;
        //        //tvCampoOblig.Visibility = ViewStates.Visible;
        //    }
        //    else
        //    {
        //        tvValidaAltura.Visibility = ViewStates.Invisible;
        //       // tvCampoOblig.Visibility = ViewStates.Invisible;
        //    }

        //    if (stIDAreaServicio == "-1" || stIDTipoReclamo == "-2" || stIdBarrio == "-1" || idCalle == "-2" || edtNro.Text == "")
        //    {
        //        tvCampoOblig.Visibility = ViewStates.Visible;
        //    }
        //    else
        //    {
        //        tvCampoOblig.Visibility = ViewStates.Invisible;
        //    }
        //}
        private void SpnerAS_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            try
            {
               
                stIDAreaServicio = lstid[e.Position].ToString();
                Spinner spinner = (Spinner)sender;
                stNombreAreaServicio = string.Format("{0}", spinner.GetItemAtPosition(e.Position));
                if (stIDAreaServicio != "0")
                {
                    List<string> lstNombreTR = new List<string>();

                 

                    SelectTipoReclamo objTipoRecl = new SelectTipoReclamo();
                    objTipoRecl.tipRec_IDArServ = stIDAreaServicio;
                    string jnIdAS = JsonConvert.SerializeObject(objTipoRecl);

                    HttpClient client = new HttpClient();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string url = "http://DCWebApi.somee.com/api/TipoReclamoController/SelectTipoReclamo?stObj=" + jnIdAS;
                    HttpResponseMessage response = client.GetAsync(url).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        var ResultadoTipoReclamo = JsonConvert.DeserializeObject(response.Content.ReadAsStringAsync().Result);

                        lstIdTR = new List<Int64>();

                        //agrego "Seleccione TipoReclamo.."
                        clsLlenaCombo clstr = new clsLlenaCombo();
                        clstr.id = -2;
                        clstr.nombre = "Seleccione Reclamo...";
                        lstNombreTR.Add(clstr.nombre);
                        lstIdTR.Add(clstr.id);

                        var listaTiporeclamo = JsonConvert.DeserializeObject(ResultadoTipoReclamo.ToString());
                        foreach (var element in (JArray)listaTiporeclamo)
                        {
                            clsLlenaCombo clsc = new clsLlenaCombo();
                            clsc.id = (Int64)((JObject)element).SelectToken("$.tipRec_IDTipoReclamo");
                            clsc.nombre = ((JObject)element).SelectToken("$.tipRec_nombre").ToString();
                            lstNombreTR.Add(clsc.nombre);
                            lstIdTR.Add(clsc.id);

                        }

                        var adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, lstNombreTR);
                        spnerTR.Adapter = adapter;
                    }
                 
                }
                //if (tvValidaAreaServicio.Visibility == ViewStates.Visible && stIDAreaServicio != "-1")
                //{
                //    tvValidaAreaServicio.Visibility = ViewStates.Invisible;
                //    tvCampoOblig.Visibility = ViewStates.Invisible;
                //}
            }
            catch (Exception ex)
            {
                Toast.MakeText(ApplicationContext, "Error al encontrar los Tipo de Reclamo", ToastLength.Long).Show();
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
            catch(Exception ex)
            {
                //Toast.MakeText(ApplicationContext, "Error al procesar la foto", ToastLength.Long).Show();
            }
        }
        //public void localizacion()
        //{
        //    try
        //    {
          
                
        //        LocationManager locationManager = (LocationManager)GetSystemService(Context.LocationService);
        //        Criteria criteria = new Criteria();

        //        Location location = locationManager.GetLastKnownLocation(locationManager.GetBestProvider(criteria, true));
        //         stLatitud = location.Latitude.ToString();
        //        stLongitud = location.Longitude.ToString();
        //    }
        //    catch(Exception ex)
        //    {

        //    }
        //}
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            try
            {
               // ActivityCompat.RequestPermissions(this, new String[] { Manifest.Permission.AccessFineLocation }, 1);
                Plugin.Permissions.PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
                base.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            }
            catch(Exception ex)
            {

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
            catch(Exception ex)
            {
                //Toast.MakeText(ApplicationContext,"Error al procesar la foto", ToastLength.Long).Show();
            }
           
        }

        //localizacion en en reclamo latitud y longitud
        //private void InitializeLocationManager()
        //{
        //    try
        //    {
        //        locationManager = (LocationManager)GetSystemService(LocationService);
        //        Criteria criteriaForLocationService = new Criteria
        //        {
        //            Accuracy = Accuracy.Fine
        //        };
        //        IList<string> acceptableLocationProviders = locationManager.GetProviders(criteriaForLocationService, true);
        //        if (acceptableLocationProviders.Any())
        //        {
        //            locationProvider = acceptableLocationProviders.First();
        //        }
        //        else
        //        {
        //            locationProvider = string.Empty;
        //        }
        //        Log.Debug(TAG, "Using " + locationProvider + ".");
        //    }
        //    catch (Exception ex)
        //    {

        //    }

        //}

        //protected override void OnResume()
        //{
        //    try
        //    {
        //        base.OnResume();

        //        locationManager.RequestLocationUpdates(locationProvider, 0, 0, this);
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}
        //protected override void OnPause()
        //{
        //    base.OnPause();
        //    locationManager.RemoveUpdates(this);
        //}

        public void OnLocationChanged(Location location)
        {
            try
            {
                currentLocation = location;
                if (currentLocation == null)
                {
                    //Error Message  
                }
                else
                {
                    stLatitud = currentLocation.Latitude.ToString();
                    stLongitud = currentLocation.Longitude.ToString();
                }
            }
            catch (Exception ex)
            {

            }
        }

        public void OnProviderDisabled(string provider)
        {

        }

        public void OnProviderEnabled(string provider)
        {

        }

        public void OnStatusChanged(string provider, [GeneratedEnum] Availability status, Bundle extras)
        {

        }
    }
}