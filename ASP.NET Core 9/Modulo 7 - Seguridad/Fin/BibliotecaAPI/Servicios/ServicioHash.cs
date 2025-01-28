using BibliotecaAPI.DTOs;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace BibliotecaAPI.Servicios
{
    public class ServicioHash : IServicioHash
    {
        public ResultadoHashDTO Hash(string input)
        {
            var sal = new byte[16];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(sal);
            }

            return Hash(input, sal);
        }

        public ResultadoHashDTO Hash(string input, byte[] sal)
        {
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: input,
                salt: sal,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10_000,
                numBytesRequested: 256 / 8
                ));

            return new ResultadoHashDTO
            {
                Hash = hashed,
                Sal = sal
            };
        }
    }
}
