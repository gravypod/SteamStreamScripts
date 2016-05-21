using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;

namespace RemoteShutdown
{
	class Program
	{
		private const int PORT = 111;
		private const string PASSWORD = "PASSWORD";
		private const bool DEBUG_MODE = false;

		static void Main(string[] args)
		{
			var serverEndPoint = new IPEndPoint(IPAddress.Any, PORT);
			var winSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

			winSocket.Bind(serverEndPoint);

			while (true)
			{
				byte[] data = new byte[512];

				var sender = (EndPoint) new IPEndPoint(IPAddress.Any, 0);

				int bytesRead = winSocket.ReceiveFrom(data, ref sender);

				string clientString = Encoding.ASCII.GetString(data, 0, bytesRead);
				Console.WriteLine("Got client in");

				if (!IsAuthCorrect(clientString))
				{
					Console.WriteLine("Client rejected");
					continue;
				}

				Console.WriteLine("Shutdown");

				// Wait for input from client.
				var psi = new ProcessStartInfo(DEBUG_MODE ? "echo" : "shutdown", "/h");
				psi.CreateNoWindow = true;
				psi.UseShellExecute = false;
				Process.Start(psi);
			}
		}

		static string CalculateMD5Hash(string input)
		{
			// step 1, calculate MD5 hash from input

			MD5 md5 = System.Security.Cryptography.MD5.Create();

			byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);

			byte[] hash = md5.ComputeHash(inputBytes);

			// step 2, convert byte array to hex string

			StringBuilder sb = new StringBuilder();

			foreach (byte c in hash)
			{
				sb.Append(c.ToString("X2"));
			}

			return sb.ToString();
		}


		private static bool IsAuthCorrect(string clientAuth)
		{
			string secret = DateTime.UtcNow.ToString("yyyy'-'MM'-'dd':'HH':'mm") + PASSWORD;
			string currentAuth = CalculateMD5Hash(secret);
			clientAuth = clientAuth.Trim().ToUpper();

			Console.WriteLine(secret);
			Console.WriteLine("US " + currentAuth);
			Console.WriteLine("THEM: " + clientAuth);

			return clientAuth.Equals(currentAuth);
		}
	}
}
