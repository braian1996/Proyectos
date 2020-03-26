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
    public class clsHistorialReclamo
    {
        public string his_fechaIngreso { get; set; }
        public string his_horaIngreso { get; set; }
        public string his_observaciones { get; set; }
        public string estRec_nombre { get; set; }
        public string his_IDHistorial { get; set; }
        public string his_IDReclamo { get; set; }
    }
}