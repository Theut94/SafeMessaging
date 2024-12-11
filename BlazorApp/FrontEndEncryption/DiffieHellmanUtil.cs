using System.Security.Cryptography;

namespace WebApp.FrontEndEncryption
{
    public class DiffieHellmanUtil : IDiffieHellmanUtil
    {
        public byte[] GetCommonSecret(byte[] OtherPublicKey, byte[] MyPrivateKey)
        {
            byte[] sharedSecret;
            // Step 1: Extract the X and Y components from the public key
            byte[] keyX = new byte[OtherPublicKey.Length / 2];
            byte[] keyY = new byte[keyX.Length];

            Buffer.BlockCopy(OtherPublicKey, 0, keyX, 0, keyX.Length);
            Buffer.BlockCopy(OtherPublicKey, keyX.Length, keyY, 0, keyY.Length);

            // Step 2: Create the ECParameters with the curve and public key components (X and Y)
            ECParameters parameters = new ECParameters
            {
                Curve = ECCurve.NamedCurves.nistP256,  // Specify the curve
                Q =
                {
                    X = keyX,
                    Y = keyY
                }
            };
            // Import the private key from the byte array
            using (ECDiffieHellman user = ECDiffieHellman.Create())
            {
                // Set the private key
                user.ImportECPrivateKey(MyPrivateKey, out _);

                // Import the public key from the other party
                using (ECDiffieHellmanPublicKey otherPartyPublicKey = ECDiffieHellman.Create(parameters).PublicKey)
                {
                    // Derive the shared secret
                    sharedSecret = user.DeriveKeyMaterial(otherPartyPublicKey);
                }
            }

            return sharedSecret;
        }

        public Dictionary<ECDHKey, byte[]> GetKeys()
        {
            Dictionary<ECDHKey, byte[]> keyValuePairs = new Dictionary<ECDHKey, byte[]>();

            // Create a new ECDiffieHellman instance
            using (ECDiffieHellman user = ECDiffieHellman.Create())
            {
                // Export the private key
                keyValuePairs.Add(ECDHKey.Private, user.ExportECPrivateKey());

                // Export the public key
                keyValuePairs.Add(ECDHKey.Public, user.ExportSubjectPublicKeyInfo());
            }

            return keyValuePairs;
        }

    }

    public enum ECDHKey
    {
        Private,Public
    }
}
