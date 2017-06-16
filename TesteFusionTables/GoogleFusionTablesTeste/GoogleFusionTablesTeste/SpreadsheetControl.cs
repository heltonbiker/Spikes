using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Google.GData.Spreadsheets;
using Google.GData.Client;
using Google.GData.Documents;

namespace GoogleFusionTablesTeste
{
    public class SpreadsheetControl
    {
        WorksheetFeed wsFeed;
        WorksheetEntry worksheet;
        SpreadsheetsService service;
        Google.GData.Spreadsheets.SpreadsheetQuery query;
        SpreadsheetFeed feed;
        SpreadsheetEntry spreadsheet;
        AtomLink listFeedLink;
        ListFeed listFeed;
        ListQuery listQuery;


        public SpreadsheetControl()
        {
            service = new SpreadsheetsService("GoogleFusionTablesTeste-v1");
            service.setUserCredentials("controlevert3d@gmail.com", "miotecvert3d");
            query = new Google.GData.Spreadsheets.SpreadsheetQuery();
            feed = service.Query(query);
            spreadsheet = (SpreadsheetEntry)feed.Entries
                   .Single(e => e.Title.Text == "PlanilhaMiograph");
            wsFeed = spreadsheet.Worksheets;
            worksheet = (WorksheetEntry)wsFeed.Entries[0];
            listFeedLink = worksheet.Links.FindService(GDataSpreadsheetsNameTable.ListRel, null);
            listQuery = new ListQuery(listFeedLink.HRef.ToString());
            listFeed = service.Query(listQuery);            
        }

        // TODO: APRENDER A CRIAR SPREADSHEET COM O GOOGLE DRIVE API!!!

        public void addWorksheet(String nomeWorksheet)
        {

            if (feed.Entries.Count > 0)
            {
                // TODO se não tiver planilha nenhuma
            }
            worksheet.Title.Text = nomeWorksheet;
            worksheet.Cols = 10;
            worksheet.Rows = 20;
            service.Insert(wsFeed, worksheet);
        }

        public void addRowToSpreadsheet(String nome, String peso)
        {
            ListEntry row = new ListEntry();
            row.Elements.Add(new ListEntry.Custom() { LocalName = "nome", Value = nome });
            row.Elements.Add(new ListEntry.Custom() { LocalName = "peso", Value = peso });

            service.Insert(listFeed, row);
        }

        public void addSpreadsheet()
        {
           DocumentsService docService = new DocumentsService("GoogleFusionTablesTeste-v1");
           DocumentEntry entry = new DocumentEntry();
           docService.setUserCredentials("controlevert3d@gmail.com", "miotecvert3d");
           entry.Title.Text = "Adicionando novo Spreadsheet";
           entry.Categories.Add(DocumentEntry.SPREADSHEET_CATEGORY);
           DocumentEntry newEntry = service.Insert(DocumentsListQuery.documentsBaseUri, entry);
        }
    }
}