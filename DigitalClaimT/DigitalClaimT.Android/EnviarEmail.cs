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
    public class EnviarEmail
    {
        public string rec_codigo { get; set; }
        public string usu_username { get; set; }
        public string usu_password { get; set; }
        public string rec_fechaAlta { get; set; }
        public string tipRec_nombre { get; set; }
        public string usu_nombre { get; set; }
        public string usu_email { get; set; }
        public string estRec_nombre { get; set; }
        public string boExiste { get; set; }
    }
}