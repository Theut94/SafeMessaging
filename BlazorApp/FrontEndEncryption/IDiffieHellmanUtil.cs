namespace WebApp.FrontEndEncryption
{
    public interface IDiffieHellmanUtil
    {
        Dictionary<ECDHKey,byte[]> GetKeys();
        byte[] GetCommonSecret(byte[] OtherPublicKey, byte[] MyPrivateKey);

    }
}
