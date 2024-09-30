using DevExpress.Xpf.Core;
using Microsoft.Extensions.DependencyInjection;
using OMS.ViewModels;
using OMS.VM.Settings;
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

namespace OMS
{
    public partial class ProfileView : ThemedWindow
    {
        public ProfileView()
        {
            InitializeComponent();
            this.DataContext = AppServiceProvider.GetServiceProvider().GetRequiredService<ProfileModel>();
        }

        private void btnUpdateOrder_Click(object sender, RoutedEventArgs e)
        {
            if (this.DataContext is ProfileModel model)
            {
                bool isUpdated = model.UpdateUser(out string message);
                if (isUpdated)
                {
                    MessageBox.Show("User Updated Successfully!", "User Update", MessageBoxButton.OK);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Cannot Update User!", "User Update", MessageBoxButton.OK);
                }

            }
        }
    }
}
