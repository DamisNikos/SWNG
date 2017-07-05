using NGramGenerator.objects;


namespace NGramGenerator.Algorithm
{
    class PostProcessing
    {
        public PostProcessing() { }
        //Step-1
        //1.Intersect profile1 and profile2
        //2.Apply criterion to find the similarity value (sizeof(IntersectedProfiles) / max(sizeOf(profile1),sizeOf(profile2)))
        //3.Return similarity score
        public float FindSimilarity(DocumentProfileChar profile1, DocumentProfileChar profile2) {
            float similarity;
            Library lib = new Library();
            DocumentProfileChar intersect = lib.IntersectProfiles(profile1, profile2);
            float sizeOfProfile1 = profile1.getNgramsCollection().Count;
            float sizeOfProfile2 = profile2.getNgramsCollection().Count;
            float sizeOfIntersection = intersect.getNgramsCollection().Count;
            float maxSize;
            maxSize = sizeOfProfile1 > sizeOfProfile2 ? sizeOfProfile1 : sizeOfProfile2;

            similarity = sizeOfIntersection / maxSize;
            return similarity;

        }

        //--------------------------------------------------------------------------------------------------------------------------------//
        //STEP-BY-STEP
        public float FindSimilarity(DocumentProfileChar profile1, DocumentProfileChar profile2, DocumentProfileChar intersect)
        {
            float similarity;
            float sizeOfProfile1 = profile1.getNgramsCollection().Count;
            float sizeOfProfile2 = profile2.getNgramsCollection().Count;
            float sizeOfIntersection = intersect.getNgramsCollection().Count;
            float maxSize;
            maxSize = sizeOfProfile1 > sizeOfProfile2 ? sizeOfProfile1 : sizeOfProfile2;

            similarity = sizeOfIntersection / maxSize;
            return similarity;

        }
        //--------------------------------------------------------------------------------------------------------------------------------//

    }
}
