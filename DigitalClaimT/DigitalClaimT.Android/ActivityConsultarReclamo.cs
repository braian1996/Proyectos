using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DigitalClaimT.Droid
{
    [Activity(Label = "")]
    public class ActivityConsultarReclamo : Activity
    {

        ListView lstConsultarReclamo;
        EditText edtConsultar;
        //List<clsConsultarReclamo> lstCombo = null;
        //List<string> lstEstadoReclamoNom = null;
        List<clsConsultarReclamo> lst = new List<clsConsultarReclamo>();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            try
            {
                base.OnCreate(savedInstanceState);

                SetContentView(Resource.Layout.ConsultarReclamo);

                lstConsultarReclamo = FindViewById<ListView>(Resource.Id.listViewCR);
                //Spinner spnFiltrado = FindViewById<Spinner>(Resource.Id.spinnerEstadoReclamo);
                lstConsultarReclamo.ChoiceMode = ChoiceMode.Single;
                edtConsultar = FindViewById<EditText>(Resource.Id.edtNumeroReclamo);
                edtConsultar.TextChanged += EdtConsultar_TextChanged;




                string stIdUsuario = Intent.GetStringExtra("idUsuario");

                clsReclamoConsulta objConsuReclamo = new clsReclamoConsulta();
                objConsuReclamo.stFiltro = "usu_ID=" + stIdUsuario;
                // string stFiltro = "stFiltro" + ":" + "usu_ID=" + stIdUsuario;

                string stValorIDUsuario = JsonConvert.SerializeObject(objConsuReclamo);

                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string urlConsultarReclamo = "http://DCWebApi.somee.com/api/ReclamoController/SelectReclamo?stObj=" + stValorIDUsuario;
                HttpResponseMessage response = client.GetAsync(urlConsultarReclamo).Result;
                if (response.IsSuccessStatusCode)
                {
                    var ResultadoConsultarReclamo = JsonConvert.DeserializeObject(response.Content.ReadAsStringAsync().Result);
                    var listaReclamo = JsonConvert.DeserializeObject(ResultadoConsultarReclamo.ToString());
                    foreach (var element in (JArray)listaReclamo)
                    {
                        clsConsultarReclamo objConsu = new clsConsultarReclamo();

                        //string fr = ((JObject)element).SelectToken("$.rec_fechaAlta").ToString();
                        //DateTime fa = Convert.ToDateTime(fr);

                        objConsu.rec_fechaAlta = ((JObject)element).SelectToken("$.rec_fechaAlta").ToString();
                        objConsu.rec_ID = ((JObject)element).SelectToken("$.rec_ID").ToString();
                        objConsu.rec_codigo = ((JObject)element).SelectToken("$.rec_codigo").ToString();
                        objConsu.tipRec_nombre = ((JObject)element).SelectToken("$.tipRec_nombre").ToString();
                        objConsu.arServ_nombre = ((JObject)element).SelectToken("$.arServ_nombre").ToString();
                        objConsu.usu_ID = ((JObject)element).SelectToken("$.usu_ID").ToString();
                        objConsu.usu_DNI = ((JObject)element).SelectToken("$.usu_DNI").ToString();
                        objConsu.bar_nombre = ((JObject)element).SelectToken("$.bar_nombre").ToString();
                        objConsu.rec_direccion = ((JObject)element).SelectToken("$.rec_direccion").ToString();
                        objConsu.rec_Foto = ((JObject)element).SelectToken("$.rec_Foto").ToString();
                        //if (foto != "")
                        //{
                            //objConsu.rec_Foto = foto;
                        //}


                        lst.Add(objConsu);
                    }
                }
                ClsLista clsfiltro = new ClsLista(this, lst);
                lstConsultarReclamo.Adapter = clsfiltro;
            }
            catch (Exception ex)
            {

                
            }

         
        }


        private void EdtConsultar_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            if (lst.Count != 0)
            {
                if (edtConsultar.Text != "")
                {
                    lstConsultarReclamo.Adapter = null;
                    List<clsConsultarReclamo> clsFiltro = new List<clsConsultarReclamo>();
                    foreach (var item in lst)
                    {

                        if (item.rec_codigo.Contains(edtConsultar.Text) || item.rec_direccion.Contains(edtConsultar.Text) || item.bar_nombre.Contains(edtConsultar.Text) || item.arServ_nombre.Contains(edtConsultar.Text) || item.tipRec_nombre.Contains(edtConsultar.Text))
                        {
                            clsConsultarReclamo objLLenarReclamo = new clsConsultarReclamo();
                            objLLenarReclamo.rec_fechaAlta = item.rec_fechaAlta;
                            objLLenarReclamo.rec_ID = item.rec_ID;
                            objLLenarReclamo.rec_codigo = item.rec_codigo;
                            objLLenarReclamo.usu_ID = item.usu_ID;
                            objLLenarReclamo.usu_DNI = item.usu_DNI;
                            objLLenarReclamo.bar_nombre = item.bar_nombre;
                            objLLenarReclamo.rec_direccion = item.rec_direccion;
                            objLLenarReclamo.arServ_nombre = item.arServ_nombre;
                            objLLenarReclamo.tipRec_nombre = item.tipRec_nombre;
                            clsFiltro.Add(objLLenarReclamo);
                        }
                    }
                    ClsLista clsfiltro = new ClsLista(this, clsFiltro);
                    lstConsultarReclamo.Adapter = clsfiltro;
                    //edtConsultar.Text = "";
                }
                else
                {
                    lstConsultarReclamo.Adapter = null;
                    ClsLista cls = new ClsLista(this, lst);
                    lstConsultarReclamo.Adapter = cls;
                    //resultadoConsu = "";
                    //edtConsultar.Text = "";
                }
            }
        }

        
      
    }
}