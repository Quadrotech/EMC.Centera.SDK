/******************************************************************************

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
using System.Text;
using System.Collections;
using System.Runtime.InteropServices;
using EMC.Centera.FPTypes;

namespace EMC.Centera.SDK
{	
	/** 
	 * An object representing an FPClipRef.
	 * @author Graham Stuart
	 * @version
	 */

	public class FPClip : FPObject 
	{
		FPClipRef theClip;
		FPPool thePool;

		/**
		 * Create a new Clip object in the supplied Pool.
		 * See API Guide: FPClip_Create 
		 *
		 * @param inPool	The Pool to create the Clip in.
		 * @param inName	The name of the clip.
		 * @return The int value of the option.
		 */
		public FPClip(FPPool inPool,  String inName) 
		{
			thePool = inPool;
			theClip = FPApi.Clip.Create(thePool, inName);
			AddObject(theClip, this);
		}


		/**
		 * Create a Clip object by opening an existing clip in the supplied Pool.
		 * See API Guide: FPClip_Open
		 *
		 * @param inPool	The Pool containing the Clip.
		 * @param inClipID	The ID of the clip to be opened.
		 * @param inOpenMode	The mode to open the clip in (Flat or Tree).
		 */
		public FPClip(FPPool inPool,  String inClipID, int inOpenMode) 
		{
			thePool = inPool;
			theClip = FPApi.Clip.Open(inPool, inClipID, (FPInt) inOpenMode);
			AddObject(theClip, this);
		}


		/**
		 * Create a new clip in the supplied Pool by reading a raw clip from a stream.
		 * See API Guide: FPClip_RawOpen
		 *
		 * @param inPool	The Pool to create the Clip in.
		 * @param inClipID	The ID of the Clip being read - must match the new Clip ID.
		 * @param inStream	The stream to read the clip from.
		 * @param inOptions	A suitable option.
		 */
		public FPClip(FPPool inPool, String inClipID, FPStream inStream, long inOptions) 
		{
			thePool = inPool;
			theClip = FPApi.Clip.RawOpen(thePool, inClipID, inStream, (FPLong) inOptions);
			AddObject(theClip, this);
		}
		

		/**
		 * Create a Clip using an existing FPClipRef.
		 *
		 * @param c	The FPClipRef.
		 */
		internal FPClip(FPClipRef c)
		{
			theClip = c;
			thePool = FPApi.Clip.GetPoolRef(c);
			AddObject(theClip, this);
		}

        /**
         * Copy constructor.
         * 
         * @param c The FPClip to make a copy of.
        public FPClip(FPClip c)
        {
            theClip = c.theClip;
        }
         GLS **/

        /**
         * Implicit cast between an existing Clip object and an FPClipRef. 
         *
         * @param c A Clip object..
         * @return The FPClipRef associated with this Clip.
         */
		static public implicit operator FPClipRef(FPClip c) 
		{
			return c.theClip;
		}

		/**
		 * Implicit cast between an FPClipRef and an FPClip (which must already exist). 
		 *
		 * @param clipRef	An FPClipRef.
		 * @return A new Clip object.
		 */
		static public implicit operator FPClip(FPClipRef clipRef) 
		{
			// Find the relevant Tag object in the hashtable for this FPTagRef
			FPClip clipObject = null;

			if (SDKObjects.Contains(clipRef))
			{
				clipObject = (FPClip) SDKObjects[clipRef];
			}
            else
            {
                throw new FPLibraryException("FPClipRef is not asscociated with an FPClip object", FPMisc.WRONG_REFERENCE_ERR);
            }

			return clipObject;
		}

		/**
		 * Explicitly Close the Clip. See API Guide: FPClip_Close
		 */
		public override void Close()
		{
			if (myTopTag != null)
			{
				RemoveObject(myTopTag);
				myTopTag.Close();
			}

			if (theClip != 0)
			{
				RemoveObject(theClip);
                FPApi.Clip.Close(theClip);
			}

			theClip = 0;
		}

		/**
		 * The Pool associated with this Clip. See API Guide: FPClip_GetPoolRef 
		 *
		 * @return A Pool object.
		 */
		public FPPool FPPool 
		{
			get
			{
				return thePool;
			}
		}

		public FPTag AddTag(String tagName)
		{
			return new FPTag(TopTag, tagName);
		}

		private FPTag myTopTag = null;

		/**
		 * The top tag within the Clip. See API Guide: FPClip_GetTopTag 
		 *
		 */
		public FPTag TopTag
		{
			get
			{
                if (myTopTag == null)
                {
                    FPTagRef t = FPApi.Clip.GetTopTag(this);

                    if (!SDKObjects.Contains(t))
                        myTopTag = new FPTag(t);
                    else
                        myTopTag = (FPTag) SDKObjects[t];
                }

				return myTopTag;
			}
		}

		/**
		 * The number of blobs within this Clip. See API Guide: FPClip_GetNumBlobs
		 *
		 */
		public int NumBlobs
		{
			get
			{
				return (int) FPApi.Clip.GetNumBlobs(this);
			}
		}

		/**
		 * The number of tags within this Clip. See API Guide: FPClip_GetNumTags
		 *
		 */
		public int NumTags
		{
			get
			{
				return (int) FPApi.Clip.GetNumTags(this);
			}
		}

		/**
		 * The total size of this Clip. See API Guide: FPClip_GetTotalSize
		 *
		 */
		public long TotalSize
		{
			get
			{
				return (long) FPApi.Clip.GetTotalSize(this);
			}
		}

		/**
		 * A String representing the ID of this Clip. See API Guide: FPClip_GetClipID
		 *
		 */
		public String ClipID
		{
			get
			{
				StringBuilder outClipID = new StringBuilder(FPMisc.STRING_BUFFER_SIZE);

				FPApi.Clip.GetClipID(this, outClipID);
				return outClipID.ToString();
			}
		}

		/**
		 * The name of this Clip.
		 *
		 */
		public String Name
		{
			get
			{
                byte[] outString;
				FPInt bufSize = 0;
				FPInt len = 0;

				do
				{
					bufSize += FPMisc.STRING_BUFFER_SIZE;
					len = bufSize;
					outString = new byte[(int) bufSize];
					FPApi.Clip.GetName(this, ref outString, ref len);
				} while (len > bufSize);

                return Encoding.UTF8.GetString(outString, 0, (int)len - 1);
			}
			set
			{
				FPApi.Clip.SetName(this, value);
			}
		}

		/**
		 * Return a String form of this Clip
		 *
		 * @return The String representing the ID of this Clip.
		 */
		public override string ToString()
		{
			return ClipID;
		}

		/**
		 * The creation date of this Clip using default buffer of size FPMisc.STRING_BUFFER_SIZE. See API Guide: FPClip_GetCreationDate
		 *
		 */
		public DateTime CreationDate
		{
			get
			{
				StringBuilder outString = new StringBuilder();
				FPInt bufSize = 0;
				FPInt len = 0;

				do
				{
					bufSize += FPMisc.STRING_BUFFER_SIZE;
					len = bufSize;
					outString.EnsureCapacity((int) bufSize);
					FPApi.Clip.GetCreationDate(this, outString, ref len);
				} while (len > bufSize);
			
				return FPMisc.GetDateTime(outString.ToString());
			}
		}

		/**
		 * The retention period represented by a TimeSpan value. 
		 * Infinite Retention is represented a value of FPMisc.INFINITE_RETENTION_PERIOD ticks.
		 * To set this to the default RetentionPeriod of the cluster use a value of FPMisc.DEFAULT_RETENTION_PERIOD ticks. 
		 *
		 */
		public TimeSpan RetentionPeriod 
		{
			get
			{
				int period = (int) FPApi.Clip.GetRetentionPeriod(this);

				if (period < 0)
				{
					return new TimeSpan(period);
				}
				else
				{
					return new TimeSpan(0, 0, period);
				}
			}

			set
			{
				if (value.Ticks > 0)
				{
					FPApi.Clip.SetRetentionPeriod(this, (FPLong) value.TotalSeconds);
				}
				else
				{
					FPApi.Clip.SetRetentionPeriod(this, (FPLong) value.Ticks);
				}
			}
		}


		/**
		 * The retention expiry date.
		 *
		 */
		public DateTime RetentionExpiry
		{
			get
			{
				if (RetentionPeriod.Ticks < 0)
                {
                    if (RetentionPeriod.Ticks == FPMisc.INFINITE_RETENTION_PERIOD)
    					return new DateTime(9999,12,31);
                    else // Cluster DEFAULT_RETENTION is being used and the clip was written by a pre 3.1 SDK
                        return CreationDate.Add(thePool.RetentionDefault);
                }
				else
					return CreationDate.Add(RetentionPeriod);
			}

			set
			{
				RetentionPeriod = new TimeSpan(value.Ticks - this.FPPool.ClusterTime.Ticks);
			}
		}

		/**
		 * The EBR expiry date.
		 *
		 */
		public DateTime EBRExpiry
		{
			get
			{
				// The EBR event has not been triggered so as it
				// is not yet in effect we can always deleted it
				DateTime epoch = new DateTime(1970, 1, 1);
				if (EBREventTime == epoch)
					return epoch;

				if (EBRPeriod.Ticks != FPMisc.INFINITE_RETENTION_PERIOD)
					return EBREventTime.Add(EBRPeriod);
				else
					return new DateTime(9999,12,31);
			}

			set
			{
				EBRPeriod = new TimeSpan(value.Ticks - this.FPPool.ClusterTime.Ticks);
			}
		}

		/**
		 * The time of the EBR Event
		 * 
		 */
		public DateTime EBREventTime
		{
			get
			{
                StringBuilder outString = new StringBuilder();
                FPInt bufSize = 0;
                FPInt len = 0;

                do
                {
                    bufSize += FPMisc.STRING_BUFFER_SIZE;
                    len = bufSize;
                    outString.EnsureCapacity((int)bufSize);
                    FPApi.Clip.GetEBREventTime(this, outString, ref len);
                } while (len > bufSize);

                return FPMisc.GetDateTime(outString.ToString());
			}
		}


		/* Advised by SDK team to not implement this as it is too heavily based on Cluster
		 * edition and C* versions.
		 * 
		public bool EligibleForDeletion
		{
			get
			{
				if (RetentionExpiry < FPPool.ClusterTime)
					return true;
				else
					// Not true for Basic mode cluster! 3.0 API adds querying for this capability so fix.
					return false;
			}
		}
		*/

		/**
		 * The modification state of the clip. See API Guide: FPClip_IsModified
		 *
		 */
		public bool Modified
		{
			get
			{
				if (FPApi.Clip.IsModified(this) == FPBool.True)
					return true;
				else
					return false;
			}
		}


		/**
		 * The next tag within this Clip. See API Guide: FPClip_FetchNext
		 *
		 */
		public FPTag NextTag 
		{
			get
			{
                FPTag tag;
                FPTagRef t = FPApi.Clip.FetchNext(this);

                if (!SDKObjects.Contains(t))
                    tag = new FPTag(t);
                else
                    tag = (FPTag) SDKObjects[t];

                return tag;
			}
		}

		/**
		 * Write the Clip to the Centera. See API Guide: FPClip_Write
		 *
		 * @return String representing the ID of the clip written to the Centera.
		 */
		public String Write() 
		{
			StringBuilder outClipID = new StringBuilder(FPMisc.STRING_BUFFER_SIZE);

			FPApi.Clip.Write(this, outClipID);
			return outClipID.ToString();
		}

		/**
		 * Read this Clip in raw form to a Stream. See API Guide: FPClip_RawRead
		 *
		 * @param inStream	The Stream object to read the clip into.
		 */
		public void RawRead(FPStream inStream) 
		{
			FPApi.Clip.RawRead(this, inStream);
		}

		/**
		 * Set a description attribute in the Clip level metadata. See API Guide: FPClip_SetDescriptionAttribute
		 * 
		 * @param inAttrName	The name of the description attribute.
		 * @param inAttrValue	The value for the description attribute.
		 */
		public void SetAttribute(String inAttrName,  String inAttrValue) 
		{
			FPApi.Clip.SetDescriptionAttribute(this, inAttrName, inAttrValue);
		}

		/**
		 * Remove a description attribute from the Clip level metadata. See API Guide: FPClip_SetDescriptionAttribute
		 * 
		 * @param inAttrName	The name of the description attribute.
		 */
		public void RemoveAttribute(String inAttrName) 
		{
			FPApi.Clip.RemoveDescriptionAttribute(this, inAttrName);
		}

		/**
		 * Get a description attribute from the Clip level metadata. See API Guide: FPClip_GetDescriptionAttribute
		 * 
		 * @param inAttrName	The name of the description attribute.
		 * @return The String value of the Description attribute.
		 */
		public String GetAttribute(String inAttrName)
		{
            byte[] outString;
			FPInt bufSize = 0;
			FPInt len = 0;

			do
			{
				bufSize += FPMisc.STRING_BUFFER_SIZE;
				len = bufSize;
				outString = new byte[(int) bufSize];
				FPApi.Clip.GetDescriptionAttribute(this, inAttrName, ref outString, ref len);
			} while (len > bufSize);

            return Encoding.UTF8.GetString(outString, 0, (int)len - 1);

		}

		/**
		 * Get a description attribute from the Clip level metadata using the index of the
		 * attribute. See API Guide: FPClip_GetDescriptionAttributeIndex
		 * 
		 * @param inIndex		The index of the attribute to retrieve.
		 * @return The FPAttribute at the specifed index position.
		 */
		public FPAttribute GetAttributeByIndex(int inIndex) 
		{
            byte[] nameString;
            byte[] valueString;
			FPInt nameSize = 0, nameLen = 0, valSize = 0, valLen = 0;
			
			do
			{
				nameSize += FPMisc.STRING_BUFFER_SIZE;
				nameLen = nameSize;
				nameString = new byte[(int) nameSize];

				valSize += FPMisc.STRING_BUFFER_SIZE;
				valLen = valSize;
				valueString = new byte[(int) valSize];

				FPApi.Clip.GetDescriptionAttributeIndex(this, (FPInt) inIndex, ref nameString, ref nameLen, ref valueString, ref valLen);
			} while (nameLen > nameSize || valLen > valSize);

			return new FPAttribute(Encoding.UTF8.GetString(nameString, 0, (int)nameLen - 1), Encoding.UTF8.GetString(valueString, 0, (int)valLen - 1));

		}

		/**
		 * The number of Description Attributes associated with this clip.
		 * 
		 */
		public int NumAttributes
		{
			get
			{
				return (int) FPApi.Clip.GetNumDescriptionAttributes(this);;
			}
		}


		/**
		 * The retention class name associated with this clip.
		 * 
		 */
		public String RetentionClassName
		{
			get
			{
                byte[] outString;
				FPInt bufSize = 0;
				FPInt len = 0;

				do
				{
					bufSize += FPMisc.STRING_BUFFER_SIZE;
					len = bufSize;
					outString = new byte[(int) bufSize];
					FPApi.Clip.GetRetentionClassName(this, ref outString, ref len);
				} while (len > bufSize);

                return Encoding.UTF8.GetString(outString, 0, (int)len - 1);
			}

			set
			{
				FPRetentionClass rcRef = thePool.RetentionClasses.GetClass(value);
				FPApi.Clip.SetRetentionClass(this, rcRef);
			}
		}

		/**
		 * The retention class associated with this clip.
		 * 
		 */
		public FPRetentionClass FPRetentionClass
		{
			get
			{
				FPRetentionClass	rcRef = thePool.RetentionClasses.GetClass(RetentionClassName);
				
				return rcRef;
			}
			set
			{
				FPApi.Clip.SetRetentionClass(this, value);
			}
		}

		/**
		 * Remove any RetentionClass associated with the Clip. See API Guide: FPClip_RemoveRetentionClass
		 * 
		 */
		public void RemoveRetentionClass() 
		{
			FPApi.Clip.RemoveRetentionClass(this);
		}

		
		/**
		 * Validates that the Retention Class set on the Clip exists in the RetentionClassList supplied.
		 * See API Guide: FPClip_ValidateRetentionClass
		 * 
		 * @return Boolean indicating Valid or Invalid Retention Class.
		 */
		public bool ValidateRetentionClass(FPRetentionClassCollection coll) 
		{

			if (coll.ValidateClass(this.Name))
				return true;
			else
				return false;
		}
		
		/**
		 * Validates that the Retention Class set on the Clip exists in the Pool for this Clip.
		 * 
		 * @return Boolean indicating Valid or Invalid Retention Class.
		 */
		public bool ValidateRetentionClass() 
		{
			if (FPApi.Clip.ValidateRetentionClass(FPApi.Pool.GetRetentionClassContext(thePool), this) == FPBool.True)
				return true;
			else
				return false;
		}
		
		
		/**
		 * Returns the (non-ASCII based) Canonical form of a Clip ID. Used for translating Clip IDs between platforms that use
		 * different character sets. See API Guide: FPClip_GetCanonicalForm
		 * 
		 * @param inClipID The ClipID to convert to Canonical Form.
		 * @param bufSize The size of the buffer to allocate to hold the Canonical Form.
		 * @return The Canonical Form of the clip.
		 */
		public static byte[] GetCanonicalFormat(String inClipID, int bufSize) 
		{
			byte[] outClipID = new byte[bufSize];

			FPApi.Clip.GetCanonicalFormat(inClipID, outClipID);

			return outClipID;
		}

		/**
		 * Returns the (non-ASCII based) Canonical form of the ID of this Clip. Used for translating Clip IDs between platforms that use
		 * different character sets. See API Guide: FPClip_GetCanonicalForm
		 * 
		 * @param bufSize The size of the buffer to allocate to hold the Canonical Form.
		 * @return The Canonical Form of the clip.
		 */
		public byte[] GetCanonicalFormat(int bufSize) 
		{
			byte[] outClipID = new byte[bufSize];

			FPApi.Clip.GetCanonicalFormat(ClipID, outClipID);

			return outClipID;
		}
		
		/**
		 * Returns the (non-ASCII based) Canonical form of a Clip ID. Used for translating Clip IDs between
		 * platforms that use different character sets. Buffer of FPMisc.STRING_BUFFER_SIZE is used.
		 * See API Guide: FPClip_GetCanonicalForm
		 * 
		 * @param inClipID A standard String format Clip ID.
		 * @return The Canonical Form of the clip.
		 */
		public static byte[] GetCanonicalFormat(String inClipID)
		{
			return GetCanonicalFormat(inClipID, FPMisc.STRING_BUFFER_SIZE);
		}

		/**
		 * The (non-ASCII based) Canonical form of the ClipID of this Clip. Used for translating Clip IDs between
		 * platforms that use different character sets. Buffer of FPMisc.STRING_BUFFER_SIZE is used.
		 * See API Guide: FPClip_GetCanonicalForm
		 * 
		 */
		public byte[] CanonicalForm
		{
			get
			{
				return GetCanonicalFormat(ClipID, FPMisc.STRING_BUFFER_SIZE);
			}
		}
			
		/**
		 * Get the standard (ASCII based) form of a Clip ID in Canonical Form. Used for translating Clip IDs between
		 * platforms that use different character sets. Buffer of FPMisc.STRING_BUFFER_SIZE is used.
		 * See API Guide: FPClip_GetStringForm
		 * 
		 * @param inClipID A Clip ID in Canonical Form.
		 * @return The Canonical Form of the clip.
		 */
		public static String GetStringFormat(byte[] inClipID) 
		{
			StringBuilder clipString = new StringBuilder(FPMisc.STRING_BUFFER_SIZE);

			FPApi.Clip.GetStringFormat(inClipID, clipString);

			return clipString.ToString();
		}


		private FPTagCollection myTags = null;

		/**
		 * An ArrayList containing the Tag objects on a Clip. This should ONLY be used for reading of the Tags
		 * on a finalized clip - modification and creation are not supported.
		 */ 
		public FPTagCollection Tags
		{
			get
			{
				if (myTags == null)
					myTags = new FPTagCollection(this);

				return myTags;
			}
		}

		public FPAttributeCollection myAttributes = null;

		/**
		 * An ArrayList containing the DescriptionAttribute objects on a Clip. This should ONLY be used for reading of the Tags
		 * on a finalized clip - modification and creation are not supported.
		 */ 
		public FPAttributeCollection Attributes
		{
			get
			{
				if (myAttributes == null)
					myAttributes = new FPAttributeCollection(this);

				return myAttributes;
			}
		}

		public bool OnHold
		{
			get
			{
				if (FPApi.Clip.GetRetentionHold(this) == FPBool.True)
					return true;
				else
					return false;
			}
		}

		public void SetRetentionHold(bool holdState, string holdID)
		{
			if (holdState)
				FPApi.Clip.SetRetentionHold(this, FPBool.True, holdID);
			else
				FPApi.Clip.SetRetentionHold(this, FPBool.False, holdID);
		}


		public TimeSpan EBRPeriod
		{
			get
			{
				int period = (int) FPApi.Clip.GetEBRPeriod(this);

				if (period < 0)
				{
                    return new TimeSpan(period);
				}
				else
				{
					return new TimeSpan(0, 0, period);
				}
			}

			set
			{
				if (value.Ticks > 0)
				{
					FPApi.Clip.EnableEBRWithPeriod(this, (FPLong) value.TotalSeconds);
				}
				else
				{
					FPApi.Clip.EnableEBRWithPeriod(this, (FPLong) value.Ticks);
				}
			}
		}

		public String EBRClassName
		{
			get
			{
				return FPApi.Clip.GetEBRClassName(this);
			}
		}

		public FPRetentionClass EBRClass
		{
			set
			{
				FPApi.Clip.EnableEBRWithClass(this, value);
			}
		}
				
		public bool EBREnabled
		{
			get
			{
				if (FPApi.Clip.IsEBREnabled(this) == FPBool.True)
					return true;
				else
					return false;
			}
		}

		public void TriggerEBREvent()
		{
			FPApi.Clip.TriggerEBREvent(this);
		}
		
		public TimeSpan TriggerEBRPeriod
		{
			set
			{
				if (value.Ticks != FPMisc.INFINITE_RETENTION_PERIOD)
				{
					FPApi.Clip.TriggerEBREventWithPeriod(this, (FPLong) value.TotalSeconds);
				}
				else
				{
					FPApi.Clip.TriggerEBREventWithPeriod(this, (FPLong) value.Ticks);
				}
			}
		}
		
		public FPRetentionClass TriggerEBRClass
		{
			set
			{
				FPApi.Clip.TriggerEBREventWithClass(this, value);
			}
		}
	}  // end of class Clip

}
