cryptoHelper={
    async generateKeyPair() {
        const algorithm = {
            name: "ECDSA",
            namedCurve: "P-256"
        };
        const keyPair = await crypto.subtle.generateKey(
            algorithm,
            true, // Whether the key is extractable
            ["sign", "verify"] // Key usages
        );
        return {
            publicKey: await crypto.subtle.exportKey("jwk", keyPair.publicKey),
            privateKey: await crypto.subtle.exportKey("jwk", keyPair.privateKey)
        };
    },

    async deriveSharedSecret(publicKeyJwk, privateKeyJwk) {
        const algorithm = {
            name: "ECDH",
            public: await crypto.subtle.importKey("jwk", publicKeyJwk, { name: "ECDH", namedCurve: "P-256" }, false, ["deriveBits"]),
            private: await crypto.subtle.importKey("jwk", privateKeyJwk, { name: "ECDH", namedCurve: "P-256" }, false, ["deriveBits"])
        };
        const sharedSecret = await crypto.subtle.deriveBits(algorithm, algorithm.private, 256);
        return new Uint8Array(sharedSecret);
    }
};
