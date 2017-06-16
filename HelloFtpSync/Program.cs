using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using WinSCP;

namespace HelloFtpSync
{
	class Program
	{
		static void Main(string[] args)
		{
			try
			{
				SessionOptions sessionOptions = new SessionOptions
				{
					Protocol = Protocol.Ftp,
					HostName = "miotec.com.br",
					UserName = "miotec19",
					Password = "Miotec2010",
				};

				const string remotePath = "BioGames";
				const string localPath = @"c:\Miotec\BioGames";

				Directory.CreateDirectory(localPath);

				using (Session session = new Session())
				{
					session.Open(sessionOptions);



					//RemoteDirectoryInfo remoteinfo = session.ListDirectory(remotePath);

					//foreach (RemoteFileInfo f in remoteinfo.Files)
					//{
					//	Console.WriteLine(f.FullName);
					//}



					SynchronizationResult syncResult;

					syncResult = session.SynchronizeDirectories(
						SynchronizationMode.Local,
						localPath,
						remotePath, false, true,
						SynchronizationCriteria.Either);

					if (syncResult.Downloads.Count == 0 && syncResult.Failures.Count == 0)
					{
						MessageBox.Show("sincronizado");
					}
					else
					{
						MessageBox.Show("não sincronizado");
					}




					//List<string> files =
					//	session.EnumerateRemoteFiles(remotePath, "*", EnumerationOptions.MatchDirectories)
					//		.Where(path => path.IsDirectory)
					//		.Select(fileInfo => fileInfo.FullName)
					//		.ToList();

					//foreach (var fname in files)
					//	Console.WriteLine(fname);


					/*
					List<string> prevFiles = null;

					while (true)
					{
						// Collect file list
						List<string> files =
							session.EnumerateRemoteFiles(remotePath, "*.*", EnumerationOptions.AllDirectories)
								.Select(fileInfo => fileInfo.FullName)
								.ToList();
						if (prevFiles == null)
						{
							// In the first round, just print number of files found
							Console.WriteLine("Found {0} files", files.Count);
						}
						else
						{
							// Then look for differences against the previous list
							IEnumerable<string> added = files.Except(prevFiles);
							if (added.Any())
							{
								Console.WriteLine("Added files:");
								foreach (string path in added)
								{
									Console.WriteLine(path);
								}
							}

							IEnumerable<string> removed = prevFiles.Except(files);
							if (removed.Any())
							{
								Console.WriteLine("Removed files:");
								foreach (string path in removed)
								{
									Console.WriteLine(path);
								}
							}
						}

						prevFiles = files;

						Console.WriteLine("Sleeping 10s...");
						Thread.Sleep(10000);
					}
					*/
				}

				Console.ReadKey();
			}
			catch (Exception e)
			{
				Console.WriteLine("Error: {0}", e);
			}
		}
	}
}
