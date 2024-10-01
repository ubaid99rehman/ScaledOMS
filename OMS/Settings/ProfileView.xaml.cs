using DevExpress.Xpf.Core;
using Microsoft.Extensions.DependencyInjection;
using OMS.VM.Settings;
using System.Windows;

namespace OMS
{
    public partial class ProfileView : ThemedWindow
    {
        //Constructor
        public ProfileView()
        {
            InitializeComponent();
            this.DataContext = AppServiceProvider.GetServiceProvider().GetRequiredService<ProfileModel>();
        }

        //Update Button Click
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
