using Microsoft.Win32;
using System.Data;
using System.IO;
using System.Windows.Controls;
using System.Windows;
using WPFtesting;

private void Window_Loaded(object sender, RoutedEventArgs e)
{
    OpenFileDialog ofd = new OpenFileDialog();
    if (ofd.ShowDialog() == true)
    {
        string[] DataArray;

        DataTable dt = new DataTable();
        dt.Columns.Add("ID", typeof(int));
        dt.Columns.Add("Number", typeof(int));
        dt.Columns.Add("Kunde", typeof(string));
        dt.Columns.Add("Artikel", typeof(string));

        using (StreamReader sr = new StreamReader(ofd.FileName))
        {
            while (!sr.EndOfStream)
            {
                DataArray = sr.ReadLine().Split(new char[] { ',' });

                if (DataArray.Length >= 4)
                {
                    int id;
                    int number;
                    if (int.TryParse(DataArray[0], out id) && int.TryParse(DataArray[1], out number))
                    {
                        dt.Rows.Add(id, number, DataArray[2], DataArray[3]);
                    }
                    else
                    {
                        // Handle invalid data or conversion errors
                    }
                }
            }
        }

        MainDataSource DataInfo = new MainDataSource();
        DataInfo.ID = 0; // Set some default values
        DataInfo.Number = 0;
        DataInfo.Kunde = "";
        DataInfo.Artikel = "";

        dtGridView.DataContext = DataInfo;
        dtGridView.ItemsSource = dt.DefaultView;
    }
}
