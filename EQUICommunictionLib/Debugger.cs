using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Windows.Forms;

namespace EQUICommunictionLib
{
   sealed partial class Debugger
    {
        bool bLogStack = true;
        bool bLogToFile = true;

        public bool LogStack
        {
            get { return bLogStack; }
            set { bLogStack = value; }
        }

        public bool LogToFile
        {
            get { return bLogToFile; }
            set { bLogToFile = value; }
        }

        public void Init()
        {
            Trace.Listeners.Add(new TextWriterTraceListener(@"c:\temp\eqDatabase.log"));
            Trace.AutoFlush = true;
            Trace.Indent();
            Trace.Unindent();
            Trace.Flush();
        }

        public void Exeption(Exception ex)
        {
            if (bLogToFile) 
            {
                Trace.WriteLine("DT: " + System.DateTime.Now + " E: " + ex.Message);
                if (bLogStack)
                {
                    Trace.WriteLine("StackTrace: '{0}'", Environment.StackTrace);
                }
            }
            else
            {
               Debug.WriteLine("DT: " + System.DateTime.Now + " E: " + ex.Message);
                if (bLogStack)
                {
                    Debug.WriteLine("StackTrace: '{0}'", Environment.StackTrace);
                }
            }
        }

       public void Message(string message)
        {
            if (bLogToFile)
            {
                Trace.WriteLine("DT: " + System.DateTime.Now + " M: " + message);
                if (bLogStack)
                {
                    Trace.WriteLine("StackTrace: '{0}'", Environment.StackTrace);
                }
            }
            else
            {
                Debug.WriteLine("DT: " + System.DateTime.Now + " M: " + message);
                if (bLogStack)
                {
                    Debug.WriteLine("StackTrace: '{0}'", Environment.StackTrace);
                }
            }
            MessageBox.Show(message,"Sorry", MessageBoxButtons.OK);
        }
    }
}
