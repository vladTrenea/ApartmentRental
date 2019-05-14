using System;
using System.Security.Cryptography;
using System.Text;

using ApartmentRentalWebApi.Business.Core.Services;

namespace ApartmentRentalWebApi.Business.Impl.Services
{
	public class HashService : IHashService
	{
		private readonly string _hashAlgorithm;

		public HashService(string hashAlgorithm)
		{
			_hashAlgorithm = hashAlgorithm;
		}

		/// <summary>
		/// Using SHA1 algorithm, generates the hash code of the input string.
		/// </summary>
		/// <param name="inputString">
		/// The input string.
		/// </param>
		/// <returns>
		/// Hash content of the encrypted string
		/// </returns>
		public string EncodeString(string inputString)
		{
			// convert the input string to byte array
			byte[] byteInput = ConvertStringToByteArray(inputString);

			// Create hash value from inputString using SHA1 instance returned by Crypto Config system
			byte[] hashValue = ((HashAlgorithm)CryptoConfig.CreateFromName(_hashAlgorithm)).ComputeHash(byteInput);

			// return the string representation of the hash
			return BitConverter.ToString(hashValue);
		}

		/// <summary>
		/// Converts to byte array the string argument
		/// </summary>
		/// <param name="s">
		/// The string to be converted
		/// </param>
		/// <returns>
		/// byte array from string
		/// </returns>
		private static byte[] ConvertStringToByteArray(string s)
		{
			// returns the binary representation of the string s (treated as unicode)
			return (new UnicodeEncoding()).GetBytes(s);
		}
	}
}