using System.Security.Cryptography;

namespace WebApp.FrontEndEncryption
{
    public class DiffieHellmanUtil : IDiffieHellmanUtil
    {
        public byte[] GetCommonSecret(byte[] OtherPublicKey, byte[] MyPrivateKey)
        {
            byte[] sharedSecret;
            using (ECDiffieHellmanCng user = new ECDiffieHellmanCng(CngKey.Import(MyPrivateKey, CngKeyBlobFormat.EccPrivateBlob)))
            {
                user.KeyDerivationFunction = ECDiffieHellmanKeyDerivationFunction.Hash;
                user.HashAlgorithm = CngAlgorithm.Sha256;

                // Import the public key from the other party
                using (ECDiffieHellmanPublicKey otherPartyPublicKey = ECDiffieHellmanCngPublicKey.FromByteArray(OtherPublicKey, CngKeyBlobFormat.EccPublicBlob))
                {
                    // Derive the shared secret
                    sharedSecret = user.DeriveKeyMaterial(otherPartyPublicKey);
                }
            }
            return sharedSecret;
        }

        public Dictionary<ECDHKey, byte[]> GetKeys()
        {
            Dictionary <ECDHKey, byte[]> keyValuePairs = new Dictionary <ECDHKey, byte[] >();
            using (ECDiffieHellmanCng user = new ECDiffieHellmanCng())
            {
                user.KeyDerivationFunction = ECDiffieHellmanKeyDerivationFunction.Hash;
                user.HashAlgorithm = CngAlgorithm.Sha256;

                keyValuePairs.Add(ECDHKey.Private, user.Key.Export(CngKeyBlobFormat.EccPrivateBlob));

                keyValuePairs.Add(ECDHKey.Public, user.Key.Export(CngKeyBlobFormat.EccPublicBlob));
            }
            return keyValuePairs;
        }

    }

    public enum ECDHKey
    {
        Private,Public
    }
}
