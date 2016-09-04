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
        string ClipID { get; }
        string Name { get; set; }
        DateTime CreationDate { get; }
        TimeSpan RetentionPeriod { get; set; }
        DateTime RetentionExpiry { get; set; }
        DateTime EBRExpiry { get; set; }
        DateTime EBREventTime { get; }
        bool Modified { get; }
        FPTag NextTag { get; }
        int NumAttributes { get; }
        string RetentionClassName { get; set; }
        FPRetentionClass FPRetentionClass { get; set; }
        byte[] CanonicalForm { get; }
        FPTagCollection Tags { get; }
        FPAttributeCollection Attributes { get; }
        bool OnHold { get; }
        TimeSpan EBRPeriod { get; set; }
        string EBRClassName { get; }
        FPRetentionClass EBRClass { set; }
        bool EBREnabled { get; }
        TimeSpan TriggerEBRPeriod { set; }
        FPRetentionClass TriggerEBRClass { set; }
        FPTag AddTag(string tagName);
        string ToString();
        string Write();
        void RawRead(FPStream inStream);
        void SetAttribute(string inAttrName,  string inAttrValue);
        void RemoveAttribute(string inAttrName);
        string GetAttribute(string inAttrName);
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