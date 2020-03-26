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
    public class clsConsultarOrdenServicio
    {
        public string orServ_IDOrdenServicio { get; set; }
        public string orServ_numero { get; set; }
        public string orServ_fechaAlta { get; set; }
        public string orServ_fechaCierre { get; set; }
        public string orServ_fechaVencimiento { get; set; }
        public string orServ_fechaInicio { get; set; }
        public string orServ_observaciones { get; set; }
        public string orServ_EstadoOrdenServicio { get; set; }
        public int orServ_IDEstado { get; set; }
        public string orServ_IDAreaServicio { get; set; }   
    }
}