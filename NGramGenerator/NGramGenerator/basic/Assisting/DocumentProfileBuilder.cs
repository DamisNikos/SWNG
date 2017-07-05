using System;
using System.Collections.Generic;


namespace NGramGenerator
{
    class DocumentProfileBuilder
    {
        private string[] top50words;


        public DocumentProfileBuilder() {
            Library lib = new Library();
            top50words = lib.GetText(@"sampleTextFiles\Top50UsedWords.docx", "document");
        }

        //Step-2
        //1.Get the stopNword presentation
        //2.Calculate the size of nGram presentation
        //3.Create the document's profile in n-gram stopNword
        public DocumentProfileStopNWords GetDocumentProfileStopNWords(string path, string option, int nGramSize) {

            List<String> swPresentation = this.GetStopWordPresentation(path, option);
            //calculate the size of nGram presentation
            int targetIndex = swPresentation.Count + 1 - nGramSize;
            List<String[]> docProfile = new List<String[]>();
            //iterate through each n-gram
            for (int i = 0; i < targetIndex; i++)
            {
                String[] ngram = new String[nGramSize];
                //add words to each n-gram
                for (int j = 0; j < nGramSize; j++)
                {
                    ngram[j] = swPresentation[i + j];
                }
                //add current n-gram to the profile
                docProfile.Add(ngram);
            }
            DocumentProfileStopNWords profile = new DocumentProfileStopNWords(docProfile);
            return profile;
        }
        //--------------------------------------------------------------------------------------------------------------------------------//
        //STEP-BY-STEP
        public DocumentProfileStopNWords GetDocumentProfileStopNWords(List<String> swPresentation, int nGramSize)
        {
            //calculate the size of nGram presentation
            int targetIndex = swPresentation.Count + 1 - nGramSize;
            List<String[]> docProfile = new List<String[]>();
            //iterate through each n-gram
            for (int i = 0; i < targetIndex; i++)
            {
                String[] ngram = new String[nGramSize];
                //add words to each n-gram
                for (int j = 0; j < nGramSize; j++)
                {
                    ngram[j] = swPresentation[i + j];
                }
                //add current n-gram to the profile
                docProfile.Add(ngram);
            }
            DocumentProfileStopNWords profile = new DocumentProfileStopNWords(docProfile);
            return profile;
        }
        //--------------------------------------------------------------------------------------------------------------------------------//

        //Step-1
        //1.Get the normalized(all lowercase, no punctuation) text presentation
        //2.Create the stopNword presentation
        //3.Return the stopNword presentation
        private List<String> GetStopWordPresentation(string path, string option) {
            Library lib = new Library();
            string[] docWords = lib.GetText(path, option);

            List<string> StopWordPresentation = new List<string>();
            //iterate through all document's words in text presentation
            foreach (string word in docWords)
            {
                //iterate through top 50 used words
                foreach (string commonword in top50words)
                {
                    //if match is found add this word in the stopNword presentation
                    if (word.Equals(commonword))
                    {
                        StopWordPresentation.Add(word);
                    }
                }
            }
            return StopWordPresentation;
        }
        //--------------------------------------------------------------------------------------------------------------------------------//
        //STEP-BY-STEP
        public List<String> GetStopWordPresentation(string[] docWords)
        {
            List<string> StopWordPresentation = new List<string>();
            //iterate through all document's words in text presentation
            foreach (string word in docWords)
            {
                //iterate through top 50 used words
                foreach (string commonword in top50words)
                {
                    //if match is found add this word in the stopNword presentation
                    if (word.Equals(commonword))
                    {
                        StopWordPresentation.Add(word);
                    }
                }
            }
            return StopWordPresentation;
        }
        //--------------------------------------------------------------------------------------------------------------------------------//





    }
}
