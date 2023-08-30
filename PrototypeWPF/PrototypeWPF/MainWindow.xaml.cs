using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using OfficeOpenXml;

namespace PrototypeWPF
{
    public partial class MainWindow : Window
    {
        public class Item
        {
            public int AuftragsNr01 { get; set; }
            public int AuftragsNr02 { get; set; }
            public long ArtikelNrSPNr { get; set; }
            public string Charge { get; set; }
            public string Kunde { get; set; }
            public string Artikelbezeichnung { get; set; }
            public string Zeichnungsnummer { get; set; }
            public string Arbeitsauftrag { get; set; }
            public int AnzahlTeile { get; set; }
            public bool MessBericht { get; set; }
            public bool MFU { get; set; }
            public bool PFU { get; set; }
            public bool MSA { get; set; }
            public bool Prio { get; set; }
            public string EintragsDatum { get; set; }
        }


        public ObservableCollection<Item> Items { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            Items = new ObservableCollection<Item>
            {
                new Item { AuftragsNr01 = 023, AuftragsNr02 = -23, ArtikelNrSPNr = 20051880000, Kunde = "Dormakaba", Artikelbezeichnung = "Housing Base STD FST controllern", Zeichnungsnummer = "1751-130-1140", Arbeitsauftrag = "Messpreogramm KW25 EMPB" },
                new Item { AuftragsNr01 = 024, AuftragsNr02 = -23, ArtikelNrSPNr = 20051890000, Kunde = "Dormakaba", Artikelbezeichnung = "Housing Base STD FST controllern", Zeichnungsnummer = "1751-140-1140", Arbeitsauftrag = "Messpreogramm KW25 EMPB" },
                // Add more items as needed
            };
            DataContext = this;
        }
        private ObservableCollection<Item> ReadCsvFile(string filePath)
        {
            var data = new ObservableCollection<Item>();

            var lines = File.ReadAllLines(filePath);
            if (lines.Length < 2)
            {
                return data; // No data or only header row
            }

            var columnTitles = lines[0].Split(',');

            for (int i = 1; i < lines.Length; i++)
            {
                var values = lines[i].Split(',');
                var newItem = new Item();

                for (int j = 0; j < columnTitles.Length; j++)
                {
                    var value = values[j];

                    switch (j)
                    {
                        case 0:
                            newItem.AuftragsNr01 = int.TryParse(value, out int auftragsNr01) ? auftragsNr01 : 0;
                            break;
                        case 1:
                            newItem.AuftragsNr02 = int.TryParse(value, out int auftragsNr02) ? auftragsNr02 : 0;
                            break;
                        case 2:
                            newItem.ArtikelNrSPNr = long.TryParse(value, out long artikelNrSPNr) ? artikelNrSPNr : 0;
                            break;
                        case 3:
                            newItem.Charge = value;
                            break;
                        case 4:
                            newItem.Kunde = value;
                            break;
                        case 5:
                            newItem.Artikelbezeichnung = value;
                            break;
                        case 6:
                            newItem.Zeichnungsnummer = value;
                            break;
                        case 7:
                            newItem.Arbeitsauftrag = value;
                            break;
                        case 8:
                            newItem.AnzahlTeile = int.TryParse(value, out int anzahlTeile) ? anzahlTeile : 0;
                            break;
                        case 9:
                            newItem.MessBericht = value == "x";
                            break;
                        case 10:
                            newItem.MFU = value == "x";
                            break;
                        case 11:
                            newItem.PFU = value == "x";
                            break;
                        case 12:
                            newItem.MSA = value == "x";
                            break;
                        case 13:
                            newItem.Prio = value == "x";
                            break;
                        case 14:
                            newItem.EintragsDatum = value;
                            break;
                    }
                }

                data.Add(newItem);
            }

            return data;
        }




        private void LoadDataIntoDataGrid(string filePath)
        {
            Items.Clear(); // Clear any existing data
            Items = ReadCsvFile(filePath); // Load new data

            // Set the ItemsSource of the DataGrid
            MainData.ItemsSource = Items;
        }






        private void WriteExcelFile(string filePath)
        {
            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                var worksheet = package.Workbook.Worksheets[0];
                int rowIndex = 2;

                foreach (var item in Items)
                {
                    worksheet.Cells[rowIndex, 1].Value = item.AuftragsNr01;
                    worksheet.Cells[rowIndex, 2].Value = item.AuftragsNr02;
                    worksheet.Cells[rowIndex, 3].Value = item.ArtikelNrSPNr;
                    worksheet.Cells[rowIndex, 4].Value = item.Charge;
                    worksheet.Cells[rowIndex, 5].Value = item.Kunde;
                    worksheet.Cells[rowIndex, 6].Value = item.Artikelbezeichnung;
                    worksheet.Cells[rowIndex, 7].Value = item.Zeichnungsnummer;
                    worksheet.Cells[rowIndex, 8].Value = item.Arbeitsauftrag;
                    worksheet.Cells[rowIndex, 9].Value = item.AnzahlTeile;
                    worksheet.Cells[rowIndex, 10].Value = item.MessBericht;
                    worksheet.Cells[rowIndex, 11].Value = item.MFU;
                    worksheet.Cells[rowIndex, 12].Value = item.PFU;
                    worksheet.Cells[rowIndex, 13].Value = item.MSA;
                    worksheet.Cells[rowIndex, 14].Value = item.Prio;
                    worksheet.Cells[rowIndex, 15].Value = item.EintragsDatum;

                    rowIndex++;
                }

                package.Save();
            }
        }

        private void DataGrid_RowEditEnding(object sender, System.Windows.Controls.DataGridRowEditEndingEventArgs e)
        {
            if (e.EditAction == System.Windows.Controls.DataGridEditAction.Commit)
            {
                var editedItem = e.Row.DataContext as Item;
                // Update the item in the collection

                WriteExcelFile("C:\\Users\\m.rojc\\Desktop\\Auftragsliste Messtechnik TESTING.csv");
            }
        }

        private void Button_LoadExcel_Click(object sender, RoutedEventArgs e)
        {
            ReadCsvFile("C:\\Users\\m.rojc\\Desktop\\Auftragsliste Messtechnik TESTING.csv");
            LoadDataIntoDataGrid("C:\\Users\\m.rojc\\Desktop\\Auftragsliste Messtechnik TESTING.csv");
        }


        private void Button_SaveToExcel_Click(object sender, RoutedEventArgs e)
        {
            WriteExcelFile("C:\\Users\\m.rojc\\Desktop\\Auftragsliste Messtechnik TESTING.csv");
        }

        private void Button_Tab1_Click(object sender, RoutedEventArgs e)
        {
            tabControl.SelectedIndex = 0; // Show Tab 1 content
        }

        private void Button_Tab2_Click(object sender, RoutedEventArgs e)
        {
            tabControl.SelectedIndex = 1; // Show Tab 2 content
        }

        private void Button_Tab3_Click(object sender, RoutedEventArgs e)
        {
            tabControl.SelectedIndex = 2; // Show Tab 3 content
        }
    }
}
