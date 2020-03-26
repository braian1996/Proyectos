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
    public class clsListaDetalleReclamoAdaptador : BaseAdapter<clsHistorialReclamo>
    {
        private readonly IList<clsHistorialReclamo> _items;
        private readonly Context _context;

        public clsListaDetalleReclamoAdaptador(Context context, IList<clsHistorialReclamo> items)
        {
            _items = items;
            _context = context;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = _items[position];
            var view = convertView;

            if (view == null)
            {
                var inflater = LayoutInflater.FromContext(_context);
                view = inflater.Inflate(Resource.Layout.listViewDetalleReclamo, parent, false);
            }
            view.FindViewById<TextView>(Resource.Id.textViewFecha).Text = "Fecha: ";
            view.FindViewById<TextView>(Resource.Id.tvFecha).Text = item.his_fechaIngreso;
            view.FindViewById<TextView>(Resource.Id.textViewHora).Text = "Hora: ";
            view.FindViewById<TextView>(Resource.Id.tvHora).Text = item.his_horaIngreso;
            view.FindViewById<TextView>(Resource.Id.textViewEstado).Text = "Estado: ";
            view.FindViewById<TextView>(Resource.Id.tvEstado).Text = item.estRec_nombre;
            view.FindViewById<TextView>(Resource.Id.textViewObservacion).Text = "Observaciones: ";
            view.FindViewById<TextView>(Resource.Id.tvObservacion).Text = item.his_observaciones;

            return view;
        }


        public override int Count
        {
            get { return _items.Count; }
        }

        public override clsHistorialReclamo this[int position]
        {
            get { return _items[position]; }
        }

    }
}
