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
    public class clsListarReclamoOrdenServicio : BaseAdapter<clsLlenarReclamoOrden>
    {
        private readonly IList<clsLlenarReclamoOrden> _items;
        private readonly Context _context;
        private readonly ArrayAdapter _adapter;
        private readonly List<string> _adapterId;
        Button btnAudi;

        public clsListarReclamoOrdenServicio(Context context, IList<clsLlenarReclamoOrden> items, ArrayAdapter adapter,List<string> adapterID)
        {
            _items = items;
            _context = context;
            _adapter = adapter;
            _adapterId = adapterID;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            try
            {
                var item = _items[position];
                var view = convertView;

                if (view == null)
                {
                    var inflater = LayoutInflater.FromContext(_context);
                    view = inflater.Inflate(Resource.Layout.ListViewReclamosOrdenServicio, parent, false);
                }
                view.FindViewById<TextView>(Resource.Id.textViewCodigo).Text = "Codigo Reclamo: ";
                view.FindViewById<TextView>(Resource.Id.textViewCodigoMuestra).Text = item.rec_codigo.ToString();
                view.FindViewById<TextView>(Resource.Id.textViewFechaAlta).Text = "Fecha Alta: ";
                view.FindViewById<TextView>(Resource.Id.textViewFechaAltaMuestra).Text = item.rec_fechaAlta;
                view.FindViewById<TextView>(Resource.Id.textViewTipoReclamo).Text = "Tipo de Reclamo: ";
                view.FindViewById<TextView>(Resource.Id.textViewTipoReclamoMuestra).Text = item.tipRec_nombre;
                view.FindViewById<TextView>(Resource.Id.textViewDireccion).Text = "Dirección: ";
                view.FindViewById<TextView>(Resource.Id.textViewDireccionMuestra).Text = item.rec_direccion;
                view.FindViewById<TextView>(Resource.Id.textViewBarrio).Text = "Barrio: ";
                view.FindViewById<TextView>(Resource.Id.textViewBarrioMuestra).Text = item.bar_nombre;

                TextView tvIdRec = view.FindViewById<TextView>(Resource.Id.textViewIdReclamo);
                TextView tvCalle = view.FindViewById<TextView>(Resource.Id.textViewCalle);
                TextView tvAltura = view.FindViewById<TextView>(Resource.Id.textViewAltura);
                TextView tvidUsuario = view.FindViewById<TextView>(Resource.Id.textViewIdUsuario);
                TextView tvIdCanal = view.FindViewById<TextView>(Resource.Id.textViewidCanal);
                TextView idCalle = view.FindViewById<TextView>(Resource.Id.textViewidCalle);
                TextView idBarrio = view.FindViewById<TextView>(Resource.Id.textViewidBarrio);
                TextView Obser = view.FindViewById<TextView>(Resource.Id.textViewobser);
                TextView tvIDTipoRec = view.FindViewById<TextView>(Resource.Id.textViewIdTipoRec);
                TextView tvEmail = view.FindViewById<TextView>(Resource.Id.textViewEmail);
                TextView tvNombre = view.FindViewById<TextView>(Resource.Id.textViewNombre);
                TextView tvFoto = view.FindViewById<TextView>(Resource.Id.textViewFoto);

                tvIdRec.Text = item.rec_IDReclamo.ToString();
                tvCalle.Text = item.cal_nombre;
                tvAltura.Text = item.rec_altura.ToString();
                tvEmail.Text = item.usu_email;
                tvidUsuario.Text = item.rec_IDUsuario.ToString();
                tvIdCanal.Text = item.rec_IDCanal.ToString();
                idCalle.Text = item.cal_IDCalle.ToString();
                idBarrio.Text = item.bar_IDBarrio.ToString();
                Obser.Text = "";
                tvIDTipoRec.Text = item.rec_IDTipoReclamo.ToString();
                tvNombre.Text = item.usu_nombre;
                if (item.rec_Foto !="")
                {
                    tvFoto.Text = item.rec_Foto;
                }
                else
                {
                    tvFoto.Text = "";
                }

                btnAudi = view.FindViewById<Button>(Resource.Id.btnAudiRec);


                if (!btnAudi.HasOnClickListeners)
                { // Con esto evitas de asignarle mas de una vez un listener
                    btnAudi.Click += delegate {

                        BtnAudi_Click(tvIdRec.Text); // Acá le pasarias el/los datos que necesites.

                    };
                }

                Spinner spnEst = view.FindViewById<Spinner>(Resource.Id.spnReclamoOrden);
                spnEst.Adapter = _adapter;
                spnEst.ItemSelected += SpnEst_ItemSelected;
                //spnEst.SetSelection(obtenerPosicionItem(spnEst, item.estRec_nombre));

                return view;
            }
            catch (Exception ex)
            {

               
            }

            return null;
        }

        private void BtnAudi_Click(string idCodigoRec)
        {
            try
            {
                Intent secondActivityAuditoria = new Intent(_context, typeof(ActivityGenerarAuditoria));
                secondActivityAuditoria.PutExtra("idRec", idCodigoRec);
                _context.StartActivity(secondActivityAuditoria);
            }
            catch (Exception ex)
            {

                
            }
        }

        private void SpnEst_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            try
            {
                string idEstadoRec = _adapterId[e.Position].ToString();
                //if (idEstadoRec == "6")
                //{
                //    btnAudi.Enabled = true;
                //}
                //else
                //{
                //    btnAudi.Enabled = false;
                //}

              

            }
            catch (Exception)
            {

            }
            
        }

        public override int Count
        {
            get { return _items.Count; }
        }

        public override clsLlenarReclamoOrden this[int position]
        {
            get { return _items[position]; }
        }
        public static int obtenerPosicionItem(Spinner spinner, String ValorInicializa)
        {
            //Creamos la variable posicion y lo inicializamos en 0
            int posicion = 0;
            //Recorre el spinner en busca del ítem que coincida con el parametro `String fruta`
            //que lo pasaremos posteriormente
            for (int i = 0; i < spinner.Count; i++)
            {
                //Almacena la posición del ítem que coincida con la búsqueda
                if (spinner.GetItemAtPosition(i).ToString().Equals(ValorInicializa))
                {
                    posicion = i;
                }
            }
            //Devuelve un valor entero (si encontro una coincidencia devuelve la
            // posición 0 o N, de lo contrario devuelve 0 = posición inicial)
            return posicion;
        }
    }
}