using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ForLab
{
    public enum MethodResultStatus
    {
        Sucsess,
        Fail
    }
    static public class DAO
    {
        public static MethodResultStatus binWriteObjectToFile<Type>(Type serObject, string fileName)
        {
            try
            {
                BinaryFormatter bf = new BinaryFormatter();
                using (FileStream stream = new FileStream(fileName, FileMode.Create))
                {
                    bf.Serialize(stream, serObject);
                }
                return MethodResultStatus.Sucsess;
            }
            catch
            {
            }
            
            return MethodResultStatus.Fail;
        }
        public static Type binReadFileToObject<Type>(Type serObject, string fullPathFileName, out MethodResultStatus methodResultStatus)
        {
            BinaryFormatter bf = new BinaryFormatter();
            for (int i = 0; i < PmData.CountOfTries; i++)
            {
                try
                {
                    using (FileStream stream = new FileStream(fullPathFileName, FileMode.Open))
                    {
                        serObject = (Type)bf.Deserialize(stream);
                    }
                    methodResultStatus = MethodResultStatus.Sucsess;
                    return serObject;
                }
                catch
                {
                }
            }
            methodResultStatus = MethodResultStatus.Fail;
            return serObject;
        }

    }
}
