using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    class builder
    {
        internal static void BuildNewImportedObject(ref ImportedObject importedObject, string[] values)
        {
           

                importedObject.Type = values[0];
            if (values.Length > 1)
            {
                importedObject.Name = values[1];
                importedObject.Schema = values[2];
                importedObject.ParentName = values[3];
                importedObject.ParentType = values[4];
                importedObject.DataType = values[5];
                
            }
           if (values.Length>6)
                importedObject.IsNullable = values[6];


        }
    }
}
