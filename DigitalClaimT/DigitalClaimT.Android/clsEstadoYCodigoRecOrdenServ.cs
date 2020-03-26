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
    public class clsEstadoYCodigoRecOrdenServ
    {
        public string his_IDReclamo { get; set; }
        public string his_fechaIngreso { get; set; }
        public string his_horaIngreso { get; set; }
        public string his_observaciones { get; set; }
        public int his_IDEstado { get; set; }
        public string rec_motivo { get; set; }
    }
}