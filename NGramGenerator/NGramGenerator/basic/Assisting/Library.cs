using NGramGenerator.objects;
using System;
using System.Collections.Generic;
using System.Text;
using Toxy;

namespace NGramGenerator
{
    class Library
    { 
        //Intersect two profiles (either stopNwords or letter) and return a new intersected profile
        public DocumentProfileStopNWords IntersectProfiles(DocumentProfileStopNWords profile1, DocumentProfileStopNWords profile2)
        {
            List<String[]> intersection = new List<string[]>();
            foreach (var ngram1 in profile1.getNgramsCollection())
            {
                int countEquals = 0;
                foreach (var ngram2 in profile2.getNgramsCollection())
                {
                    int countEqualWords = 0;
                    for (int i = 0; i < ngram1.Length; i++)
                    {
                        if (ngram1[i].Equals(ngram2[i]))
                        {
                            countEqualWords++;
                        }
                    }
                    if (countEqualWords == ngram1.Length)
                    {
                        countEquals++;
                    }
                }
                if (countEquals > 0)
                {
                    intersection.Add(ngram1);
                }
            }
            DocumentProfileStopNWords profile = new DocumentProfileStopNWords(intersection);
            return profile;
        }
        public DocumentProfileChar IntersectProfiles(DocumentProfileChar profile1, DocumentProfileChar profile2) {
            List<char[]> ngramsCollection = new List<char[]>();
            foreach (var ngram1 in profile1.getNgramsCollection())
            {
                int countEquals = 0;
                foreach (var ngram2 in profile2.getNgramsCollection())
                {
                    int countEqualLetters = 0;
                    for (int i = 0; i < ngram1.Length; i++)
                    {
                        if (ngram1[i].Equals(ngram2[i]))
                        {
                            countEqualLetters++;
                        }
                    }
                    if (countEqualLetters == ngram1.Length)
                    {
                        countEquals++;
                    }
                }
                if (countEquals > 0)
                {
                    ngramsCollection.Add(ngram1);
                }
            }
            return new DocumentProfileChar(ngramsCollection);
        }

        //Parse document(option=['document'->.docx, .pdf] or ['txt'->.txt])
        //Removes punctuation and returns the words of the document in an array of strings
        public string[] GetText(string path, string option)
        {
            string text = null;
            try
            {
                ParserContext context = new ParserContext(path);
                if (option.Equals("txt"))
                {
                    ITextParser parser = ParserFactory.CreateText(context);
                    text = parser.Parse().ToString().ToLower().Replace('\n', ' ').Replace('\r', ' ')
                .Replace('\t', ' ');
                }
                else if (option.Equals("document"))
                {
                    IDocumentParser parser = ParserFactory.CreateDocument(context);
                    text = parser.Parse().ToString().ToLower().Replace('\n', ' ').Replace('\r', ' ')
                .Replace('\t', ' ');
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception found");
                Console.WriteLine(e.Message);
            }
            text = RemovePunctuation(text);
            string[] words = text.Split(default(Char[]), StringSplitOptions.RemoveEmptyEntries);
            return words;
        }
        //Removes punctuation
        private string RemovePunctuation(string s)
        {
            var sb = new StringBuilder();

            foreach (char c in s)
            {
                if (!char.IsPunctuation(c))
                    sb.Append(c);
            }

            s = sb.ToString();
            return s;
        }

        

    }
}
