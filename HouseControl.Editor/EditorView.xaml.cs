using System.Windows;
using System.Windows.Controls;

namespace HouseControl.Editor
{
    public partial class EditorView : UserControl
    {
        private EditorViewModel viewModel;

        public EditorView()
        {
            InitializeComponent();
            viewModel = new EditorViewModel();
            DataContext = viewModel;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            viewModel.SaveSchedule();
        }
    }
}
