using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Cartisan.Security {
    public static class EncryptionUtility {
        /// <summary>
        /// 对称加密
        /// </summary>
        /// <param name="encryptType">加密类型</param>
        /// <param name="originalString">需要加密的字符串</param>
        /// <param name="ivString">初始化向量</param>
        /// <param name="keyString">加密密钥</param>
        /// <returns>加密后的字符串</returns>
        public static string SymmetricEncrypt(SymmetricEncryptType encryptType, string originalString, string ivString,
            string keyString) {
            if(string.IsNullOrEmpty(originalString) || string.IsNullOrEmpty(ivString) || string.IsNullOrEmpty(keyString)) {
                return originalString;
            }

            return new SymmetricEncrypt(encryptType) {
                IVString = ivString,
                KeyString = keyString
            }.Encrypt(originalString);
        }

        /// <summary>
        /// 对称解密
        /// </summary>
        /// <param name="encryptType">加密类型</param>
        /// <param name="encryptedString">需要解密的字符串</param>
        /// <param name="ivString">初始化向量</param>
        /// <param name="keyString">加密密钥</param>
        /// <returns>解密后的字符串</returns>
        public static string SymmetricDncrypt(SymmetricEncryptType encryptType, string encryptedString, string ivString,
            string keyString) {
            if(string.IsNullOrEmpty(encryptedString) || string.IsNullOrEmpty(ivString) ||
                string.IsNullOrEmpty(keyString)) {
                return encryptedString;
            }

            return new SymmetricEncrypt(encryptType) {
                IVString = ivString,
                KeyString = keyString
            }.Decrypt(encryptedString);
        }

        /// <summary>
        /// 标准MD5加密
        /// </summary>
        /// <param name="originalString"></param>
        /// <returns></returns>
        public static string MD5(string originalString) {
            byte[] hash = new MD5CryptoServiceProvider().ComputeHash(Encoding.UTF8.GetBytes(originalString));
            return string.Join("", hash.Select(h => h.ToString("x").PadLeft(2, '0')));
        }

        /// <summary>
        /// 提供更丰富的MD5加密方式
        /// </summary>
        /// <param name="toEncrypt"></param>
        /// <param name="mode"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string MD5Encrypt(string toEncrypt, CipherMode mode = CipherMode.CBC, string key = "CARTISAN") {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            des.Mode = mode;
            byte[] inputByteArray = Encoding.UTF8.GetBytes(toEncrypt);
            des.Key = ASCIIEncoding.ASCII.GetBytes(key);
            des.IV = ASCIIEncoding.ASCII.GetBytes(key);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            StringBuilder ret = new StringBuilder();
            foreach(byte b in ms.ToArray()) {
                ret.AppendFormat("{0:X2}", b);
            }
            ret.ToString();
            return ret.ToString();

        }

        /// <summary>
        /// base64编码
        /// </summary>
        /// <param name="originalString">待编码的字符串</param>
        /// <returns>
        /// 编码后的字符串
        /// </returns>
        public static string Base64_Encode(string originalString) {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(originalString));
        }

        /// <summary>
        /// base64解码
        /// </summary>
        /// <param name="originalString">待解码的字符串</param>
        /// <returns>
        /// 解码后的字符串
        /// </returns>
        public static string Base64_Decode(string originalString) {
            return Encoding.UTF8.GetString(Convert.FromBase64String(originalString));
        }
    }
}