using System.Collections;

namespace EMC.Centera.SDK
{
    public class FPTagCollection:ArrayList
    {

        /// <summary> 
        ///A collection of Tags on a Clip..
        ///@author Graham Stuart
        ///@version
         /// </summary>
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