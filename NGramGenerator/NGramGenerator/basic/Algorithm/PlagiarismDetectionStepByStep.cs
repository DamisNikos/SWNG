using NGramGenerator.Assisting;
using NGramGenerator.objects;
using System;
using System.Collections.Generic;


namespace NGramGenerator.Algorithm
{
    class PlagiarismDetectionStepByStep
    {
        public PlagiarismDetectionStepByStep(){}

        public float RunAlgoritm(string pathToSuspicious, string pathToOriginal, int criterion1Size,int criterion2Size, int charNGramSize) {
            Library library = new Library();
            DocumentProfileBuilder stopNwordsBuilder = new DocumentProfileBuilder();
            CanditateRetrieval retriever = new CanditateRetrieval();
            PassageBoundaryDetection detector = new PassageBoundaryDetection();
            DocumentCharProfileBuilder letterBuidler = new DocumentCharProfileBuilder();
            PostProcessing postProcessor = new PostProcessing();

            //=========================================Step-1 CREATING THE PROFILES OF STOP-N-WORDS NGRAMS===========================================
            //Step-1.1
            //Get a normalised(all lowercase, no punctuation) list of all the words in the documents
            string[] wordsOfSuspicious = library.GetText(pathToSuspicious, "document");
            string[] wordsOfOriginal = library.GetText(pathToOriginal, "document");
            //Step-1.2
            //Get a list of strings of all 50 most used words contained in the document
            List<String> stopNwordPresentationSuspicious = stopNwordsBuilder.GetStopWordPresentation(wordsOfSuspicious);
            List<String> stopNwordPresentationOriginal = stopNwordsBuilder.GetStopWordPresentation(wordsOfOriginal);
            //Step-1.3
            //Get the document's profile in stopword ngrams
            DocumentProfileStopNWords stopNWordsProfileSuspicious = stopNwordsBuilder.GetDocumentProfileStopNWords(stopNwordPresentationSuspicious, criterion1Size);
            DocumentProfileStopNWords stopNWordsProfileOriginal = stopNwordsBuilder.GetDocumentProfileStopNWords(stopNwordPresentationOriginal, criterion1Size);


            //===================================================Step-2 Canditate Retrieval============================================================
            //Step-2.1
            //Get the intersected(common ngrams) profile of the 2 documents
            DocumentProfileStopNWords canditateIntersection = library.IntersectProfiles(stopNWordsProfileSuspicious, stopNWordsProfileOriginal);
            //Step-2.2
            //Apply criterion (1) to the canditate intersection to filter out false positives
            DocumentProfileStopNWords finalCanditate = retriever.ApplyCanditateRetrievalCriterion(canditateIntersection);

            //===============================================Step-3 Passage Boundary Detection======================================================
            //Step-3.1. REDO Step-1.3 to produce profile of ngrams with different size in order to apply criterion (2)
            DocumentProfileStopNWords ProfileSuspiciousBoundaries = stopNwordsBuilder.GetDocumentProfileStopNWords(stopNwordPresentationSuspicious, criterion2Size);
            DocumentProfileStopNWords ProfileOriginalBoundaries = stopNwordsBuilder.GetDocumentProfileStopNWords(stopNwordPresentationOriginal, criterion2Size);
            //Step-3.2 REDO Step-2.1 to intersect the profiles
            DocumentProfileStopNWords boundariesIntersection = library.IntersectProfiles(ProfileSuspiciousBoundaries, ProfileOriginalBoundaries);
            //Step-3.4
            //Apply criterion (2) to avoid noise of coincidental matches
            DocumentProfileStopNWords boundariesFinalCommon = detector.ApplyMatchCriterion(boundariesIntersection);
            //Step-3.5
            //Get a list M of matched Ngrams
            //where members of M are ordered according to the first appearance of a match in the suspicious document
            List<int[]> setOfMatchedNGrams = detector.MatchedNgramSet(ProfileSuspiciousBoundaries, ProfileOriginalBoundaries, boundariesFinalCommon);
            //Step-3.6 Apply criterion (3)
            //MISSING!!!!
            //Step-3.7 Apply criterion (4)
            //MISSING!!!!

            //=======================================Step-4 CREATING THE PROFILES OF LETTER NGRAMS=============================================
            //Step-4.1 REDO Step-1.1
            //Step-4.2
            //Get the document's profile in letters ngrams (duplicated entries are removed)
            DocumentProfileChar letterProfileSuspicious = letterBuidler.GetDocumentCharProfileBuilder(wordsOfSuspicious,charNGramSize);
            DocumentProfileChar letterProfileOriginal = letterBuidler.GetDocumentCharProfileBuilder(wordsOfOriginal, charNGramSize);

            //=================================================Step-5 Post-processing==========================================================
            //Step-5.1  (overloading method used on step-2.1 and step-3.2)
            //Get the intersected(common ngrams) profile of the 2 documents
            DocumentProfileChar similarityIntersection = library.IntersectProfiles(letterProfileSuspicious, letterProfileOriginal);
            //Step-5.2
            //Apply criterion (5) to find the similarity score between the 2 profiles
            float similarity = postProcessor.FindSimilarity(letterProfileSuspicious, letterProfileOriginal, similarityIntersection);



            return similarity;



        }
    }
}
