using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace DigitalClaimT.Droid
{
    public class clsConsultarReclamo
    {
        public string rec_ID { get; set; }
        public string rec_codigo { get; set; }
        public string rec_fechaAlta { get; set; }
        public string arServ_nombre { get; set; }
        public string tipRec_nombre { get; set; }
        public string cal_nombre { get; set; }
        public string rec_altura { get; set; }
        public string rec_direccion { get; set; }
        public string bar_nombre { get; set; }
        public string usu_ID { get; set; }
        public string usu_DNI { get; set; }
        public string rec_Foto { get; set; }
    }
}