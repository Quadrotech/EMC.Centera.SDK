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
    public interface IFPPool
    {
        string BlobNamingSchemes { get; }
        long Capacity { get; }
        string CenteraEdition { get; }
        string CentraStarVersion { get; }
        void ClipAuditedDelete(string inClipID, string inReason, long inOptions);
        void ClipAuditedDelete(string inClipID, string inReason);
        int ClipBufferSize { get; set; }
        FPClip ClipCreate(string inName);
        void ClipDelete(string inClipID);
        bool ClipExists(string inClipID);
        FPClip ClipOpen(string inClipID, int inOpenMode);
        FPClip ClipRawOpen(string inClipID, FPStream inStream);
        FPClip ClipRawOpen(string inClipID, FPStream inStream, long inOptions);
        string ClusterID { get; }
        string ClusterName { get; }
        DateTime ClusterTime { get; }
        bool CollisionAvoidanceEnabled { get; set; }
        string DefaultRetenionScheme { get; }
        bool DeleteAllowed { get; }
        string DeletePools { get; }
        bool DeletionsLogged { get; }
        bool EBRSupported { get; }
        bool ExistsAllowed { get; }
        string ExistsPools { get; }
        TimeSpan FixedRetentionMax { get; }
        TimeSpan FixedRetentionMin { get; }
        long FreeSpace { get; }
        bool HoldAllowed { get; }
        string HoldPools { get; }
        bool HoldSupported { get; }
        bool MultiClusterFailoverEnabled { get; set; }
        string PoolMappings { get; }
        string PoolProfiles { get; }
        int PrefetchBufferSize { get; set; }
        bool PrivilegedDeleteAllowed { get; }
        string PrivilegedDeletePools { get; }
        string ProfileClip { get; set; }
        bool QueryAllowed { get; }
        string QueryPools { get; }
        bool ReadAllowed { get; }
        string ReadPools { get; }
        string ReplicaAddress { get; }
        FPRetentionClassCollection RetentionClasses { get; }
        TimeSpan RetentionDefault { get; }
        bool RetentionMinMax { get; }
        int Timeout { get; set; }
        string ToString();
        TimeSpan VariableRetentionMax { get; }
        TimeSpan VariableRetentionMin { get; }
        bool WriteAllowed { get; }
        string WritePools { get; }
    }
}
