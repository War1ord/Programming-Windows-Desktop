/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocatorTemplate xmlns:vm="clr-namespace:TimeTracking.UI.ViewModel"
                                   x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"
*/

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using System.Collections.Generic;
using TimeTracking.UI.Model;

namespace TimeTracking.UI.Mvvm
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class ViewModelLocator
    {
        static ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            if (ViewModelBase.IsInDesignModeStatic)
            {
                SimpleIoc.Default.Register<IDataService, Design.DesignDataService>();
            }
            else
            {
                SimpleIoc.Default.Register<IDataService, DataService>();
            }

            SimpleIoc.Default.Register<ViewModel.MainViewModel>();
            SimpleIoc.Default.Register<ViewModel.ManageClientsOrProjectsViewModel>();
            SimpleIoc.Default.Register<ViewModel.WorkItemsListViewModel>();
        }

        public ViewModel.MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ViewModel.MainViewModel>();
            }
        }

        public ViewModel.WorkItemsListViewModel WorkItemsList
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ViewModel.WorkItemsListViewModel>();
            }
        }

        public ViewModel.ManageClientsOrProjectsViewModel ManageClientsOrProjects
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ViewModel.ManageClientsOrProjectsViewModel>();
            }
        }
	    
        public List<Models.ClientOrProject> ClientsOrProjectsList
        {
            get
            {
                return new Business.WorkItems().GetClientsOrProjectsList();
            }
        }

        /// <summary>
        /// Cleans up all the resources.
        /// </summary>
        public static void Cleanup()
        {
        }
    }
}