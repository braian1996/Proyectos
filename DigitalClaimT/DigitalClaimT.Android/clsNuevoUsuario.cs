﻿using System;
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
    public class NuevoUsuario
    {
        public string usu_username { get; set; }
        public string usu_password { get; set; }
        public string usu_dni { get; set; }
        public string usu_nombre { get; set; }
        public string usu_apellido { get; set; }
        public string usu_telefono { get; set; }
        public string usu_email { get; set; }
        public int usu_IDRol { get; set; }
        //public long usu_IDAreaServicio { get; set; }
        //public int usu_IDTipoPersonal { get; set; }
    }
}