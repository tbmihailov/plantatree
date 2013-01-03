﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Data.EntityClient;
using System.ComponentModel;
using System.Xml.Serialization;
using System.Runtime.Serialization;

[assembly: EdmSchemaAttribute()]

namespace PlantATree.Web
{
    #region Contexts
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    public partial class PlantATreeEntities : ObjectContext
    {
        #region Constructors
    
        /// <summary>
        /// Initializes a new PlantATreeEntities object using the connection string found in the 'PlantATreeEntities' section of the application configuration file.
        /// </summary>
        public PlantATreeEntities() : base("name=PlantATreeEntities", "PlantATreeEntities")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        /// <summary>
        /// Initialize a new PlantATreeEntities object.
        /// </summary>
        public PlantATreeEntities(string connectionString) : base(connectionString, "PlantATreeEntities")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        /// <summary>
        /// Initialize a new PlantATreeEntities object.
        /// </summary>
        public PlantATreeEntities(EntityConnection connection) : base(connection, "PlantATreeEntities")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        #endregion
    
        #region Partial Methods
    
        partial void OnContextCreated();
    
        #endregion
    
        #region ObjectSet Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public ObjectSet<Tree> Trees
        {
            get
            {
                if ((_Trees == null))
                {
                    _Trees = base.CreateObjectSet<Tree>("Trees");
                }
                return _Trees;
            }
        }
        private ObjectSet<Tree> _Trees;

        #endregion
        #region AddTo Methods
    
        /// <summary>
        /// Deprecated Method for adding a new object to the Trees EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddToTrees(Tree tree)
        {
            base.AddObject("Trees", tree);
        }

        #endregion
    }
    

    #endregion
    
    #region Entities
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="PlantATree.Model", Name="Tree")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class Tree : EntityObject
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new Tree object.
        /// </summary>
        /// <param name="treeId">Initial value of the TreeId property.</param>
        public static Tree CreateTree(global::System.Int32 treeId)
        {
            Tree tree = new Tree();
            tree.TreeId = treeId;
            return tree;
        }

        #endregion
        #region Primitive Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 TreeId
        {
            get
            {
                return _TreeId;
            }
            set
            {
                if (_TreeId != value)
                {
                    OnTreeIdChanging(value);
                    ReportPropertyChanging("TreeId");
                    _TreeId = StructuralObject.SetValidValue(value);
                    ReportPropertyChanged("TreeId");
                    OnTreeIdChanged();
                }
            }
        }
        private global::System.Int32 _TreeId;
        partial void OnTreeIdChanging(global::System.Int32 value);
        partial void OnTreeIdChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String CreatorName
        {
            get
            {
                return _CreatorName;
            }
            set
            {
                OnCreatorNameChanging(value);
                ReportPropertyChanging("CreatorName");
                _CreatorName = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("CreatorName");
                OnCreatorNameChanged();
            }
        }
        private global::System.String _CreatorName;
        partial void OnCreatorNameChanging(global::System.String value);
        partial void OnCreatorNameChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String Message
        {
            get
            {
                return _Message;
            }
            set
            {
                OnMessageChanging(value);
                ReportPropertyChanging("Message");
                _Message = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("Message");
                OnMessageChanged();
            }
        }
        private global::System.String _Message;
        partial void OnMessageChanging(global::System.String value);
        partial void OnMessageChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public Nullable<global::System.Decimal> CoordinateX
        {
            get
            {
                return _CoordinateX;
            }
            set
            {
                OnCoordinateXChanging(value);
                ReportPropertyChanging("CoordinateX");
                _CoordinateX = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("CoordinateX");
                OnCoordinateXChanged();
            }
        }
        private Nullable<global::System.Decimal> _CoordinateX;
        partial void OnCoordinateXChanging(Nullable<global::System.Decimal> value);
        partial void OnCoordinateXChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public Nullable<global::System.Decimal> CoordinateY
        {
            get
            {
                return _CoordinateY;
            }
            set
            {
                OnCoordinateYChanging(value);
                ReportPropertyChanging("CoordinateY");
                _CoordinateY = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("CoordinateY");
                OnCoordinateYChanged();
            }
        }
        private Nullable<global::System.Decimal> _CoordinateY;
        partial void OnCoordinateYChanging(Nullable<global::System.Decimal> value);
        partial void OnCoordinateYChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public Nullable<global::System.Decimal> CoordinateZ
        {
            get
            {
                return _CoordinateZ;
            }
            set
            {
                OnCoordinateZChanging(value);
                ReportPropertyChanging("CoordinateZ");
                _CoordinateZ = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("CoordinateZ");
                OnCoordinateZChanged();
            }
        }
        private Nullable<global::System.Decimal> _CoordinateZ;
        partial void OnCoordinateZChanging(Nullable<global::System.Decimal> value);
        partial void OnCoordinateZChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public Nullable<global::System.DateTime> CreationDate
        {
            get
            {
                return _CreationDate;
            }
            set
            {
                OnCreationDateChanging(value);
                ReportPropertyChanging("CreationDate");
                _CreationDate = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("CreationDate");
                OnCreationDateChanged();
            }
        }
        private Nullable<global::System.DateTime> _CreationDate;
        partial void OnCreationDateChanging(Nullable<global::System.DateTime> value);
        partial void OnCreationDateChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String CreatorEmail
        {
            get
            {
                return _CreatorEmail;
            }
            set
            {
                OnCreatorEmailChanging(value);
                ReportPropertyChanging("CreatorEmail");
                _CreatorEmail = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("CreatorEmail");
                OnCreatorEmailChanged();
            }
        }
        private global::System.String _CreatorEmail;
        partial void OnCreatorEmailChanging(global::System.String value);
        partial void OnCreatorEmailChanged();

        #endregion
    
    }

    #endregion
    
}