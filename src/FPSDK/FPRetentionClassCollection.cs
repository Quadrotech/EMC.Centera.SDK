using System;
using System.Collections;
using EMC.Centera.SDK.FPTypes;

namespace EMC.Centera.SDK
{
    public class FPRetentionClassCollection:ArrayList
    {
        private FPRetentionClassContextRef myContext;

        internal FPRetentionClassContextRef RCContext
        {
            get
            {
                return myContext;
            }
            set
            {
                myContext = value;
            }
        }

        internal FPRetentionClassCollection(FPPool p) 
        { 
            RCContext = Native.Pool.GetRetentionClassContext(p);

            FPRetentionClassRef c = Native.RetentionClassContext.GetFirstClass(RCContext);

            while(c != 0)
            {
                Add(new FPRetentionClass(c));
                c = Native.RetentionClassContext.GetNextClass(RCContext);
            }
        }	
        /**
		 * Get the Period associated with a named RetentionClass in the RetentionClassList.
		 *
		 * @param	inName	The name of the RetentionClass in the list to get the period for.
		 * @return	The period (as a TimeSpan) associated with the named RetentionClass.
		 */
        public TimeSpan GetPeriod(string inName) 
        {
            if (ValidateClass(inName))
            {
                FPRetentionClassRef theRef = Native.RetentionClassContext.GetNamedClass(RCContext, inName);
                return new TimeSpan(0, 0, (int) Native.RetentionClass.GetPeriod(theRef));
            }
            else
            {
                return new TimeSpan(0);
            }
        }

        /**
		 * Get a named RetentionClass from the RetentionClassList. The class must exist or an exception is thrown.
		 * See API Guide: FPRetentionClassContext_GetNamedClass
		 * 
		 * @param	inName	The name of the RetentionClass to get from the list.
		 * @return	The named RetentionClass
		 */
        public FPRetentionClass GetClass(string inName) 
        {
            FPRetentionClass retVal = null;
            
            foreach (FPRetentionClass rc in this)
            {
                if (rc.Name.CompareTo(inName) == 0)
                {
                    retVal = rc;
                    break;
                }
            }

            if (retVal == null)
                throw new FPLibraryException("Invalid Retention Class name", -10019);
            else
                return retVal;
        }

        /**
		 * Check for the existence of a named RetentionClass in the RetentionClassList.
		 * See API Guide: FPRetentionClassContext_GetNamedClass
		 * 
		 * @param	inName	The name of the RetentionClass to get from the list.
		 * @return	The named RetentionClass
		 */
        public bool ValidateClass(string inName)
        {
            foreach (FPRetentionClass rc in this)
            {
                if (rc.Name.CompareTo(inName) == 0)
                    return true;
            }

            return false;
        }

    }
}