using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using MigraDoc.DocumentObjectModel;
using System.Windows;

namespace ReportView
{
    static class ReportStyles
    {
        public static void LoadStyles(Document document)
        {
            MigraDoc.DocumentObjectModel.Style style;

            //Normal
            style = document.Styles["Normal"];
            style.Font.Name = "Segoe UI";

            //Heading1
            style = document.Styles["Heading1"];
            style.Font.Size = 14;
            style.Font.Bold = true;
            style.Font.Color = MagicColor("cAir");
            style.ParagraphFormat.PageBreakBefore = true;
            style.ParagraphFormat.SpaceAfter = "20mm";
            style.ParagraphFormat.FirstLineIndent = "10mm";
            style.ParagraphFormat.KeepWithNext = true;

            //Heading2
            style = document.Styles["Heading2"];
            style.Font.Size = 12;
            style.Font.Bold = true;
            style.Font.Color = MagicColor("cEMG");
            style.ParagraphFormat.PageBreakBefore = false;
            style.ParagraphFormat.SpaceBefore = "6mm";
            style.ParagraphFormat.SpaceAfter = "6mm";
            style.ParagraphFormat.LeftIndent = 20;
            style.ParagraphFormat.KeepWithNext = true;

            //Footer
            style = document.Styles["Footer"];
            style.Font.Name = "Segoe UI";
            style.ParagraphFormat.Alignment = ParagraphAlignment.Right;
        }

        private static MigraDoc.DocumentObjectModel.Color MagicColor(string p)
        {
            System.Windows.Media.Color cor = (System.Windows.Media.Color)Application.Current.FindResource(p);
            return new MigraDoc.DocumentObjectModel.Color(cor.A, cor.R, cor.G, cor.B);
        }
    }
}
