using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Google.GData.Spreadsheets;
using Google.GData.Client;
using Google.GData.Documents;
using System.IO;


namespace Miotec.Vert3d.Faturamento
{
    public class AcessoRemotoDummy : IFaturamentoAcessoRemoto {

        WorksheetFeed wsFeed;
        WorksheetEntry worksheet;
        SpreadsheetsService service;
        Google.GData.Spreadsheets.SpreadsheetQuery query;
        SpreadsheetFeed feed;
        SpreadsheetEntry spreadsheet;
        AtomLink listFeedLink;
        ListFeed listFeed;
        ListQuery listQuery;
        String nomePlanilha;
        String path;

        public AcessoRemotoDummy()
        {
            path = @"C:\Miotec\Vert3d\Config\NomePlanilhaGoogle.ini";
            nomePlanilha = NomeSpreadsheetGoogle();
            service = new SpreadsheetsService("TesteVertFaturamento-v1");
            service.setUserCredentials("controlevert3d@gmail.com", "miotecvert3d");
            query = new Google.GData.Spreadsheets.SpreadsheetQuery();
            feed = service.Query(query);
            spreadsheet = (SpreadsheetEntry)feed.Entries
                   .Single(e => e.Title.Text == nomePlanilha);
            wsFeed = spreadsheet.Worksheets;
            worksheet = (WorksheetEntry)wsFeed.Entries[0];
            listFeedLink = worksheet.Links.FindService(GDataSpreadsheetsNameTable.ListRel, null);
            listQuery = new ListQuery(listFeedLink.HRef.ToString());
            listFeed = service.Query(listQuery);
            NomeSpreadsheetGoogle();
        }
        public void AdicionaNovaLinha(int data, string nome, bool status)
        {
            ListEntry row = new ListEntry();
            row.Elements.Add(new ListEntry.Custom() { LocalName = "data", Value = data + "" });
            row.Elements.Add(new ListEntry.Custom() { LocalName = "nome", Value = nome });
            row.Elements.Add(new ListEntry.Custom() { LocalName = "status", Value = status + "" });
            service.Insert(listFeed, row);
        }

        public string NomeSpreadsheetGoogle()
        {
            using (StreamReader sr = new StreamReader(path))
            {
                String line = sr.ReadToEnd();
                return line;
            }
        }
    }
}
