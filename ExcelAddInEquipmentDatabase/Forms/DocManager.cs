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
            lv_result.Columns.Add("Index", -2, HorizontalAlignment.Left);
            lv_result.Columns.Add("Filename", -2, HorizontalAlignment.Left);
            lv_result.Columns.Add("FullFilePath", -2, HorizontalAlignment.Left);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<string> LOGSearchpaths = new List<String>() { @"\\gnlsnm0101.gen.volvocars.net\6308-APP-NASROBOTBCK0001\robot_ga\VEDOC AAOSR\ABB\IRC5-NGAC\Sharepoint_FP_3Doc_17w05d1" };
            List<string> ResultList = ReqSearchDir(LOGSearchpaths, "*.PDF", tb_InFile.Text);
            lv_result.Items.Clear();

            if (ResultList.Count() > 25)
            {
                DialogResult result = MessageBox.Show(
                                    string.Format(@"Are you sure? 
                                    Your pdl search for '{0}' returned {1} files
                                    Searching them all could take a long time",tb_InFile.Text,ResultList.Count())
                                    , "Confirmation", MessageBoxButtons.YesNo);
                if (result == DialogResult.No){ return; } //abort
            }
            
            foreach (string file in ResultList)
            {
                try
                {
                    getbookmarks(file);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(file + "  Error: " + ex.Message);
                }
            }
  
        }



  void getbookmarks(string fullFilePath)
        {
            using (PdfDocument document = PdfReader.Open(fullFilePath, PdfDocumentOpenMode.Import))
            {
                PdfDictionary outline = document.Internals.Catalog.Elements.GetDictionary("/Outlines");
                if (outline != null)
                {
                    PrintBookmark(outline, fullFilePath);
                }
                else
                {
                    Debug.WriteLine("Does not have index :" + fullFilePath);
                }
            }
        }

  void PrintBookmark(PdfDictionary bookmark, string sFullFilepath)
        {
            string sBookmark = bookmark.Elements.GetString("/Title");
            if (sBookmark.Contains(tb_inIndex.Text))
              {
                  ListViewItem lvitem = new ListViewItem(sBookmark);
                  lvitem.SubItems.Add(Path.GetFileName(sFullFilepath));
                  lvitem.SubItems.Add(sFullFilepath);
                  lv_result.Items.Add(lvitem);
              }
           //Debug.WriteLine(bookmark.Elements.GetString("/Title"));
            for (PdfDictionary child = bookmark.Elements.GetDictionary("/First"); child != null; child = child.Elements.GetDictionary("/Next"))
            {
                PrintBookmark(child, sFullFilepath);
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
              var allFiles = Directory.GetFiles(filepath, as_mask, SearchOption.AllDirectories);
              foreach (string f in allFiles)
              {
                  if (Path.GetFileName(f).Contains(as_fileNameMask)) { List.Add(f);}
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
