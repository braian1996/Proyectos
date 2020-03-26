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
    public class clsLlenarReclamoOrden
    {
        //public string orServ_IDOrdenServicio { get; set; }
        //public string rec_codigo { get; set; }
        //public string rec_fechaAlta { get; set; }
        //public string arServ_nombre { get; set; }
        //public string tipRec_nombre { get; set; }
        //public string rec_direccion { get; set; }
        //public string bar_nombre { get; set; }
        //public string estRec_nombre { get; set; }

        public long rec_IDReclamo { get; set; }
        public long rec_codigo { get; set; }
        public string rec_fechaAlta { get; set; }
        public int? rec_altura { get; set; }
        public string rec_observaciones { get; set; }
        public string rec_Foto { get; set; }
        public long rec_IDTipoReclamo { get; set; }
        public int rec_IDCanal { get; set; }
        public long rec_IDUsuario { get; set; }
        public string cal_nombre { get; set; }
        public string rec_direccion { get; set; }
        public string bar_nombre { get; set; }
        public string tipRec_nombre { get; set; }
        public string estRec_nombre { get; set; }
        public string usu_nombre { get; set; }
        public string usu_email { get; set; }
        public string his_horaIngreso { get; set; }
        public bool usu_boExiste { get; set; }
        public int cal_IDCalle { get; set; }
        public int bar_IDBarrio { get; set; }


    }
}