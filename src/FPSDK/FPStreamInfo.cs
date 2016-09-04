using System.Runtime.InteropServices;

namespace EMC.Centera.SDK
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct FPStreamInfo
    {

        public short	mVersion ;         // current version of FPStreamInfo
        public void* 	mUserData ;        // application-specific data, untouched by Generic Streams

        public long		mStreamPos ;       // current position
        public long		mMarkerPos ;       // position of marker
        public long		mStreamLen ;       // length of stream, if known, else -1

        public byte		mAtEOF ;           // have we reached the end of the stream?
        public byte		mReadFlag ;        // indicator for the direction of the transfer

        public void*	 mBuffer ;          // databuffer supplied by application
        public long		mTransferLen ;     // number of bytes actually transferred
    }
}