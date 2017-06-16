using System.Windows.Controls;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.IO;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using System.Collections.Generic;
using System;


namespace RelatorioMigradoc
{

    public partial class MainWindow : UserControl
    {
        PdfDocumentRenderer renderer;
        Section section;
        Document document;
        Color corBordas;
        Color corFontesPrincipais;
        double tamanhoFonteHeader;

        public MainWindow()
        {
            InitializeComponent();

            corBordas = Colors.DarkCyan;
            corFontesPrincipais = Colors.Blue;

            document = new Document();
            tamanhoFonteHeader = 11;


            this.section = document.AddSection();


            document.DefaultPageSetup.PageFormat = PageFormat.A4;

            GeraHeader();
            GeraAvaliacao();
            GeraMapaRelevo();
            GeraCurvaturas();
            section.AddPageBreak();
            GeraIndicesPosturais();
            GeraIndicesLocais();
            GeraInterpretacaoResultados();


            string ddl = DdlWriter.WriteToString(document);


            renderer = new PdfDocumentRenderer(true, PdfSharp.Pdf.PdfFontEmbedding.Always);
            renderer.Document = document;
            renderer.RenderDocument();


            preview.Ddl = ddl;     

        }

 
        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            renderer.PdfDocument.Save("Teste.pdf");
        }

        public void GeraHeader()
        {
            Table table1 = new Table();
            table1.Borders.Width = 0.75;
            table1.Borders.Color = corBordas;

            Column column = table1.AddColumn(Unit.FromCentimeter(3));

           table1.AddColumn(Unit.FromCentimeter(13));
            

            Row row = table1.AddRow();
            table1.AddRow();
            table1.AddRow();
            table1.AddRow();
            table1.AddRow();

            Paragraph paragraph = new Paragraph();
            paragraph.AddText("Imagem");



            Cell cell = row.Cells[0];
            cell.Add(paragraph);
            cell.Borders.Bottom.Visible = false;

            row = table1.Rows[1];
            cell = row.Cells[0];
            cell.Borders.Bottom.Visible = false;


            row = table1.Rows[0];
            cell = row.Cells[1];

            Font font = new Font("Verdana");
            //PARAGRAFO PACIENTE
            var paraPaciente = new Paragraph();
            paraPaciente.AddText("Paciente:");
            paraPaciente.Format.Font.Color = corFontesPrincipais;
            paraPaciente.Format.Font.ApplyFont(font);
            paraPaciente.Format.Font.Size = tamanhoFonteHeader;
            cell.Add(paraPaciente);
            cell.Shading.Color = Colors.LightCyan;
            cell.Borders.Bottom.Visible = false;

            //PARAGRAFO MEDICO EXAMINADOR
            row = table1.Rows[1];
            cell = row.Cells[1];
            var paraMedico = new Paragraph();
            paraMedico.AddText("Médico Examinador:");
            paraMedico.Format.Font.Color = corFontesPrincipais;
            paraMedico.Format.Font.ApplyFont(font);
            paraMedico.Format.Font.Size = tamanhoFonteHeader;
            cell.Add(paraMedico);
            cell.Shading.Color = Colors.LightCyan;
            cell.Borders.Bottom.Visible = false;

            row = table1.Rows[2];
            cell = row.Cells[1];

            //PARAGRAFO DATA EXAME
            var paraData = new Paragraph();
            paraData.AddText("Exame realizado em ");
            paraData.Format.Font.Color = corFontesPrincipais;
            paraData.Format.Font.Size = tamanhoFonteHeader;
            cell.Add(paraData);
            cell.Shading.Color = Colors.LightCyan;

            cell = row.Cells[0];
            cell.Borders.Bottom.Visible = false;

            row = table1.Rows[3];
            cell = row.Cells[0];
            cell.Borders.Bottom.Visible = false;

            var paraDados = new Paragraph();
            paraDados.AddText("Dados do paciente:");
            paraDados.Format.Font.Color = corFontesPrincipais;
            paraDados.Format.Font.Size = tamanhoFonteHeader;
            cell = row.Cells[1];
            cell.Add(paraDados);


            document.LastSection.Add(table1);

        }

        public void GeraMapaRelevo()
        {
            Table table = new Table();
            table.Borders.Width = 0.75;
            table.Borders.Color = corBordas;
            Column column = table.AddColumn(Unit.FromCentimeter(7));

            //ALTERAR LINHA ABAIXO PARA REMOVER BORDA
            column.Borders.Right.Color = Colors.Black;


            table.AddColumn(Unit.FromCentimeter(1));
            column.Format.Alignment = ParagraphAlignment.Center;

            table.AddColumn(Unit.FromCentimeter(7));
            table.AddColumn(Unit.FromCentimeter(1));

            Row row = table.AddRow();

            Paragraph paragraph1 = new Paragraph();
            paragraph1.AddText("Mapa de Relevo (cm)");

            Paragraph paragraph2 = new Paragraph();
            paragraph2.AddText("Mapa de Curvatura (cm)");
            paragraph2.Format.Alignment = ParagraphAlignment.Center;

            paragraph1.Format.Font.Size = 11;
            paragraph1.Format.Font.Color = corFontesPrincipais;

            paragraph2.Format.Font.Size = 11;
            paragraph2.Format.Font.Color = corFontesPrincipais;

            Cell cell = row.Cells[0];

            //ALTERAR LINHA ABAIXO PARA REMOVER BORDA
            cell.Borders.Bottom.Color = Colors.Black;

            cell.AddParagraph("\n");
            cell.Add(paragraph1);
            cell.AddParagraph("\n");

            cell = row.Cells[1];
            //ALTERAR LINHA ABAIXO PARA REMOVER BORDA
            cell.Borders.Bottom.Color = Colors.Black;


            cell = row.Cells[2];
            //ALTERAR LINHA ABAIXO PARA REMOVER BORDA
            cell.Borders.Bottom.Color = Colors.Black;
            cell.Borders.Right.Color = Colors.Black;

            cell.AddParagraph("\n");
            cell.Add(paragraph2);
            cell.AddParagraph("\n");

            //ALTERAR LINHA ABAIXO PARA REMOVER BORDA
            table.Columns[2].Borders.Right.Color = Colors.Black;

            cell = row.Cells[3];
            cell.Borders.Bottom.Color = Colors.Black;
            row = table.AddRow();

            cell = row.Cells[0];
            cell.AddParagraph("\n");

            MigraDoc.DocumentObjectModel.Shapes.Image imagemCurvatura = cell.AddImage("C:/Users/pc1/Downloads/fringe (7).png");
            imagemCurvatura.LockAspectRatio = false;
            imagemCurvatura.Width = new Unit(45, UnitType.Millimeter);
            imagemCurvatura.Height = new Unit(65, UnitType.Millimeter);
            cell.AddParagraph("\n");


            cell = row.Cells[2];
            cell.AddParagraph("\n");
            MigraDoc.DocumentObjectModel.Shapes.Image imgagemRelevo = cell.AddImage("C:/Users/pc1/Downloads/fringe (7).png");
            imgagemRelevo.LockAspectRatio = false;
            imgagemRelevo.Width = new Unit(45, UnitType.Millimeter);
            imgagemRelevo.Height = new Unit(65, UnitType.Millimeter);
            cell.AddParagraph("\n");

            document.LastSection.Add(table);
        }

        private void GeraAvaliacao()
        {
            Paragraph paragraph1 = section.AddParagraph("\n");
            Paragraph paragraph = section.AddParagraph("AVALIAÇÃO POSTURAL POR ESTEREOMETRIA");
            paragraph.Format.Font.Size = 13;
            paragraph.Format.Alignment = ParagraphAlignment.Center;
            paragraph.Format.Font.Color = corFontesPrincipais;
            Paragraph paragraph2 = section.AddParagraph("\n");
        }

        private void GeraCurvaturas()
        {
            Table table = new Table();
            table.Borders.Width = 0.75;
            table.Borders.Color = corBordas;

            Column column = table.AddColumn(Unit.FromCentimeter(4.666666666666666666666666666666667));
            table.AddColumn(Unit.FromCentimeter(1));
            column.Format.Alignment = ParagraphAlignment.Center;

            table.AddColumn(Unit.FromCentimeter(4.666666666666666666666666666666667));
            table.AddColumn(Unit.FromCentimeter(1));
            table.AddColumn(Unit.FromCentimeter(4.666666666666666666666666666666667));

            Row row = table.AddRow();
            table.AddRow();

            Paragraph paragraph1 = new Paragraph();
            paragraph1.AddText("Projeção Frontal [cm]");

            Paragraph paragraph2 = new Paragraph();
            paragraph2.AddText("Projeção Lateral [cm]");

            Paragraph paragraph3 = new Paragraph();
            paragraph3.AddText("Rotação Axial [º]");

            paragraph1.Format.Font.Size = 11;
            paragraph1.Format.Font.Color = corFontesPrincipais;

            paragraph2.Format.Font.Size = 11;
            paragraph2.Format.Font.Color = corFontesPrincipais;

            paragraph3.Format.Font.Size = 11;
            paragraph3.Format.Font.Color = corFontesPrincipais;


            Cell cell = row.Cells[0];
            cell.Borders.Bottom.Color = Colors.Black;
            cell.Borders.Right.Color = Colors.Black;
            cell.Add(paragraph1);


            cell = row.Cells[1];
            cell.Borders.Bottom.Color = Colors.Black;
            cell.Borders.Right.Color = Colors.Black;
            cell.AddParagraph("\n");


            cell = row.Cells[2];
            cell.Borders.Bottom.Color = Colors.Black;
            cell.Borders.Right.Color = Colors.Black;
            cell.Add(paragraph2);
            cell.AddParagraph("\n");


            cell = row.Cells[3];
            cell.Borders.Bottom.Color = Colors.Black;
            cell.Borders.Right.Color = Colors.Black;


            cell = row.Cells[4];
            cell.Borders.Bottom.Color = Colors.Black;
            cell.Add(paragraph3);
            cell.AddParagraph("\n");

            row = table.Rows[1];
            cell = row.Cells[0];
            cell.Borders.Right.Color = Colors.Black;

            cell = row.Cells[1];
            cell.Borders.Right.Color = Colors.Black;

            cell = row.Cells[2];
            cell.Borders.Right.Color = Colors.Black;

            cell = row.Cells[3];
            cell.Borders.Right.Color = Colors.Black;


           

            document.LastSection.AddParagraph("\n");
            document.LastSection.Add(table);
        }

        private void GeraIndicesPosturais()
        {

            Table table = new Table();
            table.Borders.Width = 0.75;
            table.Borders.Color = corBordas;
            Column column = table.AddColumn(Unit.FromCentimeter(2));
            table.AddColumn(Unit.FromCentimeter(8));
            table.AddColumn(Unit.FromCentimeter(6));
            Row row = table.AddRow();
            table.AddRow();
            table.AddRow();
            table.AddRow();
            table.AddRow();

            
            Cell cell = row.Cells[1];
            Paragraph paraIndicesP = new Paragraph();
            paraIndicesP.AddText("\nÍNDICES POSTURAIS");
            paraIndicesP.Format.Font.Color = corFontesPrincipais;
            paraIndicesP.Format.Font.Size = tamanhoFonteHeader;
            paraIndicesP.Format.Alignment = ParagraphAlignment.Right;
            cell.Borders.Right.Visible = false;
            cell.Borders.Bottom.Visible = false;
            cell.Add(paraIndicesP);


            row = table.Rows[1];
            cell = row.Cells[1];
            cell.Borders.Bottom.Visible = false;
            cell.Borders.Right.Visible = false;
            Paragraph paraComprimento = new Paragraph();
            paraComprimento.Format.Alignment = ParagraphAlignment.Left;
            paraComprimento.AddText("Comprimento do Tronco");
            cell.Add(paraComprimento);

            row = table.Rows[2];
            cell = row.Cells[1];
            cell.Borders.Bottom.Visible = false;
            cell.Borders.Right.Visible = false;
            Paragraph paraLateralTronco = new Paragraph();
            paraLateralTronco.Format.Alignment = ParagraphAlignment.Left ;
            paraLateralTronco.AddText("Desequilibrio lateral do tronco");
            cell.Add(paraLateralTronco);


            row = table.Rows[3];
            cell = row.Cells[1];
            cell.Borders.Bottom.Visible = false;
            cell.Borders.Right.Visible = false;
            Paragraph paraInclinacao = new Paragraph();
            paraInclinacao.Format.Alignment = ParagraphAlignment.Left;
            paraInclinacao.AddText("Inclinação lateral da pelve");
            cell.Add(paraInclinacao);


            row = table.Rows[4];
            cell = row.Cells[1];
            cell.Borders.Right.Visible = false;
            Paragraph paraPosterior = new Paragraph();
            paraPosterior.AddText("Desequilibrio ântero-posterior do tronco");
            paraPosterior.Format.Alignment = ParagraphAlignment.Left;
            cell.Add(paraPosterior);      

            document.LastSection.Add(table);
            document.LastSection.AddParagraph("\n");

            row = table.Rows[0];
            cell = row.Cells[2];
            cell.Borders.Bottom.Visible = false;

            row = table.Rows[1];
            cell = row.Cells[2];
            cell.Borders.Bottom.Visible = false;

            row = table.Rows[2];
            cell = row.Cells[2];
            cell.Borders.Bottom.Visible = false;

            row = table.Rows[3];
            cell = row.Cells[2];
            cell.Borders.Bottom.Visible = false;

            row = table.Rows[0];
            cell = row.Cells[0];
            cell.Borders.Bottom.Visible = false;
            cell.Borders.Right.Visible = false;

            row = table.Rows[1];
            cell = row.Cells[0];
            cell.Borders.Bottom.Visible = false;
            cell.Borders.Right.Visible = false;

            row = table.Rows[2];
            cell = row.Cells[0];
            cell.Borders.Bottom.Visible = false;
            cell.Borders.Right.Visible = false;

            row = table.Rows[3];
            cell = row.Cells[0];
            cell.Borders.Bottom.Visible = false;
            cell.Borders.Right.Visible = false;

            row = table.Rows[4];
            cell = row.Cells[0];
            cell.Borders.Right.Visible = false;


            
        }

        private void GeraIndicesLocais()
        {
            Table table = new Table();
            table.Borders.Width = 0.75;
            table.Borders.Color = corBordas;
            Column column = table.AddColumn(Unit.FromCentimeter(1.5));
            table.AddColumn(Unit.FromCentimeter(2.9));
            table.AddColumn(Unit.FromCentimeter(2.9));
            table.AddColumn(Unit.FromCentimeter(2.9));
            table.AddColumn(Unit.FromCentimeter(2.9));
            table.AddColumn(Unit.FromCentimeter(2.9));
            Cell cell = new Cell();

            int tamanhoFonte = 9;

            List<string> listaDeNiveis = new List<string>();

            //FOR QUE PRENCHE A LISTA DE NIVEIS.

            for (int i = 0; i < 19; i++)
            {
                if (i == 0)
                {
                    listaDeNiveis.Add("C7");
                }
                else if(i > 0 && i < 13)
                {
                    listaDeNiveis.Add("T"+i);
                }
                else if (i > 12 && i < 17)
                {
                    listaDeNiveis.Add("L"+(i-11));
                }
                else if(i > 17)
                {
                    listaDeNiveis.Add("S1");
                }
            }

            Row row = table.AddRow();
            //FOR QUE GERA E PRENCHE A TABELA
            for (int i = 0; i < 20; i++)
            {
                if (i > 0 && i < listaDeNiveis.Count)
                {
                    table.AddRow();
                    row = table.Rows[i];
                    cell = row.Cells[0];
                    string s = listaDeNiveis[i];
                    cell.AddParagraph(s);
                }
            }

            Random random = new Random();
            int j = 1;
            for (int i = 0; i < 20; i++)
            {
                  if (i > 0 && i < listaDeNiveis.Count)
                  {                    
                      row = table.Rows[i];
                      cell = row.Cells[j];
                      if(j <=2)
                      {
                          Paragraph paragraph = new Paragraph();
                          paragraph.AddText(String.Format("{0:00.0}", random.NextDouble()*10) + " cm");
                          paragraph.Format.Alignment = ParagraphAlignment.Center;
                          cell.Add(paragraph);
                      }
                      else
                      {
                          Paragraph paragraph = new Paragraph();
                          paragraph.AddText(String.Format("{0:00.0}", random.NextDouble()*10) + " º");
                          paragraph.Format.Alignment = ParagraphAlignment.Center;
                          cell.Add(paragraph);
                      }
                          
                     
                  }
                  if (i == listaDeNiveis.Count && j < 5)
                  {
                      i = 0;
                      j++;
                  }
            }
            
            row = table.Rows[0];
            cell = row.Cells[0];
            Paragraph paraNiveis = new Paragraph();
            paraNiveis.AddText("Níveis");
            paraNiveis.Format.Font.Size = tamanhoFonte;
            cell.Add(paraNiveis);

            cell = row.Cells[1];
            Paragraph paraPos = new Paragraph();
            paraPos.AddText("Posição Vertical");
            paraPos.Format.Font.Size = tamanhoFonte;
            cell.Add(paraPos);

            cell = row.Cells[2];
            Paragraph paraDesloc = new Paragraph();
            paraDesloc.AddText("Deslocamento Horizontal");
            paraDesloc.Format.Font.Size = tamanhoFonte;
            cell.Add(paraDesloc);

            cell = row.Cells[3];
            Paragraph paraAntero = new Paragraph();
            paraAntero.AddText("Inclinação Ântero Posterior");
            paraAntero.Format.Font.Size = tamanhoFonte;
            cell.Add(paraAntero);

            cell = row.Cells[4];
            Paragraph paraLateral = new Paragraph();
            paraLateral.AddText("Inclinação Lateral");
            paraLateral.Format.Font.Size = tamanhoFonte;
            cell.Add(paraLateral);

            cell = row.Cells[5];
            Paragraph paraRotacao = new Paragraph();
            paraRotacao.AddText("Rotação Axial");
            paraRotacao.Format.Font.Size = tamanhoFonte;
            cell.Add(paraRotacao);


            document.LastSection.Add(table);
            document.LastSection.AddParagraph("\n");
         }

        private void GeraInterpretacaoResultados()
        {
            Table table = new Table();
            table.Borders.Width = 0.75;
            table.Borders.Color = corBordas;
            Column column = table.AddColumn(Unit.FromCentimeter(4));
            table.AddColumn(Unit.FromCentimeter(8));
            table.AddColumn(Unit.FromCentimeter(4));
            Row row = table.AddRow();
            table.AddRow();

            row = table.Rows[0];
            Cell cell = row.Cells[1];

            Paragraph paragraph = new Paragraph();
            paragraph.AddText("INTERPRETAÇÃO DOS RESULTADOS:");
            paragraph.Format.Alignment = ParagraphAlignment.Center;
            cell.Add(paragraph);
            row.Format.SpaceAfter = MigraDoc.DocumentObjectModel.Unit.FromCentimeter(8);

            cell.Borders.Right.Visible = false;

            cell = row.Cells[0];
            cell.Borders.Right.Visible = false;
            cell.Borders.Bottom.Visible = false;

            row = table.Rows[1];
            cell = row.Cells[0];
            cell.Borders.Right.Visible = false;

            cell = row.Cells[1];
            cell.Borders.Right.Visible = false;
            

            document.LastSection.Add(table);
        }

    }
}