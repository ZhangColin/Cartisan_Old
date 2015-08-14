namespace Cartisan.Security {
    /// <summary>
    /// 对称加密类型
    /// </summary>
    public enum SymmetricEncryptType: byte {
        DES,
        RC2,
        Rijndael,
        TripleDES
    }
}