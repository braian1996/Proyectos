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

namespace DigitalClaimT.Droid
{
     public class clsLogin
    {
        public string usu_username { get; set; }
        public string usu_password { get; set; }

        public string su_IDSesion { get; set; }
        public string su_fechaInicio { get; set; }
        public string su_horaInicio { get; set; }

        public string su_fechaFin { get; set; }
        public string su_horaFin { get; set; }
    }
}