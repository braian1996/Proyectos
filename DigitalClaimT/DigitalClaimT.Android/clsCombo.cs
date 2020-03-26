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
    public class clsCombo : BaseAdapter<clsLlenaCombo>
    {
            private readonly IList<clsLlenaCombo> _items;
            private readonly Context _context;

            public clsCombo(Context context, IList<clsLlenaCombo> items)
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
                    view = inflater.Inflate(Resource.Layout.listviewCOMBO, parent, false);
                }

                view.FindViewById<TextView>(Resource.Id.tvNombre).Text = item.nombre;
                view.FindViewById<TextView>(Resource.Id.tvNro).Text = item.id.ToString();


                return view;
            }

            public override int Count
            {
                get { return _items.Count; }
            }

            public override clsLlenaCombo this[int position]
            {
                get { return _items[position]; }
            }
        
    }
}