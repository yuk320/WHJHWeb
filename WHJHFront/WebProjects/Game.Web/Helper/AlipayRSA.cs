using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.IO;
using System.Text;

namespace Game.Web.Helper
{
    // ReSharper disable once InconsistentNaming
    public sealed class AlipayRSA
    {
        /// <summary>
        /// 验证签名
        /// </summary>
        /// <param name="content">需要验证的内容</param>
        /// <param name="signedString">签名结果</param>
        /// <param name="publicKey">公钥</param>
        /// <param name="inputCharset">编码格式</param>
        /// <returns></returns>
        public static bool Verify(string content, string signedString, string publicKey, string inputCharset)
        {
            Encoding code = Encoding.GetEncoding(inputCharset);
            byte[] codoBytes = code.GetBytes(content);
            byte[] data = Convert.FromBase64String(signedString);
            RSAParameters paraPub = ConvertFromPublicKey(publicKey);
            RSACryptoServiceProvider rsaPub = new RSACryptoServiceProvider();
            rsaPub.ImportParameters(paraPub);

            SHA1 sh = new SHA1CryptoServiceProvider();
            var result = rsaPub.VerifyData(codoBytes, sh, data);
            return result;
        }

        /// <summary>
        /// 用RSA解密
        /// </summary>
        /// <param name="resData">待解密字符串</param>
        /// <param name="privateKey">私钥</param>
        /// <param name="inputCharset">编码格式</param>
        /// <returns>解密结果</returns>
        public static string DecryptData(string resData, string privateKey, string inputCharset)
        {
            byte[] dataToDecrypt = Convert.FromBase64String(resData);
            List<byte> result = new List<byte>();

            for(int j = 0; j < dataToDecrypt.Length / 128; j++)
            {
                byte[] buf = new byte[128];
                for(int i = 0; i < 128; i++)
                {
                    buf[i] = dataToDecrypt[i + 128 * j];
                }
                result.AddRange(Decrypt(buf, privateKey));
            }
            byte[] source = result.ToArray();
            char[] asciiChars = new char[Encoding.GetEncoding(inputCharset).GetCharCount(source, 0, source.Length)];
            Encoding.GetEncoding(inputCharset).GetChars(source, 0, source.Length, asciiChars, 0);
            return new string(asciiChars);
        }

        private static IEnumerable<byte> Decrypt(byte[] data, string privateKey)
        {
            RSACryptoServiceProvider rsa = DecodePemPrivateKey(privateKey);
            return rsa.Decrypt(data, false);
        }

        /// <summary>
        /// 解析java生成的pem文件私钥
        /// </summary>
        /// <param name="pemstr"></param>
        /// <returns></returns>
        private static RSACryptoServiceProvider DecodePemPrivateKey(string pemstr)
        {
            var pkcs8Privatekey = Convert.FromBase64String(pemstr);
            {

                RSACryptoServiceProvider rsa = DecodePrivateKeyInfo(pkcs8Privatekey);
                return rsa;
            }
        }

        private static RSACryptoServiceProvider DecodePrivateKeyInfo(byte[] pkcs8)
        {

            byte[] seqOid = { 0x30, 0x0D, 0x06, 0x09, 0x2A, 0x86, 0x48, 0x86, 0xF7, 0x0D, 0x01, 0x01, 0x01, 0x05, 0x00 };

            MemoryStream mem = new MemoryStream(pkcs8);
            int lenstream = (int)mem.Length;
            BinaryReader binr = new BinaryReader(mem);    //wrap Memory Stream with BinaryReader for easy reading

            try
            {

                var twobytes = binr.ReadUInt16();
                switch (twobytes)
                {
                    case 0x8130:
                        binr.ReadByte();	//advance 1 byte
                        break;
                    case 0x8230:
                        binr.ReadInt16();	//advance 2 bytes
                        break;
                    default:
                        return null;
                }


                var bt = binr.ReadByte();
                if(bt != 0x02)
                    return null;

                twobytes = binr.ReadUInt16();

                if(twobytes != 0x0001)
                    return null;

                var seq = binr.ReadBytes(15);
                if(!CompareBytearrays(seq, seqOid))	//make sure Sequence for OID is correct
                    return null;

                bt = binr.ReadByte();
                if(bt != 0x04)	//expect an Octet string 
                    return null;

                bt = binr.ReadByte();		//read next byte, or next 2 bytes is  0x81 or 0x82; otherwise bt is the byte count
                switch (bt)
                {
                    case 0x81:
                        binr.ReadByte();
                        break;
                    case 0x82:
                        binr.ReadUInt16();
                        break;
                }
                //------ at this stage, the remaining sequence should be the RSA private key

                byte[] rsaprivkey = binr.ReadBytes((int)(lenstream - mem.Position));
                RSACryptoServiceProvider rsacsp = DecodeRSAPrivateKey(rsaprivkey);
                return rsacsp;
            }

            catch(Exception)
            {
                return null;
            }

            finally { binr.Close(); }

        }


        private static bool CompareBytearrays(ICollection<byte> a, IList<byte> b)
        {
            if(a.Count != b.Count)
                return false;
            int i = 0;
            foreach(byte c in a)
            {
                if(c != b[i])
                    return false;
                i++;
            }
            return true;
        }

        // ReSharper disable once InconsistentNaming
        private static RSACryptoServiceProvider DecodeRSAPrivateKey(byte[] privkey)
        {
            // ---------  Set up stream to decode the asn.1 encoded RSA private key  ------
            MemoryStream mem = new MemoryStream(privkey);
            BinaryReader binr = new BinaryReader(mem);    //wrap Memory Stream with BinaryReader for easy reading
            try
            {
                var twobytes = binr.ReadUInt16();
                switch (twobytes)
                {
                    case 0x8130:
                        binr.ReadByte();	//advance 1 byte
                        break;
                    case 0x8230:
                        binr.ReadInt16();	//advance 2 bytes
                        break;
                    default:
                        return null;
                }

                twobytes = binr.ReadUInt16();
                if(twobytes != 0x0102)	//version number
                    return null;
                var bt = binr.ReadByte();
                if(bt != 0x00)
                    return null;


                //------  all private key components are Integer sequences ----
                var elems = GetIntegerSize(binr);
                var modulus = binr.ReadBytes(elems);

                elems = GetIntegerSize(binr);
                var e = binr.ReadBytes(elems);

                elems = GetIntegerSize(binr);
                var d = binr.ReadBytes(elems);

                elems = GetIntegerSize(binr);
                var p = binr.ReadBytes(elems);

                elems = GetIntegerSize(binr);
                var q = binr.ReadBytes(elems);

                elems = GetIntegerSize(binr);
                var dp = binr.ReadBytes(elems);

                elems = GetIntegerSize(binr);
                var dq = binr.ReadBytes(elems);

                elems = GetIntegerSize(binr);
                var iq = binr.ReadBytes(elems);

                // ------- create RSACryptoServiceProvider instance and initialize with public key -----
                RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
                RSAParameters rsAparams = new RSAParameters
                {
                    Modulus = modulus,
                    Exponent = e,
                    D = d,
                    P = p,
                    Q = q,
                    DP = dp,
                    DQ = dq,
                    InverseQ = iq
                };
                rsa.ImportParameters(rsAparams);
                return rsa;
            }
            catch(Exception)
            {
                return null;
            }
            finally { binr.Close(); }
        }

        private static int GetIntegerSize(BinaryReader binr)
        {
            int count;
            var bt = binr.ReadByte();
            if(bt != 0x02)		//expect integer
                return 0;
            bt = binr.ReadByte();

            switch (bt)
            {
                case 0x81:
                    count = binr.ReadByte();	// data size in next byte
                    break;
                case 0x82:
                    var highbyte = binr.ReadByte();
                    var lowbyte = binr.ReadByte();
                    byte[] modint = { lowbyte, highbyte, 0x00, 0x00 };
                    count = BitConverter.ToInt32(modint, 0);
                    break;
                default:
                    count = bt;     // we already have the data size
                    break;
            }

            while(binr.ReadByte() == 0x00)
            {	//remove high order zeros in data
                count -= 1;
            }
            binr.BaseStream.Seek(-1, SeekOrigin.Current);		//last ReadByte wasn't a removed zero, so back up a byte
            return count;
        }

        #region 解析.net 生成的Pem
        private static RSAParameters ConvertFromPublicKey(string pemFileConent)
        {

            byte[] keyData = Convert.FromBase64String(pemFileConent);
            if(keyData.Length < 162)
            {
                throw new ArgumentException("pem file content is incorrect.");
            }
            byte[] pemModulus = new byte[128];
            byte[] pemPublicExponent = new byte[3];
            Array.Copy(keyData, 29, pemModulus, 0, 128);
            Array.Copy(keyData, 159, pemPublicExponent, 0, 3);
            RSAParameters para = new RSAParameters();
            para.Modulus = pemModulus;
            para.Exponent = pemPublicExponent;
            return para;
        }

//        private static RSAParameters ConvertFromPrivateKey(string pemFileConent)
//        {
//            byte[] keyData = Convert.FromBase64String(pemFileConent);
//            if(keyData.Length < 609)
//            {
//                throw new ArgumentException("pem file content is incorrect.");
//            }
//
//            int index = 11;
//            byte[] pemModulus = new byte[128];
//            Array.Copy(keyData, index, pemModulus, 0, 128);
//
//            index += 128;
//            index += 2;//141
//            byte[] pemPublicExponent = new byte[3];
//            Array.Copy(keyData, index, pemPublicExponent, 0, 3);
//
//            index += 3;
//            index += 4;//148
//            byte[] pemPrivateExponent = new byte[128];
//            Array.Copy(keyData, index, pemPrivateExponent, 0, 128);
//
//            index += 128;
//            index += ((int)keyData[index + 1] == 64 ? 2 : 3);//279
//            byte[] pemPrime1 = new byte[64];
//            Array.Copy(keyData, index, pemPrime1, 0, 64);
//
//            index += 64;
//            index += ((int)keyData[index + 1] == 64 ? 2 : 3);//346
//            byte[] pemPrime2 = new byte[64];
//            Array.Copy(keyData, index, pemPrime2, 0, 64);
//
//            index += 64;
//            index += ((int)keyData[index + 1] == 64 ? 2 : 3);//412/413
//            byte[] pemExponent1 = new byte[64];
//            Array.Copy(keyData, index, pemExponent1, 0, 64);
//
//            index += 64;
//            index += ((int)keyData[index + 1] == 64 ? 2 : 3);//479/480
//            byte[] pemExponent2 = new byte[64];
//            Array.Copy(keyData, index, pemExponent2, 0, 64);
//
//            index += 64;
//            index += ((int)keyData[index + 1] == 64 ? 2 : 3);//545/546
//            byte[] pemCoefficient = new byte[64];
//            Array.Copy(keyData, index, pemCoefficient, 0, 64);
//
//            RSAParameters para = new RSAParameters();
//            para.Modulus = pemModulus;
//            para.Exponent = pemPublicExponent;
//            para.D = pemPrivateExponent;
//            para.P = pemPrime1;
//            para.Q = pemPrime2;
//            para.DP = pemExponent1;
//            para.DQ = pemExponent2;
//            para.InverseQ = pemCoefficient;
//            return para;
//        }
        #endregion
    }
}