using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyDatabase
{
    public struct DatabaseField
    {
        public string Name { get; set; }
        public FieldType Type { get; set; }
        //public int FieldSize { get; set; }
        public object Value { get; set; }

        public DatabaseField(string name, FieldType type) : this(name, type, null)
        {

        }

        public DatabaseField(string name, FieldType type, int value) : this(name, type, (object)value)
        {

        }

        public DatabaseField(string name, FieldType type, float value) : this(name, type, (object)value)
        {

        }

        public DatabaseField(string name, FieldType type, bool value) : this(name, type, (object)value)
        {

        }

        public DatabaseField(string name, FieldType type, string value) : this(name, type, (object)value)
        {

        }

        public DatabaseField(string name, FieldType type, object vaule)
        {
            Name = name;
            Type = type;
            Value = vaule;
        }

        public enum FieldType
        {
            INTEGER,
            FLOAT,
            REAL,
            NUMRIC,
            BOOLEAN,
            TIME,
            DATE,
            TIMESTAMP,
            VARCHAR,
            NVARCHAR,
            TEXT,
            BLOB
        }
    }
}
