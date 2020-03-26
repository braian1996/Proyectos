using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DigitalClaimT
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class frCalle : ContentPage
	{
        string palaForm = "";
		public frCalle (string stPalaFormulario)
		{
			InitializeComponent ();
            palaForm = stPalaFormulario;
            ocultar(palaForm);
		}
        public void ocultar(string nombreFormulario)
        {
            if (nombreFormulario=="Calles")
            {
                gridCalle.IsVisible = true;
                gridCanal.IsVisible = false;
                gridEntidad.IsVisible = false;
                gridEstados.IsVisible = false;
                gridPrioridad.IsVisible = false;
                gridTemas.IsVisible = false;
            }
            else
            {
                if (nombreFormulario == "Canales")
                {
                    gridCalle.IsVisible = false;
                    gridCanal.IsVisible = true;
                    gridEntidad.IsVisible = false;
                    gridEstados.IsVisible = false;
                    gridPrioridad.IsVisible = false;
                    gridTemas.IsVisible = false;
                }
                else
                {
                    if (nombreFormulario == "Entidades")
                    {
                        gridCalle.IsVisible = false;
                        gridCanal.IsVisible = false;
                        gridEntidad.IsVisible = true;
                        gridEstados.IsVisible = false;
                        gridPrioridad.IsVisible = false;
                        gridTemas.IsVisible = false;
                    }
                    else
                    {
                        if (nombreFormulario == "Estados")
                        {
                            gridCalle.IsVisible = false;
                            gridCanal.IsVisible = false;
                            gridEntidad.IsVisible = false;
                            gridEstados.IsVisible = true;
                            gridPrioridad.IsVisible = false;
                            gridTemas.IsVisible = false;
                        }
                        else
                        {
                            if (nombreFormulario == "Prioridades")
                            {
                                gridCalle.IsVisible = false;
                                gridCanal.IsVisible = false;
                                gridEntidad.IsVisible = false;
                                gridEstados.IsVisible = false;
                                gridPrioridad.IsVisible = true;
                                gridTemas.IsVisible = false;
                            }
                            else
                            {
                                if (nombreFormulario == "Temas")
                                {
                                    gridCalle.IsVisible = false;
                                    gridCanal.IsVisible = false;
                                    gridEntidad.IsVisible = false;
                                    gridEstados.IsVisible = false;
                                    gridPrioridad.IsVisible = false;
                                    gridTemas.IsVisible = true;
                                }
                            }
                        }
                    }
                }
            }
        }
	}
}