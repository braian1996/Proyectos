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
    public class clsDetalleReclamo
    {
        public string rat_IDRating { get; set; }
        public string rat_rating { get; set; }
        public string rat_comentario { get; set; }
        public string rat_IDReclamo { get; set; }
        public string rat_fechaAlta { get; set; }
    }
}