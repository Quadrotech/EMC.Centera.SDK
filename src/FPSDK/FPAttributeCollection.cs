using System.Collections;

namespace EMC.Centera.SDK
{
    /// <summary>
///A collection of Attributes existing on a Tag or DescriptionAttributes on a Clip..
 /// </summary>
    public sealed class FPAttributeCollection:ArrayList
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