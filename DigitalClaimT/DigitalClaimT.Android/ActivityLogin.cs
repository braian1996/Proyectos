using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web.Services.Protocols;
using Android.Animation;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Views.Animations;
using Android.Widget;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using Org.Json;

namespace DigitalClaimT.Droid
{
    [Activity(Label = "")]
    public class ActivityLogin : Activity
    {
        EditText t1;
        EditText t2;
        AutoCompleteTextView edtEmail;
        //ImageView imgLogi;
        private ProgressDialog progressDialog;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            try
            {
                base.OnCreate(savedInstanceState);

                SetContentView(Resource.Layout.login);

                ActionBar.Hide();
                OnBackPressed();

                t1 = FindViewById<EditText>(Resource.Id.editText1);
                t2 = FindViewById<EditText>(Resource.Id.editText2);
                TextView tvLink = FindViewById<TextView>(Resource.Id.textViewOlvidasteContra);
                TextView tvLinkCrearUsuario = FindViewById<TextView>(Resource.Id.textViewRegistrarUsuario);
                //imgLogi = FindViewById<ImageView>(Resource.Id.imageView1);
                //imgLogi.SetImageResource(Resource.Drawable.digitalClaim1);
                Button btnlogin1 = FindViewById<Button>(Resource.Id.button1);
                Button btnSalirLoginn = FindViewById<Button>(Resource.Id.btnSalirToolBarLogin);

                btnlogin1.Click += Btnlogin1_Click;
                //btnSalirLogin.Click += BtnSalirLogin_Click;
                btnSalirLoginn.Click += BtnSalirLoginn_Click;

                tvLink.Click += TvLink_Click;
                tvLinkCrearUsuario.Click += TvLinkCrearUsuario_Click;

                progressDialog = new ProgressDialog(this);
            }
            catch (Exception ex)
            {

            }
    

        }

        private void BtnSalirLoginn_Click(object sender, EventArgs e)
        {
            try
            {
                FinishAffinity();
            }
            catch (Exception ex)
            {

                
            }
        }

        override
        public void OnBackPressed()
        {

        }
      

        private void TvLinkCrearUsuario_Click(object sender, EventArgs e)
        {
            try
            {
                Intent secondActivityCrearCuenta = new Intent(this, typeof(ActivityCrearCuenta));
                StartActivity(secondActivityCrearCuenta);
            }
            catch (Exception ex)
            {

               
            }
        }

        private void TvLink_Click(object sender, EventArgs e)
        {
            try
            {
                AlertDialog.Builder builder = new AlertDialog.Builder(this);
                builder.SetTitle("Recuperar Contraseña");
                LayoutInflater inflater = LayoutInflater;
                View v = inflater.Inflate(Resource.Layout.alertDialogRecupeContra, null);
                builder.SetView(v);
                builder.SetPositiveButton("ENVIAR", btnEnviarRecuContra);

               edtEmail = (AutoCompleteTextView)v.FindViewById<AutoCompleteTextView>(Resource.Id.mail);


                AlertDialog alertdialog = builder.Create();
                alertdialog.Show();
            }
            catch (Exception ex)
            {

                
            }
        }

        private void btnEnviarRecuContra(object sender, DialogClickEventArgs e)
        {
            try
            {
                RecuperarPassword objRePass = new RecuperarPassword();
                objRePass.usu_email = edtEmail.Text;
                string objEmail = JsonConvert.SerializeObject(objRePass);
           
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string urlRecuPass = "http://DCWebApi.somee.com/api/LoginController/RecuperarPassword?stObj=" + objEmail;
                HttpResponseMessage response = client.GetAsync(urlRecuPass).Result;
                if (response.IsSuccessStatusCode)
                {
                }
            }
            catch (Exception ex)
            {

                
            }
        }

        private void Btnlogin1_Click(object sender, EventArgs e)
        {
            try
            {
                progressDialog.SetMessage("Iniciado sesión");
                //muestras el ProgressDialog
                progressDialog.Indeterminate = true;
                progressDialog.SetCancelable(false);
                progressDialog.Show();

                if (t1.Text != "" && t2.Text != "")
                {

    
                    clsLogin objlogin = new clsLogin();
                    objlogin.usu_username = t1.Text;
                    objlogin.usu_password = t2.Text;
                    objlogin.su_fechaInicio = DateTime.Now.ToString("dd/MM/yyyy");
                    objlogin.su_horaInicio = DateTime.Now.ToString("hh:mm");
                    var Json = JsonConvert.SerializeObject(objlogin);
                    HttpClient client = new HttpClient();

                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    string url = "http://DCWebApi.somee.com/api/LoginController/ValidarSesion?stObj=" + Json;
                    HttpResponseMessage response = client.GetAsync(url).Result;
                    if (response.IsSuccessStatusCode)
                    {

                        var ResultadoLogin = JsonConvert.DeserializeObject(response.Content.ReadAsStringAsync().Result);

                        JObject o = JObject.Parse(ResultadoLogin.ToString());
                        clsSesion ObjSesion = new clsSesion();
                        ObjSesion.usu_IDRol = o["usu_IDRol"].ToString();
                        ObjSesion.usu_username = o["usu_username"].ToString();
                        ObjSesion.usu_IDUsuario = o["usu_IDUsuario"].ToString();
                        ObjSesion.usu_password = o["usu_password"].ToString();
                        ObjSesion.usu_nombre = o["usu_nombre"].ToString();
                        ObjSesion.usu_apellido = o["usu_apellido"].ToString();
                        ObjSesion.usu_dni = o["usu_dni"].ToString();
                        ObjSesion.usu_telefono = o["usu_telefono"].ToString();
                        ObjSesion.usu_email = o["usu_email"].ToString();
                        string valorAreaServicio = o["usu_IDAreaServicio"].ToString();
                        
                        ObjSesion.usu_IDSesion = o["usu_IDSesion"].ToString();

                        if (valorAreaServicio != null)
                        {
                            ObjSesion.usu_IDAreaServicio = o["usu_IDAreaServicio"].ToString();
                            ObjSesion.usu_arServNombre = o["usu_areaServicio"].ToString();
                        }
                        if (ObjSesion.usu_IDRol != "" && ObjSesion.usu_username != "")
                        {
                            Intent secondActivityIntentParcelable = new Intent(this, typeof(ActivityMenu));
                            secondActivityIntentParcelable.PutExtra("usuarioNombre", ObjSesion.usu_username);
                            secondActivityIntentParcelable.PutExtra("usuarioId", ObjSesion.usu_IDUsuario);
                            secondActivityIntentParcelable.PutExtra("usuarioIdRol", ObjSesion.usu_IDRol.ToString());
                            secondActivityIntentParcelable.PutExtra("NombreCompleto", ObjSesion.usu_nombre + " " + ObjSesion.usu_apellido);
                            secondActivityIntentParcelable.PutExtra("email", ObjSesion.usu_email);
                            secondActivityIntentParcelable.PutExtra("telefono", ObjSesion.usu_telefono);
                            secondActivityIntentParcelable.PutExtra("CodigoReclamo", "0");
                            secondActivityIntentParcelable.PutExtra("idAreaServicio", ObjSesion.usu_IDAreaServicio.ToString());
                            secondActivityIntentParcelable.PutExtra("AreaServicioNombre", ObjSesion.usu_arServNombre.ToString());
                            secondActivityIntentParcelable.PutExtra("password", ObjSesion.usu_password);
                            secondActivityIntentParcelable.PutExtra("nombre", ObjSesion.usu_nombre);
                            secondActivityIntentParcelable.PutExtra("apellido", ObjSesion.usu_apellido);
                            secondActivityIntentParcelable.PutExtra("dni", ObjSesion.usu_dni);
                            secondActivityIntentParcelable.PutExtra("IDSesion", ObjSesion.usu_IDSesion);
                            StartActivity(secondActivityIntentParcelable);
                            //Finish();

                           // progressDialog.Dismiss();
                            new Handler().PostDelayed(() =>
                            {
                                progressDialog.Dismiss();
                            }, 3000);
                        }
                    }
                }
                else
                {
                    AlertDialog ad = new AlertDialog.Builder(this).Create();
                    ad.SetTitle("Error al ingresar");
                    ad.SetMessage("Por favor ingrese su Usuario y Contraseña!");
                    ad.SetButton("Ok", (g, h) => { });
                    ad.Show();
                    t1.Text = "";
                    t2.Text = "";

                    new Handler().PostDelayed(() =>
                    {
                        progressDialog.Dismiss();
                    }, 1000);
                }
                //Finish();
            }
            catch (SoapException ex)
            {

            }
            catch (Exception ex)
            {
                AlertDialog ad = new AlertDialog.Builder(this).Create();
                ad.SetTitle("Error");
                ad.SetMessage("Por favor verifique usuario y contraseña!");
                ad.SetButton("Ok", (g, h) => { });
                ad.Show();
                t1.Text = "";
                t2.Text = "";

                new Handler().PostDelayed(() =>
                {
                    progressDialog.Dismiss();
                }, 1000);
            }



        }


    }

}