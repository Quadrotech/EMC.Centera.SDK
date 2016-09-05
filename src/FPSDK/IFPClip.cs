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