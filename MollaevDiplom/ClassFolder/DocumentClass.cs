using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace MollaevDiplom.ClassFolder
{
    internal class DocumentClass
    {
        public static byte[] ConvertDocumentToByteArray(string path = null)
        {
            if (path == null)
            {
                OpenFileDialog op = new OpenFileDialog();
                op.InitialDirectory = "";
                op.Filter = "All Word docs |;*.docx;";

                if (op.ShowDialog() == true)
                {
                    string fileName = op.FileName;
                    var file = File.ReadAllBytes(fileName);
                    return file;
                }
                return null;
            }
            else
            {
                var file = File.ReadAllBytes(path);
                return file;
            }
        }

        public async static void ConvertByteArrayToDocument(byte[] array)
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.InitialDirectory = "";
            saveDialog.Filter = "DOCX-format|*.docx";

            if (saveDialog.ShowDialog() == true)
            {
                string fileName = saveDialog.FileName;
                await Task.Run(() =>
                {
                    File.WriteAllBytes(fileName, array);
                });
                MBClass.InfoMB("Документ скачан!");
            }
        }
    }
}
