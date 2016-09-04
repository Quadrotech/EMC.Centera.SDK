using System;

namespace EMC.Centera.SDK
{
    public interface IFPTag
    {
        void Close();
        FPTag Copy(FPTag inNewParent, int inOptions);
        FPClip FPClip { get; }
        FPTag NextSibling { get; }
        FPTag PrevSibling { get; }
        FPTag FirstChild { get; }
        FPTag Parent { get; }
        String Name { get; }
        int NumAttributes { get; }
        long BlobSize { get; }
        int BlobStatus { get; }
        FPAttributeCollection Attributes { get; }
        void Delete();
        string ToString();
        void SetAttribute(String inAttrName,  String inAttrValue);
        void SetAttribute(String inAttrName, long inAttrValue);
        void SetAttribute(String inAttrName, bool inAttrValue);
        String GetStringAttribute(String inAttrName);
        long GetLongAttribute(String inAttrName);
        bool GetBoolAttribute(String inAttrName);
        void RemoveAttribute(String inAttrName);
        FPAttribute GetAttributeByIndex(int inIndex);
        void BlobWrite(FPStream inStream);
        void BlobWrite(FPStream inStream, long inOptions);
        void BlobWritePartial(FPStream inStream, long inSequenceID);
        void BlobWritePartial(FPStream inStream, long inSequenceID, long inOptions);
        void BlobRead(FPStream inStream);
        void BlobRead(FPStream inStream, long inOptions);
        void BlobReadPartial(FPStream inStream, long inOffset, long inReadLength);
        void BlobReadPartial(FPStream inStream, long inOffset, long inReadLength, long inOptions);
        void Dispose();
        void Dispose(bool disposing);
    }
}