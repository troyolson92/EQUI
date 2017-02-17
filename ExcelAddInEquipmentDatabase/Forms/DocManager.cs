using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using System.Diagnostics;
using System.IO;


namespace ExcelAddInEquipmentDatabase.Forms
{
    public partial class DocManager : Form
    {
        public DocManager()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<string> LOGSearchpaths = new List<String>() { @"\\gnlsnm0101.gen.volvocars.net\6308-APP-NASROBOTBCK0001\robot_ga\VEDOC AAOSR\ABB\IRC5-NGAC\Sharepoint_FP_3Doc_17w05d1" };
            List<string> ResultList = ReqSearchDir(LOGSearchpaths, "*.PDF", tb_InFile.Text);
            listBox1.Items.Clear(); 
          //  string file = @"Z:\robot_ga\VEDOC AAOSR\ABB\IRC5-NGAC\Sharepoint_FP_3Doc_17w05d1\SW_doc\Type H\ABB_FP_Handling_VCC V3.3.pdf";
            foreach (string file in ResultList)
            {
                getbookmarks(file);
            }
  
        }



  void getbookmarks(string fullFilePath)
        {
            using (PdfDocument document = PdfReader.Open(fullFilePath, PdfDocumentOpenMode.Import))
            {
                PdfDictionary outline = document.Internals.Catalog.Elements.GetDictionary("/Outlines");
                PrintBookmark(outline, Path.GetFileName(fullFilePath));
            }
        }

  void PrintBookmark(PdfDictionary bookmark, string sActFile)
        {
            string item = bookmark.Elements.GetString("/Title");   
              if (item.Contains(tb_inIndex.Text))
              {
                  listBox1.Items.Add(sActFile + ": " + item);
              }
           Debug.WriteLine(bookmark.Elements.GetString("/Title"));
            for (PdfDictionary child = bookmark.Elements.GetDictionary("/First"); child != null; child = child.Elements.GetDictionary("/Next"))
            {
                PrintBookmark(child,sActFile);
            }
        }

  //search for files
  static List<string> ReqSearchDir(List<string> als_filepaths, string as_mask, string as_fileNameMask)
  {
      List<string> List = new List<string>();
      try
      {
          foreach (string filepath in als_filepaths)
          {
              Debug.WriteLine("\r Searching: {1} Found: {0:D3}", List.Count, filepath.Substring(Math.Max(0, filepath.Length - 40)));
              var allFiles = Directory.GetFiles(filepath, as_mask, SearchOption.AllDirectories);
              foreach (string f in allFiles)
              {
                  if (f.Contains(as_fileNameMask)) { List.Add(f);}
              }
          }
      }
      catch (Exception ex)
      {
          Debug.WriteLine(ex.Message);
      }
      return List;
  }


    }
}
