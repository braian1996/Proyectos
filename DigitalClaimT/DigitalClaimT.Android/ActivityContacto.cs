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

namespace DigitalClaimT.Droid
{
    [Activity(Label = "")]
    public class ActivityContacto : Activity
    {
        Button btnEnviar;
        Button btnAtras;
        EditText edtNomYApel;
        EditText edtTelefono;
        EditText edtEmail;
        EditText edtMensaje;

        string stNombreYApellido;
        string stEmail;
        string stTelefono;
        string idusuario;
        string idRol;
        string stUsername;
        string stNombre;
        string stApellido;
        string stDni;
        string stPassword;
   

        protected override void OnCreate(Bundle savedInstanceState)
        {

            try
            {
                base.OnCreate(savedInstanceState);

                // Create your application here
                SetContentView(Resource.Layout.Contacto);

                ActionBar.Hide();
                OnBackPressed();

                edtNomYApel = FindViewById<EditText>(Resource.Id.edtNombreyApellido);
                edtTelefono = FindViewById<EditText>(Resource.Id.edtTelefono);
                edtEmail = FindViewById<EditText>(Resource.Id.edtEmail);
                edtMensaje = FindViewById<EditText>(Resource.Id.edtMensaje);
                btnEnviar = FindViewById<Button>(Resource.Id.btnEnviar);
                btnAtras = FindViewById<Button>(Resource.Id.btnAtrasToolBar);

                stNombreYApellido = Intent.GetStringExtra("NombreYApellido"); 
                stEmail = Intent.GetStringExtra("email");
                stTelefono = Intent.GetStringExtra("telefono");
                idusuario = Intent.GetStringExtra("idusuario");
                idRol = Intent.GetStringExtra("usuarioIdRol");
                stUsername = Intent.GetStringExtra("usuarioNombre");
                stTelefono = Intent.GetStringExtra("telefono");
                stPassword = Intent.GetStringExtra("password");
                stNombre = Intent.GetStringExtra("nombre");
                stApellido = Intent.GetStringExtra("apellido");
                stDni = Intent.GetStringExtra("dni");
                

                edtNomYApel.Text = stNombreYApellido;
                edtTelefono.Text = stTelefono;
                edtEmail.Text = stEmail;

                btnEnviar.Click += BtnEnviar_Click;
                btnAtras.Click += BtnAtras_Click;
            }
            catch (Exception ex)
            {

                
            }
      
        }

        private void BtnAtras_Click(object sender, EventArgs e)
        {
            try
            {
                Intent secondActivityIntentParcelable = new Intent(this, typeof(ActivityMenu));
                secondActivityIntentParcelable.PutExtra("usuarioNombre", stUsername);
                secondActivityIntentParcelable.PutExtra("usuarioId", idusuario);
                secondActivityIntentParcelable.PutExtra("usuarioIdRol", idRol);
                secondActivityIntentParcelable.PutExtra("NombreCompleto", stNombreYApellido);
                secondActivityIntentParcelable.PutExtra("email", stEmail);
                secondActivityIntentParcelable.PutExtra("CodigoReclamo", "0");
                secondActivityIntentParcelable.PutExtra("telefono", stTelefono);
                secondActivityIntentParcelable.PutExtra("nombre", stNombre);
                secondActivityIntentParcelable.PutExtra("apellido", stApellido);
                secondActivityIntentParcelable.PutExtra("dni", stDni);
                secondActivityIntentParcelable.PutExtra("password", stPassword);

                StartActivity(secondActivityIntentParcelable);
                Finish();
            }
            catch (Exception ex)
            {

                
            }
        }

        override
        public void OnBackPressed()
        {

        }
        private void BtnEnviar_Click(object sender, EventArgs e)
        {
            try
            {
                if (edtMensaje.Text != "")
                {
                    clsContacto objContacto = new clsContacto();
                    objContacto.con_fechaAlta = DateTime.Now.ToString("dd/MM/yyyy");
                    objContacto.con_IDUsuario = idusuario;
                    objContacto.con_mensaje = edtMensaje.Text;

                    string valorContacto = JsonConvert.SerializeObject(objContacto);

                    HttpClient client = new HttpClient();

                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    string url = "http://DCWebApi.somee.com/api/ReclamoController/RegistrarContacto?stObj=" + valorContacto;
                    HttpResponseMessage response = client.GetAsync(url).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        AlertDialog.Builder builder = new AlertDialog.Builder(this);
                        builder.SetTitle("Mensaje enviado");
                        builder.SetIcon(Resource.Drawable.check);
                        builder.SetMessage("El mensaje se ha enviado con éxito. Su consulta será revisado y le enviaremos una respuesta a su correo.");
                        builder.SetPositiveButton("Ok", btnOk);


                        AlertDialog alertdialog = builder.Create();
                        alertdialog.Show();
                    }
                }
              

            }
            catch (Exception)
            {

                
            }
        }

        private void btnOk(object sender, DialogClickEventArgs e)
        {
            try
            {
                Intent secondActivityIntentParcelable = new Intent(this, typeof(ActivityMenu));
                secondActivityIntentParcelable.PutExtra("usuarioNombre", stUsername);
                secondActivityIntentParcelable.PutExtra("usuarioId", idusuario);
                secondActivityIntentParcelable.PutExtra("usuarioIdRol", idRol);
                secondActivityIntentParcelable.PutExtra("NombreCompleto", stNombreYApellido);
                secondActivityIntentParcelable.PutExtra("email", stEmail);
                secondActivityIntentParcelable.PutExtra("CodigoReclamo", "0");
                secondActivityIntentParcelable.PutExtra("telefono", stTelefono);
                secondActivityIntentParcelable.PutExtra("nombre", stNombre);
                secondActivityIntentParcelable.PutExtra("apellido", stApellido);
                secondActivityIntentParcelable.PutExtra("dni", stDni);
                secondActivityIntentParcelable.PutExtra("password", stPassword);

                StartActivity(secondActivityIntentParcelable);
                Finish();
            }
            catch (Exception ex)
            {

                
            }
        }
    }
}