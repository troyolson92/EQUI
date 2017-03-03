using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Threading;

using iTextSharp.text;
using iTextSharp.text.pdf;

namespace ExcelAddInEquipmentDatabase.Forms
{
    public partial class DocManager : Form
    {
        private List<string[]> Resultbuffer;
        private BackgroundWorker bw;

        public DocManager()
        {
            InitializeComponent();
            //
            bw = new BackgroundWorker();
            Resultbuffer = new List<string[]>();
            //
            lv_result.Columns.Add("Filename", -2, HorizontalAlignment.Left);
            lv_result.Columns.Add("Bookmark", -2, HorizontalAlignment.Left);
            lv_result.Columns.Add("Page", -2, HorizontalAlignment.Left);
            lv_result.Columns.Add("FullFilePath", -2, HorizontalAlignment.Left);
            lv_result.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            //
            lbl_info.Text = "Idle";
            //
            BindingSource bs = new BindingSource();
            bs.DataSource = new List<string> {
                  "Type A"
                    , "Type D and HD"
                    , "Type H"
                    , "Type HDp"
                    , "Type HN"
                    , "Type Hse"
                    , "Type HSeSe"
                    , "Type HRe"
                    , "Type NutRunner"
                    , "Type Se"
                    , "Type T"
            };
            cb_path.DataSource = bs;
            //
        }

        private void btn_find_Click(object sender, EventArgs e)
        {
            btn_find.Enabled = false;
            lv_result.Items.Clear();
            Resultbuffer.Clear();
            //
            List<string> LOGSearchpaths = new List<String>() 
            { 
@"\\gnl9011102\proj\6308-Shr-VCC03100\TechnischeDocs\Robots\ABB\IRC5 - NGAC\Sharepoint_FP_3Doc_17w05d1" 
,@"\\gnlsnm0101.gen.volvocars.net\proj\6308-Shr-VC024800\OBJECTBEHEER GA\Robots\12. SW + Tools\RobotDatabase"
            };
            lbl_info.Text = "Scanning for files";
            Cursor.Current = Cursors.AppStarting;
            List<string> ResultList = ReqSearchDir(LOGSearchpaths, "*.PDF", cb_path.Text);
            Cursor.Current = Cursors.Default;
            lbl_info.Text = string.Format("Found: {0} files ",ResultList.Count());
            //
            if (ResultList.Count() > 25)
            {
                DialogResult result = MessageBox.Show(
                                    string.Format(@"Are you sure? 
                                    Your pdl search for '{0}' returned {1} files
                                    Searching them all could take a long time", cb_path.Text, ResultList.Count())
                                    , "Confirmation", MessageBoxButtons.YesNo);
                if (result == DialogResult.No) { return; } //abort
            }
            //process files (in background)
            /*
            bw.DoWork += (o, args) => bw_processFiles(ResultList);
            bw.RunWorkerCompleted += (o, args) => bw_AddToListview();
            bw.WorkerReportsProgress = true;
            bw.ProgressChanged += bw_ProgressChanged;
            bw.RunWorkerAsync();
            */
            //bw not working DAMM
            Cursor.Current = Cursors.WaitCursor;
            bw_processFiles(ResultList);
            bw_AddToListview();
            Cursor.Current = Cursors.Default;
            //
            btn_find.Enabled = true;
        }

 //background worker calls 
        //process all files (do work)
        private void bw_processFiles(List<string> FileList)
        {
            foreach (string file in FileList)
            {
                Resultbuffer.AddRange(ItextSharpGetbookmarks(file));
            }
        }

        //bw finished
        private void bw_AddToListview()
        {
            foreach (string[] item in Resultbuffer)
            {
                ListViewItem lvitem = new ListViewItem(Path.GetFileName(item[0]));
                lvitem.SubItems.Add(item[1]);
                lvitem.SubItems.Add(item[2]);
                lvitem.SubItems.Add(item[3]);
                lv_result.Items.Add(lvitem);
            }
            lv_result.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
        }


//PDF file stuff 
      //process pdf file 
        private List<string[]> ItextSharpGetbookmarks(string filename)
        {
            //add an item for the "hit" on the file to the listview
            List<string[]> result = new List<string[]>();
            string[] fileresult = { Path.GetFileName(filename), "", "", filename };
            result.Add(fileresult);
            //
            PdfReader pdfReader = new PdfReader(filename);
            IList<Dictionary<string, object>> bookmarks = SimpleBookmark.GetBookmark(pdfReader);
            if (bookmarks != null)
            {
                result.AddRange(ItextSharpRecursive_search(bookmarks, filename));
            }
            return result;
        }

      //Search a PDF file and recursively in bookmarks and return wanted results
      private List<string[]> ItextSharpRecursive_search(IList<Dictionary<string, object>> ilist, string filename)
        {
            List<string[]> result = new List<string[]>();
            string bmTitle = null;
            string bmPage = null;
            foreach (Dictionary<string, object> bk in ilist)
            {
                foreach (KeyValuePair<string, object> kvr in bk)
                {
                    if (kvr.Key == "Kids" || kvr.Key == "kids")
                    {
                        IList<Dictionary<string, object>> child = (IList<Dictionary<string, object>>)kvr.Value;
                        result.AddRange(ItextSharpRecursive_search(child, filename));
                    }
                    else if (kvr.Key == "Title" || kvr.Key == "title")
                    {
                         bmTitle = kvr.Value.ToString();
                    }
                    else if (kvr.Key == "Page" || kvr.Key == "page")
                    {
                         bmPage = Regex.Match(kvr.Value.ToString(), "[0-9]+").Value;
                    }
                }
                //check if we need it
                //Debug.WriteLine("bmTitle: {0}  bmPage {1} filename: {2}", bmTitle, bmPage, Path.GetFileName(filename));
                if (bmTitle.Contains(tb_inIndex.Text))
                {
                    string[] fileresult = { Path.GetFileName(filename), bmTitle, bmPage, filename };
                    result.Add(fileresult);
                }
            }
            return result;
        }

      //open pdf on page 
      private void lv_result_MouseDoubleClick(object sender, MouseEventArgs e)
      {
          foreach (ListViewItem item in lv_result.SelectedItems)
          {
              System.Diagnostics.Process myProcess = new System.Diagnostics.Process();
              myProcess.StartInfo.FileName = "acrord32.exe";
              myProcess.StartInfo.Arguments = string.Format(" /n /A \"page={0}\" \"{1}\"", item.SubItems[2].Text, item.SubItems[3].Text); //works
              try { myProcess.Start(); }
              catch (Exception ex) { Debug.WriteLine("Failed to open pdf: " + ex.Message); }
          }
      }

//search
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

      private void folderBrowserDialog1_HelpRequest(object sender, EventArgs e)
      {

      }
    }
}
