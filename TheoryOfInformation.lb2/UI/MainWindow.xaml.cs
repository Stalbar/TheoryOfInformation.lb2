using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TheoryOfInformation.lb2.Encoding;

namespace TheoryOfInformation.lb2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.Text = string.Empty;
            tb.GotFocus -= TextBox_GotFocus;
        }

        private void BT_addToFileClick(object sender, RoutedEventArgs e)
        {
            using (BinaryWriter bw = new BinaryWriter(File.Open(TB_fileName.Text, FileMode.Create)))
            {
                bw.Write(TB_info.Text);
            }
            TB_info.Text = string.Empty;
            TB_fileName.Text = string.Empty;
        }

        private void TB_fileChange(object sender, TextChangedEventArgs e)
        {
            BT_addToFile.IsEnabled = (!TB_fileName.Text.Equals(string.Empty) && !TB_info.Text.Equals(string.Empty));
        }

        private void KeyValidationTextBox(object Sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-1]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void TB_PerformFileNameChange(object sender, TextChangedEventArgs e)
        {
            BT_Perform.IsEnabled = (!TB_PerformFileName.Text.Equals(String.Empty) && !TB_ResultFileName.Text.Equals(""));
        }

        private void BT_Perform_Click(object sender, RoutedEventArgs e)
        {
            Encoder.Encode(new int[] { 2, 35 }, TB_initialKey.Text, TB_PerformFileName.Text, TB_ResultFileName.Text, 
                TB_Source, TB_GeneratedKey, TB_Result);
        }
    }
}
