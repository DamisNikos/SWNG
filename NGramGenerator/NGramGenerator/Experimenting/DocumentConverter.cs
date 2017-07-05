using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NGramGenerator.Experimenting.Model;
using System.IO;

namespace NGramGenerator.Experimenting
{
    class DocumentConverter
    {
        //Converting from any file to byte array for trasportation and storage use maybe
        public Document FileToByteArray(string fileName)
        {
            byte[] fileContent = null;

            using (FileStream fs = new System.IO.FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                using (BinaryReader binaryReader = new BinaryReader(fs))
                {

                    long byteLength = new FileInfo(fileName).Length;
                    fileContent = binaryReader.ReadBytes((Int32)byteLength);

                }
            }
            Document document= new Document();
            document.DocName = fileName;
            document.DocContent = fileContent;
            return document;
        }

        //recovering a file (given correct fileName path) from a byte array
        public void ShowDocument(string fileName, byte[] fileContent)
        {
            using (Stream file = File.OpenWrite(fileName))
            {
                file.Write(fileContent, 0, fileContent.Length);
            }
        }
    }
}
