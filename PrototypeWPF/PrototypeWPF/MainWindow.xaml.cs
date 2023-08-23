using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Dynamic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
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

        private ObservableCollection<Item> ReadExcelFile(string filePath)
        {
            var data = new ObservableCollection<Item>();

            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                var worksheet = package.Workbook.Worksheets[0];
                for (int row = 2; row <= worksheet.Dimension.Rows; row++)
                {
                    data.Add(new Item
                    {
                        AuftragsNr01 = Convert.ToInt32(worksheet.Cells[row, 1].Value),
                        AuftragsNr02 = Convert.ToInt32(worksheet.Cells[row, 2].Value),
                        ArtikelNrSPNr = Convert.ToInt64(worksheet.Cells[row, 3].Value),
                        Kunde = worksheet.Cells[row, 4].Value.ToString(),
                        Artikelbezeichnung = worksheet.Cells[row, 5].Value.ToString(),
                        Zeichnungsnummer = worksheet.Cells[row, 6].Value.ToString(),
                        Arbeitsauftrag = worksheet.Cells[row, 7].Value.ToString()
                    });
                }
            }

            return data;
        }

        private void LoadDataIntoDataGrid(string filePath)
        {
            Items.Clear(); // Clear any existing data

            var lines = File.ReadAllLines(filePath);
            if (lines.Length < 2)
            {
                return; // No data or only header row
            }

            var columnTitles = lines[0].Split(',');

            // Print column titles for debugging
            Debug.WriteLine("Column Titles:");
            foreach (var title in columnTitles)
            {
                Debug.WriteLine(title);
            }
            for (int i = 1; i < lines.Length; i++)
            {
                var values = lines[i].Split(',');
                var newItem = new Item();

                for (int j = 0; j < columnTitles.Length; j++)
                {
                    var property = typeof(Item).GetProperty(columnTitles[j]);
                    if (property != null && values.Length > j)
                    {
                        var value = values[j];
                        property.SetValue(newItem, value);
                    }
                }

                Items.Add(newItem);
            }
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
