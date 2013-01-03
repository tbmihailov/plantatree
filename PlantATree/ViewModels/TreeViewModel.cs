using GalaSoft.MvvmLight;
using System;

namespace PlantATree.ViewModels
{
    public class TreeViewModel : ViewModelBase
    {
        public TreeViewModel(TreeService.Tree tree)
        {
            this.tree = tree;
        }
        public TreeViewModel()
        {
            this.tree = new TreeService.Tree();
        }

        private TreeService.Tree tree;
        public TreeService.Tree Tree
        {
            get { return tree; }
            set { tree = value; }
        }

        #region Properties

        public int TreeId
        {
            get
            {
                return tree.TreeId;
            }

            set
            {
                if (tree.TreeId == value)
                {
                    return;
                }
                tree.TreeId = value;
                RaisePropertyChanged("TreeId");
            }
        }

        public string CreatorName
        {
            get
            {
                return tree.CreatorName;
            }

            set
            {
                if (tree.CreatorName == value)
                {
                    return;
                }
                tree.CreatorName = value;
                RaisePropertyChanged("CreatorName");
            }
        }


        public string Message
        {
            get
            {
                return tree.Message;
            }

            set
            {
                if (tree.Message == value)
                {
                    return;
                }
                tree.Message = value;
                RaisePropertyChanged("Message");
            }
        }


        public double CoordinateX
        {
            get
            {
                return (tree.CoordinateX.HasValue ? (double)tree.CoordinateX : 0.0);
            }

            set
            {
                if (tree.CoordinateX == (decimal)value)
                {
                    return;
                }
                tree.CoordinateX = (decimal)value;
                RaisePropertyChanged("CoordinateX");
            }
        }


        public double CoordinateY
        {
            get
            {
                return (tree.CoordinateY.HasValue ? (double)tree.CoordinateY : 0.0);
            }

            set
            {
                if (tree.CoordinateY == (decimal)value)
                {
                    return;
                }
                tree.CoordinateY = (decimal)value;
                RaisePropertyChanged("CoordinateY");
            }
        }


        public DateTime? CreationDate
        {
            get
            {
                return tree.CreationDate;
            }

            set
            {
                if (tree.CreationDate == value)
                {
                    return;
                }
                tree.CreationDate = value;
                RaisePropertyChanged("CreationDate");
            }
        }


        public string CreatorEmail
        {
            get
            {
                return tree.CreatorEmail;
            }

            set
            {
                if (tree.CreatorEmail == value)
                {
                    return;
                }
                tree.CreatorEmail = value;
                RaisePropertyChanged("CreatorEmail");
            }
        }
        #endregion
    }
}