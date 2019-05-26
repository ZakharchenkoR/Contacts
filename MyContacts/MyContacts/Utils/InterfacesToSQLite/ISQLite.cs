using System;
using System.Collections.Generic;
using System.Text;

namespace MyContacts.Utils.InterfacesToSQLite
{
    public interface ISQLite
    {
        string GetDataBasePath(string filename);
    }
}
