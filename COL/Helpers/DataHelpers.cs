using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace COL.Helpers
{
    public class DataHelpers
    {
        public static T GetDataContent<T>(string filePath)
        {
            T result = default(T);
            using (StreamReader streamReader = new StreamReader(filePath))
            {
                if (streamReader == null)
                    return default(T);
                using (XmlReader reader = XmlReader.Create(streamReader))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(T));
                    result = (T)serializer.Deserialize(reader);
                }
            }
            return result;
        }
    }
}
