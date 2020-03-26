using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.Maps;

using System.IO;



namespace DigitalClaimT
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class reclamo : TabbedPage
    {
		public reclamo ()
		{
			InitializeComponent ();
            cpNR.Appearing += CpNR_Appearing;
            BtnUbiRe.Clicked += BtnUbiRe_Clicked;
            BtnAsignarUbi.Clicked += BtnAsignarUbi_Clicked;
        }

        private void BtnAsignarUbi_Clicked(object sender, EventArgs e)
        {

            //var map = new Map(
            //MapSpan.FromCenterAndRadius(
            //       new Position(37, -122), Distance.FromMiles(0.3)))
            //{
            //    IsShowingUser = true,
            //    HeightRequest = 100,
            //    WidthRequest = 960,
            //    VerticalOptions = LayoutOptions.FillAndExpand
            //};
            //MainMenu.MoveToRegion(
            //MapSpan.FromCenterAndRadius(
            //  new Position(37, -100), Distance.FromMiles(1)));


            //var locator = CrossGeolocator.Current;
            //locator.DesiredAccuracy = 50;
            //TimeSpan tm = TimeSpan.FromTicks(10000);
            //TimeSpan t1 = TimeSpan.FromSeconds(5);
            //double t2 = Convert.ToDouble(3000);

            //var position = await locator.GetPositionAsync(tm);
            //double latitud = 0;
            //double longitud = 0;

            //if (locator.IsGeolocationAvailable)
            //{
            //    if (locator.IsGeolocationEnabled)
            //    {
            //        if (!locator.IsListening)
            //        {
            //            await locator.StartListeningAsync(t1, t2);
            //        }
            //        locator.PositionChanged += (cambio, args) =>
            //        {
            //            var loc = args.Position;
            //            latitud = position.Latitude;
            //            longitud = position.Longitude;
            //            PositionMap(latitud, longitud);
            //            AddPins(latitud, longitud);


            //        };
            //    }
            //}
            //var geoCoderPosition = new Xamarin.Forms.Maps.Position(position.Latitude, position.Longitude);
            //var geocoder = new Geocoder();
            //Xamarin.FormsMaps.Init();
            //var addresses = await geocoder.GetAddressesForPositionAsync(geoCoderPosition);
            //string stCalle = "";
            //string stNumero = "";
            //string numero = "";
            //string cadenaCYN = "";
            //foreach (var address in addresses)
            //{
            //    String cadena = address.ToString().Replace("/", "/n");
            //    string[] separada = cadena.Split(',');
            //    string cadenaAltura = separada[0];
            //    for (int i = 0; i < cadenaAltura.Length; i++)
            //    {
            //        cadenaCYN = cadenaAltura.Substring(i, 1);
            //        switch (cadenaCYN)
            //        {
            //            case "0":
            //            case "1":
            //            case "2":
            //            case "3":
            //            case "4":
            //            case "5":
            //            case "6":
            //            case "7":
            //            case "8":
            //            case "9": stNumero = stNumero + cadenaCYN; break;
            //            default: stCalle = stCalle + cadenaCYN; break;
            //        }
            //    }
            //    for (int i = 0; i < stNumero.Length; i++)
            //    {
            //        numero = stNumero.Substring(i, 1);
            //        if (numero == "-")
            //        {
            //            stNumero = stNumero + "-" + numero;
            //        }
            //    }
            //    txtCalle.Text = stCalle;
            //    txtAltura.Text = stNumero;
            //    break;
            //}
        }

        private void BtnUbiRe_Clicked(object sender, EventArgs e)
        {
          
            


        }
        //private void AddPins(double latitud, double longitud)
        //{
            
        //    Customers cls = new Customers();
        //    cls.Latitude = latitud;
        //    cls.Longitude = longitud;
        //        var pin = new Pin
        //        {
        //            Type = PinType.Place,
        //            Position = new Position(cls.Latitude, cls.Longitude),
        //            Label = "Ubica Real",
        //            Address = "posicion"
        //        };

        //        MainMenu.Pins.Add(pin);
            
        //}

        //private void PositionMap( double latitud,double longitud)
        //{
           

        //    MainMenu.MoveToRegion(
        //         MapSpan.FromCenterAndRadius(
        //              new Position(latitud, longitud),
        //              Distance.FromMiles(1)));
        //}
        private void CpNR_Appearing(object sender, EventArgs e)
        {
           
        }
        //protected override MarkerOptions CreateMarker(Pin pin)
        //{
        //    var marker = new MarkerOptions();
        //    marker.SetPosition(new LatLng(pin.Position.Latitude, pin.Position.Longitude));
        //    marker.SetTitle(pin.Label);
        //    marker.SetSnippet(pin.Address);
        //    marker.SetIcon(BitmapDescriptorFactory.FromResource(Resource.Drawable.Pin));
        //    return marker;
        //}
        //public void setLocation(Location loc)
        //{
        //    //Obtener la direcci—n de la calle a partir de la latitud y la longitud 
        //    if (loc.getLatitude() != 0.0 && loc.getLongitude() != 0.0)
        //    {
        //        try
        //        {
        //            Geocoder geocoder = new Geocoder();
        //            List<Address> list = geocoder.GetAddressesForPositionAsync(loc.getLatitude(), loc.getLongitude(), 1);
        //            if (!list.isEmpty())
        //            {
        //                Address address = list.get(0);
        //                messageTextView2.setText("Mi direcci—n es: \n" + address.getAddressLine(0));
        //            }
        //        }
        //        catch (IOException e)
        //        {
        //            e.printStackTrace();
        //        }
        //    }
        //}
    }
}