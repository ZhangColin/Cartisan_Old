using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Cartisan.Security {
    /// <summary>
    /// 对称加密算法
    /// </summary>
    public class SymmetricEncrypt {
        private SymmetricEncryptType _encryptionType;
        private string _originalString;
        private string _encryptedString;
        private SymmetricAlgorithm _cryptoProvider;

        /// <summary>
        /// 默认采用DES算法
        /// </summary>
        public SymmetricEncrypt() {
            this._encryptionType = SymmetricEncryptType.DES;
            this.SetEncryptor();
        }

        public SymmetricEncrypt(SymmetricEncryptType encryptionType) {
            this._encryptionType = encryptionType;
            this.SetEncryptor();
        }

        public SymmetricEncrypt(SymmetricEncryptType encryptionType, string originalString) {
            this._encryptionType = encryptionType;
            this._originalString = originalString;
            this.SetEncryptor();
        }

        /// <summary>
        /// 对称加密算法提供者
        /// </summary>
        public SymmetricAlgorithm CryptoProvider {
            get { return this._cryptoProvider; }
            set { this._cryptoProvider = value; }
        }

        /// <summary>
        /// 原始字符串
        /// </summary>
        public string OriginalString {
            get { return this._originalString; }
            set { this._originalString = value; }
        }

        /// <summary>
        /// 加密后的字符串
        /// </summary>
        public string EncryptedString {
            get { return this._encryptedString; }
            set { this._encryptedString = value; }
        }

        /// <summary>
        /// 对称加密算法密钥
        /// </summary>
        public byte[] Key {
            get { return this._cryptoProvider.Key; }
            set { this._cryptoProvider.Key = value; }
        }

        /// <summary>
        /// 加密密钥
        /// </summary>
        public string KeyString {
            get { return Convert.ToBase64String(this._cryptoProvider.Key); }
            set { this._cryptoProvider.Key = Convert.FromBase64String(value); }
        }

        /// <summary>
        /// 初始化向量
        /// </summary>
        public byte[] IV {
            get { return this._cryptoProvider.IV; }
            set { this._cryptoProvider.IV = value; }
        }

        /// <summary>
        /// 初始化向量（Base64）
        /// </summary>
        public string IVString {
            get { return Convert.ToBase64String(this._cryptoProvider.IV); }
            set { this._cryptoProvider.IV = Convert.FromBase64String(value); }
        }

        /// <summary>
        /// 进行对称加密
        /// </summary>
        /// <returns></returns>
        public string Encrypt() {
            ICryptoTransform encryptor = this._cryptoProvider.CreateEncryptor(this._cryptoProvider.Key,
                this._cryptoProvider.IV);
            byte[] buffer = Encoding.Unicode.GetBytes(this._originalString);
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
            cryptoStream.Write(buffer, 0, buffer.Length);
            cryptoStream.FlushFinalBlock();
            cryptoStream.Close();
            this._encryptedString = Convert.ToBase64String(memoryStream.ToArray());
            return this._encryptedString;
        }

        /// <summary>
        /// 进行对称加密
        /// </summary>
        /// <param name="originalString">原始字符串</param>
        /// <returns></returns>
        public string Encrypt(string originalString) {
            this._originalString = originalString;
            return this.Encrypt();
        }

        public string Encrypt(string originalString, SymmetricEncryptType encryptionType) {
            this._originalString = originalString;
            this._encryptionType = encryptionType;
            return this.Encrypt();
        }

        /// <summary>
        /// 进行对称解密
        /// </summary>
        /// <returns></returns>
        public string Decrypt() {
            ICryptoTransform decryptor = this._cryptoProvider.CreateDecryptor(this._cryptoProvider.Key,
                this._cryptoProvider.IV);
            byte[] buffer = Convert.FromBase64String(this._encryptedString);
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Write);
            cryptoStream.Write(buffer, 0, buffer.Length);
            cryptoStream.FlushFinalBlock();
            cryptoStream.Close();
            this._originalString = Encoding.Unicode.GetString(memoryStream.ToArray());
            return this._originalString;
        }

        /// <summary>
        /// 进行对称加密
        /// </summary>
        /// <param name="encryptedString">需要解密的字符串</param>
        /// <returns></returns>
        public string Decrypt(string encryptedString) {
            this._encryptedString = encryptedString;
            return this.Decrypt();
        }

        /// <summary>
        /// 进行对称加密
        /// </summary>
        /// <param name="encryptedString">需要解密的字符串</param>
        /// <param name="encryptionType">字符串加密类型</param>
        /// <returns></returns>
        public string Decrypt(string encryptedString, SymmetricEncryptType encryptionType) {
            this._encryptedString = encryptedString;
            this._encryptionType = encryptionType;
            return this.Decrypt();
        }

        /// <summary>
        /// 设置加密算法
        /// </summary>
        private void SetEncryptor() {
            switch(this._encryptionType) {
                case SymmetricEncryptType.DES:
                    this._cryptoProvider = new DESCryptoServiceProvider();
                    break;
                case SymmetricEncryptType.RC2:
                    this._cryptoProvider = new RC2CryptoServiceProvider();
                    break;
                case SymmetricEncryptType.Rijndael:
                    this._cryptoProvider = new RijndaelManaged();
                    break;
                case SymmetricEncryptType.TripleDES:
                    this._cryptoProvider = new TripleDESCryptoServiceProvider();
                    break;
            }
            this._cryptoProvider.GenerateKey();
            this._cryptoProvider.GenerateIV();
        }

        /// <summary>
        /// 生成随机密钥
        /// </summary>
        /// <returns></returns>
        public string GenerateKey() {
            this._cryptoProvider.GenerateKey();
            return Convert.ToBase64String(this._cryptoProvider.Key);
        }

        /// <summary>
        /// 生成随机初始化向量
        /// </summary>
        /// <returns></returns>
        public string GenerateIV() {
            this._cryptoProvider.GenerateIV();
            return Convert.ToBase64String(this._cryptoProvider.IV);
        }
    }
}