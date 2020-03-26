using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DigitalClaimT
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class frmAuditorias : ContentPage
	{
		public frmAuditorias ()
		{
			InitializeComponent ();

            btnTomarFoto.Clicked += BtnTomarFoto_Clicked;
		}

        private async void BtnTomarFoto_Clicked(object sender, EventArgs e)
        {
            var opAlma = new StoreCameraMediaOptions()
            {
                Directory = "Auditoria",
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
                //imgFoto = foto.Path;
                foto.Dispose();
                return stream;
            });
        }
    }
}