using System;
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

        private void FileNameButton_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.FileName = "ScheduleData";
            dlg.DefaultExt = ".json";
            dlg.Filter = "JSON (.json)|*.json" +
                         "|All Files|*.*";

            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                viewModel.FileName = dlg.FileName;
            }
        }
    }
}
