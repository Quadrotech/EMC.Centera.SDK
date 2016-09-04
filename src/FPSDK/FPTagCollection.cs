using System.Collections;

namespace EMC.Centera.SDK
{
    public class FPTagCollection:ArrayList
    {
        internal FPTagCollection(FPClip c) 
        { 
            FPTag t;

            for (int i = 0; i < c.NumTags; i++)
            {
                t = c.NextTag;
                Add(t);
            }
			
        }
    }
}