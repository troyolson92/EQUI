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
        public DocManager()
        {
            InitializeComponent();
            lv_result.Columns.Add("Filename", -2, HorizontalAlignment.Left);
            lv_result.Columns.Add("Bookmark", -2, HorizontalAlignment.Left);
            lv_result.Columns.Add("Page", -2, HorizontalAlignment.Left);
            lv_result.Columns.Add("FullFilePath", -2, HorizontalAlignment.Left);
            lv_result.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            lbl_info.Text = "Idle";
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
        }

        private void btn_find_Click(object sender, EventArgs e)
        {
            btn_find.Enabled = false;
            lv_result.Items.Clear();
            List<string> LOGSearchpaths = new List<String>() { @"\\gnl9011102\proj\6308-Shr-VCC03100\TechnischeDocs\Robots\ABB\IRC5 - NGAC\Sharepoint_FP_3Doc_17w05d1" };
            lbl_info.Text = "Scanning for files";
            Cursor.Current = Cursors.AppStarting;
            List<string> ResultList = ReqSearchDir(LOGSearchpaths, "*.PDF", cb_path.Text);
            Cursor.Current = Cursors.Default;
            btn_find.Enabled = true;
            lbl_info.Text = string.Format("Found: {0} files ",ResultList.Count());
            if (ResultList.Count() > 25)
            {
                DialogResult result = MessageBox.Show(
                                    string.Format(@"Are you sure? 
                                    Your pdl search for '{0}' returned {1} files
                                    Searching them all could take a long time", cb_path.Text, ResultList.Count())
                                    , "Confirmation", MessageBoxButtons.YesNo);
                if (result == DialogResult.No) { return; } //abort
            }
            //process files in other task.
            //Task.Factory.StartNew(() => processFiles(ResultList)); //crosstrheading issue 
            btn_find.Enabled = false;
            processFiles(ResultList);
            lbl_info.Text = "Done";
            lv_result.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            btn_find.Enabled = true;
        }

        void processFiles(List<string> FileList)
        {
            int iProgress = 0;
            foreach (string file in FileList)
            {
                //add an item for the "hit" on the file to the listview
                ListViewItem lvitem = new ListViewItem(Path.GetFileName(file));
                lvitem.SubItems.Add("");  // no page bevause not a bookmark
                lvitem.SubItems.Add(""); // add empty because not a bookmark
                lvitem.SubItems.Add(file);
                lv_result.Items.Add(lvitem);

                iProgress += 1;
                Debug.WriteLine(string.Format("Processing files: {0}/{1} ", iProgress, FileList.Count()));
                ItextSharpGetbookmarks(file);
            }
        }

        void ItextSharpGetbookmarks(string filename)
        {
            PdfReader pdfReader = new PdfReader(filename);
            IList<Dictionary<string, object>> bookmarks = SimpleBookmark.GetBookmark(pdfReader);
            if (bookmarks == null) {return; } //some files have no bookmarks
            ItextSharpRecursive_search(bookmarks,filename);
        }

        public void ItextSharpRecursive_search(IList<Dictionary<string, object>> ilist,string filename)
        {
            string bmTitle = null;
            string bmPage = null;

            foreach (Dictionary<string, object> bk in ilist)
            {
                foreach (KeyValuePair<string, object> kvr in bk)
                {
                    if (kvr.Key == "Kids" || kvr.Key == "kids")
                    {
                        IList<Dictionary<string, object>> child = (IList<Dictionary<string, object>>)kvr.Value;
                        ItextSharpRecursive_search(child, filename);
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
                    ListViewItem lvitem = new ListViewItem(Path.GetFileName(filename));
                    lvitem.SubItems.Add(bmTitle);
                    lvitem.SubItems.Add(bmPage);
                    lvitem.SubItems.Add(filename);
                    lv_result.Items.Add(lvitem);
                }
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

  private void lv_result_MouseDoubleClick(object sender, MouseEventArgs e)
  {
      foreach (ListViewItem item in lv_result.SelectedItems)
      {
           System.Diagnostics.Process myProcess = new System.Diagnostics.Process();
           myProcess.StartInfo.FileName = "acrord32.exe";
           myProcess.StartInfo.Arguments = string.Format(" /n /A \"page={0}\" \"{1}\"", item.SubItems[2].Text, item.SubItems[3].Text); //works
           myProcess.Start();
      }
  }


    }
}
