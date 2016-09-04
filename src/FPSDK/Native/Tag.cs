using EMC.Centera.SDK.FPTypes;

namespace EMC.Centera.SDK.Native
{
    public class Tag 
    {

        public static FPTagRef Create(FPTagRef inParent,  string inName) 
        {
            FPTagRef retval = SDK.FPTag_Create8(inParent, inName);
            SDK.CheckAndThrowError();
            return retval;
        }
        public static void Close(FPTagRef inTag) 
        {
            SDK.FPTag_Close(inTag);
            SDK.CheckAndThrowError();
        }
        public static FPTagRef Copy(FPTagRef inTag, FPTagRef inNewParent, FPInt inOptions) 
        {
            FPTagRef retval = SDK.FPTag_Copy(inTag, inNewParent, inOptions);
            SDK.CheckAndThrowError();
            return retval;
        }
        public static FPClipRef GetClipRef(FPTagRef inTag) 
        {
            FPClipRef retval = SDK.FPTag_GetClipRef(inTag);
            SDK.CheckAndThrowError();
            return retval;
        }
        public static FPTagRef GetSibling(FPTagRef inTag) 
        {
            FPTagRef retval = SDK.FPTag_GetSibling(inTag);
            SDK.CheckAndThrowError();
            return retval;
        }
        public static FPTagRef GetPrevSibling(FPTagRef inTag) 
        {
            FPTagRef retval = SDK.FPTag_GetPrevSibling(inTag);
            SDK.CheckAndThrowError();
            return retval;
        }
        public static FPTagRef GetFirstChild(FPTagRef inTag) 
        {
            FPTagRef retval = SDK.FPTag_GetFirstChild(inTag);
            SDK.CheckAndThrowError();
            return retval;
        }
        public static FPTagRef GetParent(FPTagRef inTag) 
        {
            FPTagRef retval = SDK.FPTag_GetParent(inTag);
            SDK.CheckAndThrowError();
            return retval;
        }
        public static void Delete(FPTagRef inTag) 
        {
            SDK.FPTag_Delete(inTag);
            SDK.CheckAndThrowError();
        }
        public static void GetTagName(FPTagRef inTag, ref byte[] outName, ref FPInt ioNameLen) 
        {
            SDK.FPTag_GetTagName8(inTag, outName, ref ioNameLen);
            SDK.CheckAndThrowError();
        }
        public static void SetStringAttribute(FPTagRef inTag,  string inAttrName,  string inAttrValue) 
        {
            SDK.FPTag_SetStringAttribute8(inTag, inAttrName, inAttrValue);
            SDK.CheckAndThrowError();
        }
        public static void SetLongAttribute(FPTagRef inTag,  string inAttrName, FPLong inAttrValue) 
        {
            SDK.FPTag_SetLongAttribute8(inTag, inAttrName, inAttrValue);
            SDK.CheckAndThrowError();
        }
        public static void SetBoolAttribute(FPTagRef inTag,  string inAttrName, FPBool inAttrValue) 
        {
            SDK.FPTag_SetBoolAttribute8(inTag, inAttrName, inAttrValue);
            SDK.CheckAndThrowError();
        }
        public static void GetStringAttribute(FPTagRef inTag,  string inAttrName,  ref byte[] outAttrValue, ref FPInt ioAttrValueLen) 
        {
            SDK.FPTag_GetStringAttribute8(inTag, inAttrName, outAttrValue, ref ioAttrValueLen);
            SDK.CheckAndThrowError();
        }
        public static FPLong GetLongAttribute(FPTagRef inTag,  string inAttrName) 
        {
            FPLong retval = SDK.FPTag_GetLongAttribute8(inTag, inAttrName);
            SDK.CheckAndThrowError();
            return retval;
        }
        public static FPBool GetBoolAttribute(FPTagRef inTag,  string inAttrName) 
        {
            FPBool retval = SDK.FPTag_GetBoolAttribute8(inTag, inAttrName);
            SDK.CheckAndThrowError();
            return retval;
        }
        public static void RemoveAttribute(FPTagRef inTag,  string inAttrName) 
        {
            SDK.FPTag_RemoveAttribute8(inTag, inAttrName);
            SDK.CheckAndThrowError();
        }
        public static FPInt GetNumAttributes(FPTagRef inTag) 
        {
            FPInt retval = SDK.FPTag_GetNumAttributes(inTag);
            SDK.CheckAndThrowError();
            return retval;
        }
        public static void GetIndexAttribute(FPTagRef inTag, FPInt inIndex, ref byte[] outAttrName, ref FPInt ioAttrNameLen, ref byte[] outAttrValue, ref FPInt ioAttrValueLen) 
        {
            SDK.FPTag_GetIndexAttribute8(inTag, inIndex, outAttrName, ref ioAttrNameLen, outAttrValue, ref ioAttrValueLen);
            SDK.CheckAndThrowError();
        }
        public static FPLong GetBlobSize(FPTagRef inTag) 
        {
            FPLong retval = SDK.FPTag_GetBlobSize(inTag);
            SDK.CheckAndThrowError();
            return retval;
        }
        public static void BlobWrite(FPTagRef inTag, FPStreamRef inStream, FPLong inOptions) 
        {
            SDK.FPTag_BlobWrite(inTag, inStream, inOptions);
            SDK.CheckAndThrowError();
        }
        public static void BlobRead(FPTagRef inTag, FPStreamRef inStream, FPLong inOptions) 
        {
            SDK.FPTag_BlobRead(inTag, inStream, inOptions);
            SDK.CheckAndThrowError();
        }
        public static void BlobReadPartial(FPTagRef inTag, FPStreamRef inStream, FPLong inOffset, FPLong inReadLength, FPLong inOptions) 
        {
            SDK.FPTag_BlobReadPartial(inTag, inStream, inOffset, inReadLength, inOptions);
            SDK.CheckAndThrowError();
        }
        public static void BlobPurge(FPTagRef inTag) 
        {
            SDK.FPTag_BlobPurge(inTag);
            SDK.CheckAndThrowError();
        }
        public static FPInt BlobExists(FPTagRef inTag) 
        {
            FPInt retval = SDK.FPTag_BlobExists(inTag);
            SDK.CheckAndThrowError();
            return retval;
        }


        public static void BlobWritePartial (FPTagRef inTag, FPStreamRef inStream, FPLong inOptions, FPLong inSequenceID)
        {
            SDK.FPTag_BlobWritePartial (inTag, inStream, inOptions, inSequenceID);
            SDK.CheckAndThrowError();
        }


    }
}