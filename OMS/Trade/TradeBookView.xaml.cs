using DevExpress.Xpf.Grid;
using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace OMS.Trade
{
    /// <summary>
    /// Interaction logic for TradeBookView.xaml
    /// </summary>
    public partial class TradeBookView : UserControl
    {
        public TradeBookView()
        {
            InitializeComponent();
        }

        [Obsolete]
        private void AlignLeft_Click(object sender, RoutedEventArgs e)
        {
            foreach (var column in dataGrid.Columns)
            {
                column.HorizontalHeaderContentAlignment = HorizontalAlignment.Left;
                column.ActualEditSettings.HorizontalContentAlignment = (DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment)HorizontalAlignment.Left;
            }
            dataGrid.HorizontalAlignment = HorizontalAlignment.Left;
        }

        private void AlignRight_Click(object sender, RoutedEventArgs e)
        {
            foreach(var column in dataGrid.Columns)
            {
                column.HorizontalHeaderContentAlignment = HorizontalAlignment.Right;
                column.ActualEditSettings.HorizontalContentAlignment = (DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment)HorizontalAlignment.Right;
            }
            dataGrid.HorizontalAlignment = HorizontalAlignment.Right;
            
        }

        private void AlignCenter_Click(object sender, RoutedEventArgs e)
        {
            foreach (var column in dataGrid.Columns)
            {
                column.HorizontalHeaderContentAlignment = HorizontalAlignment.Center;
                column.ActualEditSettings.HorizontalContentAlignment = (DevExpress.Xpf.Editors.Settings.EditSettingsHorizontalAlignment)HorizontalAlignment.Center;
            }
            dataGrid.HorizontalAlignment = HorizontalAlignment.Center;
        }

        private void AlignTop_Click(object sender, RoutedEventArgs e)
        {
            watchListView.VerticalAlignment = VerticalAlignment.Top;
        }

        private void AlignBottom_Click(object sender, RoutedEventArgs e)
        {
            watchListView.VerticalAlignment = VerticalAlignment.Bottom;
        }

    }
}
