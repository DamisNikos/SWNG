using System;
using System.Collections.Generic;


namespace NGramGenerator
{
    class PassageBoundaryDetection
    {
        public PassageBoundaryDetection() { }

        //Step-1
        //1.Get the profiles of the suspicious and original document
        //2.Apply criterion(2)
        //3.Create a list M(dx,ds) with the indexes of the common n-grams (produced in 2.) between the two profiles
        //  i.e. M={(1,1), (2,2), (3,5)} (ngram 1 of the plagiarized document is matched with ngram 1 of original...)
        //4.Return M
        public List<int[]> MatchedNgramSet(string pathToDocument1, string pathToDocument2) {
            DocumentProfileBuilder builder = new DocumentProfileBuilder();
            DocumentProfileStopNWords profile1 = builder.GetDocumentProfileStopNWords(pathToDocument1, "Document", 8);
            DocumentProfileStopNWords profile2 = builder.GetDocumentProfileStopNWords(pathToDocument2, "Document", 8);

            Library library = new Library();
            DocumentProfileStopNWords commonProfile = ApplyMatchCriterion(library.IntersectProfiles(profile1, profile2));

            List<int[]> setOfMatched = new List<int[]>();
            foreach (var ngram in commonProfile.getNgramsCollection())
            {
                int index1 = profile1.getNgramsCollection().IndexOf(ngram);
                //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!//
                //.....................IndexOF() does not work for the second profile..................//
                int location =0;
                for (int index2 = 0; index2 < profile2.getNgramsCollection().Count; index2++)
                {
                    int matches = 0;
                    for (int i = 0; i < profile2.getNgramsCollection()[index2].Length; i++)
                    {
                        if (profile2.getNgramsCollection()[index2][i].Equals(ngram[i])) {
                            matches++;
                        }
                    }
                    if (matches == profile2.getNgramsCollection()[index2].Length) {
                        location = index2;
                    }
                }
                setOfMatched.Add(new int[] { location, index1 });
            }
            return setOfMatched;
        }
        //--------------------------------------------------------------------------------------------------------------------------------//
        //STEP-BY-STEP
        public List<int[]> MatchedNgramSet(DocumentProfileStopNWords profile1, DocumentProfileStopNWords profile2, DocumentProfileStopNWords commonProfile)
        {
            List<int[]> setOfMatched = new List<int[]>();
            foreach (var ngram in commonProfile.getNgramsCollection())
            {
                int index1 = profile1.getNgramsCollection().IndexOf(ngram);
                //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!//
                //.....................IndexOF() does not work for the second profile..................//
                int location = 0;
                for (int index2 = 0; index2 < profile2.getNgramsCollection().Count; index2++)
                {
                    int matches = 0;
                    for (int i = 0; i < profile2.getNgramsCollection()[index2].Length; i++)
                    {
                        if (profile2.getNgramsCollection()[index2][i].Equals(ngram[i]))
                        {
                            matches++;
                        }
                    }
                    if (matches == profile2.getNgramsCollection()[index2].Length)
                    {
                        location = index2;
                    }
                }
                setOfMatched.Add(new int[] { location, index1 });
            }
            return setOfMatched;
        }
        //--------------------------------------------------------------------------------------------------------------------------------//

        //Step-2
        //1.Get the interesected profile
        //2.Aplly criterion G.belongsTo(p1 and p2) if member(g,C)<n2,
        //  where C is a list of the most 6 common words, member() is the number of member in g that belong to C
        //3.Return the intersection of the 2 profiles with only the n-gramms that satisfy criterion (2)
        public DocumentProfileStopNWords ApplyMatchCriterion(DocumentProfileStopNWords profile) {
            String[] mostCommon6 = new String[] { "the", "of", "and", "a", "in", "to" };
            List<String[]> nGramCollection = new List<string[]>();
            //iterate through each n-gram
            foreach (var ngram in profile.getNgramsCollection())
            {
                int membersofC = 0;
                //iterate through each word in the n-gram
                foreach (var word in ngram)
                {
                    //iterate through each word in C
                    for (int i = 0; i < mostCommon6.Length; i++)
                    {
                        //if match is found increase members
                        if (word.Equals(mostCommon6[i]))
                        {
                            membersofC++;
                            break;
                        }
                    }
                }
                //if criterion (2) is satisfied add this ngram to the collection
                if ((membersofC < ngram.Length))
                {
                    nGramCollection.Add(ngram);
                }
            }
            return new DocumentProfileStopNWords(nGramCollection);
        }


        public void PrintMatchedList(List<int[]> mList) {
            Console.Write("M={");
            foreach (var item in mList)
            {

                Console.Write("(");
                for (int i = 0; i < item.Length; i++)
                {
                    Console.Write(item[i]);
                    if (!(i == item.Length - 1))
                    {
                        Console.Write(",");
                    }
                }
                Console.Write(")");
                if (!(mList.IndexOf(item) == mList.Count - 1))
                {
                    Console.Write(", ");

                }
            }
            Console.Write("}");

        }
    }
}
