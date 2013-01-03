using GalaSoft.MvvmLight;
using PlantATree.TreeService;
using PlantATree.Services;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight.Messaging;
using PlantATree.Messages;
using GalaSoft.MvvmLight.Command;
using System.Windows.Input;
using System.Windows;

namespace PlantATree.ViewModels
{
    public class ForestNatureViewModel : ViewModelBase
    {
        private int totalTreesNeededThisYear = 193404000;
        ITreeDataService dataService;

        public ForestNatureViewModel()
        {
            dataService = ServiceProvider.Instance.TreeDataService;
            Trees = new ObservableCollection<TreeViewModel>();
            this.RegisterMessages();
            this.LoadTrees();
        }

        public void RegisterMessages()
        {
            Messenger.Default.Register<IsPlantingModePropertyChangedMessage>(this, OnIsPlantingModeChanged);
            Messenger.Default.Register<EndPlantingModeMessage>(this, OnEndPlantingMode);
            Messenger.Default.Register<AddTreeToTreesListMessage>(this, OnAddTreeToTreesList);
        }

        public void OnAddTreeToTreesList(AddTreeToTreesListMessage msg)
        {
            this.AddNewTreeToList(msg.Tree);
        }

        public void OnEndPlantingMode(EndPlantingModeMessage msg)
        {
            this.IsPlantingMode = false;
        }
        public void OnIsPlantingModeChanged(IsPlantingModePropertyChangedMessage msg)
        {
            this.IsPlantingMode = msg.NewValue;
        }

        public void LoadTrees()
        {
            dataService.GetTreesList(OnGetTreesList);
        }

        public void OnGetTreesList(ObservableCollection<PlantATree.TreeService.Tree> trees)
        {
            foreach (var tree in trees)
            {
                TreeViewModel newTreeViewModel = new TreeViewModel(tree);
                this.AddNewTreeToList(newTreeViewModel);
            }
        }

        #region LoadTreesCommand

        private RelayCommand _loadTreesCommand;
        /// <summary>
        /// The <see cref="LoadTreesCommand" />.
        /// </summary>

        public RelayCommand LoadTreesCommand
        {
            get
            {
                if (_loadTreesCommand == null)
                {
                    _loadTreesCommand = new RelayCommand(
                            () =>
                            {
                                LoadTreesExecute();
                            },
                            () => CanLoadTrees
                        );
                }
                return _loadTreesCommand;
            }
            set
            {
                _loadTreesCommand = value;
            }
        }

        /// <summary>
        /// Executes when LoadTreesCommand is called
        /// </summary>
        public void LoadTreesExecute()
        {
            LoadTrees();
        }

        /// <summary>
        /// The <see cref="CanLoadTrees" /> property's name.
        /// </summary>
        public const string CanLoadTreesPropertyName = "CanLoadTrees";

        private bool _canLoadTrees = true;

        /// <summary>
        /// Gets the CanLoadTrees property.
        /// </summary>
        public bool CanLoadTrees
        {
            get
            {
                return _canLoadTrees;
            }

            set
            {
                if (_canLoadTrees == value)
                {
                    return;
                }

                var oldValue = _canLoadTrees;
                _canLoadTrees = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(CanLoadTreesPropertyName);

                //tells the controls that are binded to the Command if it can execute
                LoadTreesCommand.RaiseCanExecuteChanged();
            }
        }
        #endregion

        #region Trees
        /// <summary>
        /// The <see cref="Trees" /> property's name.
        /// </summary>
        public const string TreesPropertyName = "Trees";

        private ObservableCollection<TreeViewModel> _trees;

        /// <summary>
        /// Gets the Trees property.
        /// </summary>
        public ObservableCollection<TreeViewModel> Trees
        {
            get
            {
                return _trees;
            }

            set
            {
                if (_trees == value)
                {
                    return;
                }

                var oldValue = _trees;
                _trees = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(TreesPropertyName);
            }
        }

        public void AddNewTreeToList(TreeViewModel treeVieModel)
        {

            Trees.Add(treeVieModel);

            var newTree = treeVieModel;
            RenderTreeMessage renderTreeMessage = new RenderTreeMessage(newTree);
            Messenger.Default.Send<RenderTreeMessage>(renderTreeMessage);
        }
        #endregion

        #region ShowAboutWindowCommand

        private RelayCommand _showAboutWindowCommand;
        /// <summary>
        /// The <see cref="ShowAboutWindowCommand" />.
        /// </summary>

        public RelayCommand ShowAboutWindowCommand
        {
            get
            {
                if (_showAboutWindowCommand == null)
                {
                    _showAboutWindowCommand = new RelayCommand(
                            () =>
                            {
                                ShowAboutWindowExecute();
                            },
                            () => CanShowAboutWindow
                        );
                }
                return _showAboutWindowCommand;
            }
            set
            {
                _showAboutWindowCommand = value;
            }
        }

        /// <summary>
        /// Executes when ShowAboutWindowCommand is called
        /// </summary>
        public void ShowAboutWindowExecute()
        {
            Messenger.Default.Send<ShowAboutMessage>(new ShowAboutMessage());
        }


        /// <summary>
        /// The <see cref="CanShowAboutWindow" /> property's name.
        /// </summary>
        public const string CanShowAboutWindowPropertyName = "CanShowAboutWindow";

        private bool _canShowAboutWindow = true;

        /// <summary>
        /// Gets the CanShowAboutWindow property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public bool CanShowAboutWindow
        {
            get
            {
                return _canShowAboutWindow;
            }

            set
            {
                if (_canShowAboutWindow == value)
                {
                    return;
                }

                var oldValue = _canShowAboutWindow;
                _canShowAboutWindow = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(CanShowAboutWindowPropertyName);

                //tells the controls that are binded to the Command if it can execute
                ShowAboutWindowCommand.RaiseCanExecuteChanged();
            }
        }
        #endregion

        #region IsPlantinng Property
        /// <summary>
        /// The <see cref="IsPlantingMode" /> property's name.
        /// </summary>
        public const string IsPlantingPropertyName = "IsPlantingMode";

        private bool _isPlanting = false;

        /// <summary>
        /// Gets the IsPlanting property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public bool IsPlantingMode
        {
            get
            {
                return _isPlanting;
            }

            set
            {
                if (_isPlanting == value)
                {
                    return;
                }

                var oldValue = _isPlanting;
                _isPlanting = value;

                // Update bindings and broadcast change using GalaSoft.MvvmLight.Messenging
                RaisePropertyChanged(IsPlantingPropertyName, oldValue, value, true);
                Messenger.Default.Send<IsPlantingModePropertyChangedMessage>(new IsPlantingModePropertyChangedMessage()
                    {
                        NewValue = value,
                        OldValue = oldValue,
                    });
            }
        }
        #endregion

        #region StartPlantingCommand

        private RelayCommand _startPlantingCommand;
        /// <summary>
        /// The <see cref="StartPlantingCommand" />.
        /// </summary>

        public RelayCommand StartPlantingCommand
        {
            get
            {
                if (_startPlantingCommand == null)
                {
                    _startPlantingCommand = new RelayCommand(
                            () =>
                            {
                                StartPlantingExecute();
                            },
                            () => CanStartPlanting
                        );
                }
                return _startPlantingCommand;
            }
            set
            {
                _startPlantingCommand = value;
            }
        }

        /// <summary>
        /// Executes when StartPlantingCommand is called
        /// </summary>
        public void StartPlantingExecute()
        {
            IsPlantingMode = true;
        }


        /// <summary>
        /// The <see cref="CanStartPlanting" /> property's name.
        /// </summary>
        public const string CanStartPlantingPropertyName = "CanStartPlanting";
        private bool _canStartPlanting = true;
        public bool CanStartPlanting
        {
            get
            {
                return _canStartPlanting;
            }

            set
            {
                if (_canStartPlanting == value)
                {
                    return;
                }

                var oldValue = _canStartPlanting;
                _canStartPlanting = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(CanStartPlantingPropertyName);

                //tells the controls that are binded to the Command if it can execute
                StartPlantingCommand.RaiseCanExecuteChanged();
            }
        }
        #endregion

        #region EndPlantingCommand

        private RelayCommand _endPlantingCommand;
        /// <summary>
        /// The <see cref="EndPlantingCommand" />.
        /// </summary>

        public RelayCommand EndPlantingCommand
        {
            get
            {
                if (_endPlantingCommand == null)
                {
                    _endPlantingCommand = new RelayCommand(
                            () =>
                            {
                                EndPlantingExecute();
                            },
                            () => CanEndPlanting
                        );
                }
                return _endPlantingCommand;
            }
            set
            {
                _endPlantingCommand = value;
            }
        }

        /// <summary>
        /// Executes when EndPlantingCommand is called
        /// </summary>
        public void EndPlantingExecute()
        {
            //Messenger.Default.Send<EndPlantingModeMessage>(new EndPlantingModeMessage());
            IsPlantingMode = false;
        }


        /// <summary>
        /// The <see cref="CanEndPlanting" /> property's name.
        /// </summary>
        public const string CanEndPlantingPropertyName = "CanEndPlanting";

        private bool _canEndPlanting = true;

        /// <summary>
        /// Gets the CanEndPlanting property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public bool CanEndPlanting
        {
            get
            {
                return _canEndPlanting;
            }

            set
            {
                if (_canEndPlanting == value)
                {
                    return;
                }

                var oldValue = _canEndPlanting;
                _canEndPlanting = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(CanEndPlantingPropertyName);

                //tells the controls that are binded to the Command if it can execute
                EndPlantingCommand.RaiseCanExecuteChanged();
            }
        }
        #endregion

        private int totalTreesNeededPerYear = 193404000;
        public int TotalTreesNeeded
        {
            get
            {
                return totalTreesNeededPerYear;
            }

        }

        #region ShowHelpWindowCommand

        private RelayCommand _showHelpWindowCommand;
        /// <summary>
        /// The <see cref="ShowHelpWindowCommand" />.
        /// </summary>

        public RelayCommand ShowHelpWindowCommand
        {
            get
            {
                if (_showHelpWindowCommand == null)
                {
                    _showHelpWindowCommand = new RelayCommand(
                            () =>
                            {
                                ShowHelpWindowExecute();
                            },
                            () => CanShowHelpWindow
                        );
                }
                return _showHelpWindowCommand;
            }
            set
            {
                _showHelpWindowCommand = value;
            }
        }

        /// <summary>
        /// Executes when ShowHelpWindowCommand is called
        /// </summary>
        public void ShowHelpWindowExecute()
        {
            Messenger.Default.Send<ShowHelpWindowMesage>(new ShowHelpWindowMesage());
        }


        /// <summary>
        /// The <see cref="CanShowHelpWindow" /> property's name.
        /// </summary>
        public const string CanShowHelpWindowPropertyName = "CanShowHelpWindow";

        private bool _canShowHelpWindow = true;

        /// <summary>
        /// Gets the CanShowHelpWindow property.
        /// TODO Update documentation:
        /// Changes to that property's value raise the PropertyChanged event. 
        /// This property's value is broadcasted by the Messenger's default instance when it changes.
        /// </summary>
        public bool CanShowHelpWindow
        {
            get
            {
                return _canShowHelpWindow;
            }

            set
            {
                if (_canShowHelpWindow == value)
                {
                    return;
                }

                var oldValue = _canShowHelpWindow;
                _canShowHelpWindow = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(CanShowHelpWindowPropertyName);

                //tells the controls that are binded to the Command if it can execute
                ShowHelpWindowCommand.RaiseCanExecuteChanged();
            }
        }
        #endregion
        ////public override void Cleanup()
        ////{
        ////    // Clean own resources if needed

        ////    base.Cleanup();
        ////}
    }
}