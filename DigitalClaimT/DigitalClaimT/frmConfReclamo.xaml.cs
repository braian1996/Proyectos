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
	public partial class frmConfReclamo : ContentPage
	{
		public frmConfReclamo ( string stC, string stN, string iFotof)
		{
           

            InitializeComponent();

            if (stC != "" && stN != "")
            {
                txtAltura.Text = stN;
                txtCalle.Text = stC;
                imgFoto.Source = ImageSource.FromFile(iFotof);
            }
		}
	}
}