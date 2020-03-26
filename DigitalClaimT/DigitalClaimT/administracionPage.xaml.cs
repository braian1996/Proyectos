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
	public partial class administracionPage : MasterDetailPage
	{
		public administracionPage ()
		{
			InitializeComponent ();
            mymenu();
            ListMenu.ItemSelected += ListMenu_ItemSelected;

            ListMenu.ItemSelected -= ListMenu_ItemSelected;
        }

        private void ListMenu_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            //string stForm = "";
            //var menu = e.SelectedItem as clsMenu;
            //if (menu != null)
            //{
            //    if (menu.MenuTitle == "Calles")
            //    {
            //        stForm = menu.MenuTitle;
            //        Detail = new NavigationPage(new frCalle(stForm));
            //        IsPresented = false;
            //    }
            //    else
            //    {
            //        if (menu.MenuTitle == "Canales")
            //        {
            //            stForm = menu.MenuTitle;
            //            IsPresented = false;
            //            Detail = new NavigationPage(new frCalle(stForm));
            //        }
            //        else
            //        {
            //            if (menu.MenuTitle == "Entidades")
            //            {
            //                stForm = menu.MenuTitle;
            //                IsPresented = false;
            //                Detail = new NavigationPage(new frCalle(stForm));
            //            }
            //            else
            //            {
            //                if (menu.MenuTitle == "Estados")
            //                {
            //                    stForm = menu.MenuTitle;
            //                    IsPresented = false;
            //                    Detail = new NavigationPage(new frCalle(stForm));
            //                }
            //                else
            //                {
            //                    if (menu.MenuTitle == "Prioridades")
            //                    {
            //                        stForm = menu.MenuTitle;
            //                        IsPresented = false;
            //                        Detail = new NavigationPage(new frCalle(stForm));
            //                    }
            //                    else
            //                    {
            //                        if (menu.MenuTitle == "Temas")
            //                        {
            //                            stForm = menu.MenuTitle;
            //                            IsPresented = false;
            //                            Detail = new NavigationPage(new frCalle(stForm));
            //                        }
            //                    }
            //                }
            //            }
            //        }
            //    }
            //}
        }

        public void mymenu()
        {
           
        //    Detail = new NavigationPage(new detalle());
        //    List<clsMenu> menu = new List<clsMenu>
        //    {
        //    new clsMenu {Page = new detalle(), MenuTitle = "Calles"},
        //         new clsMenu { Page = new detalle(), MenuTitle = "Canales",},
        //          new clsMenu { Page = new detalle(), MenuTitle = "Entidades"},
        //           new clsMenu { Page = new detalle(), MenuTitle = "Estados"},
        //            new clsMenu { Page = new detalle(), MenuTitle = "Prioridades" },
        //             new clsMenu { Page = new detalle(), MenuTitle = "Temas" },
        //              new clsMenu { Page = new detalle(), MenuTitle = "SubTemas" }
        //};
        //    ListMenu.ItemsSource = menu;
        }
      
	}
}