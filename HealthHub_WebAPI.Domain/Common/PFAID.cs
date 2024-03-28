using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthHub_WebAPI.Domain.DTO.Common
{
    public class PFAID
    {
        public byte[] UID;

        public PFAID()
        {
            UID = GenerateUUID();
        }

        public PFAID(byte[] ID)
        {
            UID = ID;
        }
        public PFAID(string ID)
        {
            UID = FromString(ID);
        }

        //It is used to generate UUID and return byte array of 18 length
        private byte[] GenerateUUID()
        {
            //The globally unique identifier is generated and 
            //is passed to the FromString method of the PFAUID class as input parameter.
            string strGuid = Guid.NewGuid().ToString();

            //When an input hexadecimal string is passed to this method, 
            //the given string value is splitted by character "-" and it is being stored in the string Array.
            string[] strGuidWithoutHyphens = strGuid.Split(new char[] { '-' });

            string strGuidReversed = "";


            //This piece of code is disabled since we can also reverse and 
            //concatenate the string array at a time using a single for loop.

            //string[] strGuidReversed = strGuidWithoutHyphens.Reverse().ToArray();
            //strHexaDecimal = "";
            //for (int i = 0; i < strGuidReversed.Length; i++)
            //    strHexaDecimal += strGuidReversed[i].ToString();


            //Here, the given string array is reversed and the HexaDecimal string value is passed further.
            for (int i = strGuidWithoutHyphens.Length - 1; i >= 0; i--)
                strGuidReversed += strGuidWithoutHyphens[i].ToString();

            //In order to convert the HexaDecimal string value to byte array of 16 bytes length, 
            //an object instance of Guid class is created with hexadecimal string as input.
            Guid myGuid = new Guid(strGuidReversed);

            //With ToByteArray method, the guid value is converted to byte array. 
            byte[] guidByteArray = myGuid.ToByteArray();

            //In PFA, 18 bytes space is required to store UUID in the database. 
            //The last 2 trailing bytes are used to indicate the database. 
            //Hence, a byte array is declared with 18 length.
            byte[] ouputByteArray = new byte[18];

            //To add 2 trailing bytes (zeros) at the end of byte array, 
            //a for loop is run to copy the value from one byte array to another. 
            //As the bytearray size is already 18, last 2 elements will be set to zero by default.
            for (int i = 0; i < guidByteArray.Length; i++)
                ouputByteArray[i] = guidByteArray[i];

            //Finally, the byte array value is being returned.
            return ouputByteArray;
        }

        //It takes HexaDecimal string and converts it to ByteArray. 
        public byte[] FromString(string strHexa)
        {
            byte[] bytes = null;
            if (string.IsNullOrEmpty(strHexa) || strHexa == "0") return new byte[0];
            if (strHexa == "null")
            {
            }
            int NumberChars = strHexa.Length;
            bytes = new byte[NumberChars / 2];
            for (int i = 0; i < NumberChars; i += 2)
                bytes[i / 2] = Convert.ToByte(strHexa.Substring(i, 2), 16);
            return bytes;
        }

        //It takes the ByteArray and converts it to the HexaDecimal string.
        public override string ToString()
        {
            //The provided byte array is converted to HexaDecimal string 
            //and the resultant string is returned.
            if (UID == null) return null;
            string strHexa = BitConverter.ToString(UID);
            return strHexa.Replace("-", "");
        }
    }
}
