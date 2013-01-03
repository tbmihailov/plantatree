/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:PlantATree.ViewModels"
                                   x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"
*/

using GalaSoft.MvvmLight;
namespace PlantATree.ViewModels
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            if (ViewModelBase.IsInDesignModeStatic)
            {
                // Create design time view models
            }
            else
            {
                // Create run time view models
            }
        }

        #region ForestNature ViewModel
        private static ForestNatureViewModel _forestNatureViewModel;

        /// <summary>
        /// Gets the ForestNatureViewModel property.
        /// </summary>
        public static ForestNatureViewModel ForestNatureViewModelStatic
        {
            get
            {
                if (_forestNatureViewModel == null)
                {
                    CreateForestNatureViewModel();
                }

                return _forestNatureViewModel;
            }
        }

        /// <summary>
        /// Gets the ForestNatureViewModel property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public ForestNatureViewModel ForestNatureViewModel
        {
            get
            {
                return ForestNatureViewModelStatic;
            }
        }

        /// <summary>
        /// Provides a deterministic way to delete the ForestNatureViewModel property.
        /// </summary>
        public static void ClearForestNatureViewModel()
        {
            _forestNatureViewModel.Cleanup();
            _forestNatureViewModel = null;
        }

        /// <summary>
        /// Provides a deterministic way to create the ForestNatureViewModel property.
        /// </summary>
        public static void CreateForestNatureViewModel()
        {
            if (_forestNatureViewModel == null)
            {
                _forestNatureViewModel = new ForestNatureViewModel();
            }
        }
        #endregion

        #region NewTree 
        private static NewTreeViewModel _newTreeViewModel;

        /// <summary>
        /// Gets the NewTreeViewModel property.
        /// </summary>
        public static NewTreeViewModel NewTreeViewModelStatic
        {
            get
            {
                if (_newTreeViewModel == null)
                {
                    CreateNewTreeViewModel();
                }

                return _newTreeViewModel;
            }
        }

        /// <summary>
        /// Gets the NewTreeViewModel property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public NewTreeViewModel NewTreeViewModel
        {
            get
            {
                return NewTreeViewModelStatic;
            }
        }

        /// <summary>
        /// Provides a deterministic way to delete the NewTreeViewModel property.
        /// </summary>
        public static void ClearNewTreeViewModel()
        {
            _newTreeViewModel.Cleanup();
            _newTreeViewModel = null;
        }

        /// <summary>
        /// Provides a deterministic way to create the NewTreeViewModel property.
        /// </summary>
        public static void CreateNewTreeViewModel()
        {
            if (_newTreeViewModel == null)
            {
                _newTreeViewModel = new NewTreeViewModel();
            }
        }

        #endregion

        /// <summary>
        /// Cleans up all the resources.
        /// </summary>
        public static void Cleanup()
        {
            ClearForestNatureViewModel();
            ClearNewTreeViewModel();
        }

    }
}