using DevExpress.Xpf.Core;
using OMS.Core.Models;

namespace OMS.Orders
{
    public partial class EditOrder : ThemedWindow
    {
        Order SelectedOrder;

        public EditOrder(Order order)
        {
            InitializeComponent();
            SelectedOrder = order;
        }
    }
}
