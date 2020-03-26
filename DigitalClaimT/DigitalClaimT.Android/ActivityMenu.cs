using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Web.Services.Protocols;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Net.Http.Headers;

namespace DigitalClaimT.Droid
{
    [Activity(Label = "")]
    public class ActivityMenu : Activity
    {
        Button btnEnviarRecl;
        Button btnConsuRecl;
        Button btnContacto;
        Button btnPerfil;
        Button btnGestiOrdenSer;
        Button btnAudito;
        TextView usuario;
        TextView tvAreaServicioMuestra;
        TextView tvAreaServicioNombre;
        TextView tvUsuarioNombre;
        string stIdUsuario;
        string stNombreCompleto;
        string stEmail;
        string stNomUsuario;
        string stIdRolUsuario;
        string stCodigoReclamo;
        string stAreaServicio;
        string stAreaServicioNombre;
        string stTelefono;
        string stPassword;
        string stNombre;
        string stApellido;
        string stIdSesion;
        string stDni;
        Button btnSalirToolbar;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            try
            {

                base.OnCreate(savedInstanceState);

                // Create your application here
                SetContentView(Resource.Layout.menu);

                ActionBar.Hide();
                OnBackPressed();

                ImageView imgLogi = FindViewById<ImageView>(Resource.Id.imageView1);
                imgLogi.SetImageResource(Resource.Drawable.digitalClaim1);

                // imgMenu = FindViewById<ImageView>(Resource.Id.imageView2);
                


                btnEnviarRecl = FindViewById<Button>(Resource.Id.btnEnviarRe);
                btnConsuRecl = FindViewById<Button>(Resource.Id.btnConsuRecl);
                btnContacto = FindViewById<Button>(Resource.Id.btnContacto);
                btnSalirToolbar = FindViewById<Button>(Resource.Id.btnSalirToolBar);
                btnPerfil = FindViewById<Button>(Resource.Id.btnPerfil);
                btnGestiOrdenSer = FindViewById<Button>(Resource.Id.btnGesOrSer);
                btnAudito = FindViewById<Button>(Resource.Id.btnAuditori);
                usuario = FindViewById<TextView>(Resource.Id.textViewUsuarioTB);
                tvAreaServicioMuestra = FindViewById<TextView>(Resource.Id.textViewMuestra);
                tvAreaServicioNombre = FindViewById<TextView>(Resource.Id.textViewAreaServicio);
                tvUsuarioNombre = FindViewById<TextView>(Resource.Id.textView1UsuarioNombre);


                btnEnviarRecl.Click += BtnEnviarRecl_Click;
                btnConsuRecl.Click += BtnConsuRecl_Click;
                btnGestiOrdenSer.Click += BtnGestiOrdenSer_Click;
                btnAudito.Click += BtnAudito_Click;
                btnContacto.Click += BtnContacto_Click;
                btnPerfil.Click += BtnPerfil_Click;
                btnSalirToolbar.Click += BtnSalirToolbar_Click;

                stNomUsuario = Intent.GetStringExtra("usuarioNombre");
                stIdUsuario = Intent.GetStringExtra("usuarioId");
                stIdRolUsuario = Intent.GetStringExtra("usuarioIdRol");
                stNombreCompleto = Intent.GetStringExtra("NombreCompleto");
                stEmail = Intent.GetStringExtra("email");
                stCodigoReclamo = Intent.GetStringExtra("CodigoReclamo");
                stAreaServicio = Intent.GetStringExtra("idAreaServicio");
                stAreaServicioNombre = Intent.GetStringExtra("AreaServicioNombre");
                stTelefono = Intent.GetStringExtra("telefono");
                stPassword = Intent.GetStringExtra("password");
                stNombre = Intent.GetStringExtra("nombre");
                stApellido = Intent.GetStringExtra("apellido");
                stDni = Intent.GetStringExtra("dni");
                stIdSesion = Intent.GetStringExtra("IDSesion");

                if (stCodigoReclamo != "0")
                {
                    AlertDialog ad = new AlertDialog.Builder(this).Create();
                    ad.SetTitle("Reclamo Registrado" + " " + stCodigoReclamo);
                    ad.SetIcon(Resource.Drawable.check);
                    ad.SetMessage("Su reclamo a sido registrado correctamente. El código mostrado en pantalla corrsponde al de su reclamo. Se envió a su casilla de correo para que pueda consultarlo posteriormente");
                    ad.SetButton("OK", (g, h) =>
                    {
                    });
                    ad.Show();
                }

                usuario.Text = stNomUsuario;
                tvAreaServicioNombre.Text = stAreaServicioNombre;

                if (stIdRolUsuario == "1")
                {
                    btnEnviarRecl.Visibility = ViewStates.Visible;
                    btnConsuRecl.Visibility = ViewStates.Visible;
                    btnContacto.Visibility = ViewStates.Visible;
                    btnPerfil.Visibility = ViewStates.Visible;
                    btnGestiOrdenSer.Visibility = ViewStates.Gone;
                    btnAudito.Visibility = ViewStates.Gone;
                    tvAreaServicioMuestra.Visibility = ViewStates.Invisible;
                    tvAreaServicioNombre.Visibility = ViewStates.Invisible;
                }
                else
                {
                    if (stIdRolUsuario == "6" || stIdRolUsuario == "4")
                    {
                        btnEnviarRecl.Visibility = ViewStates.Gone;
                        btnConsuRecl.Visibility = ViewStates.Gone;
                        btnContacto.Visibility = ViewStates.Gone;
                        btnPerfil.Visibility = ViewStates.Gone;
                        btnGestiOrdenSer.Visibility = ViewStates.Visible;
                        btnAudito.Visibility = ViewStates.Visible;
                        tvAreaServicioMuestra.Visibility = ViewStates.Visible;
                        tvAreaServicioNombre.Visibility = ViewStates.Visible;
                        tvUsuarioNombre.Text = "Usuario Operativo: ";

                    }

                }
              
            }
            catch(Exception ex)
            {

            }
        }
        override
        public void OnBackPressed()
        {

        }
        private void BtnSalirToolbar_Click(object sender, EventArgs e)
        {
            try
            {
               

                AlertDialog.Builder builder = new AlertDialog.Builder(this);
                builder.SetTitle("Salir");
                builder.SetMessage("¿Desea salir de la aplicación?");
                builder.SetPositiveButton("Aceptar", btnAceptarSalir);
                builder.SetNegativeButton("Cancelar", btnCancelarSalir);


                AlertDialog alertdialog = builder.Create();
                alertdialog.Show();
            }
            catch (Exception ex)
            {

               
            }
        }

        private void btnAceptarSalir(object sender, DialogClickEventArgs e)
        {
            try
            {
                clsLogin objCierreSesion = new clsLogin();
                objCierreSesion.su_fechaFin = DateTime.Now.ToString("dd/MM/yyyy");
                objCierreSesion.su_horaFin = DateTime.Now.ToString("hh:mm");
                objCierreSesion.su_IDSesion = stIdSesion;
                var stCierreSesion = JsonConvert.SerializeObject(objCierreSesion);

                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string urlCierreSesion = "http://DCWebApi.somee.com/api/LoginController/RegistrarCierreSesion?stObj=" + stCierreSesion;
                HttpResponseMessage responseCierreSesion =  client.GetAsync(urlCierreSesion).Result;
                if (responseCierreSesion.IsSuccessStatusCode)
                {
                    string ResultadoCierreSesion = JsonConvert.DeserializeObject(responseCierreSesion.Content.ReadAsStringAsync().Result).ToString();
                    if (ResultadoCierreSesion == "1")
                    {
                        // FinishAffinity();
                        StartActivity(typeof(ActivityLogin));
                        Finish();
                    }
                    else
                    {
                        StartActivity(typeof(ActivityLogin));
                        Finish();
                    }
                }
           
            }
            catch (Exception)
            {

                
            }
        }

        private void btnCancelarSalir(object sender, DialogClickEventArgs e)
        {
            try
            {
               
            }
            catch (Exception ex)
            {

                
            }
        }

        private void BtnPerfil_Click(object sender, EventArgs e)
        {
            try
            {
                Intent secondActivityIntentPerfil = new Intent(this, typeof(ActivityPerfil));
                secondActivityIntentPerfil.PutExtra("nombre", stNombre);
                secondActivityIntentPerfil.PutExtra("apellido", stApellido);
                secondActivityIntentPerfil.PutExtra("email", stEmail);
                secondActivityIntentPerfil.PutExtra("usuarioNombre", stNomUsuario);
                secondActivityIntentPerfil.PutExtra("password", stPassword);
                secondActivityIntentPerfil.PutExtra("dni", stDni);
                secondActivityIntentPerfil.PutExtra("telefono", stTelefono);

                secondActivityIntentPerfil.PutExtra("usuarioId", stIdUsuario);
                secondActivityIntentPerfil.PutExtra("usuarioIdRol", stIdRolUsuario);
                secondActivityIntentPerfil.PutExtra("NombreCompleto", stNombreCompleto);
                secondActivityIntentPerfil.PutExtra("CodigoReclamo", "0");

                StartActivity(secondActivityIntentPerfil);
                Finish();
            }
            catch (Exception)
            {

                
            }
        }

        private void BtnContacto_Click(object sender, EventArgs e)
        {
            try
            {
                //Intent secondActivityIntentContacto = new Intent(this, typeof(ActivityContacto));
                //secondActivityIntentContacto.PutExtra("NombreYApellido", stNombreCompleto);
                //secondActivityIntentContacto.PutExtra("telefono", stTelefono);
                //secondActivityIntentContacto.PutExtra("email", stEmail);
                //secondActivityIntentContacto.PutExtra("idusuario", stIdUsuario);
                //secondActivityIntentContacto.PutExtra("usuarioIdRol", stIdRolUsuario);
                //secondActivityIntentContacto.PutExtra("usuarioNombre", stNomUsuario);
                //secondActivityIntentContacto.PutExtra("password", stPassword);
                //secondActivityIntentContacto.PutExtra("nombre", stNombre);
                //secondActivityIntentContacto.PutExtra("apellido", stApellido);
                //secondActivityIntentContacto.PutExtra("dni", stDni);
                //StartActivity(secondActivityIntentContacto);
                //Finish();

            }
            catch (Exception)
            {

                
            }
        }

        private void BtnAudito_Click(object sender, EventArgs e)
        {
            try
            {
                Intent secondActivityIntentPerfil = new Intent(this, typeof(ActivityPerfil));
                secondActivityIntentPerfil.PutExtra("nombre", stNombre);
                secondActivityIntentPerfil.PutExtra("apellido", stApellido);
                secondActivityIntentPerfil.PutExtra("email", stEmail);
                secondActivityIntentPerfil.PutExtra("usuarioNombre", stNomUsuario);
                secondActivityIntentPerfil.PutExtra("password", stPassword);
                secondActivityIntentPerfil.PutExtra("dni", stDni);
                secondActivityIntentPerfil.PutExtra("telefono", stTelefono);

                secondActivityIntentPerfil.PutExtra("usuarioId", stIdUsuario);
                secondActivityIntentPerfil.PutExtra("usuarioIdRol", stIdRolUsuario);
                secondActivityIntentPerfil.PutExtra("NombreCompleto", stNombreCompleto);
                secondActivityIntentPerfil.PutExtra("CodigoReclamo", "0");
                secondActivityIntentPerfil.PutExtra("areaservicio", stAreaServicioNombre);

                StartActivity(secondActivityIntentPerfil);
                Finish();
            }
            catch(Exception ex)
            {

            }
        }

        private void BtnGestiOrdenSer_Click(object sender, EventArgs e)
        {
            try
            {
                //StartActivity(typeof(ActivityOrdenDeServicio));
                Intent secondActivityIntentOrdenServicio = new Intent(this, typeof(ActivityOrdenDeServicio));
                secondActivityIntentOrdenServicio.PutExtra("idAreaServicio", stAreaServicio);
                StartActivity(secondActivityIntentOrdenServicio);
            }
            catch(Exception ex)
            {

            }
        }

        private void BtnConsuRecl_Click(object sender, EventArgs e)
        {
            try
            {
                Intent secondActivityConsuReclamo = new Intent(this, typeof(ActivityConsultarReclamo));
                secondActivityConsuReclamo.PutExtra("idUsuario", stIdUsuario);
                StartActivity(secondActivityConsuReclamo);
            }
            catch(Exception ex)
            {

            }
        }

        private void BtnEnviarRecl_Click(object sender, EventArgs e)
        {
            try
            {
                //clsSesion objValiReclam = new clsSesion();
                //objValiReclam.usu_IDUsuario = stIdUsuario;
                //var ValorValidacionReclamo = JsonConvert.SerializeObject(objValiReclam);

                //HttpClient client = new HttpClient();
                //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //string urlValidarRealiReclamo = "http://DCWebApi.somee.com/api/ReclamoController/ValidarRealizacionReclamo?stObj=" + ValorValidacionReclamo;
                //HttpResponseMessage responseValidarRealiReclamo = client.GetAsync(urlValidarRealiReclamo).Result;
                //if (responseValidarRealiReclamo.IsSuccessStatusCode)
                //{
                //    string ResultadoValidacionReclamo = JsonConvert.DeserializeObject(responseValidarRealiReclamo.Content.ReadAsStringAsync().Result).ToString();

                //    if (ResultadoValidacionReclamo != "null")
                //    {
                        //JObject objFechaReclamo = JObject.Parse(ResultadoValidacionReclamo.ToString());
                        //string stValidarFecha = objFechaReclamo["rec_fechaAlta"].ToString();


                        //string stFechaHoy = DateTime.Now.ToString("dd/MM/yyyy");

                        //if (stValidarFecha != stFechaHoy)
                        //{
                            Intent secondActivityEnviarReclamo = new Intent(this, typeof(ActivityEnviarReclamo));
                            secondActivityEnviarReclamo.PutExtra("idUsuario", stIdUsuario);
                            secondActivityEnviarReclamo.PutExtra("NombreCompleto", stNombreCompleto);
                            secondActivityEnviarReclamo.PutExtra("email", stEmail);
                            secondActivityEnviarReclamo.PutExtra("usuarioNombre", stNomUsuario);
                            secondActivityEnviarReclamo.PutExtra("idRol", stIdRolUsuario);
                            StartActivity(secondActivityEnviarReclamo);
                            // Finish();
                        //}
                        //else
                        //{
                            //AlertDialog ad = new AlertDialog.Builder(this).Create();
                            //ad.SetTitle("Registrar Reclamo");
                            //ad.SetIcon(Resource.Drawable.check);
                            //ad.SetMessage("No puede registrar un reclamo debido a que ya ha registrado uno en el día de la fecha, espere 24hs para poder llevar a cabo la operación nuevamente.");
                            //ad.SetButton("Ok", (g, h) => { });
                            //ad.Show();
                        //}


                //    }
                //    else
                //    {
                //        Intent secondActivityEnviarReclamo = new Intent(this, typeof(ActivityEnviarReclamo));
                //        secondActivityEnviarReclamo.PutExtra("idUsuario", stIdUsuario);
                //        secondActivityEnviarReclamo.PutExtra("NombreCompleto", stNombreCompleto);
                //        secondActivityEnviarReclamo.PutExtra("email", stEmail);
                //        secondActivityEnviarReclamo.PutExtra("usuarioNombre", stNomUsuario);
                //        secondActivityEnviarReclamo.PutExtra("idRol", stIdRolUsuario);
                //        StartActivity(secondActivityEnviarReclamo);
                //        // Finish();
                //    }
                //}
            }
            catch (Exception ex)
            {

            }
            
        }
    }
}