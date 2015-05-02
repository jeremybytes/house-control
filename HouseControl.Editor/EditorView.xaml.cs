using Microsoft.Win32;
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
            var dialog = new OpenFileDialog()
            {
                FileName = "ScheduleData",
                DefaultExt = ".json",
                Filter = "JSON (.json)|*.json" + "|All Files|*.*",
            };

            Nullable<bool> result = dialog.ShowDialog();
            if (result == true)
            {
                viewModel.FileName = dialog.FileName;
            }
        }
    }
}
