﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace PlantATree.Controls
{
    public partial class Cloud : Canvas
    {
        public Cloud()
        {
            InitializeComponent();
        }

        private void Path_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
           // MessageBox.Show("Cloud Clicked!");
        }
    }
}
