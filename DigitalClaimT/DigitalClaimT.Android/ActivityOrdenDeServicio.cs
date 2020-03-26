using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

using Android.App;
using Android.Content;
using Android.Icu.Text;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Util;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DigitalClaimT.Droid
{
    [Activity(Label = "")]
    public class ActivityOrdenDeServicio : Activity
    {
        ListView lst;
        Button btnGeneAudi;
        Button btnBuscarOS;
        Button btnGuardarOS;
        string stIdAreaServicio;
      
    

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.OrdenDeServicio);
            lst = FindViewById<ListView>(Resource.Id.lstOrdenServicio);
            btnGeneAudi= FindViewById<Button>(Resource.Id.btnGenerarAuditoria);
            btnBuscarOS = FindViewById<Button>(Resource.Id.btnBuscar);
            btnGuardarOS = FindViewById<Button>(Resource.Id.btnGuardar);
            //btnLStos = FindViewById<Button>(Resource.Id.btnLSTorServ);
            btnGeneAudi.Click += BtnGeneAudi_Click;
            btnBuscarOS.Click += BtnBuscarOS_Click;
           
            stIdAreaServicio = Intent.GetStringExtra("idAreaServicio");
           

        }


      

        private void BtnGuardarOS_Click(object sender, EventArgs e)
        {
            try
            {
                
            }
            catch(Exception ex)
            {

            }
        }

        private void BtnBuscarOS_Click(object sender, EventArgs e)
        {
            try
            {
                List<clsConsultarOrdenServicio> lstOS = new List<clsConsultarOrdenServicio>();

                SelectOrdenServicio objOrdenServicio = new SelectOrdenServicio();
                objOrdenServicio.usu_IDAreaServicio = stIdAreaServicio;
                string stIdOrdenServicio = JsonConvert.SerializeObject(objOrdenServicio);

                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string urlConsultarOrdenServicio = "http://DCWebApi.somee.com/api/OrdenServicioController/SelectOrdenServicio?stObj=" + stIdOrdenServicio;
                HttpResponseMessage response = client.GetAsync(urlConsultarOrdenServicio).Result;
                if (response.IsSuccessStatusCode)
                {

                    var ResultadoConsultarOrdenServicio = JsonConvert.DeserializeObject(response.Content.ReadAsStringAsync().Result);

                    var ValorOS = JsonConvert.DeserializeObject(ResultadoConsultarOrdenServicio.ToString());

                    foreach (var element in (JArray)ValorOS)
                    {

                        clsConsultarOrdenServicio objConsu = new clsConsultarOrdenServicio();
                        string stFechaAlta = ((JObject)element).SelectToken("$.orServ_fechaAlta").ToString();
                        

                        string stFechaCierre = ((JObject)element).SelectToken("$.orServ_fechaCierre").ToString();
                        //string stFechaInicio = ((JObject)element).SelectToken("$.orServ_fechaInicio").ToString();


                        string stFechaVenc = ((JObject)element).SelectToken("$.orServ_fechaVencimiento").ToString();


                        objConsu.orServ_fechaAlta =  stFechaAlta;
                        objConsu.orServ_numero =  ((JObject)element).SelectToken("$.orServ_numero").ToString();
                        objConsu.orServ_observaciones =  ((JObject)element).SelectToken("$.orServ_observ").ToString();
                        if (stFechaCierre != "")
                        {
                            objConsu.orServ_fechaCierre = stFechaCierre;
                        }
                        else
                        {
                            objConsu.orServ_fechaCierre = "";
                        }
                        //if (stFechaInicio != "")
                        //{
                        //    objConsu.orServ_fechaInicio = stFechaInicio;
                        //}
                        //else
                        //{
                        //    objConsu.orServ_fechaInicio = "";
                        //}
                        objConsu.orServ_fechaVencimiento = stFechaVenc;
                        objConsu.orServ_EstadoOrdenServicio = ((JObject)element).SelectToken("$.orServ_EstadoOrdenServicio").ToString();
                        objConsu.orServ_IDOrdenServicio = ((JObject)element).SelectToken("$.orServ_IDOrdenServicio").ToString();
                        objConsu.orServ_IDAreaServicio = stIdAreaServicio;

                       
                            lstOS.Add(objConsu);
                     
                        
                    }


                    List<string> lstEstadoOrdenServicioNombre = new List<string>();
                    List<string> lstEstadoOrdenServicioID = new List<string>();
                    string urlEstadoOrdenServicio = "http://DCWebApi.somee.com/api/OrdenServicioController/SelectEstadoOrdenServicio";
                    HttpResponseMessage responseOS = client.GetAsync(urlEstadoOrdenServicio).Result;
                    if (responseOS.IsSuccessStatusCode)
                    {
                        var ResultadoEstadoOrdenServicio = JsonConvert.DeserializeObject(responseOS.Content.ReadAsStringAsync().Result);
                        var ValorEstadoOS = JsonConvert.DeserializeObject(ResultadoEstadoOrdenServicio.ToString());
                       
                        foreach (var element in (JArray)ValorEstadoOS)
                        {
                            clsEstadoOrdenServicio objEstadoOS = new clsEstadoOrdenServicio();
                            objEstadoOS.estOrd_IDEstado =((JObject)element).SelectToken("$.estOrd_IDEstadoOrdenServicio").ToString();
                            objEstadoOS.estOrd_nombre = ((JObject)element).SelectToken("$.estOrd_nombre").ToString();

                            lstEstadoOrdenServicioID.Add(objEstadoOS.estOrd_IDEstado);
                            lstEstadoOrdenServicioNombre.Add(objEstadoOS.estOrd_nombre);

                        }
                    }
                    
                    var adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, lstEstadoOrdenServicioNombre);
                    clsListarOS clslos = new clsListarOS(this, lstOS, adapter,lstEstadoOrdenServicioID);
                    lst.Adapter = clslos;
                }


            }
            catch(Exception ex)
            {
                AlertDialog ad = new AlertDialog.Builder(this).Create();
                ad.SetTitle("Error");
                ad.SetMessage("Error.Excepción");
                ad.SetButton("Ok", (g, h) => { });
                ad.Show();
            }
        }

        private void BtnGeneAudi_Click(object sender, EventArgs e)
        {
            try
            {
                StartActivity(typeof(ActivityGenerarAuditoria));
            }
            catch(Exception ex)
            {

            }
        }
    }
}