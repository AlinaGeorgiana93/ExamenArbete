using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Configuration;

public class Encryptions 
{
    private readonly AesEcryptionOptions _aesOption;
    
    public Encryptions(IOptions<AesEcryptionOptions> aesOptions)
    {
        _aesOption = aesOptions.Value;
        _aesOption.HashKeyIv(Pbkdf2HashToBytes);
    }

    public string AesEncryptToBase64<T> (T sourceToEncrypt) 
    {
        string stringToEncrypt = JsonConvert.SerializeObject(sourceToEncrypt);    
        byte[] dataset = System.Text.Encoding.Unicode.GetBytes(stringToEncrypt);

        //Encrypt using AES
        byte[] encryptedBytes;
        using (SymmetricAlgorithm algorithm = Aes.Create())
        using (ICryptoTransform encryptor = algorithm.CreateEncryptor(_aesOption.KeyHash, _aesOption.IvHash))
        {
            encryptedBytes = encryptor.TransformFinalBlock(dataset, 0, dataset.Length);
        }
        
        return Convert.ToBase64String(encryptedBytes);
    }

    public T AesDecryptFromBase64<T> (string encryptedBase64) 
    {
        byte[] encryptedBytes = Convert.FromBase64String(encryptedBase64);

        byte[] decryptedBytes;
        using (SymmetricAlgorithm algorithm = Aes.Create())
        using (ICryptoTransform decryptor = algorithm.CreateDecryptor(_aesOption.KeyHash, _aesOption.IvHash))
        {
            decryptedBytes = decryptor.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);
        }
        
        string decryptedString = System.Text.Encoding.Unicode.GetString(decryptedBytes);
        T decryptedObject = JsonConvert.DeserializeObject<T>(decryptedString);
                
        return decryptedObject;
    }
public string AesEncryptStringToBase64(string plainText)
{
    byte[] dataset = Encoding.Unicode.GetBytes(plainText);

    byte[] encryptedBytes;
    using (SymmetricAlgorithm algorithm = Aes.Create())
    using (ICryptoTransform encryptor = algorithm.CreateEncryptor(_aesOption.KeyHash, _aesOption.IvHash))
    {
        encryptedBytes = encryptor.TransformFinalBlock(dataset, 0, dataset.Length);
    }

    return Convert.ToBase64String(encryptedBytes);
}
public string AesDecryptStringFromBase64(string encryptedBase64)
{
    byte[] encryptedBytes = Convert.FromBase64String(encryptedBase64);

    byte[] decryptedBytes;
    using (SymmetricAlgorithm algorithm = Aes.Create())
    using (ICryptoTransform decryptor = algorithm.CreateDecryptor(_aesOption.KeyHash, _aesOption.IvHash))
    {
        decryptedBytes = decryptor.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);
    }

    return Encoding.Unicode.GetString(decryptedBytes);
}


    public byte[] Pbkdf2HashToBytes (int nrBytes, string password)
    {
        byte[] registeredPasswordKeyDerivation = KeyDerivation.Pbkdf2(
            password: password,
            salt: Encoding.UTF8.GetBytes(_aesOption.Salt),
            prf: KeyDerivationPrf.HMACSHA512,
            iterationCount: _aesOption.Iterations,
            numBytesRequested: nrBytes);

        return registeredPasswordKeyDerivation;
    }

    public string EncryptPasswordToBase64(string password)    
    {
        //Hash a password using salt and streching
        byte[] encrypted = Pbkdf2HashToBytes(64, password);
        return Convert.ToBase64String(encrypted);
    }
    public string EncryptPersonalNumberToBase64(string personalNumber)    
    {
        //Hash a password using salt and streching
        byte[] encrypted = Pbkdf2HashToBytes(64, personalNumber);
        return Convert.ToBase64String(encrypted);
    }

    
        // Encrypt the last 4 digits of a personal number
            public string EncryptLast4Digits(string personalNumber)
        {
            if (string.IsNullOrWhiteSpace(personalNumber) || personalNumber.Length < 4)
                throw new ArgumentException("PersonalNumber must be at least 4 characters");

            string prefix = personalNumber[..^4];
            string last4 = personalNumber[^4..];
            string encryptedLast4 = AesEncryptStringToBase64(last4);

            return $"{prefix}:{encryptedLast4}";
        }


        // Decrypt the last 4 digits of a personal number
        public string DecryptLast4Digits(string encryptedPersonalNumber)
        {
            var parts = encryptedPersonalNumber.Split(':');
            if (parts.Length != 2)
                throw new FormatException("Encrypted personal number format is invalid");

            string prefix = parts[0];
            string encryptedLast4 = parts[1];
            string decryptedLast4 = AesDecryptStringFromBase64(encryptedLast4);

            return prefix + decryptedLast4; // Combine the prefix with decrypted last 4 digits
        }
    }
