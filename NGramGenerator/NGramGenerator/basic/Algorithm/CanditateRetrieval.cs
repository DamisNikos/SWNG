using System;
using System.Collections.Generic;


namespace NGramGenerator
{
    class CanditateRetrieval
    {
        public CanditateRetrieval() { }

        //Step-1
        //1.Intersect the two sets which represent the two profiles
        //2.Apply criterion (1)
        //3.Get the suspicious n-grams
        public DocumentProfileStopNWords Retrieve(DocumentProfileStopNWords profile1, DocumentProfileStopNWords profile2) {
            Library library = new Library();
            return ApplyCanditateRetrievalCriterion(library.IntersectProfiles(profile1, profile2));
        }
       
        //Step-2
        //1.Get the interesected profile
        //2.Aplly criterion G.belongsTo(p1 and p2) if member(g,C)<n-1 and maxseq(g,C)<n-2,
        //  where C is a list of the most 6 common words, member() is the number of member in g that belong to C
        //  and maxseq() is the maximal sequence of members of g that belong to C.
        //3.Return the intersection of the 2 profiles with only the n-gramms that satisfy criterion (1)
        public DocumentProfileStopNWords ApplyCanditateRetrievalCriterion(DocumentProfileStopNWords profile)
        {
            String[] mostCommon6 = new String[] { "the", "of", "and", "a", "in", "to" };
            List<String[]> nGramCollection = new List<string[]>();
            //iterate through all ngrams that belongs to the interesection of the two profiles
            foreach (var ngram in profile.getNgramsCollection())
            {
                int maxseq = 0;
                int membersofC = 0;
                int currentsq = 0;
                //iterate through all members of the ngram
                foreach (var word in ngram)
                {
                    bool found = false;
                    //iterate through all words in C
                    for (int i = 0; i < mostCommon6.Length; i++)
                    {
                        //increase sequence and member if match is found and stop iterating C
                        if (word.Equals(mostCommon6[i]))
                        {
                            membersofC++;
                            currentsq++;
                            found = true;
                            break;
                        }
                    }
                    //if match is not found compare this sequence against previous maximal and update accordingly
                    if (!found)
                    {
                        if (maxseq < currentsq) maxseq = currentsq;
                        currentsq = 0;
                    }

                }
                //if criterion (1) is satisfied add this ngram to the collection
                if ((membersofC < ngram.Length - 1) && (maxseq < ngram.Length - 2))
                {
                    nGramCollection.Add(ngram);
                }
            }
            return new DocumentProfileStopNWords(nGramCollection);
        }
    }
}
