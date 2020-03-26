using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Configuration;
using Newtonsoft.Json;

namespace DigitalClaimT
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class login : ContentPage
	{
        
        public login ()
		{
			InitializeComponent ();
            BtnSesion.Clicked += BtnSesion_Clicked;
            BtnCrearCuenta.Clicked += BtnCrearCuenta_Clicked;
            img.Source = ImageSource.FromResource("DigitalClaimT.logo_DigitalClaimLogin.png");
            
            //agrego esta linea para evitar repetir 2 veces la pantalla menu
            BtnSesion.Clicked -= BtnSesion_Clicked;
            BtnCrearCuenta.Clicked -= BtnCrearCuenta_Clicked;

            var forgetPassword_tap = new TapGestureRecognizer();
            forgetPassword_tap.Tapped += (s, e) =>
            {
                this.Navigation.PushModalAsync(new frmConfirmarContra());
            };
            lblLink.GestureRecognizers.Add(forgetPassword_tap);
        }
     
        private void LoginTapped()
        {
            this.Navigation.PushModalAsync(new frmRegistrarse());
        }
        private void BtnCrearCuenta_Clicked(object sender, EventArgs e)
        {
            this.Navigation.PushModalAsync(new frmRegistrarse());
        }
       
        private  void BtnSesion_Clicked(object sender, EventArgs e)
        {
            
                string queryString = "http://webservicedc.somee.com/Service2.svc?wsdl/ValidarUsuario?stUsuario=" + txtNombre.Text + "&?stPassword=" + txtContra.Text;

            // string queryString = "http://localhost:50479/Service1.svc";

            //DisplayAlert("info", resu, "ok");
            //this.Navigation.PushModalAsync(new menudc());

            var resu = getServiceData(queryString);
          
           // DisplayAlert("info", resu.Result, "ok");
        }
        public static async Task<string> getServiceData(string queryString)
        {
            try
            {
                string quote = null;
                HttpClient client = new HttpClient();

                var requestTask = await client.GetAsync(queryString).ConfigureAwait(true);

                if (requestTask.IsSuccessStatusCode)
                {
                    string json = requestTask.Content.ReadAsStringAsync().Result;
                    if (!string.IsNullOrEmpty(json))
                    {
                        quote = await Task.Run(() =>
                          JsonConvert.DeserializeObject<string>(json)
                        ).ConfigureAwait(true);
                    }
                   
                }

                return quote;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        //public static async Task<string> GetQuote(string u, string p)
        //{
        //    string queryString = "http://localhost:50479/Service1.svc/ValidateLogin/" + u + "," + p;

        //    dynamic results = await getServiceData(queryString).ConfigureAwait(false);

        //    if (results != null)
        //    {

        //        return results;
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}

    }
}