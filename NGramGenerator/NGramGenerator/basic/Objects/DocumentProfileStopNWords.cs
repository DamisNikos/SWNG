using System;
using System.Collections.Generic;

namespace NGramGenerator
{
    class DocumentProfileStopNWords
    {
        private List<String[]> ngramsCollection;

        public DocumentProfileStopNWords(List<String[]> ngrams) {
            ngramsCollection = ngrams;
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

        public List<String[]> getNgramsCollection() {
            return this.ngramsCollection;
        }
    }
}
