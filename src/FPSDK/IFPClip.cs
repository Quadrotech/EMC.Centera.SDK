using System;

namespace EMC.Centera.SDK
{
    public interface IFPClip
    {
        void Close();
        FPPool FPPool { get; }
        FPTag TopTag { get; }
        int NumBlobs { get; }
        int NumTags { get; }
        long TotalSize { get; }
        String ClipID { get; }
        String Name { get; set; }
        DateTime CreationDate { get; }
        TimeSpan RetentionPeriod { get; set; }
        DateTime RetentionExpiry { get; set; }
        DateTime EBRExpiry { get; set; }
        DateTime EBREventTime { get; }
        bool Modified { get; }
        FPTag NextTag { get; }
        int NumAttributes { get; }
        String RetentionClassName { get; set; }
        FPRetentionClass FPRetentionClass { get; set; }
        byte[] CanonicalForm { get; }
        FPTagCollection Tags { get; }
        FPAttributeCollection Attributes { get; }
        bool OnHold { get; }
        TimeSpan EBRPeriod { get; set; }
        String EBRClassName { get; }
        FPRetentionClass EBRClass { set; }
        bool EBREnabled { get; }
        TimeSpan TriggerEBRPeriod { set; }
        FPRetentionClass TriggerEBRClass { set; }
        FPTag AddTag(String tagName);
        string ToString();
        String Write();
        void RawRead(FPStream inStream);
        void SetAttribute(String inAttrName,  String inAttrValue);
        void RemoveAttribute(String inAttrName);
        String GetAttribute(String inAttrName);
        FPAttribute GetAttributeByIndex(int inIndex);
        void RemoveRetentionClass();
        bool ValidateRetentionClass(FPRetentionClassCollection coll);
        bool ValidateRetentionClass();
        byte[] GetCanonicalFormat(int bufSize);
        void SetRetentionHold(bool holdState, string holdID);
        void TriggerEBREvent();
        void Dispose();
        void Dispose(bool disposing);
    }
}