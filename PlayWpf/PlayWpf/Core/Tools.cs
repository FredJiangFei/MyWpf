using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayWpf.Core
{
    public static class Tools
    {
        public static byte[] GetByteArrayfromPRS(string PRSString)
        {
            if (PRSString.Length < 1)
            {
                return new byte[0];
            }

            byte[] array = new byte[PRSString.Length / 2];
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = byte.Parse(PRSString.Substring(i * 2, 2), NumberStyles.HexNumber);
            }

            return array;
        }

        public static byte[] GetSegmentFromByteArray(byte[] Source, int index, int count)
        {
            byte[] array = new byte[count];
            for (int i = index; i < index + count; i++)
            {
                array[i - index] = Source[i];
            }
            return array;
        }

        public static string ConvertBinaryToString(byte[] SourceBits, int StartBit, int BitCnt, int Radix, int MinDigits, int MaxDigits)
        {
            int i = 0;
            byte[] array = new byte[MaxDigits];
            if (MinDigits > MaxDigits)
            {
                MinDigits = MaxDigits;
            }
            array[0] = 0;
            int num = StartBit + BitCnt - 1;
            for (int j = StartBit; j <= num; j++)
            {
                int num2 = ((SourceBits[(int)BitByTeoffset(j)] & BitByTemask(j)) != 0) ? 1 : 0;
                for (int k = 0; k < i; k++)
                {
                    int num3 = (array[k] << 1) + num2;
                    if (num3 >= Radix)
                    {
                        num3 -= Radix;
                        num2 = 1;
                    }
                    else
                    {
                        num2 = 0;
                    }
                    array[k] = (byte)num3;
                }
                if (num2 == 1 && i < MaxDigits)
                {
                    array[i] = 1;
                    i++;
                }
            }
            while (i < MinDigits)
            {
                array[i] = 0;
                i++;
            }

            StringBuilder text = new StringBuilder();
            for (int l = i - 1; l >= 0; l--)
            {
                text.Append(Nibble2HexChar(array[l]));
            }

            return text.ToString();
        }

        public static string ConvertASCIIStringToString(string asciiString)
        {
            if (string.IsNullOrEmpty(asciiString))
            {
                return string.Empty;
            }

            List<byte> bytes = new List<byte>();
            for (int i = 0; i < asciiString.Length; i += 2)
            {
                string tempStr = new string(asciiString.Skip(i).Take(2).ToArray());
                if (!byte.TryParse(tempStr, out var result))
                {
                    bytes.Add(byte.MinValue);
                }
                bytes.Add(result);
            }

            return Encoding.ASCII.GetString(bytes.ToArray());
        }

        public static string FilterSpecificSymbol(string text, params char[] allowSymbol)
        {
            if (string.IsNullOrEmpty(text))
            {
                return string.Empty;
            }

            for (int i = 0; i < text.Length; i++)
            {
                if (!char.IsLetterOrDigit(text[i]) && allowSymbol?.Any(x => x == text[i]) == false)
                {
                    text = text.Replace(text[i], char.MinValue);
                }
            }

            return text.Replace("\0", string.Empty);
        }

        private static byte BitByTemask(int bitNr)
        {
            return (byte)(128 >> (bitNr & 7 & 31));
        }

        private static byte BitByTeoffset(int BitNr)
        {
            return (byte)(BitNr >> 3);
        }

        private static char Nibble2HexChar(byte Nibble)
        {
            Nibble &= 15;
            if (Nibble >= 10)
            {
                return (char)(65 + Nibble - 10);
            }
            return (char)(48 + Nibble);
        }

        public static string FindPort()
        {
            int num = 0;
            string text;
            do
            {
                text = RegQuerySZ("SYSTEM\\CurrentControlSet\\Services\\usbser\\Enum", num.ToString());
                if (text == null)
                {
                    text = RegQuerySZ("SYSTEM\\CurrentControlSet\\Services\\elateccdc\\Enum", num.ToString());
                }
                if (text == null)
                {
                    return null;
                }
                num++;
            }
            while (!text.ToUpper().StartsWith("USB\\VID_09D8&PID_0420\\"));
            return RegQuerySZ("SYSTEM\\CurrentControlSet\\Enum\\" + text + "\\Device Parameters", "PortName");
        }

        private static string RegQuerySZ(string SubKeyName, string ValueName)
        {
            RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(SubKeyName);
            if (registryKey == null)
            {
                return null;
            }
            object value = registryKey.GetValue(ValueName);
            registryKey.Close();
            return value?.ToString();
        }

        public static string StrToHex(string mStr)
        {
            return BitConverter.ToString(Encoding.ASCII.GetBytes(mStr));
        } /* StrToHex */
        public static string HexToStr(string mHex)
        {
            if (mHex.Length <= 0) return "";
            byte[] vBytes = new byte[mHex.Length / 2];
            for (int i = 0; i < mHex.Length; i += 2)
            {
                if (!byte.TryParse(mHex.Substring(i, 2), NumberStyles.HexNumber, null, out vBytes[i / 2]))
                {
                    vBytes[i / 2] = 0;
                }
            }

            return Encoding.ASCII.GetString(vBytes);
        }
    }
}
