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
    public class clsListarOS : BaseAdapter<clsConsultarOrdenServicio>
    {
        private readonly IList<clsConsultarOrdenServicio> _items;
        private readonly Context _context;
        private ArrayAdapter _adapterNomEstadoOS;
        private List<string> _LstID;

        public clsListarOS(Context context, IList<clsConsultarOrdenServicio> items, ArrayAdapter adapterNom, List<string> lstID)
        {
            _items = items;
            _context = context;
            _adapterNomEstadoOS = adapterNom;
            _LstID = lstID;
        
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
                    view = inflater.Inflate(Resource.Layout.listViewOrdenServicio, parent, false);


                }

                view.FindViewById<TextView>(Resource.Id.tvNOS).Text = "Numero de Orden de Servicio: ";
                view.FindViewById<TextView>(Resource.Id.tvNOSMuestra).Text = item.orServ_numero;
                view.FindViewById<TextView>(Resource.Id.tvFechaCreacion).Text = "Fecha Alta: ";
                view.FindViewById<TextView>(Resource.Id.tvFechaCreacionMuestra).Text = item.orServ_fechaAlta;
                view.FindViewById<TextView>(Resource.Id.tvFechaVenci).Text = "Fecha de Vencimiento: ";
                view.FindViewById<TextView>(Resource.Id.tvFechaVenciMuestra).Text = item.orServ_fechaVencimiento;
                view.FindViewById<TextView>(Resource.Id.tvObservacion).Text = "Observación: ";
                view.FindViewById<TextView>(Resource.Id.tvObservacionMuestra).Text = item.orServ_observaciones;
                //view.FindViewById<TextView>(Resource.Id.tvFechaInicio).Text = "Fecha de Inicio: ";
                //view.FindViewById<TextView>(Resource.Id.tvFechaInicioMuestra).Text = item.orServ_fechaInicio;
                view.FindViewById<TextView>(Resource.Id.tvFechaCierre).Text = "Fecha de Cierre: ";
                view.FindViewById<TextView>(Resource.Id.tvFechaCierreMuestra).Text = item.orServ_fechaCierre;
                view.FindViewById<TextView>(Resource.Id.tvEstado).Text = "Estado :";
                view.FindViewById<TextView>(Resource.Id.textViewIdOrdenServicio).Text = item.orServ_IDOrdenServicio;


                TextView tvIDorden = view.FindViewById<TextView>(Resource.Id.textViewIdOrdenServicio);
                TextView tvOrdeSerNum = view.FindViewById<TextView>(Resource.Id.tvNOSMuestra);
                TextView tvFechaAlta = view.FindViewById<TextView>(Resource.Id.tvFechaCreacionMuestra);
                TextView tvFechaVenci = view.FindViewById<TextView>(Resource.Id.tvFechaVenciMuestra);
                TextView tvObservacion = view.FindViewById<TextView>(Resource.Id.tvObservacionMuestra);
                //TextView tvFechainicio = view.FindViewById<TextView>(Resource.Id.tvFechaInicioMuestra);
                TextView tvFechaCierreMuestra = view.FindViewById<TextView>(Resource.Id.tvFechaCierreMuestra);

                tvIDorden.Text = item.orServ_IDOrdenServicio;
                tvOrdeSerNum.Text = item.orServ_numero;
                tvFechaAlta.Text = item.orServ_fechaAlta;
                tvFechaVenci.Text = item.orServ_fechaVencimiento;
                tvObservacion.Text = item.orServ_observaciones;
                //tvFechainicio.Text = item.orServ_fechaInicio;
                tvFechaCierreMuestra.Text = item.orServ_fechaCierre;

                Spinner spnEstado = view.FindViewById<Spinner>(Resource.Id.spinner1);
                spnEstado.Adapter = _adapterNomEstadoOS;
                var btnListado = view.FindViewById<Button>(Resource.Id.btnLSTorServ);
                spnEstado.ItemSelected += SpnEstado_ItemSelected;
                spnEstado.SetSelection(obtenerPosicionItem(spnEstado, item.orServ_EstadoOrdenServicio));

                if (!btnListado.HasOnClickListeners)
                { // Con esto evitas de asignarle mas de una vez un listener
                    btnListado.Click += delegate {

                        ClsListarOS_Click(tvIDorden.Text, tvOrdeSerNum.Text, tvFechaAlta.Text, tvFechaVenci.Text, tvObservacion.Text, tvFechaCierreMuestra.Text,item.orServ_IDAreaServicio,item.orServ_fechaInicio); // Acá le pasarias el/los datos que necesites.

                    };
                }


                return view;
            }
            catch (Exception ex)
            {

                
            }
            return null;
        }

        private void SpnEstado_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            try
            {
                string stPosEstadoOrdenServ = _LstID[e.Position].ToString();
            }
            catch (Exception)
            {

            }
            
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

        private void ClsListarOS_Click(string idOrdenServicio, string NroOrdenServicio, string FechaAlta, string FechaVenci, string Observacion, string FechaCierre, string IDAreaServicio,string fechainicio)
        {
            try
            {
                Intent secondActivityListadoReclamoOS = new Intent(_context, typeof(ActivityListadoOrdenServicio));
                secondActivityListadoReclamoOS.PutExtra("IDOrdenServicio", idOrdenServicio);
                secondActivityListadoReclamoOS.PutExtra("NroOrdenServicio", NroOrdenServicio);
                secondActivityListadoReclamoOS.PutExtra("fechalta", FechaAlta);
                secondActivityListadoReclamoOS.PutExtra("fechavenc", FechaVenci);
                secondActivityListadoReclamoOS.PutExtra("observacion", Observacion);
                secondActivityListadoReclamoOS.PutExtra("fechainicio", fechainicio);
                secondActivityListadoReclamoOS.PutExtra("fechacierre", FechaCierre);
                secondActivityListadoReclamoOS.PutExtra("idareaserv", IDAreaServicio);
                _context.StartActivity(secondActivityListadoReclamoOS);
                //_ValorItemSelecLista = null;  

            }
            catch (Exception ex)
            {

            }
  
        }

        public override int Count
        {
            get { return _items.Count; }
        }

        public override clsConsultarOrdenServicio this[int position]
        {
            get { return _items[position]; }
        }
    }
}