﻿using System.Collections.Generic;

namespace antlr_cs
{
    /// <summary>
    /// In a real scenario DataRepository would contain methods to access the data in the proper cell;
    /// in our example is just a Dictionary with some keys and numbers.
    /// </summary>
    public class DataRepository
    {
        private readonly Dictionary<string, int> _data = new Dictionary<string, int>();

        public DataRepository()
        {
            _data.Add("A1", 10);
            _data.Add("B2", 33);
        }

        public int this[string id] => _data.ContainsKey(id) ? _data[id] : 0;
    }
}
