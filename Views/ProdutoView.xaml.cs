﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfAppPedidos.ViewModels;

namespace WpfAppPedidos.Views
{
    /// <summary>
    /// Lógica interna para ProdutoView.xaml
    /// </summary>
    public partial class ProdutoView : Window
    {
        public ProdutoView()
        {
            InitializeComponent();
            DataContext = new ProdutoViewModel();
        }
    }
}
