using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Google.GData.Spreadsheets;
using Google.GData.Client;
using Google.GData.Documents;

namespace Miotec.Vert3d.Faturamento
{
    class AcessoRemotoGoogle : IFaturamentoAcessoRemoto {

        WorksheetFeed wsFeed;
        WorksheetEntry worksheet;
        SpreadsheetsService service;
        Google.GData.Spreadsheets.SpreadsheetQuery query;
        SpreadsheetFeed feed;
        SpreadsheetEntry spreadsheet;
        AtomLink listFeedLink;
        ListFeed listFeed;
        ListQuery listQuery;
        public AcessoRemotoGoogle()
        {
            service = new SpreadsheetsService("TesteVertFaturamento-v1");
            service.setUserCredentials("controlevert3d@gmail.com", "miotecvert3d");
            query = new Google.GData.Spreadsheets.SpreadsheetQuery();
            feed = service.Query(query);
            spreadsheet = (SpreadsheetEntry)feed.Entries
                   .Single(e => e.Title.Text == "PlanilhaTesteFaturamento");
            wsFeed = spreadsheet.Worksheets;
            worksheet = (WorksheetEntry)wsFeed.Entries[0];
            listFeedLink = worksheet.Links.FindService(GDataSpreadsheetsNameTable.ListRel, null);
            listQuery = new ListQuery(listFeedLink.HRef.ToString());
            listFeed = service.Query(listQuery);          
        }

        public void AdicionaNovaLinha(int data, String nome, bool status)
        {
            ListEntry row = new ListEntry();
            row.Elements.Add(new ListEntry.Custom() { LocalName = "data", Value = data+"" });
            row.Elements.Add(new ListEntry.Custom() { LocalName = "nome", Value = nome });
            row.Elements.Add(new ListEntry.Custom() { LocalName = "status", Value = status+"" });

            service.Insert(listFeed, row);
        }

    }
}
