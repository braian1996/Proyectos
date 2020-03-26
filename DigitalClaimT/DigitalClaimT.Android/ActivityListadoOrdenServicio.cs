using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DigitalClaimT.Droid
{
    [Activity(Label = "")]
    public class ActivityListadoOrdenServicio : Activity
    {
        ListView lstListadoReclamoOrden;
        List<string> lstEstadoReclamoID;
        Button btnIniciarTrabajo;
        Button btnReclamoOrdenCambioEstado;
        //AutoCompleteTextView edtMotivo;
        string stIdOrdenServicio;
        string stNroOrdenServicio;
        string stFechaAltaOrdenServicio;
        string stFechaVenOrdenServicio;
        string stObserOrdenServicio;
        string stFechaIniciOrdenServicio;
        string stFechaCieOrdenServicio;
        string stIdAreaServ;


        List<clsEstadoReclamo> lstEstadoReclamo = new List<clsEstadoReclamo>();
    

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            try
            {
                // Create your application here
                SetContentView(Resource.Layout.listadoReclamoOrdenServ);

                lstListadoReclamoOrden = FindViewById<ListView>(Resource.Id.lstReclamoOrdenServicio); 
                btnReclamoOrdenCambioEstado = FindViewById<Button>(Resource.Id.btnGuardarReclamoOrden);
                btnIniciarTrabajo = FindViewById<Button>(Resource.Id.btnIniciarTrabajo);

                btnIniciarTrabajo.Click += BtnIniciarTrabajo_Click;
                btnReclamoOrdenCambioEstado.Click += BtnReclamoOrdenCambioEstado_Click;

                 stIdOrdenServicio = Intent.GetStringExtra("IDOrdenServicio");
                 stNroOrdenServicio = Intent.GetStringExtra("NroOrdenServicio");
                 stFechaAltaOrdenServicio = Intent.GetStringExtra("fechalta");
                 stFechaVenOrdenServicio = Intent.GetStringExtra("fechavenc");
                 stObserOrdenServicio = Intent.GetStringExtra("observacion");
                 stFechaIniciOrdenServicio = Intent.GetStringExtra("fechainicio");
                 stFechaCieOrdenServicio = Intent.GetStringExtra("fechacierre");
                 stIdAreaServ = Intent.GetStringExtra("idareaserv");

                clsConsultarOrdenServicio objBuscarOS = new clsConsultarOrdenServicio();
                objBuscarOS.orServ_IDOrdenServicio = stIdOrdenServicio;
                string valorIDOS = JsonConvert.SerializeObject(objBuscarOS);
                List<clsLlenarReclamoOrden> lstReclamoOrden = new List<clsLlenarReclamoOrden>();

                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string urlbuscarReclamosOrdenServicio = "http://DCWebApi.somee.com/api/OrdenServicioController/SelectReclamosPorOrden?stObj=" + valorIDOS;
                HttpResponseMessage response = client.GetAsync(urlbuscarReclamosOrdenServicio).Result;
                if (response.IsSuccessStatusCode)
                {
                    var ResultadoReclamosOrdenServicio = JsonConvert.DeserializeObject(response.Content.ReadAsStringAsync().Result);

                    var ValorROS = JsonConvert.DeserializeObject(ResultadoReclamosOrdenServicio.ToString());

                    foreach (var element in (JArray)ValorROS)
                    {
                        clsLlenarReclamoOrden objLlenarReclamoOrden = new clsLlenarReclamoOrden();

                        
                        objLlenarReclamoOrden.rec_codigo = Convert.ToInt64( ((JObject)element).SelectToken("$.rec_codigo").ToString());
                        objLlenarReclamoOrden.rec_fechaAlta =  ((JObject)element).SelectToken("$.rec_fechaAlta").ToString();
                        objLlenarReclamoOrden.cal_nombre = ((JObject)element).SelectToken("$.cal_nombre").ToString();
                        objLlenarReclamoOrden.rec_altura = Convert.ToInt32( ((JObject)element).SelectToken("$.rec_altura").ToString());
                        objLlenarReclamoOrden.tipRec_nombre =  ((JObject)element).SelectToken("$.tipRec_nombre").ToString();
                        objLlenarReclamoOrden.rec_direccion = ((JObject)element).SelectToken("$.rec_direccion").ToString(); 
                        objLlenarReclamoOrden.bar_nombre =  ((JObject)element).SelectToken("$.bar_nombre").ToString();
                        objLlenarReclamoOrden.estRec_nombre = ((JObject)element).SelectToken("$.estRec_nombre").ToString();
                        objLlenarReclamoOrden.rec_IDTipoReclamo = Convert.ToInt64(((JObject)element).SelectToken("$.rec_IDTipoReclamo").ToString());
                        objLlenarReclamoOrden.rec_IDReclamo = Convert.ToInt64(((JObject)element).SelectToken("$.rec_IDReclamo").ToString());

                        objLlenarReclamoOrden.rec_IDCanal = Convert.ToInt32(((JObject)element).SelectToken("$.rec_IDCanal").ToString());
                        objLlenarReclamoOrden.rec_IDUsuario = Convert.ToInt32(((JObject)element).SelectToken("$.rec_IDUsuario").ToString());
                        objLlenarReclamoOrden.rec_Foto = ((JObject)element).SelectToken("$.rec_Foto").ToString();
                        objLlenarReclamoOrden.usu_email = ((JObject)element).SelectToken("$.usu_email").ToString();
                        objLlenarReclamoOrden.usu_nombre = ((JObject)element).SelectToken("$.usu_nombre").ToString();
                        objLlenarReclamoOrden.cal_IDCalle = Convert.ToInt32( ((JObject)element).SelectToken("$.cal_IDCalle").ToString());
                        objLlenarReclamoOrden.bar_IDBarrio = Convert.ToInt32(((JObject)element).SelectToken("$.bar_IDBarrio").ToString());



                        lstReclamoOrden.Add(objLlenarReclamoOrden);
                    }

                    List<string> lstEstadoReclamoNom = new List<string>();
                    lstEstadoReclamoID = new List<string>();

                    string urlEstadoReclamo = "http://DCWebApi.somee.com/api/ReclamoController/SelectEstadoReclamo";
                    HttpResponseMessage responseEstadoReclamo = client.GetAsync(urlEstadoReclamo).Result;
                    if (responseEstadoReclamo.IsSuccessStatusCode)
                    {
                        var ResultadoEstadoReclamo = JsonConvert.DeserializeObject(responseEstadoReclamo.Content.ReadAsStringAsync().Result);

                        var ValorEstadoReclamo = JsonConvert.DeserializeObject(ResultadoEstadoReclamo.ToString());

                        foreach (var element in (JArray)ValorEstadoReclamo)
                        {
                            clsEstadoReclamo objEstRec = new clsEstadoReclamo();
                            objEstRec.estRec_IDEstado = ((JObject)element).SelectToken("$.estRec_IDEstado").ToString();
                            objEstRec.estRec_nombre = ((JObject)element).SelectToken("$.estRec_nombre").ToString();

                            lstEstadoReclamoID.Add(objEstRec.estRec_IDEstado);
                            lstEstadoReclamoNom.Add(objEstRec.estRec_nombre);
                            lstEstadoReclamo.Add(objEstRec);
                        }
                    }
                       

                        var adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, lstEstadoReclamoNom);
                    clsListarReclamoOrdenServicio objLlenaReclamoOrden = new clsListarReclamoOrdenServicio(this, lstReclamoOrden,adapter,lstEstadoReclamoID);
                    lstListadoReclamoOrden.Adapter = objLlenaReclamoOrden;
                }

                //btnReclamoOrdenCambioEstado.Enabled = true;
                //int count = lstListadoReclamoOrden.Count;
                //for (int i = 0; i < count; i++)
                //{
                //    ViewGroup row = (ViewGroup)lstListadoReclamoOrden.GetChildAt(i);
                //    Spinner spnEstadoReclamoRow = (Spinner)row.FindViewById(Resource.Id.spnReclamoOrden);
                //    spnEstadoReclamoRow.Enabled = false;
                //}

            }
            catch (Exception ex)
            {
                //Toast.MakeText(ApplicationContext, "Error al cargar las Ordenes de Servicios", ToastLength.Long).Show();
            }
        }

        private void BtnIniciarTrabajo_Click(object sender, EventArgs e)
        {
            try
            {
                List<EnviarEmail> lstEnviarEmail = new List<EnviarEmail>();

                List<clsEstadoYCodigoRecOrdenServ> lstEnviarCod = new List<clsEstadoYCodigoRecOrdenServ>();

                int count = lstListadoReclamoOrden.Count;
                for (int i = 0; i < count; i++)
                {
                    ViewGroup row = (ViewGroup)lstListadoReclamoOrden.GetChildAt(i);
                    TextView tvIdRecRec = (TextView)row.FindViewById(Resource.Id.textViewIdReclamo);
                    Spinner spnEstadoReclamoRow = (Spinner)row.FindViewById(Resource.Id.spnReclamoOrden);
                    spnEstadoReclamoRow.SetSelection(obtenerPosicionItem(spnEstadoReclamoRow, "En proceso"));

                    clsEstadoYCodigoRecOrdenServ objCodRec = new clsEstadoYCodigoRecOrdenServ();
                    objCodRec.his_IDReclamo = tvIdRecRec.Text;
                    objCodRec.his_fechaIngreso = DateTime.Now.ToString("dd/MM/yyyy");
                    objCodRec.his_horaIngreso = DateTime.Now.ToString("hh:mm");
                    objCodRec.his_IDEstado = 3;
                    objCodRec.his_observaciones = "Inicio del Trabajo de la Orden de Servicio";

                    lstEnviarCod.Add(objCodRec);
                }
                string stSerializaCodRec = JsonConvert.SerializeObject(lstEnviarCod);

                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string urliniciarTrabajo = "http://DCWebApi.somee.com/api/OrdenServicioController/CambioEstadoReclamo?stObj=" + stSerializaCodRec;
                HttpResponseMessage responseIT = client.GetAsync(urliniciarTrabajo).Result;
                if (responseIT.IsSuccessStatusCode)
                {
                    var ResultadoIniciarTrabajo = JsonConvert.DeserializeObject(responseIT.Content.ReadAsStringAsync().Result);



                    string urlEnviarEmail = "http://DCWebApi.somee.com/api/OrdenServicioController/EnviarEmailEstado?stObj=" + ResultadoIniciarTrabajo;
                    HttpResponseMessage responseEnviarEmail = client.GetAsync(urlEnviarEmail).Result;
                    if (responseEnviarEmail.IsSuccessStatusCode)
                    {
                        string ResultadoEnviarEmail = JsonConvert.DeserializeObject(responseEnviarEmail.Content.ReadAsStringAsync().Result).ToString();
                        if (ResultadoEnviarEmail == "1")
                        {
                            clsConsultarOrdenServicio objCon = new clsConsultarOrdenServicio();

                            objCon.orServ_IDAreaServicio = stIdAreaServ;
                            objCon.orServ_IDEstado = 2;
                            objCon.orServ_fechaVencimiento = stFechaVenOrdenServicio;
                            objCon.orServ_fechaAlta = stFechaAltaOrdenServicio;
                            objCon.orServ_fechaInicio = DateTime.Now.ToString("dd/MM/yyyy");
                            objCon.orServ_fechaCierre = stFechaCieOrdenServicio;
                            objCon.orServ_IDOrdenServicio = stIdOrdenServicio;
                            objCon.orServ_numero = stNroOrdenServicio;
                            objCon.orServ_observaciones = stObserOrdenServicio;

                            stFechaIniciOrdenServicio = objCon.orServ_fechaInicio;

                            string stSerializaCambioEstadoOS = JsonConvert.SerializeObject(objCon);

                            string urlCambiarEstadoOrden = "http://DCWebApi.somee.com/api/OrdenServicioController/ActualizarOrdenServicio?stObj=" + stSerializaCambioEstadoOS;
                            HttpResponseMessage responseCambiarEstadoOrden = client.GetAsync(urlCambiarEstadoOrden).Result;
                            if (responseCambiarEstadoOrden.IsSuccessStatusCode)
                            {
                                string ResultadoCambiEstadoOrden = JsonConvert.DeserializeObject(responseCambiarEstadoOrden.Content.ReadAsStringAsync().Result).ToString();

                                AlertDialog.Builder builder = new AlertDialog.Builder(this);
                                builder.SetTitle("Orden de Servicio");
                                builder.SetIcon(Resource.Drawable.check);
                                builder.SetMessage("Se ha iniciado el trabajo de la Orden de Servicio en la fecha de" + " " + objCon.orServ_fechaInicio);
                                builder.SetPositiveButton("ok", btnok);

                                AlertDialog alertdialog = builder.Create();
                                alertdialog.Show();

                            }

                        }
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
                btnIniciarTrabajo.Enabled = false;
                btnReclamoOrdenCambioEstado.Enabled = true;

                //int count = lstListadoReclamoOrden.Count;
                //for (int i = 0; i < count; i++)
                //{
                //    ViewGroup row = (ViewGroup)lstListadoReclamoOrden.GetChildAt(i);
                //    Spinner spnEstadoReclamoRow = (Spinner)row.FindViewById(Resource.Id.spnReclamoOrden);
                //    spnEstadoReclamoRow.Enabled = true;
                //}
            }
            catch (Exception ex)
            {

              
            }
        }

        public static int obtenerPosicionItem(Spinner spinner, String ValorInicializa)
        {
            //Creamos la variable posicion y lo inicializamos en 0
            int posicion = 0;
            //Recorre el spinner en busca del ítem que coincida con el parametro `String fruta`
            //que lo pasaremos posteriormente
            for (int i = 0; i < spinner.Count; i++)
            {
                //Almacena la posición del ítem que coincida con la búsqueda
                if (spinner.GetItemAtPosition(i).ToString().Equals(ValorInicializa))
                {
                    posicion = i;
                }
            }
            //Devuelve un valor entero (si encontro una coincidencia devuelve la
            // posición 0 o N, de lo contrario devuelve 0 = posición inicial)
            return posicion;
        }

        private void BtnReclamoOrdenCambioEstado_Click(object sender, EventArgs e)
        {
            try
            {
                AlertDialog.Builder builder = new AlertDialog.Builder(this);
                builder.SetTitle("Orden de Servicio");
                builder.SetMessage("¿Desea guardar la orden de servicio?");
                builder.SetPositiveButton("si", btnSiGuardarOrden);
                builder.SetNegativeButton("no", btnNoGuardarOrden);
                AlertDialog alertdialog = builder.Create();
                alertdialog.Show();
            }
            catch (Exception ex)
            {

               
            }
        }
        string idEstado;
       
        TextView tvidReclamo;
        private void btnSiGuardarOrden(object sender, DialogClickEventArgs e)
        {
            try
            {

                List<clsEstadoYCodigoRecOrdenServ> lstGuardarOrden = new List<clsEstadoYCodigoRecOrdenServ>();

                int count = lstListadoReclamoOrden.Count;
                for (int i = 0; i < count; i++)
                {
                    ViewGroup row = (ViewGroup)lstListadoReclamoOrden.GetChildAt(i);
                    Spinner spnEstadoReclamoRow = (Spinner)row.FindViewById(Resource.Id.spnReclamoOrden);
                    TextView tvCodigoReclamo = (TextView)row.FindViewById(Resource.Id.textViewCodigoMuestra);
                    string text = spnEstadoReclamoRow.SelectedItem.ToString();

                    clsEstadoReclamo estadoReclamo = lstEstadoReclamo.FirstOrDefault(x => x.estRec_nombre == text);
                    idEstado = estadoReclamo.estRec_IDEstado;
                    tvidReclamo = (TextView)row.FindViewById(Resource.Id.textViewIdReclamo);

                    clsEstadoYCodigoRecOrdenServ objCodRec = new clsEstadoYCodigoRecOrdenServ();
                    objCodRec.his_IDReclamo = tvidReclamo.Text;
                    objCodRec.his_fechaIngreso = DateTime.Now.ToString("dd/MM/yyyy");
                    objCodRec.his_horaIngreso = DateTime.Now.ToString("hh:mm");
                    objCodRec.his_IDEstado = Convert.ToInt32(idEstado);
                    objCodRec.his_observaciones = "Orden de Servicio finalizada.";

                    lstGuardarOrden.Add(objCodRec);

                }
          
                string stSerializaGuarda = JsonConvert.SerializeObject(lstGuardarOrden);

                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string urlGuardarOrden = "http://DCWebApi.somee.com/api/OrdenServicioController/CambioEstadoReclamo?stObj=" + stSerializaGuarda;
                HttpResponseMessage responseGO = client.GetAsync(urlGuardarOrden).Result;
                if (responseGO.IsSuccessStatusCode)
                {
                    var ResultadoGuardarOrden = JsonConvert.DeserializeObject(responseGO.Content.ReadAsStringAsync().Result);


                    string urlEnviarEmail = "http://DCWebApi.somee.com/api/OrdenServicioController/EnviarEmailEstado?stObj=" + ResultadoGuardarOrden;
                    HttpResponseMessage responseEnviarEmail = client.GetAsync(urlEnviarEmail).Result;
                    if (responseEnviarEmail.IsSuccessStatusCode)
                    {
                        string ResultadoEnviarEmail = JsonConvert.DeserializeObject(responseEnviarEmail.Content.ReadAsStringAsync().Result).ToString();
                        if (ResultadoEnviarEmail == "1")
                        {
                            clsConsultarOrdenServicio objCon = new clsConsultarOrdenServicio();

                            objCon.orServ_IDAreaServicio = stIdAreaServ;
                            objCon.orServ_IDEstado = 3;
                            objCon.orServ_fechaVencimiento = stFechaVenOrdenServicio;
                            objCon.orServ_fechaAlta = stFechaAltaOrdenServicio;
                            objCon.orServ_fechaInicio = stFechaIniciOrdenServicio;
                            objCon.orServ_fechaCierre = DateTime.Now.ToString("dd/MM/yyyy"); 
                            objCon.orServ_IDOrdenServicio = stIdOrdenServicio;
                            objCon.orServ_numero = stNroOrdenServicio;
                            objCon.orServ_observaciones = stObserOrdenServicio;

                            string stSerializaCambioEstadoOSc = JsonConvert.SerializeObject(objCon);

                            string urlCambiarEstadoOrdenCierre = "http://DCWebApi.somee.com/api/OrdenServicioController/ActualizarOrdenServicio?stObj=" + stSerializaCambioEstadoOSc;
                            HttpResponseMessage responseCambiarEstadoOrdenCierre = client.GetAsync(urlCambiarEstadoOrdenCierre).Result;
                            if (responseCambiarEstadoOrdenCierre.IsSuccessStatusCode)
                            {
                                string ResultadoCambiEstadoOrden = JsonConvert.DeserializeObject(responseCambiarEstadoOrdenCierre.Content.ReadAsStringAsync().Result).ToString();

                                AlertDialog.Builder builderr = new AlertDialog.Builder(this);
                                builderr.SetTitle("Orden de Servicio");
                                builderr.SetIcon(Resource.Drawable.check);
                                builderr.SetMessage("Se ha guardado el trabajo de la Orden de Servicio con Fecha de Cierre" + " " + objCon.orServ_fechaCierre);
                                builderr.SetPositiveButton("Aceptar", btnAceptarAD);
                                AlertDialog alertdialogg = builderr.Create();
                                alertdialogg.Show();
                            }

                            for (int i = 0; i < count; i++)
                            {
                                ViewGroup row = (ViewGroup)lstListadoReclamoOrden.GetChildAt(i);
                                Spinner spnEstadoReclamoRow = (Spinner)row.FindViewById(Resource.Id.spnReclamoOrden);
                                TextView tvCodigoReclamo = (TextView)row.FindViewById(Resource.Id.textViewCodigoMuestra);
                                TextView tvTipoReclamo = (TextView)row.FindViewById(Resource.Id.textViewTipoReclamoMuestra);
                                tvidReclamo = (TextView)row.FindViewById(Resource.Id.textViewIdReclamo);


                                TextView tvCalleR = (TextView)row.FindViewById(Resource.Id.textViewCalle);
                                TextView tvAlturaR = (TextView)row.FindViewById(Resource.Id.textViewAltura);
                                TextView tvFechaAltaR = (TextView)row.FindViewById(Resource.Id.textViewFechaAltaMuestra);
                                TextView tvBarrioR = (TextView)row.FindViewById(Resource.Id.textViewBarrioMuestra);
                                TextView tvIdUsuario = (TextView)row.FindViewById(Resource.Id.textViewIdUsuario);
                                TextView tvIdCalle = (TextView)row.FindViewById(Resource.Id.textViewidCalle);
                                TextView idBarrio = (TextView)row.FindViewById(Resource.Id.textViewidBarrio);
                                TextView tvIdCanal = (TextView)row.FindViewById(Resource.Id.textViewidCanal);
                                TextView tvObser = (TextView)row.FindViewById(Resource.Id.textViewobser);
                                TextView tvIDTR = (TextView)row.FindViewById(Resource.Id.textViewIdTipoRec);
                                TextView tvEmail = (TextView)row.FindViewById(Resource.Id.textViewEmail);
                                TextView tvNombre = (TextView)row.FindViewById(Resource.Id.textViewNombre);
                                TextView tvFoto = (TextView)row.FindViewById(Resource.Id.textViewFoto);

                                tvidReclamo = (TextView)row.FindViewById(Resource.Id.textViewIdReclamo);

                                string textEstado = spnEstadoReclamoRow.SelectedItem.ToString();

                                if (textEstado == "Cancelado")
                                {
                                    clsLlenarReclamoOrden objLlenarReclamoOrden = new clsLlenarReclamoOrden();

                                    objLlenarReclamoOrden.rec_fechaAlta = tvFechaAltaR.Text;
                                    objLlenarReclamoOrden.cal_nombre = tvCalleR.Text;
                                    objLlenarReclamoOrden.rec_altura = Convert.ToInt32(tvAlturaR.Text);
                                    objLlenarReclamoOrden.rec_IDTipoReclamo = Convert.ToInt64(tvIDTR.Text);
                                    objLlenarReclamoOrden.tipRec_nombre = tvTipoReclamo.Text;
                                    objLlenarReclamoOrden.bar_nombre = tvBarrioR.Text;
                                    objLlenarReclamoOrden.estRec_nombre = textEstado;
                                    objLlenarReclamoOrden.rec_IDCanal = Convert.ToInt32(tvIdCanal.Text);
                                    objLlenarReclamoOrden.bar_IDBarrio = Convert.ToInt32(idBarrio.Text);
                                    objLlenarReclamoOrden.cal_IDCalle = Convert.ToInt32(tvIdCalle.Text);
                                    objLlenarReclamoOrden.rec_IDUsuario = Convert.ToInt32(tvIdUsuario.Text);
                                    objLlenarReclamoOrden.his_horaIngreso = DateTime.Now.ToString("hh:mm");
                                    objLlenarReclamoOrden.usu_email = tvEmail.Text;
                                    objLlenarReclamoOrden.usu_nombre = tvNombre.Text;
                                    objLlenarReclamoOrden.usu_boExiste = true;
                                    objLlenarReclamoOrden.his_horaIngreso = DateTime.Now.ToString("hh:mm");

                                    if (tvFoto.Text != "")
                                    {
                                        objLlenarReclamoOrden.rec_Foto = tvFoto.Text;
                                    }
                                    else
                                    {
                                        objLlenarReclamoOrden.rec_Foto = "";
                                    }

                                    string ValorReclamo = JsonConvert.SerializeObject(objLlenarReclamoOrden);

                                    HttpClient clientt = new HttpClient();
                                    clientt.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                                    string urlNuevoReclamo = "http://DCWebApi.somee.com/api/ReclamoController/RegistrarReclamo?stObj=" + ValorReclamo;
                                    HttpResponseMessage responseNuevoReclamo = clientt.GetAsync(urlNuevoReclamo).Result;
                                    if (responseNuevoReclamo.IsSuccessStatusCode)
                                    {
                                        string codNuevoReclamo = JsonConvert.DeserializeObject(responseNuevoReclamo.Content.ReadAsStringAsync().Result).ToString();

                                    }
                                }


                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

              
            }
        }

        private void btnAceptarAD(object sender, DialogClickEventArgs e)
        {
            try
            {
                Intent secondActivityOS = new Intent(this,typeof(ActivityOrdenDeServicio));
                secondActivityOS.PutExtra("IDOrdenServicio", stIdAreaServ);
               
                StartActivity(secondActivityOS);
                Finish();
            }
            catch (Exception ex)
            {

                
            }
        }

        private void btnNoGuardarOrden(object sender, DialogClickEventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {

                
            }
        }

        //private void btnoGuardar(object sender, DialogClickEventArgs e)
        //{
        //    try
        //    {
        //        string valMotivo = "";
        //        List<clsEstadoYCodigoRecOrdenServ> lstGuardarOrden = new List<clsEstadoYCodigoRecOrdenServ>();

        //        int count = lstListadoReclamoOrden.Count;
        //        for (int i = 0; i < count; i++)
        //        {
        //            ViewGroup row = (ViewGroup)lstListadoReclamoOrden.GetChildAt(i);
        //            Spinner spnEstadoReclamoRow = (Spinner)row.FindViewById(Resource.Id.spnReclamoOrden);
        //            TextView tvCodigoReclamo = (TextView)row.FindViewById(Resource.Id.textViewCodigoMuestra);
        //            TextView tvTipoReclamo = (TextView)row.FindViewById(Resource.Id.textViewTipoReclamoMuestra);
        //            TextView tvidReclamo = (TextView)row.FindViewById(Resource.Id.textViewIdReclamo);

        //            string text = spnEstadoReclamoRow.SelectedItem.ToString();
        //            clsEstadoReclamo estadoReclamo = lstEstadoReclamo.FirstOrDefault(x => x.estRec_nombre == text);
        //            string idEstado = estadoReclamo.estRec_IDEstado;

        //            if (idEstado == "4" || idEstado == "5")
        //            {
        //                AlertDialog.Builder builder = new AlertDialog.Builder(this);
        //                builder.SetTitle("Reclamo:" + " " + tvTipoReclamo.Text);
        //                LayoutInflater inflater = LayoutInflater;
        //                View v = inflater.Inflate(Resource.Layout.layoutAlertDialog, null);
        //                builder.SetView(v);
        //                builder.SetPositiveButton("Ok", btnOkMotivo);
        //                TextView tvTipoReclamoAD = v.FindViewById<TextView>(Resource.Id.textViewTipoReclamoMotivo);
        //                TextView tvMotivoTitulo = v.FindViewById<TextView>(Resource.Id.textViewMotivoTitulo);


        //                tvTipoReclamoAD.Text = "Codigo: " + tvCodigoReclamo.Text;
        //                AutoCompleteTextView edtMotivo = (AutoCompleteTextView)v.FindViewById<AutoCompleteTextView>(Resource.Id.acTextViewMotivo);

        //                if (idEstado == "4")
        //                {
        //                    tvMotivoTitulo.Text = "Motivo de Cancelación:";
        //                }
        //                else
        //                {
        //                    tvMotivoTitulo.Text = "Motivo de Rechazo:";
        //                }

        //                AlertDialog alertdialog = builder.Create();
        //                alertdialog.Show();

        //                valMotivo = edtMotivo.Text;
        //            }
        //        }
               

        //        //HttpClient client = new HttpClient();
        //        //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //        //string urlGuardarTrabajo = "http://DCWebApi.somee.com/api/OrdenServicioController/CambioEstadoReclamo?stObj=" + stGuardaRec;
        //        //HttpResponseMessage responseGT = client.GetAsync(urlGuardarTrabajo).Result;
        //        //if (responseGT.IsSuccessStatusCode)
        //        //{
        //        //    var ResultadoGuardarTrabajo = JsonConvert.DeserializeObject(responseGT.Content.ReadAsStringAsync().Result);

        //        //    string urlEnviarEmail = "http://DCWebApi.somee.com/api/OrdenServicioController/EnviarEmailEstado?stObj=" + ResultadoGuardarTrabajo;
        //        //    HttpResponseMessage responseEnviarEmail = client.GetAsync(urlEnviarEmail).Result;
        //        //    if (responseEnviarEmail.IsSuccessStatusCode)
        //        //    {
        //        //        string ResultadoEnviarEmail = JsonConvert.DeserializeObject(responseEnviarEmail.Content.ReadAsStringAsync().Result).ToString();
        //        //        if (ResultadoEnviarEmail == "1")
        //        //        {
        //        //            AlertDialog.Builder builder = new AlertDialog.Builder(this);
        //        //            builder.SetTitle("Orden de Servicio");
        //        //            builder.SetIcon(Resource.Drawable.check);
        //        //            builder.SetMessage("guardo Orden de Servicio");
        //        //            builder.SetPositiveButton("ok", btnoGuardar);

        //        //            AlertDialog alertdialog = builder.Create();
        //        //            alertdialog.Show();
        //        //        }
        //        //    }
        //        //}
        //    }
        //    catch (Exception ex)
        //    {

               
        //    }
        //}

        private void btnOkMotivo(object sender, DialogClickEventArgs e)
        {
            try
            {
                string valMotivo = "";
                List<clsEstadoYCodigoRecOrdenServ> lstGuardarOrden = new List<clsEstadoYCodigoRecOrdenServ>();

                ////int count = lstListadoReclamoOrden.Count;
                ////for (int i = 0; i < count; i++)
                ////{
                //valMotivo = edtMotivo.Text;
                clsEstadoYCodigoRecOrdenServ objCodRec = new clsEstadoYCodigoRecOrdenServ();
                objCodRec.his_IDReclamo = tvidReclamo.Text;
                objCodRec.his_fechaIngreso = DateTime.Now.ToString("dd/MM/yyyy");
                objCodRec.his_horaIngreso = DateTime.Now.ToString("hh:mm");
                objCodRec.his_IDEstado = Convert.ToInt32(idEstado);
                objCodRec.his_observaciones = "Orden de Servicio finalizada.";
                objCodRec.rec_motivo = valMotivo;

                lstGuardarOrden.Add(objCodRec);
               
                ////}

                ////}
                //string stGuardaRec = JsonConvert.SerializeObject(lstGuardarOrden);
            }
            catch (Exception)
            {

             
            }
        }
    }
}