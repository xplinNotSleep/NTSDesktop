using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;

namespace AG.COM.SDM.Utility
{
    public class PrinterHelper
    {
        private static PrintDocument m_PrintDocument = new PrintDocument();
        /// <summary>   
        /// 获取本机默认打印机名称   
        /// </summary>   
        public static String DefaultPrinter
        {
            get
            {
                string defaultPrinter = m_PrintDocument.PrinterSettings.PrinterName;

                foreach (String fPrinterName in PrinterSettings.InstalledPrinters)
                {
                    if (defaultPrinter == fPrinterName)
                    {
                        return defaultPrinter;
                    }                 
                }
                return PrinterSettings.InstalledPrinters.Count > 0 ? Convert.ToString(PrinterSettings.InstalledPrinters[0]) : "";
            }
        }

        /// <summary>   
        /// 获取本机的打印机列表。列表中的第一项就是默认打印机。   
        /// </summary>   
        public static List<String> GetAllPrinters()
        {
            List<String> fPrinters = new List<string>();
            fPrinters.Add(DefaultPrinter); // 默认打印机始终出现在列表的第一项   
            foreach (String fPrinterName in PrinterSettings.InstalledPrinters)
            {
                if (!fPrinters.Contains(fPrinterName))
                    fPrinters.Add(fPrinterName);
            }
            return fPrinters;
        }  
    }
}
