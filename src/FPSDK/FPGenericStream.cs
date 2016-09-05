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
using System.IO;
using EMC.Centera.SDK.FPTypes;

namespace EMC.Centera.SDK
{


    /// <summary> 
    ///A Generic Stream  object.
    ///@author Graham Stuart
    ///@version
    /// </summary>
    public class FPGenericStream : FPStream
    {
        readonly unsafe FPStreamInfo	*theInfo;
        protected Stream userStream;
        readonly FPCallback prepare;
        readonly FPCallback complete;
        readonly FPCallback mark;
        readonly FPCallback reset;
        readonly FPCallback close;

        /// <summary>
		///Creates a Generic Stream object using the standard callback methods. See API Guide: FPStream_CreateGenericStream
		///See API Guide: FPStream_CreateGenericStream
		///
		///@param userStream	Instance of an object derived from the Stream class. This object
		///						is used by the calling application to transfer the data to the Generic
		///						Stream. Stream direction is set to input to the Centera if the CanRead
		///						property of the userStream is true. Note: The derived Stream class should
		///						implement the Seek functionality is this is used to provide Marker Support
		///						for the SDK. The direction of the Stream is determined by the CanRead property
        ///                     of the supplied Stream.
		///@param userData		An IntPtr which can be used to reference a user object that may be required
		///						when working with the input or output data buffer.
		 /// </summary>
        public FPGenericStream(Stream userStream, IntPtr userData)
            :this(userStream.CanRead ? StreamDirection.InputToCentera : StreamDirection.OutputFromCentera, 
                new FPStreamCallbacks(userStream),
                userData)
        {
        }

        /// <summary>
		///Creates a Generic Stream object using the standard callbacks. A Stream derived class is still used for transfer
        ///but the user supplies the direction required (supports bi-directional streams).
		///See API Guide: FPStream_CreateGenericStream
		///
		///@param userStream	Instance of an object derived from the Stream class. This object
		///						is used by the calling application to transfer the data to the Generic
		///						Stream. Note: The derived Stream class should implement the Seek functionality
		///                     is this is used to provide Marker Support for the SDK.
		///@param direction     The direction of the stream.
		///@param userData		An IntPtr which can be used to reference a user object that may be required
		///						when working with the input or output data buffer.
		 /// </summary>
        public FPGenericStream(Stream userStream, StreamDirection direction, IntPtr userData)
            : this(direction, new FPStreamCallbacks(userStream), userData)
        {
        }
		
        /// <summary>
		///Creates a Generic Stream object with user supplied callbacks. A Stream derived class is still used for transfer.
		///See API Guide: FPStream_CreateGenericStream
		///
		///@param userStream	Instance of an object derived from the Stream class. This object
		///						is used by the calling application to transfer the data to the Generic
		///						Stream. Stream direction is set to input to the Centera if the CanRead
		///						property of the userStream is true. Note: The derived Stream class should
		///						implement the Seek functionality is this is used to provide Marker Support
		///						for the SDK.
		///@param userCBS		Callbacks provided by the user. Typically the derived class will only
		///						require to override PopulateBuffer and / or ProcessReturnedData.
		///@param userData		An IntPtr which can be used to reference a user object that may be required
		///						when working with the input or output data buffer.
		 /// </summary>
        public FPGenericStream(Stream userStream, FPStreamCallbacks userCBS, IntPtr userData)
            :this(userStream.CanRead ? StreamDirection.InputToCentera : StreamDirection.OutputFromCentera,
                userCBS,
                userData)
        {
        }

        /// <summary>
        ///Creates a Generic Stream object with user supplied callbacks. A Stream derived class is still used for transfer.
        ///but the user supplies the direction required (supports bi-directional streams).
        ///See API Guide: FPStream_CreateGenericStream
        ///
        ///@param userStream	Instance of an object derived from the Stream class. This object
        ///						is used by the calling application to transfer the data to the Generic
        ///						Stream. Note: The derived Stream class should implement the Seek functionality is
        ///                     this is used to provide Marker Support for the SDK.
        ///@param direction     The direction of the stream.
        ///@param userCBS		Callbacks provided by the user. Typically the derived class will only
        ///						require to override PopulateBuffer and / or ProcessReturnedData.
        ///@param userData		An IntPtr which can be used to reference a user object that may be required
        ///						when working with the input or output data buffer.
         /// </summary>
        public FPGenericStream(Stream userStream, StreamDirection direction, FPStreamCallbacks userCBS, IntPtr userData)
            :this(direction, userCBS, userData)
        {
        }

        /// <summary>
		///Creates a Generic Stream object with user supplied callbacks. This is the most complex way of using
		///a GenericStream and will require the user to implement more of the functionality normally handled by
		///the standard callbacks. In particular Marker Support will require the user to make their transfer
		///method "rewindable" for up to 100MB of previously transferred data. See API Guide: FPStream_CreateGenericStream
		///See API Guide: FPStream_CreateGenericStream
		///
		///@param direction		Indicator for the type of GenericStream that is to be created i.e. input to
		///						Centera or output from Centera.
		///@param userCBS		Callbacks provided by the user. Typically the derived class will only
		///						require to override PopulateBuffer and / or ProcessReturnedData.
		///@param userData		An IntPtr which can be used to reference a user object that may be required
		///						when working with the input or output data buffer.
		 /// </summary>
        public FPGenericStream(StreamDirection direction, FPStreamCallbacks userCBS, IntPtr userData)
        {
            prepare = new FPCallback(userCBS.PrepareBuffer);
            complete = new FPCallback(userCBS.BlockTransferred);
            mark = new FPCallback(userCBS.SetMark);
            reset = new FPCallback(userCBS.ResetMark);
            close = new FPCallback(userCBS.TransferComplete);

            if (direction == StreamDirection.InputToCentera)
                theStream = SDK.FPStream_CreateGenericStream(prepare, complete, mark, reset, close, userData);
            else
                theStream = SDK.FPStream_CreateGenericStream(null, complete, mark, reset, close, userData);
            AddObject(theStream, this);

            userCBS.StreamRef = theStream;
            userStream = userCBS.userStream;

            unsafe
            {
                theInfo = SDK.FPStream_GetInfo(theStream);
                theInfo->mAtEOF = 0;
                theInfo->mStreamLen = -1;
                theInfo->mTransferLen = 0;
                theInfo->mStreamPos = 0;
                theInfo->mMarkerPos = 0;
                theInfo->mBuffer = null;

                theInfo->mReadFlag = (byte)direction;
            }
        }
		
        /// <summary>
		///Close the underlying FPStream object.
		 /// </summary>
        public override void Close()
        {
            if (theStream != 0)
            {
                RemoveObject(theStream);
                Native.Stream.Close(theStream);
                theStream = 0;
            }
        }

        /// <summary>
		///The length of the stream being transferred. Defaults to -1 (unknown length).
		 /// </summary>
        public unsafe long StreamLen
        {
            get
            {
                return theInfo->mStreamLen;
            }
            set
            {
                theInfo->mStreamLen = value;
            }
        }

        /// <summary>
		///The stream direction: 
		 /// </summary>

        public unsafe StreamDirection Direction => (StreamDirection) theInfo->mReadFlag;

        /// <summary>
		///Prints out values of the FPStreamInfo control structure.
		 /// </summary>
        public override unsafe string ToString()
        {
            return "End of file: " + theInfo->mAtEOF +
                   "\nMarker " + theInfo->mMarkerPos +
                   "\nRead flag " + theInfo->mReadFlag +
                   "\nStreamLen " + theInfo->mStreamLen +
                   "\nPos "+ theInfo->mStreamPos +
                   "\nTransferLen " + theInfo->mTransferLen +
                   "\nVersion " + theInfo->mVersion;
        }

    }
}