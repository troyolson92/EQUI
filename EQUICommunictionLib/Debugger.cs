using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Windows.Forms;

namespace EQUICommunictionLib
{
   public partial class myDebugger
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
            Init(@"c:\temp\eqDatabase.log");
        }

       public void Init(string logfile)
        {
            Trace.Listeners.Add(new TextWriterTraceListener(logfile));
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

       public void Log(string message)
       {
           if (bLogToFile)
           {
               Trace.WriteLine("DT: " + System.DateTime.Now + " M: " + message);
           }
           else
           {
               Debug.WriteLine("DT: " + System.DateTime.Now + " M: " + message);
           }

       }
    }
}
