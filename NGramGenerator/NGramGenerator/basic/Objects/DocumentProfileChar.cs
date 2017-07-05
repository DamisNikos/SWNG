using System;
using System.Collections.Generic;

namespace NGramGenerator.objects
{
    class DocumentProfileChar
    {
        private List<char[]> ngramsCollection;
        public DocumentProfileChar(List<char[]> ngrams)
        {
            ngramsCollection = ngrams;
        }


        public void RemoveDuplicates()
        {
            List<int> indexes = new List<int>();
            int currentIndex = 0;
            foreach (var ngram in ngramsCollection)
            {
                if (currentIndex > ngramsCollection.IndexOf(ngram)) {
                    ngramsCollection.RemoveAt(ngramsCollection.IndexOf(ngram));
                }
                currentIndex++;
            }
        }


        public void PrintProfile()
        {
            foreach (var ngram in this.ngramsCollection)
            {
                Console.Write("{");
                for (int i = 0; i < ngram.Length; i++)
                {
                    Console.Write(ngram[i]);
                    if (i < ngram.Length - 1)
                    {
                        Console.Write(",");
                    }
                }
                Console.Write("}");
                Console.WriteLine();
            }

        }

        public List<char[]> getNgramsCollection()
        {
            return this.ngramsCollection;
        }
    }
}
