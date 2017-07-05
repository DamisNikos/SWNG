using NGramGenerator.Algorithm;
using NGramGenerator.Experimenting;
using NGramGenerator.Experimenting.Model;
using System;
using System.Diagnostics;

namespace NGramGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            int criterion1Size = 11;
            int criterion2Size = 8;
            int letterNGramSize = 3;

            string folder = @"sampleTextFiles\";
            string suspicious = folder + "plagiarized.pdf"; // "suspicious1.docx"  -> not plagiarized  ||||   "plagiarized.docx"  -> highly modified plagiarized
            string original = folder + "original1.pdf";     // "original2.docx"    -> match above      ||||   "original1.docx"    -> match above


            //PlagiarismDetectionStepByStep algorithm = new PlagiarismDetectionStepByStep();
            //float similarity = algorithm.RunAlgoritm(suspicious, original, criterion1Size, criterion2Size, letterNGramSize);
            //Console.WriteLine($"The similarity score between {suspicious.Substring(folder.Length)} and {original.Substring(folder.Length)} is: {similarity}");
            DocumentConverter converter = new DocumentConverter();
            Document doc = converter.FileToByteArray(suspicious);
            converter.ShowDocument(folder + "\\CreatedFromApp\\tempplag1.pdf", doc.DocContent);




            Console.ReadKey();
        }


    }

}
