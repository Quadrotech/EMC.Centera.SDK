/*****************************************************************************

Copyright © 2006 EMC Corporation. All Rights Reserved
 
This file is part of .NET wrapper for the Centera SDK.

.NET wrapper is free software; you can redistribute it and/or modify it under
the terms of the GNU General Public License as published by the Free Software
Foundation version 2.

In addition to the permissions granted in the GNU General Public License
version 2, EMC Corporation gives you unlimited permission to link the compiled
version of this file into combinations with other programs, and to distribute
those combinations without any restriction coming from the use of this file.
(The General Public License restrictions do apply in other respects; for
example, they cover modification of the file, and distribution when not linked
into a combined executable.)

.NET wrapper is distributed in the hope that it will be useful, but WITHOUT ANY
WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A
PARTICULAR PURPOSE. See the GNU General Public License version 2 for more
details.

You should have received a copy of the GNU General Public License version 2
along with .NET wrapper; see the file COPYING. If not, write to:

 EMC Corporation 
 Centera Open Source Intiative (COSI) 
 80 South Street
 1/W-1
 Hopkinton, MA 01748 
 USA

******************************************************************************/

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
        string Name { get; }
        int NumAttributes { get; }
        long BlobSize { get; }
        int BlobStatus { get; }
        FPAttributeCollection Attributes { get; }
        void Delete();
        string ToString();
        void SetAttribute(string inAttrName,  string inAttrValue);
        void SetAttribute(string inAttrName, long inAttrValue);
        void SetAttribute(string inAttrName, bool inAttrValue);
        string GetStringAttribute(string inAttrName);
        long GetLongAttribute(string inAttrName);
        bool GetBoolAttribute(string inAttrName);
        void RemoveAttribute(string inAttrName);
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