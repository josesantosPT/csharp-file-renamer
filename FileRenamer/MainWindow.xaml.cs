using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FileRenamer
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

        private void bt_renamefiles_Click(object sender, RoutedEventArgs e)
        {
            List<string[]> nameslist = new List<string[]>();

            using (StreamReader sr = new StreamReader(Directory.GetCurrentDirectory()+"/list.csv"))
            {
                string currentLine;
                // currentLine will be null when the StreamReader reaches the end of file
                while ((currentLine = sr.ReadLine()) != null)
                {
                    string[] partial = currentLine.Split(';');
                    nameslist.Add(partial);
                }
            }
            
            int count = 0;
            foreach (string local_full_filepath in Directory.GetFiles(Directory.GetCurrentDirectory()))
            {
                string local_file_name = System.IO.Path.GetFileName(local_full_filepath);
                string file_path = local_full_filepath.Replace(local_file_name,"");
                                
                if(nameslist.Select(foo => foo[0]).ToList().Contains(local_file_name))
                {
                    string[] entry = nameslist.Find(foo => foo[0] == local_file_name);

                    if(entry != null)
                    {
                        string new_name = file_path + entry[1];
                        System.IO.File.Move(local_full_filepath,new_name);
                        count++;
                    }
                }
           }

           MessageBox.Show(count.ToString() + " Files were renamed");
        }
    }
}
