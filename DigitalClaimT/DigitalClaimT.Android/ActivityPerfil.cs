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
    public class ActivityPerfil : Activity
    {
        EditText edtUsername;
        EditText edtDni;
        EditText edtNombre;
        EditText edtApellido;
        EditText edtPassword;
        EditText edtEmail;
        EditText edtTelefono;
        Button btnGuardar;
        Button btnAtras;

        string stUsername;
        string idUsuario;
        string idRol;
        string stNombreYApellido;
        string stEmail;
        string stNombre;
        string stApellido;
        string stPassword;
        string stDni;
        string stTelefono;
        string areaservicio;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            try
            {
                base.OnCreate(savedInstanceState);
                SetContentView(Resource.Layout.perfil);

                ActionBar.Hide();
                OnBackPressed();

                edtUsername = FindViewById<EditText>(Resource.Id.edtUsernamePerfil);
                edtDni = FindViewById<EditText>(Resource.Id.edtDniPerfil);
                edtNombre = FindViewById<EditText>(Resource.Id.edtNombrePerfil);
                edtApellido = FindViewById<EditText>(Resource.Id.edtApellidoPerfil);
                edtPassword = FindViewById<EditText>(Resource.Id.edtContraseñaPerfil);
                edtEmail = FindViewById<EditText>(Resource.Id.edtEmailPerfil);
                edtTelefono = FindViewById<EditText>(Resource.Id.edtTelefonoPerfil);
                btnGuardar = FindViewById<Button>(Resource.Id.btnGuardarDatosPersonales);
                btnAtras = FindViewById<Button>(Resource.Id.btnAtrasToolBarPerfil);

                edtTelefono.Text = Intent.GetStringExtra("telefono");
                edtPassword.Text = Intent.GetStringExtra("password");
                edtNombre.Text = Intent.GetStringExtra("nombre");
                edtApellido.Text = Intent.GetStringExtra("apellido");
                edtDni.Text = Intent.GetStringExtra("dni");
                edtUsername.Text = Intent.GetStringExtra("usuarioNombre");
                edtEmail.Text = Intent.GetStringExtra("email");

                stEmail = Intent.GetStringExtra("email");
                stUsername = Intent.GetStringExtra("usuarioNombre");
                idUsuario = Intent.GetStringExtra("usuarioId");
                idRol = Intent.GetStringExtra("usuarioIdRol");
                stNombreYApellido = Intent.GetStringExtra("NombreCompleto");
                stTelefono = Intent.GetStringExtra("telefono");
                stPassword = Intent.GetStringExtra("password");
                stNombre = Intent.GetStringExtra("nombre");
                stApellido = Intent.GetStringExtra("apellido");
                stDni = Intent.GetStringExtra("dni");
                areaservicio = Intent.GetStringExtra("areaservicio");

                btnGuardar.Click += BtnGuardar_Click;
                btnAtras.Click += BtnAtras_Click;
            }
            catch (Exception)
            {

                
            }
            
            // Create your application here
        }
        override
        public void OnBackPressed()
        {

        }
        private void BtnAtras_Click(object sender, EventArgs e)
        {
            try
            {
                Intent secondActivityIntentParcelable = new Intent(this, typeof(ActivityMenu));
                secondActivityIntentParcelable.PutExtra("usuarioNombre", stUsername);
                secondActivityIntentParcelable.PutExtra("usuarioId", idUsuario);
                secondActivityIntentParcelable.PutExtra("usuarioIdRol", idRol);
                secondActivityIntentParcelable.PutExtra("NombreCompleto", stNombreYApellido);
                secondActivityIntentParcelable.PutExtra("email", stEmail);
                secondActivityIntentParcelable.PutExtra("CodigoReclamo", "0");
                secondActivityIntentParcelable.PutExtra("nombre", stNombre);
                secondActivityIntentParcelable.PutExtra("apellido", stApellido);
                secondActivityIntentParcelable.PutExtra("telefono", stTelefono);
                secondActivityIntentParcelable.PutExtra("dni", stDni);
                secondActivityIntentParcelable.PutExtra("password", stPassword);
                secondActivityIntentParcelable.PutExtra("AreaServicioNombre", areaservicio);

                StartActivity(secondActivityIntentParcelable);
                Finish();
            }
            catch (Exception ex)
            {

               
            }
          
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                clsPerfil objP = new clsPerfil();

                objP.usu_apellido = edtApellido.Text;
                objP.usu_nombre = edtNombre.Text;
                objP.usu_username = edtUsername.Text;
                objP.usu_password = edtPassword.Text;
                objP.usu_telefono = edtTelefono.Text;
                objP.usu_IDUsuario = idUsuario;
                objP.usu_email = edtEmail.Text;
                objP.usu_DNI = edtDni.Text;
                objP.usu_IDRol = idRol ;

                string valorPerfil = JsonConvert.SerializeObject(objP);

                HttpClient client = new HttpClient();

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                string url = "http://DCWebApi.somee.com/api/UsuarioController/ActualizarUsuario?stObj=" + valorPerfil;
                HttpResponseMessage response = client.GetAsync(url).Result;
                if (response.IsSuccessStatusCode)
                {
                    AlertDialog.Builder builder = new AlertDialog.Builder(this);
                    builder.SetTitle("Perfil Actualizado");
                    builder.SetIcon(Resource.Drawable.check);
                    builder.SetMessage("Su perfil ah sido actualizado correctamente.");
                    builder.SetPositiveButton("Ok", btnOk);


                    AlertDialog alertdialog = builder.Create();
                    alertdialog.Show();
                }

          
            }
            catch (Exception ex)
            {

               
            }
        }

        private void btnOk(object sender, DialogClickEventArgs e)
        {
            try
            {
                //Intent secondActivityIntentParcelable = new Intent(this, typeof(ActivityMenu));
                //secondActivityIntentParcelable.PutExtra("usuarioNombre", stUsername);
                //secondActivityIntentParcelable.PutExtra("usuarioId", idUsuario);
                //secondActivityIntentParcelable.PutExtra("usuarioIdRol", idRol);
                //secondActivityIntentParcelable.PutExtra("NombreCompleto", stNombreYApellido);
                //secondActivityIntentParcelable.PutExtra("email", stEmail);
                //secondActivityIntentParcelable.PutExtra("CodigoReclamo", "0");
                //secondActivityIntentParcelable.PutExtra("nombre", stNombre);
                //secondActivityIntentParcelable.PutExtra("apellido", stApellido);
                //secondActivityIntentParcelable.PutExtra("telefono", stTelefono);
                //secondActivityIntentParcelable.PutExtra("dni", stDni);
                //secondActivityIntentParcelable.PutExtra("password", stPassword);

                //StartActivity(secondActivityIntentParcelable);
                //Finish();

                Intent secondActivityIntentParcelable = new Intent(this, typeof(ActivityLogin));
                StartActivity(secondActivityIntentParcelable);
                Finish();
            }
            catch (Exception ex)
            {

                
            }
        }
    }
}