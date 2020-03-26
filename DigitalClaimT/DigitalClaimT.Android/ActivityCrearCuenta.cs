using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;

namespace DigitalClaimT.Droid
{
    [Activity(Label = "")]
    public class ActivityCrearCuenta : Activity
    {
        EditText edtUsername;
        EditText edtContraseña;
        EditText edtRepetirContraseña;
        EditText edtDNI;
        EditText edtNombre;
        EditText edtApellido;
        EditText edtEmail;
        EditText edtTelefonoo;

        TextView tvValidaUsername;
        TextView tvValidaContraseña;
        TextView tvValidaRepetirContraseña;
        TextView tvValidaDni;
        TextView tvValidaNombre;
        TextView tvValidaApellido;
        TextView tvValidaTelefono;
        TextView tvValidaEmail;
        TextView tvCampoObligatorio;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            try
            {
                base.OnCreate(savedInstanceState);

                SetContentView(Resource.Layout.CrearCuenta);
                // Create your application here


                edtUsername = FindViewById<EditText>(Resource.Id.edtNombreUsuario);
                edtContraseña = FindViewById<EditText>(Resource.Id.edtContrasena);
                edtRepetirContraseña = FindViewById<EditText>(Resource.Id.edtRepetirContrasena);
                edtDNI = FindViewById<EditText>(Resource.Id.edtDni);
                edtNombre = FindViewById<EditText>(Resource.Id.edtNombre);
                edtApellido = FindViewById<EditText>(Resource.Id.edtApellido);
                edtEmail = FindViewById<EditText>(Resource.Id.edtEmail);
                edtTelefonoo = FindViewById<EditText>(Resource.Id.edtTelefono);

                tvValidaUsername = FindViewById<TextView>(Resource.Id.tvValidaNombreUsuario);
                tvValidaContraseña = FindViewById<TextView>(Resource.Id.tvValidaContraseña);
                tvValidaRepetirContraseña = FindViewById<TextView>(Resource.Id.tvValidaRepetirContraseña);
                tvValidaDni = FindViewById<TextView>(Resource.Id.tvValidaDni);
                tvValidaNombre = FindViewById<TextView>(Resource.Id.tvValidaNombre);
                tvValidaApellido = FindViewById<TextView>(Resource.Id.tvValidaApellido);
                tvValidaTelefono = FindViewById<TextView>(Resource.Id.tvValidaTelefono);
                tvValidaEmail = FindViewById<TextView>(Resource.Id.tvValidaEmail);
                tvCampoObligatorio = FindViewById<TextView>(Resource.Id.tvCampoObligatorio);

                Button btnCrearCuentaUsuario = FindViewById<Button>(Resource.Id.btnCrearCuentaUsu);

                btnCrearCuentaUsuario.Click += BtnCrearCuentaUsuario_Click;

                edtUsername.AfterTextChanged += EdtUsername_AfterTextChanged;
                edtContraseña.AfterTextChanged += EdtContraseña_AfterTextChanged;
                edtRepetirContraseña.AfterTextChanged += EdtRepetirContraseña_AfterTextChanged;
                edtDNI.AfterTextChanged += EdtDNI_AfterTextChanged;
                edtNombre.AfterTextChanged += EdtNombre_AfterTextChanged;
                edtApellido.AfterTextChanged += EdtApellido_AfterTextChanged;
                edtTelefonoo.AfterTextChanged += EdtTelefonoo_AfterTextChanged;
                edtEmail.AfterTextChanged += EdtEmail_AfterTextChanged;
            }
            catch (Exception ex)
            {

           
            }
         

        
        }

        private void EdtEmail_AfterTextChanged(object sender, Android.Text.AfterTextChangedEventArgs e)
        {
            try
            {
                if (edtEmail.Text == "" && validarEmail(edtEmail.Text))
                {
                    tvValidaEmail.Visibility = ViewStates.Visible;
                    tvCampoObligatorio.Visibility = ViewStates.Visible;
                    tvValidaEmail.Text = "E-Mail incorrecto";
                }
                else
                {
                    tvValidaEmail.Visibility = ViewStates.Invisible;
                    tvCampoObligatorio.Visibility = ViewStates.Invisible;
                }


            }
            catch (Exception)
            {


            }
        }

        private void EdtTelefonoo_AfterTextChanged(object sender, Android.Text.AfterTextChangedEventArgs e)
        {
            try
            {
                if (edtTelefonoo.Text == "")
                {
                    tvValidaTelefono.Visibility = ViewStates.Visible;
                    tvCampoObligatorio.Visibility = ViewStates.Visible;
                }
                else
                {
                    tvValidaTelefono.Visibility = ViewStates.Invisible;
                    tvCampoObligatorio.Visibility = ViewStates.Invisible;
                }
            }
            catch (Exception)
            {


            }
        }

        private void EdtApellido_AfterTextChanged(object sender, Android.Text.AfterTextChangedEventArgs e)
        {
            try
            {
                if (edtApellido.Text == "")
                {
                    tvValidaApellido.Visibility = ViewStates.Visible;
                    tvCampoObligatorio.Visibility = ViewStates.Visible;
                }
                else
                {
                    tvValidaApellido.Visibility = ViewStates.Invisible;
                    tvCampoObligatorio.Visibility = ViewStates.Invisible;
                }
            }
            catch (Exception)
            {


            }
        }

        private void EdtNombre_AfterTextChanged(object sender, Android.Text.AfterTextChangedEventArgs e)
        {
            try
            {
                if (edtNombre.Text == "")
                {
                    tvValidaNombre.Visibility = ViewStates.Visible;
                    tvCampoObligatorio.Visibility = ViewStates.Visible;
                }
                else
                {
                    tvValidaNombre.Visibility = ViewStates.Invisible;
                    tvCampoObligatorio.Visibility = ViewStates.Invisible;
                }
            }
            catch (Exception)
            {


            }
        }

        private void EdtDNI_AfterTextChanged(object sender, Android.Text.AfterTextChangedEventArgs e)
        {
            try
            {
                if (edtDNI.Text == "")
                {
                    tvValidaDni.Visibility = ViewStates.Visible;
                    tvCampoObligatorio.Visibility = ViewStates.Visible;
                }
                else
                {
                    tvValidaDni.Visibility = ViewStates.Invisible;
                    tvCampoObligatorio.Visibility = ViewStates.Invisible;
                }
            }
            catch (Exception)
            {


            }
        }

        private void EdtRepetirContraseña_AfterTextChanged(object sender, Android.Text.AfterTextChangedEventArgs e)
        {
            try
            {
                if (edtRepetirContraseña.Text == "")
                {
                    tvValidaRepetirContraseña.Visibility = ViewStates.Visible;
                    tvCampoObligatorio.Visibility = ViewStates.Visible;
                }
                else
                {
                    tvValidaRepetirContraseña.Visibility = ViewStates.Invisible;
                    tvCampoObligatorio.Visibility = ViewStates.Invisible;
                }

                if (edtContraseña.Text != edtRepetirContraseña.Text)
                {
                    tvValidaRepetirContraseña.Visibility = ViewStates.Visible;
                    tvCampoObligatorio.Visibility = ViewStates.Visible;
                    tvValidaRepetirContraseña.Text = "Las contraseñas no coinciden";
                }
                else
                {
                    tvValidaRepetirContraseña.Visibility = ViewStates.Invisible;
                    tvCampoObligatorio.Visibility = ViewStates.Invisible;
                }
            }
            catch (Exception)
            {

                
            }
        }

        private void EdtContraseña_AfterTextChanged(object sender, Android.Text.AfterTextChangedEventArgs e)
        {
            try
            {
                if (edtContraseña.Text == "")
                {
                    tvValidaContraseña.Visibility = ViewStates.Visible;
                    tvCampoObligatorio.Visibility = ViewStates.Visible;
                }
                else
                {
                    tvValidaContraseña.Visibility = ViewStates.Invisible;
                    tvCampoObligatorio.Visibility = ViewStates.Invisible;
                }
            }
            catch (Exception)
            {

                
            }
        }

        private void EdtUsername_AfterTextChanged(object sender, Android.Text.AfterTextChangedEventArgs e)
        {
            try
            {
                if (edtUsername.Text == "")
                {
                    tvValidaUsername.Visibility = ViewStates.Visible;
                    tvCampoObligatorio.Visibility = ViewStates.Visible;
                }
                else
                {
                    tvValidaUsername.Visibility = ViewStates.Invisible;
                    tvCampoObligatorio.Visibility = ViewStates.Invisible;
                }

            }
            catch (Exception)
            {

                
            }
        }

        private Boolean validarEmail(String email)
        {
            var pattern = Patterns.EmailAddress;
            return pattern.Matcher(email).Matches();

          
        }
        private void BtnCrearCuentaUsuario_Click(object sender, EventArgs e)
        {
            try
            {
               

                if ((edtUsername.Text != "") && (edtContraseña.Text != "") && (edtRepetirContraseña.Text != "") && (edtDNI.Text != "") && (edtNombre.Text != "") && (edtApellido.Text != "") && validarEmail(edtEmail.Text) && (edtContraseña.Text == edtRepetirContraseña.Text) && (edtTelefonoo.Text != ""))
                {

                    NuevoUsuario objNuevoUsuario = new NuevoUsuario();

                    objNuevoUsuario.usu_username = edtUsername.Text;

                    objNuevoUsuario.usu_password = edtContraseña.Text;


                    objNuevoUsuario.usu_dni = edtDNI.Text;
                    objNuevoUsuario.usu_nombre = edtNombre.Text;
                    objNuevoUsuario.usu_apellido = edtApellido.Text;
                    objNuevoUsuario.usu_telefono = edtTelefonoo.Text;
                    objNuevoUsuario.usu_IDRol = 1;



                    objNuevoUsuario.usu_email = edtEmail.Text;

                    string ValorNuevousuario = JsonConvert.SerializeObject(objNuevoUsuario);

                    HttpClient client = new HttpClient();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    string urlValidarReclamo = "http://DCWebApi.somee.com/api/LoginController/RegistrarUsuario?stObj=" + ValorNuevousuario;

                    HttpResponseMessage responseNuevoUsuario =  client.GetAsync(urlValidarReclamo).Result;

                    if (responseNuevoUsuario.IsSuccessStatusCode)
                    {
                        string ResultadoValidarReclamo = JsonConvert.DeserializeObject(responseNuevoUsuario.Content.ReadAsStringAsync().Result).ToString();

                        if (ResultadoValidarReclamo != "Usuario existente")
                        {
                            AlertDialog ad = new AlertDialog.Builder(this).Create();
                            ad.SetTitle("Usuario Guardado");
                            ad.SetMessage("El usuario"+ " " + edtUsername.Text + " " + "se a creado con exito!");
                            ad.SetButton("Ok", (g, h) => { });
                            ad.Show();

                            edtUsername.Text = "";
                            edtContraseña.Text = "";
                            edtDNI.Text = "";
                            edtNombre.Text = "";
                            edtApellido.Text = "";
                            edtTelefonoo.Text = "";
                            edtEmail.Text = "";

                            StartActivity(typeof(ActivityLogin));
                            Finish();
                        }
                        else
                        {
                            AlertDialog ad = new AlertDialog.Builder(this).Create();
                            ad.SetTitle("Error al guardar");
                            ad.SetMessage("El usuario no se a podido registrar, por favor revise los datos");
                            ad.SetButton("Ok", (g, h) => { });
                            ad.Show();
                        }
                    }



                }
                else
                {
                    //AlertDialog ad = new AlertDialog.Builder(this).Create();
                    //ad.SetTitle("Error!");
                    //ad.SetMessage("Por favor verifique si las contraseñas coinciden o si quedo algun campo vacio");
                    //ad.SetButton("Ok", (g, h) => { });
                    //ad.Show();
                    ValidarCampoObligatorio();
                }


            }
            catch (Exception ex)
            {

                
            }
        }
        public void ValidarCampoObligatorio()
        {
            //edtUsername.Text != "") && (edtContraseña.Text != "") && (edtRepetirContraseña.Text != "") && (edtDNI.Text != "") && (edtNombre.Text != "") && (edtApellido.Text != "") && validarEmail(edtEmail.Text) && (edtContraseña.Text == edtRepetirContraseña.Text) && (edtTelefonoo.Text != ""))
            try
            {
                if (edtUsername.Text == "")
                {
                    tvValidaUsername.Visibility = ViewStates.Visible;
                    tvCampoObligatorio.Visibility = ViewStates.Visible;
                }
                //else
                //{
                //    tvValidaUsername.Visibility = ViewStates.Invisible;
                //    tvCampoObligatorio.Visibility = ViewStates.Invisible;
                //}

                if (edtContraseña.Text == "")
                {
                    tvValidaContraseña.Visibility = ViewStates.Visible;
                    tvCampoObligatorio.Visibility = ViewStates.Visible;
                }
                //else
                //{
                //    tvValidaContraseña.Visibility = ViewStates.Invisible;
                //    tvCampoObligatorio.Visibility = ViewStates.Invisible;
                //}

                if (edtRepetirContraseña.Text == "")
                {
                    tvValidaRepetirContraseña.Visibility = ViewStates.Visible;
                    tvCampoObligatorio.Visibility = ViewStates.Visible;
                }
                //else
                //{
                //    tvValidaRepetirContraseña.Visibility = ViewStates.Invisible;
                //    tvCampoObligatorio.Visibility = ViewStates.Invisible;
                //}

                if (edtDNI.Text == "")
                {
                    tvValidaDni.Visibility = ViewStates.Visible;
                    tvCampoObligatorio.Visibility = ViewStates.Visible;
                }
                //else
                //{
                //    tvValidaDni.Visibility = ViewStates.Invisible;
                //    tvCampoObligatorio.Visibility = ViewStates.Invisible;
                //}

                if (edtNombre.Text == "")
                {
                    tvValidaNombre.Visibility = ViewStates.Visible;
                    tvCampoObligatorio.Visibility = ViewStates.Visible;
                }
                //else
                //{
                //    tvValidaNombre.Visibility = ViewStates.Invisible;
                //    tvCampoObligatorio.Visibility = ViewStates.Invisible;
                //}

                if (edtApellido.Text == "")
                {
                    tvValidaApellido.Visibility = ViewStates.Visible;
                    tvCampoObligatorio.Visibility = ViewStates.Visible;
                }
                //else
                //{
                //    tvValidaApellido.Visibility = ViewStates.Invisible;
                //    tvCampoObligatorio.Visibility = ViewStates.Invisible;
                //}

                if (edtEmail.Text == "" && validarEmail(edtEmail.Text))
                {
                    tvValidaEmail.Visibility = ViewStates.Visible;
                    tvCampoObligatorio.Visibility = ViewStates.Visible;
                    tvValidaEmail.Text = "E-Mail incorrecto";
                }
                //else
                //{
                //    tvValidaEmail.Visibility = ViewStates.Invisible;
                //    tvCampoObligatorio.Visibility = ViewStates.Invisible;
                //}

                if (edtTelefonoo.Text == "")
                {
                    tvValidaTelefono.Visibility = ViewStates.Visible;
                    tvCampoObligatorio.Visibility = ViewStates.Visible;
                }
                //else
                //{
                //    tvValidaTelefono.Visibility = ViewStates.Invisible;
                //    tvCampoObligatorio.Visibility = ViewStates.Invisible;
                //}
            }
            catch (Exception)
            {


            }

        }
    }
}