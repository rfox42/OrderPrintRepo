using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceDigitizer
{
    class Stub
    {
        /// <summary>
        /// invoice number of the stub
        /// </summary>
        public int invoiceNum;

        /// <summary>
        /// file path of either the stubs jpg or pdf
        /// </summary>
        public string file;

        /// <summary>
        /// file path where stub should be saved to or moved to
        /// </summary>
        public string newFile;

        /// <summary>
        /// file path of duplicate pdf
        /// </summary>
        public string reviewDupe;

        /// <summary>
        /// file path of compressed jpg
        /// </summary>
        public string compressedFile;

        /// <summary>
        /// initialize variables
        /// </summary>
        public Stub()
        {
            invoiceNum = 0;
            file = null;
            newFile = null;
            reviewDupe = null;
            compressedFile = null;
        }

        /// <summary>
        /// delete original and compressed image files
        /// </summary>
        internal void deleteFile()
        {
            if (File.Exists(file))
            {
                File.Delete(file);
                if (file.EndsWith("_A.jpg") && File.Exists(file.Replace("_A", "_B")))
                {
                    File.Delete(file.Replace("_A", "_B"));
                }
            }

            if(compressedFile != null)
            {
                if (File.Exists(compressedFile))
                {
                    File.Delete(compressedFile);
                    if (compressedFile.EndsWith("_A.jpg") && File.Exists(compressedFile.Replace("_A", "_B")))
                    {
                        File.Delete(compressedFile.Replace("_A", "_B"));
                    }
                }
            }
        }
    }
}
