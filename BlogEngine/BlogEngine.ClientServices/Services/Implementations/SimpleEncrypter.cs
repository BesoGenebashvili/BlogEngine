using System;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using BlogEngine.ClientServices.ServiceHelpers;
using BlogEngine.ClientServices.Services.Abstractions;

namespace BlogEngine.ClientServices.Services.Implementations
{
    public class SimpleEncrypter : IEncrypter, IAsyncEncrypter
    {
        private readonly byte[] _secretkey;
        private readonly byte[] _initializationVector;
        private readonly SymmetricAlgorithm _algorithm;

        public SimpleEncrypter()
        {
            // TODO: use appsettings.json file for this values !
            #region Temporary code
            _secretkey = new byte[8] { 1, 2, 3, 4, 5, 6, 7, 8 };
            _initializationVector = new byte[8] { 1, 2, 3, 4, 5, 6, 7, 8 };
            _algorithm = DES.Create();
            #endregion
        }

        public EncrypterOperationResult Encrypt(string text)
        {
            NullCheckThrowArgumentNullException(text);

            try
            {
                return ProcessEncrypt(text);
            }
            catch (Exception)
            {
                return new EncrypterOperationResult(null, false);
            }
        }

        public EncrypterOperationResult Decrypt(string text)
        {
            NullCheckThrowArgumentNullException(text);

            try
            {
                return ProcessDecrypt(text);
            }
            catch (Exception)
            {
                return new EncrypterOperationResult(null, false);
            }
        }

        public async Task<EncrypterOperationResult> EncryptAsync(string text)
        {
            return await Task.Run(() => Encrypt(text));
        }

        public async Task<EncrypterOperationResult> DecryptAsync(string text)
        {
            return await Task.Run(() => Decrypt(text));
        }

        private EncrypterOperationResult ProcessEncrypt(string text)
        {
            var encryptor = _algorithm
                .CreateEncryptor(_secretkey, _initializationVector);

            byte[] inputbuffer = Encoding.Unicode.GetBytes(text);
            byte[] outputBuffer = encryptor.TransformFinalBlock(inputbuffer, 0, inputbuffer.Length);

            return new EncrypterOperationResult(Convert.ToBase64String(outputBuffer), true);
        }

        private EncrypterOperationResult ProcessDecrypt(string text)
        {
            var decryptor = _algorithm
                .CreateDecryptor(_secretkey, _initializationVector);

            byte[] inputbuffer = Convert.FromBase64String(text);
            byte[] outputBuffer = decryptor.TransformFinalBlock(inputbuffer, 0, inputbuffer.Length);

            return new EncrypterOperationResult(Encoding.Unicode.GetString(outputBuffer), true);
        }

        protected void NullCheckThrowArgumentNullException(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                throw new ArgumentNullException(nameof(text));
            }
        }
    }
}