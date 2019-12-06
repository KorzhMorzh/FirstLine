using Microsoft.VisualStudio.TestTools.UnitTesting;
using Coursework.Custom_Classes;

namespace CourseworkTests
{
    [TestClass]
    public class VigenereCipherTests

    {
        [TestMethod]
        public void EncryptLowerRussianLetter()
        {
            var wordToEncrypt = "поздравляю";
            var key = "скорпион";
            var isEncrypted = "true";
            var result = "бщцфаирщри";
            Assert.AreEqual(result, new VigenereCipher(wordToEncrypt, key, isEncrypted).NewText);
        }

        [TestMethod]
        public void EncryptUpperRussianLetter()
        {
            var wordToEncrypt = "ПОЗДРАВЛЯЮ";
            var key = "скорпион";
            var isEncrypted = "true";
            var result = "БЩЦФАИРЩРИ";
            Assert.AreEqual(result, new VigenereCipher(wordToEncrypt, key, isEncrypted).NewText);
        }

        [TestMethod]
        public void DoNotEncryptNonRussianLetter()
        {
            var wordToEncrypt = "congrats";
            var key = "скорпион";
            var isEncrypted = "true";
            var result = "congrats";
            Assert.AreEqual(result, new VigenereCipher(wordToEncrypt, key, isEncrypted).NewText);
        }

        [TestMethod]
        public void EncryptOnlyRussianLetter()
        {
            var wordToEncrypt = "поl з12д ра$#вля ю";
            var key = "скорпион";
            var isEncrypted = "true";
            var result = "бщl ц12ф аи$#рщр и";
            Assert.AreEqual(result, new VigenereCipher(wordToEncrypt, key, isEncrypted).NewText);
        }

        [TestMethod]
        public void EncryptIgnoresUpperLetterInKey()
        {
            var wordToEncrypt = "поздравляю";
            var key = "сКоРПиоН";
            var isEncrypted = "true";
            var result = "бщцфаирщри";
            Assert.AreEqual(result, new VigenereCipher(wordToEncrypt, key, isEncrypted).NewText);
        }

        [TestMethod]
        public void DecryptLowerRussianLetter()
        {
            var wordToDecrypt = "бщцфаирщри";
            var key = "скорпион";
            var isEncrypted = "false";
            var result = "поздравляю";
            Assert.AreEqual(result, new VigenereCipher(wordToDecrypt, key, isEncrypted).NewText);
        }

        [TestMethod]
        public void DecryptUpperRussianLetter()
        {
            var wordToDecrypt = "БЩЦФАИРЩРИ";
            var key = "скорпион";
            var isEncrypted = "false";
            var result = "ПОЗДРАВЛЯЮ";
            Assert.AreEqual(result, new VigenereCipher(wordToDecrypt, key, isEncrypted).NewText);
        }

        [TestMethod]
        public void DoNotDecryptNonRussianLetter()
        {
            var wordToDecrypt = "congrats";
            var key = "скорпион";
            var isEncrypted = "false";
            var result = "congrats";
            Assert.AreEqual(result, new VigenereCipher(wordToDecrypt, key, isEncrypted).NewText);
        }

        [TestMethod]
        public void DecryptOnlyRussianLetter()
        {
            var wordToDecrypt = "бщl ц12ф аи$#рщр и";
            var key = "скорпион";
            var isEncrypted = "false";
            var result = "поl з12д ра$#вля ю";
            Assert.AreEqual(result, new VigenereCipher(wordToDecrypt, key, isEncrypted).NewText);
        }

        [TestMethod]
        public void DecryptIgnoresUpperLetterInKey()
        {
            var wordToDecrypt = "бщцфаирщри";
            var key = "сКоРПиоН";
            var isEncrypted = "false";
            var result = "поздравляю";
            Assert.AreEqual(result, new VigenereCipher(wordToDecrypt, key, isEncrypted).NewText);
        }

        [TestMethod]
        public void CorrectEncryptAndThenDecrypt()
        {
            var wordToEncrypt = "поздравляю";
            var key = "сКоРПиоН";
            var encryptedWord = new VigenereCipher(wordToEncrypt,key,"true").NewText;
            var decryptedWord = new VigenereCipher(encryptedWord,key,"false").NewText;
            Assert.AreEqual(wordToEncrypt, decryptedWord);
        }
    }
}
