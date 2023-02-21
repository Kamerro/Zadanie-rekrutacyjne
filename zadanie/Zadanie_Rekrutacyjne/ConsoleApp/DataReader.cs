namespace ConsoleApp
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    public class DataReader
    {
        IEnumerable<ImportedObject> ImportedObjects;

        //One function, one thing to do, no more.
        public void ImportAndPrintData(string fileToImport, bool printData = true)
        {
            ImportedObjects = new List<ImportedObject>() { new ImportedObject() };

            var streamReader = new StreamReader(fileToImport);
            var importedLines = new List<string>();
            while (!streamReader.EndOfStream)
            {
                var line = streamReader.ReadLine();
                importedLines.Add(line);
            }

            for (int currentLine = 0; currentLine < importedLines.Count; currentLine++)
            {
                BuildImportedObjectFromLine(ReadImportedLine(importedLines[currentLine]));
            }
        }
        public void AssignNumberOfChildren()
        {
            for (int i = 0; i < ImportedObjects.Count(); i++)
            {
                var importedObject = ImportedObjects.ToArray()[i];
                foreach (var impObj in ImportedObjects)
                {
                    if (impObj.ParentType == importedObject.Type)
                    {
                        if (impObj.ParentName == importedObject.Name)
                        {
                            importedObject.NumberOfChildren = 1 + importedObject.NumberOfChildren;
                        }
                    }
                }
            }
        }
        public void PrintAllDatabasesTable()
        {
            foreach (var database in ImportedObjects)
            {
                if (database.Type == "Table")
                {
                    Console.WriteLine($"Database '{database.Name}' ({database.NumberOfChildren} tables)");

                    // print all database's tables
                    foreach (var table in ImportedObjects)
                    {
                      if (table.ParentName == database.Name)
                      {
                          Console.WriteLine($"\tTable '{table.Schema}.{table.Name}' ({table.NumberOfChildren} columns)");
                          // print all table's columns
                          foreach (var column in ImportedObjects)
                          {
                              
                            if (column.ParentName == table.Name)
                            {
                                Console.WriteLine($"\t\tColumn '{column.Name}' with {column.DataType} data type {(column.IsNullable == "1" ? "accepts nulls" : "with no nulls")}");
                            }
                          }
                      }
                    }
                }
            }
        }
        public void ClearData()
        {
            foreach (var importedObject in ImportedObjects)
            {
                importedObject.clearName();
                importedObject.clearParentName();
                importedObject.clearParentType();
                importedObject.clearSchema();
                importedObject.clearType();
            }
        }
        private void BuildImportedObjectFromLine(string[] values)
        {
            var importedObject = new ImportedObject();
            builder.BuildNewImportedObject(ref importedObject, values);
            ((List<ImportedObject>)ImportedObjects).Add(importedObject);
        }

        private string[] ReadImportedLine(string line)
        {
            var values = line.Split(';');
            return values;
        }
    }

    class ImportedObject : ImportedObjectBaseClass
    {
        public string Schema { get; set; }
        public string ParentName { get; set; }
        public string ParentType { get; set; }
        public string DataType { get; set; }
        public string IsNullable { get; set; }
        public double NumberOfChildren { get; set; }
        public void clearType()
        {
            if(!String.IsNullOrEmpty(Type)) Type.Trim().Replace(" ", "").Replace(Environment.NewLine, "").ToUpper();
        }
        public void clearName()
        {
            if (!String.IsNullOrEmpty(Name)) Name.Trim().Replace(" ", "").Replace(Environment.NewLine, "");
        }
        public void clearSchema()
        {
            if (!String.IsNullOrEmpty(Schema)) Schema.Trim().Replace(" ", "").Replace(Environment.NewLine, "");
        }
        public void clearParentName()
        {
            if (!String.IsNullOrEmpty(ParentName)) ParentName.Trim().Replace(" ", "").Replace(Environment.NewLine, "");
        }
        public void clearParentType()
        {
            if (!String.IsNullOrEmpty(ParentType)) ParentType.Trim().Replace(" ", "").Replace(Environment.NewLine, "");
        }
    }
    class ImportedObjectBaseClass
    {
        public string Name { get; set; }
        public string Type { get; set; }
    }
}
