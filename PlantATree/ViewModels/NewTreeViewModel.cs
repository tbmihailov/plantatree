using GalaSoft.MvvmLight;
using PlantATree.Services;
using GalaSoft.MvvmLight.Messaging;
using PlantATree.Messages;
using GalaSoft.MvvmLight.Command;

namespace PlantATree.ViewModels
{
    public class NewTreeViewModel : ViewModelBase
    {
        ITreeDataService dataService;
        /// <summary>
        /// Initializes a new instance of the NewTreeViewModel class.
        /// </summary>
        public NewTreeViewModel()
        {
            dataService = ServiceProvider.Instance.TreeDataService;
        }


        /// <summary>
        /// The <see cref="Tree" /> property's name.
        /// </summary>
        public const string TreePropertyName = "Tree";
        private TreeViewModel _tree;
        public TreeViewModel Tree
        {
            get
            {
                return _tree;
            }

            set
            {
                if (_tree == value)
                {
                    return;
                }

                var oldValue = _tree;
                _tree = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(TreePropertyName);
            }
        }

        public void Save()
        {
            var treeToInsert = this.Tree.Tree;
            dataService.InsertTrees(treeToInsert, OnSaveCompleted);
        }

        public void OnSaveCompleted(int savedMessageId)
        {
            if (savedMessageId == 0)
            {
                Messenger.Default.Send<TreeSaveUnsuccessfullMessage>(new TreeSaveUnsuccessfullMessage(this.Tree));
                return;
            }
            //Tree save is successfull
            Tree.TreeId = savedMessageId;
            Messenger.Default.Send<TreeSaveSuccessfullMessage>(new TreeSaveSuccessfullMessage(this.Tree));
            Messenger.Default.Send<AddTreeToTreesListMessage>(new AddTreeToTreesListMessage(this.Tree));
        }


        #region SaveCommand

        private RelayCommand _saveCommand;
        /// <summary>
        /// The <see cref="SaveCommand" />.
        /// </summary>

        public RelayCommand SaveCommand
        {
            get
            {
                if (_saveCommand == null)
                {
                    _saveCommand = new RelayCommand(
                            () =>
                            {
                                SaveExecute();
                            },
                            () => CanSave
                        );
                }
                return _saveCommand;
            }
            set
            {
                _saveCommand = value;
            }
        }

        /// <summary>
        /// Executes when SaveCommand is called
        /// </summary>
        public void SaveExecute()
        {
            this.Save();
        }

        /// <summary>
        /// The <see cref="CanSave" /> property's name.
        /// </summary>
        public const string CanSavePropertyName = "CanSave";

        private bool _canSave = true;

        public bool CanSave
        {
            get
            {
                return _canSave;
            }

            set
            {
                if (_canSave == value)
                {
                    return;
                }

                var oldValue = _canSave;
                _canSave = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(CanSavePropertyName);

                //tells the controls that are binded to the Command if it can execute
                SaveCommand.RaiseCanExecuteChanged();
            }
        }
        #endregion

        #region CancelCommand

        private RelayCommand _cancelCommand;
        /// <summary>
        /// The <see cref="CancelCommand" />.
        /// </summary>

        public RelayCommand CancelCommand
        {
            get
            {
                if (_cancelCommand == null)
                {
                    _cancelCommand = new RelayCommand(
                            () =>
                            {
                                CancelExecute();
                            },
                            () => CanCancel
                        );
                }
                return _cancelCommand;
            }
            set
            {
                _cancelCommand = value;
            }
        }

        /// <summary>
        /// Executes when CancelCommand is called
        /// </summary>
        public void CancelExecute()
        {
            //Tells the ForestNature to cancel planting
            //Messenger.Default.Send<CancelPlantingModeMessage>(new CancelPlantingModeMessage());
            Messenger.Default.Send<NewTreeCanceledMessage>(new NewTreeCanceledMessage());
        }

        /// <summary>
        /// The <see cref="CanCancel" /> property's name.
        /// </summary>
        public const string CanCancelPropertyName = "CanCancel";

        private bool _canCancel = true;
        public bool CanCancel
        {
            get
            {
                return _canCancel;
            }

            set
            {
                if (_canCancel == value)
                {
                    return;
                }

                var oldValue = _canCancel;
                _canCancel = value;

                // Update bindings, no broadcast
                RaisePropertyChanged(CanCancelPropertyName);

                //tells the controls that are binded to the Command if it can execute
                CancelCommand.RaiseCanExecuteChanged();
            }
        }
        #endregion

        public void Validate()
        {
            bool hasError = false;
            string error = string.Empty;

            string creatorName = Tree.CreatorName;
            if (string.IsNullOrEmpty(creatorName) || string.IsNullOrWhiteSpace(creatorName))
            {
                error += string.Format("{0}\n", "Please, enter your name!");
                hasError = true;
            }

            string message = Tree.Message;
            if (string.IsNullOrEmpty(message) || string.IsNullOrWhiteSpace(message))
            {
                error += string.Format("{0}\n", "Please, enter your green message!");
                hasError = true;
            }

            if (hasError)
            {
                ErrorMessage = error;
            }
        }

        #region Error Message
        private string errorMessage;
        public string ErrorMessage
        {
            get
            {
                return errorMessage;
            }

            set
            {
                if (errorMessage == value)
                {
                    return;
                }
                errorMessage = value;
                RaisePropertyChanged("ErrorMessage");
            }
        }
        #endregion
        public override void Cleanup()
        {
            // Clean own resources if needed
            base.Cleanup();
            Tree = null;
        }

    }
}