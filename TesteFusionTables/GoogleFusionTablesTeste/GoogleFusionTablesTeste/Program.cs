using System;
using System.Linq;
using Google.GData.Client;
using Google.GData.Spreadsheets;
using System.Security.Policy;
using System.Net;
using System.Text;
using System.IO;


namespace GoogleFusionTablesTeste
{
    class Program
    {
        static void Main(string[] args)
        {
            SpreadsheetControl spreadsheetControl = new SpreadsheetControl();
            spreadsheetControl.addWorksheet("Worksheet teste2");
            spreadsheetControl.addRowToSpreadsheet("Fulano","85");
            spreadsheetControl.addSpreadsheet();
        }
    }
}
