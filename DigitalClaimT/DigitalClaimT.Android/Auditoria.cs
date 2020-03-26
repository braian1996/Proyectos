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
    public class Auditoria
    {
        public long aud_IDAuditoria { get; set; }
        public string aud_fechaAlta { get; set; }
        public string aud_observaciones { get; set; }
        public string aud_foto { get; set; }
        public long aud_IDReclamo { get; set; }
    }
}