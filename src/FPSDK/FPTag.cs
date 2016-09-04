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
	 * An object representing an FPTagRef.
	 * @author Graham Stuart
	 * @version
	 */
	public class FPTag : FPObject, IFPTag
	{
		internal FPTagRef theTag;

		/**
		 * Implicit conversion of FPTag to FPTagRef
		 * 
		 * @param t	An FPTag object.
		 * @return The FPTagRef associated with the FPTag.
		 */
		static public implicit operator FPTagRef(FPTag t) 
		{
			return t.theTag;
		}

		/**
		 * Implicit conversion of FPTagRef to FPTag.
		 * 
		 * @param tagRef	An FPTagRef.
		 * @return The FPTag object associated with the FPTagRef.
		 */
		static public implicit operator FPTag(FPTagRef tagRef) 
		{
			// Find the relevant Tag object in the hastable for this FPTagRef
			FPTag tagObject = null;

			if (SDKObjects.Contains(tagRef))
			{
				tagObject = (FPTag) SDKObjects[tagRef];
			}
            else
            {
                throw new FPLibraryException("FPTagRef is not asscociated with an FPTag object", FPMisc.WRONG_REFERENCE_ERR);
            }

			
			return tagObject;
		}

		/**
		 * Create a new Tag. See API Guide: FPTag_Create
		 * 
		 * @param inParent	The parent Tag for the new Tag.
		 * @param inName	The name of the new Tag.
		 */
		public FPTag(FPTag inParent, String inName) 
		{            
			theTag = FPApi.Tag.Create(inParent, inName);
			AddObject(theTag, this);
        }

		/**
		 * Create a new Tag using an existing FPTagRef. See API Guide: FPTag_Create
		 * 
		 * @param t	Existing FPTagRef.
		 */
		internal FPTag(FPTagRef t)
		{
			theTag = t;
			AddObject(theTag, this);
		}

		/**
		 * Explicitly Close the FPTagRef. See API Guide: FPTag_Close
		 * 
		 */
		public override void Close() 
		{
			if (theTag != 0)
			{
				RemoveObject(theTag);
                try
                {
                    FPApi.Tag.Close(theTag);
                }
                catch (FPLibraryException e)
                {
                    FPLogger.ConsoleMessage("\nProblem closing tag " + e);
                }
                catch (NullReferenceException)
                {
                    FPLogger.ConsoleMessage("\nNull reference");
                }

				theTag = 0;
			}
		}
		
		/**
		 * Create a copy of this Tag with under a new parent Tag. See API Guide: FPTag_Copy
		 * 
		 * @param inNewParent	The parent tag to use for the new (copy) Tag.
		 * @param inOptions		Options for creating the copy Tag.
		 * @return The new Tag.
		 */
		public FPTag Copy(FPTag inNewParent, int inOptions) 
		{
			return new FPTag(FPApi.Tag.Copy(this, inNewParent, (FPInt) inOptions));
		}

		/**
		 * Get the Clip that this Tag is associated with. See API Guide: FPTag_GetClipRef
		 * 
		 */
		public FPClip FPClip 
		{
			get
			{
				return FPApi.Tag.GetClipRef(this);
			}
		}

		/**
		 * The next sibling Tag that this Tag is associated with. The Clip must have been opened in TREE mode.
		 * See API Guide: FPTag_GetSibling
		 * 
		 */
		public FPTag NextSibling
		{
			get
			{
				FPTagRef tagRef = FPApi.Tag.GetSibling(this);

                if (tagRef != 0)
                    return new FPTag(tagRef);
                else
                    return null;
			}
		}

		/**
		 * The previous sibling Tag that this Tag is associated with. The Clip must have been opened in TREE mode.
		 * See API Guide: FPTag_GetPrevSibling
		 *  
		 */
		public FPTag PrevSibling 
		{
			get
			{
                FPTagRef tagRef = FPApi.Tag.GetPrevSibling(this);

                if (tagRef != 0)
                    return new FPTag(tagRef);
                else
                    return null;
			}
		}

		/**
		 * The first child Tag that this Tag is associated with. The Clip must have been opened in TREE mode.
		 * See API Guide: FPTag_GetFirstChild
		 *  
		 */
		public FPTag FirstChild
		{
			get
			{
                FPTagRef tagRef = FPApi.Tag.GetFirstChild(this);

                if (tagRef != 0)
                {
                    return new FPTag(tagRef);
                }
                else
                    return null;
			}
		}

		/**
		 * The parent Tag of this Tag. The Clip must have been opened in TREE mode.
		 * See API Guide: FPTag_GetParent
		 *  
		 */
		public FPTag Parent 
		{
			get
			{
                FPTagRef tagRef = FPApi.Tag.GetParent(this);

                if (tagRef != 0)
                    return FPApi.Tag.GetParent(this);
                else
                    return null;
            }
		}

		/**
		 * Remove this Tag (and all its children) from the Clip it is associated with. All the Tags are Disposed.
		 * The Clip must have been opened in TREE mode. See API Guide: FPTag_Delete
		 * 
		 */
		public void Delete() 
		{
           FPApi.Tag.Delete(this);
           RemoveObject(theTag);
           theTag = 0;
           this.Dispose();
		}

		/**
		 * The name of the Tag. Default FPMisc.STRING_BUFFER_SIZE is used for the buffer.
		 * See API Guide: FPTag_GetTagName
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

					FPApi.Tag.GetTagName(this, ref outString, ref len);
				} while (len > bufSize);

                return Encoding.UTF8.GetString(outString, 0, (int)len - 1);
			}
		}

		/**
		 * Get a String representation of this Tag - the Tag name.
		 * 
		 * @return The String representation of this object.
		 */
		public override string ToString()
		{
			return Name;
		}

		/**
		 * Set a String attribute on this Tag. See API Guide: FPTag_SetStringAttribute
		 *
		 * @param	inAttrName	The Attribute Name to be set,
		 * @param	inAttrValue	The Value to be set.
		 */
		public void SetAttribute(String inAttrName,  String inAttrValue) 
		{
			FPApi.Tag.SetStringAttribute(this, inAttrName, inAttrValue);
		}

		/**
		 * Set a Long attribute on this Tag. See API Guide: FPTag_SetLongAttribute
		 *
		 * @param	inAttrName	The Attribute Name to be set,
		 * @param	inAttrValue	The Value to be set.
		 */
		public void SetAttribute(String inAttrName, long inAttrValue) 
		{
			FPApi.Tag.SetLongAttribute(this, inAttrName, (FPLong) inAttrValue);
		}

		/**
		 * Set a Boolean attribute on this Tag. See API Guide: FPTag_SetBooleanAttrobute
		 *
		 * @param	inAttrName	The Attribute Name to be set,
		 * @param	inAttrValue	The Value to be set.
		 */
		public void SetAttribute(String inAttrName, bool inAttrValue) 
		{
			if (inAttrValue)
				FPApi.Tag.SetBoolAttribute(this, inAttrName, FPBool.True);
			else
				FPApi.Tag.SetBoolAttribute(this, inAttrName, FPBool.False);
		}

		/**
		 * Get the value of a String attribute from this Tag. See API Guide: FPTag_GetStringAttribute
		 *
		 * @param	inAttrName	The Attribute Name to be retrieved,
		 * @return	The String value of the attribute.
		 */
		public String GetStringAttribute(String inAttrName) 
		{
            byte[] outString;
			FPInt bufSize = 0;
			FPInt len = 0;

			do
			{
				bufSize += FPMisc.STRING_BUFFER_SIZE;
				len = bufSize;
                outString = new byte[(int) bufSize];

                FPApi.Tag.GetStringAttribute(this, inAttrName, ref outString, ref len);
			} while (len > bufSize);

            return Encoding.UTF8.GetString(outString, 0, (int) len - 1);
		}


		/**
		 * Get the value of a Long attribute from this Tag. See API Guide: FPTag_GetLongAttribute
		 *
		 * @param	inAttrName	The Attribute Name to be retrieved,
		 * @return	The Long value of the attribute.
		 */
		public long GetLongAttribute(String inAttrName) 
		{
			return (long) FPApi.Tag.GetLongAttribute(this, inAttrName);
		}

		/**
		 * Get the value of a Boolean attribute from this Tag. See API Guide: FPTag_GetBoolAttribute
		 *
		 * @param	inAttrName	The Attribute Name to be retrieved,
		 * @return	The Boolean value of the attribute.
		 */
		public bool GetBoolAttribute(String inAttrName) 
		{
			if (FPApi.Tag.GetBoolAttribute(this, inAttrName) == FPBool.True)
				return true;
			else
				return false;
		}

		/**
		 * Remove an attribute from this Tag. See API Guide: FPTag_RemoveAttribute
		 *
		 * @param	inAttrName	The Attribute Name to be retrieved,
		 */
		public void RemoveAttribute(String inAttrName) 
		{
			FPApi.Tag.RemoveAttribute(this, inAttrName);
		}

		/**
		 * The number of attributes on this Tag. See API Guide: FPTag_GetNumAttributes
		 *
		 */
		public int NumAttributes 
		{
			get
			{
				return (int) FPApi.Tag.GetNumAttributes(this);
			}
		}

		/**
		 * Returns an attribute name and value from this Tag using the given index number.
		 * See API Guide: FPTag_GetIndexAttribute
		 * 
		 * @param	inIndex		The index of the attribute to retrieve.
		 * @return	An FPAttribute contining the name-value string pair.
		 */
		public FPAttribute GetAttributeByIndex(int inIndex) 
		{
            byte[] nameString;
			byte[] valString;
			FPInt nameSize = 0, nameLen = 0, valSize = 0, valLen = 0;
			
			do
			{
				nameSize += FPMisc.STRING_BUFFER_SIZE;
				nameLen = nameSize;
				nameString = new byte[(int) nameSize];

				valSize += FPMisc.STRING_BUFFER_SIZE;
				valLen = valSize;
				valString = new byte[(int) valSize];

				FPApi.Tag.GetIndexAttribute(this, (FPInt) inIndex, ref nameString, ref nameLen, ref valString, ref valLen);
			} while (nameLen > nameSize || valLen > valSize);

            return new FPAttribute(Encoding.UTF8.GetString(nameString, 0, (int)nameLen - 1), Encoding.UTF8.GetString(valString, 0, (int) valLen - 1));

		}

		/**
		 * The size of the blob on this Tag. See API Guide: FPTag_GetBlobSize
		 * 
		 */
		public long BlobSize
		{
			get
			{
				return (long) FPApi.Tag.GetBlobSize(this);
			}
		}

		/**
		 * Write the contents of a Stream to the Blob on this Tag using DEFAULT_OPTIONS.
		 * See API Guide: FPTag_BlobWrite
		 *
		 * @param	inStream	The Stream from which to read the blob that is to be written to the Tag.
		 */
		public void BlobWrite(FPStream inStream) 
		{
			BlobWrite(inStream, FPMisc.OPTION_DEFAULT_OPTIONS);
		}

		/**
		 * Write the contents of a Stream to the Blob on this Tag.
		 * See API Guide: FPTag_BlobWrite
		 *
		 * @param	inStream	The Stream from which to read the blob that is to be written to the Tag.
		 * @param	inOptions	The options avilable on the write.
		 */
		public void BlobWrite(FPStream inStream, long inOptions) 
		{
			FPApi.Tag.BlobWrite(this, inStream, (FPLong) inOptions);
		}

		/**
		 * Read a portion of the Blob on this Tag to a Stream using DEFAULT_OPTIONS.
		 * See API Guide: FPTag_BlobReadPartial
		 *
		 * @param	inStream		The Stream to write the Blob to.
		 * @param	inSequenceID	The sequenceID of this fragment of the blob.
		 */
		public void BlobWritePartial(FPStream inStream, long inSequenceID) 
		{
			BlobWritePartial(inStream, inSequenceID, FPMisc.OPTION_DEFAULT_OPTIONS);
		}

		/**
		 * Read a portion of the Blob on this Tag to a Stream. See API Guide: FPTag_BlobReadPartial
		 *
		 * @param	inStream		The Stream to write the Blob to.
		 * @param	inSequenceID	The sequenceID of this fragment of the blob.
		 * @param	inOptions		The options avilable on the read.
		 */
		public void BlobWritePartial(FPStream inStream, long inSequenceID, long inOptions) 
		{
			FPApi.Tag.BlobWritePartial(this, inStream, (FPLong) inOptions, (FPLong) inSequenceID);
		}

		/**
		 * Read the Blob on this Tag to a Stream using DEFAULT_OPTIONS.
		 * See API Guide: FPTag_BlobRead
		 *
		 * @param	inStream	The Stream to write the Blob to.
		 */
		public void BlobRead(FPStream inStream) 
		{
			BlobRead(inStream, FPMisc.OPTION_DEFAULT_OPTIONS);
		}

		/**
		 * Read the Blob on this Tag to a Stream.
		 * See API Guide: FPTag_BlobRead
		 *
		 * @param	inStream	The Stream to write the Blob to.
		 * @param	inOptions	The options avilable on the read.
		 */
		public void BlobRead(FPStream inStream, long inOptions) 
		{
			FPApi.Tag.BlobRead(this, inStream, (FPLong) inOptions);
		}

		/**
		 * Read a portion of the Blob on this Tag to a Stream using DEFAULT_OPTIONS.
		 * See API Guide: FPTag_BlobReadPartial
		 *
		 * @param	inStream		The Stream to write the Blob to.
		 * @param	inOffset		The offset position in the blob from where the read is to start.
		 * @param	inReadLength	The offset position in the blob from where the read is to start.
		 */
		public void BlobReadPartial(FPStream inStream, long inOffset, long inReadLength) 
		{
			BlobReadPartial(inStream, inOffset, inReadLength, FPMisc.OPTION_DEFAULT_OPTIONS);
		}

		/**
		 * Read a portion of the Blob on this Tag to a Stream. See API Guide: FPTag_BlobReadPartial
		 *
		 * @param	inStream		The Stream to write the Blob to.
		 * @param	inOffset		The offset position in the blob from where the read is to start.
		 * @param	inReadLength	The offset position in the blob from where the read is to start.
		 * @param	inOptions		The options avilable on the read.
		 */
		public void BlobReadPartial(FPStream inStream, long inOffset, long inReadLength, long inOptions) 
		{
			FPApi.Tag.BlobReadPartial(this, inStream, (FPLong) inOffset, (FPLong) inReadLength, (FPLong) inOptions);
		}

		/**
		 * The status of the Blob on this Tag: 1 for exists and is OK, 0 for exists but not available,
		 * -1 for no blob. See API Guide: FPTag_BlobExists
		 *
		 */
		public int BlobStatus
		{
			get
			{
				return (int) FPApi.Tag.BlobExists(this);
			}
		}

		private FPAttributeCollection myAttributes = null;

		/**
		 * An ArrayList containing the Attributes on the Tag. This should ONLY be used for reading of the Attributes
		 * on a finalized Tag - modification and creation are not supported.
		 *
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

	}

	/** 
	 * A collection of Tags on a Clip..
	 * @author Graham Stuart
	 * @version
	 */
	public class FPTagCollection:ArrayList
	{
		internal FPTagCollection(FPClip c) 
		{ 
			FPTag t;

			for (int i = 0; i < c.NumTags; i++)
			{
				t = c.NextTag;
				this.Add(t);
			}
			
		}
	}

}