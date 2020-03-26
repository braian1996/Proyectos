using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.Media.Abstractions;
using Plugin.Media;
using Plugin.Geolocator;
using Xamarin.Forms.Maps;
//using Negocio;
using System.IO;
using System.Drawing;

namespace DigitalClaimT
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FrmNuevoReclamo : ContentPage
	{
		public FrmNuevoReclamo ()
		{
			InitializeComponent ();
            BtnFoto.Clicked += BtnFoto_Clicked;
            BtnFoto.Clicked -= BtnFoto_Clicked;

            BtnSiguiente.Clicked += BtnSiguiente_Clicked;
            BtnSiguiente.Clicked -= BtnSiguiente_Clicked;
        }

        private void BtnSiguiente_Clicked(object sender, EventArgs e)
        {
            this.Navigation.PushModalAsync(new frmConfReclamo(stCalleFoto,stNroFoto,imgFoto));
        }

        string stCalleFoto = "";
        string stNroFoto = "";
        string imgFoto;

        private async void BtnFoto_Clicked(object sender, EventArgs e)
        {  
            var opAlma = new StoreCameraMediaOptions()
            {
                Directory = "Test",
                SaveToAlbum = true,
                CompressionQuality = 75,
                CustomPhotoSize = 50,
                PhotoSize = PhotoSize.MaxWidthHeight,
                MaxWidthHeight = 2000,
                DefaultCamera = CameraDevice.Front,
                Name = "MiFoto.jpg",


                };
                 
                var foto = await CrossMedia.Current.TakePhotoAsync(opAlma);
                miFoto.Source = ImageSource.FromStream(() =>
                {
                    var stream = foto.GetStream();
                    imgFoto = foto.Path;
                    foto.Dispose();
                    return stream;
                });

            

            //ubicacion real de la foto

            var locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 50;
           
            TimeSpan t1 = TimeSpan.FromSeconds(5);
            double t2 = Convert.ToDouble(3000);

            var position = await locator.GetPositionAsync(10000);
            double latitud = 0;
            double longitud = 0;

            if (locator.IsGeolocationAvailable)
            {
                if (locator.IsGeolocationEnabled)
                {
                    if (!locator.IsListening)
                    {
                        await locator.StartListeningAsync(5, t2);
                    }
                    locator.PositionChanged += (cambio, args) =>
                    {
                        var loc = args.Position;
                        latitud = position.Latitude;
                        longitud = position.Longitude;
                       
                    };
                }
            }
            var geoCoderPosition = new Xamarin.Forms.Maps.Position(position.Latitude, position.Longitude);
            var geocoder = new Geocoder();
            var addresses = await geocoder.GetAddressesForPositionAsync(geoCoderPosition);
            string stCalle = "";
            string stNumero = "";
            string numero = "";
            string cadenaCYN = "";
            foreach (var address in addresses)
            {
                String cadena = address.ToString().Replace("/", "/n");
                string[] separada = cadena.Split(',');
                string cadenaAltura = separada[0];
                for (int i = 0; i < cadenaAltura.Length; i++)
                {
                    cadenaCYN = cadenaAltura.Substring(i, 1);
                    switch (cadenaCYN)
                    {
                        case "0":
                        case "1":
                        case "2":
                        case "3":
                        case "4":
                        case "5":
                        case "6":
                        case "7":
                        case "8":
                        case "9": stNumero = stNumero + cadenaCYN; break;
                        default: stCalle = stCalle + cadenaCYN; break;
                    }
                }
                for (int i = 0; i < stNumero.Length; i++)
                {
                    numero = stNumero.Substring(i, 1);
                    if (numero == "-")
                    {
                        stNumero = stNumero + "-" + numero;
                    }
                }
                stCalleFoto = stCalle;
                stNroFoto = stNumero;
               
                break;
            }
            
        }
    

    }
}