﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;

namespace tso.files.formats.otf
{
    public class OTF
    {

        /// <summary>
        /// Constructs an OTF instance from a filepath.
        /// </summary>
        /// <param name="filepath">Path to the OTF.</param>
        public OTF(string filepath)
        {
            using (var stream = File.OpenRead(filepath))
            {
                this.Read(stream);
            }
        }

        public OTF()
        {
            //you can also create empty OTFs!
        }

        public OTFTable[] Tables;

        public OTFTable GetTable(int id){
            return Tables.FirstOrDefault(x => x.ID == id);
        }

        public void Read(Stream stream){
            var doc = new XmlDocument();
            doc.Load(stream);

            var tables = doc.GetElementsByTagName("T");
            Tables = new OTFTable[tables.Count];

            for (var i = 0; i < tables.Count; i++){
                var table = tables.Item(i);
                var tableEntry = new OTFTable();
                tableEntry.ID = int.Parse(table.Attributes["i"].Value);
                tableEntry.Name = table.Attributes["n"].Value;

                var numKeys = table.ChildNodes.Count;
                tableEntry.Keys = new OTFTableKey[numKeys];

                for (var x = 0; x < numKeys; x++){
                    var key = table.ChildNodes[x];
                    var keyEntry = new OTFTableKey();
                    keyEntry.ID = int.Parse(key.Attributes["i"].Value);
                    keyEntry.Label = key.Attributes["l"].Value;
                    keyEntry.Value = int.Parse(key.Attributes["v"].Value);
                    tableEntry.Keys[x] = keyEntry;
                }
                Tables[i] = tableEntry;
            }
        }
    }

    public class OTFTable{
        public int ID;
        public string Name;
        public OTFTableKey[] Keys;

        public OTFTableKey GetKey(int id){
            return Keys.FirstOrDefault(x => x.ID == id);
        }
    }

    public class OTFTableKey {
        public int ID;
        public string Label;
        public int Value;
    }
}
