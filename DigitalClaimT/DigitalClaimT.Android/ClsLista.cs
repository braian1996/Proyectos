using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.IO;
using Java.Net;

namespace DigitalClaimT.Droid
{
    
    public class ClsLista : BaseAdapter<clsConsultarReclamo>
    {
        private readonly IList<clsConsultarReclamo> _items;
        private readonly Context _context;

        public ClsLista(Context context, IList<clsConsultarReclamo> items)
        {
            _items = items;
            _context = context;
        }

        public override long GetItemId(int position)
        {
            return position;
        }
        string stRuta;
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            try
            {
                var item = _items[position];
                var view = convertView;

                if (view == null)
                {
                    var inflater = LayoutInflater.FromContext(_context);
                    view = inflater.Inflate(Resource.Layout.listViewItem, parent, false);
                    
                }

                view.FindViewById<TextView>(Resource.Id.tvNumeroReclamo).Text = "Codigo Reclamo: ";
                view.FindViewById<TextView>(Resource.Id.tvCodigoReclamo).Text = item.rec_codigo;
                view.FindViewById<TextView>(Resource.Id.textView4tr).Text = "Reclamo :";
                view.FindViewById<TextView>(Resource.Id.tvTipoReclamo).Text = item.tipRec_nombre;
                view.FindViewById<TextView>(Resource.Id.textView3as).Text = "Área Servicio: ";
                view.FindViewById<TextView>(Resource.Id.tvAreaServicio).Text = item.arServ_nombre;
                view.FindViewById<TextView>(Resource.Id.textView2fa).Text = "Fecha Alta: ";
                view.FindViewById<TextView>(Resource.Id.tvFechaAlta).Text = item.rec_fechaAlta;
                view.FindViewById<TextView>(Resource.Id.textView6b).Text = "Barrio: ";
                view.FindViewById<TextView>(Resource.Id.tvBarrio).Text = item.bar_nombre;
                view.FindViewById<TextView>(Resource.Id.textView5d).Text = "Dirección: ";
                view.FindViewById<TextView>(Resource.Id.tvDireccion).Text = item.rec_direccion;
                TextView tvID = view.FindViewById<TextView>(Resource.Id.tvIdReclamo);
                TextView tvUrlFoto = view.FindViewById<TextView>(Resource.Id.textViewurlFoto);
                tvID.Text = item.rec_ID;
                tvUrlFoto.Text = item.rec_Foto;

                //if (item.rec_Foto != "")
                //{
                //    stRuta = "http://www.dcwebapi.somee.com/" + item.rec_Foto;
                //}
                //else
                //{
                //    stRuta = "";
                //}
              
                Button btnDetalleReclamo = view.FindViewById<Button>(Resource.Id.btnDetalleReclamo);


                if (!btnDetalleReclamo.HasOnClickListeners)
                { // Con esto evitas de asignarle mas de una vez un listener
                    btnDetalleReclamo.Click += delegate {

                        ClsLista_Click(tvID.Text,tvUrlFoto.Text); // Acá le pasarias el/los datos que necesites.
                        
                    };
                }

                return view;
            }
            catch (Exception ex)
            {

               
            }
            return null;
        }
       
        private void ClsLista_Click(string idReclamo, string rutaFotoRec)
        {
            try
            {
                Intent secondActivityDetalleReclamo = new Intent(_context, typeof(ActivityDetalleReclamo));
                secondActivityDetalleReclamo.PutExtra("codrec", idReclamo);
                secondActivityDetalleReclamo.PutExtra("ruta", rutaFotoRec);
                _context.StartActivity(secondActivityDetalleReclamo);
            }
            catch (Exception ex)
            {
                
            }
        }

        public override int Count
        {
            get { return _items.Count; }
        }

        public override clsConsultarReclamo this[int position]
        {
            get { return _items[position]; }
        }

    }

}