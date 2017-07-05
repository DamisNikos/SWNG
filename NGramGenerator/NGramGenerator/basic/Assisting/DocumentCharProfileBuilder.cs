using NGramGenerator.objects;
using System.Collections.Generic;


namespace NGramGenerator.Assisting
{
    class DocumentCharProfileBuilder
    {
        public DocumentCharProfileBuilder() { }
        //Step-1
        //1.Get the normalized(all lowercase, no punctuation) text presentation
        //2.Add all words to one string
        //3.Calculate the size of nGram presentation
        //4.Create the document's profile in letters n-grams
        //5.Remove double entries
        //6.Return document's profile
        public DocumentProfileChar GetDocumentCharProfileBuilder(string path, int nGramSize) {
            Library lib = new Library();
            string[] docWords = lib.GetText(path, "document");
            string docWordsInOne = "";
            foreach (var word in docWords)
            {
                docWordsInOne += word;
            }

            int targetIndex = docWordsInOne.Length + 1 - nGramSize;
            List<char[]> docProfile = new List<char[]>();
            for (int i = 0; i < targetIndex; i++)
            {
                char[] ngram = new char[nGramSize];
                for (int j = 0; j < nGramSize; j++) {
                    ngram[j] = docWordsInOne[i + j];
                }
                docProfile.Add(ngram);
            }

            DocumentProfileChar profile = new DocumentProfileChar(docProfile);
            profile.RemoveDuplicates();
            return profile;
        }
        //--------------------------------------------------------------------------------------------------------------------------------//
        //STEP-BY-STEP
        public DocumentProfileChar GetDocumentCharProfileBuilder(string[] docWords, int nGramSize)
        {
            string docWordsInOne = "";
            foreach (var word in docWords)
            {
                docWordsInOne += word;
            }

            int targetIndex = docWordsInOne.Length + 1 - nGramSize;
            List<char[]> docProfile = new List<char[]>();
            for (int i = 0; i < targetIndex; i++)
            {
                char[] ngram = new char[nGramSize];
                for (int j = 0; j < nGramSize; j++)
                {
                    ngram[j] = docWordsInOne[i + j];
                }
                docProfile.Add(ngram);
            }

            DocumentProfileChar profile = new DocumentProfileChar(docProfile);
            profile.RemoveDuplicates();
            return profile;
        }
        //--------------------------------------------------------------------------------------------------------------------------------//




    }
}
