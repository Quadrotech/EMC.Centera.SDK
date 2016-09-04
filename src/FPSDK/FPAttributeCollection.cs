using System.Collections;

namespace EMC.Centera.SDK
{
    public class FPAttributeCollection:ArrayList
    {
        internal FPAttributeCollection(FPTag t) 
        { 
            for (int i = 0; i < t.NumAttributes; i++)
            {
                Add(t.GetAttributeByIndex(i));
            }
        } 
                       
        internal FPAttributeCollection(FPClip c) 
        { 
            for (int i = 0; i < c.NumAttributes; i++)
            {
                Add(c.GetAttributeByIndex(i));
            }
        } 
    }
}