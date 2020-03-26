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
   public class clsValidarReclamo
    {
        public string rec_altura { get; set; }
        public string rec_IDCalle { get; set; }
        public string rec_IDBarrio { get; set; }
        public string rec_IDTipoReclamo { get; set; }
    }
}