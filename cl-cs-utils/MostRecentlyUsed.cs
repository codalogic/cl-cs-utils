using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace cl_cs_utils
{
    public class MostRecentlyUsed : IEnumerable<string>
    {
        string file;
        int maxCount;
        List<string> list = new List<string>();

        public MostRecentlyUsed( string file, int maxCount = 20 )
        {
            this.file = file;
            this.maxCount = maxCount;

            Refresh();
        }

        public void Refresh()
        {
            if( File.Exists( file ) )
                foreach( string line in File.ReadLines( file ) )
                    list.Add( line.TrimEnd() );
        }

        // For: foreach( string s in mru )
        public IEnumerator<string> GetEnumerator()
        {
            return ((IEnumerable<string>)list).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<string>)list).GetEnumerator();
        }

        public int Count => list.Count;
    
        public string this[int index]
        {
            get { return list[index]; }
            set { list[index] = value; }
        }

        public bool Update( string newValue )
        {
            AmendList( newValue );
            return StoreList();
        }

        void AmendList( string newValue )
        {
            if( list.Count == 0 || newValue != list[0] )
            {
                list.Insert( 0, newValue );
                var maxIndex = Math.Min( list.Count, maxCount );
                for( int i = 1; i<maxIndex; ++i )
                {
                    if( list[i] == newValue )
                    {
                        list.RemoveAt( i );
                        break;
                    }
                }
            }
        }

        bool StoreList()
        {
            try
            {
                using( StreamWriter writer = new StreamWriter( file ) )
                {
                    foreach( string line in list )
                        writer.WriteLine( line );
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
