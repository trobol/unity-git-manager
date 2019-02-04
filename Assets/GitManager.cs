using System.Diagnostics;
using System.Threading;
using UnityEngine;
public static class GitManager {
	public static void ExecuteCommand (string input)
     {
		 Command(input);
		 /* 
         Thread thread = new Thread(delegate () {Command(input);});
         thread.Start();
		 */
     }
 
	static void Command (string input)
	{
		UnityEngine.Debug.Log("Go");
		ProcessStartInfo processInfo = new ProcessStartInfo("git.exe");
		processInfo.CreateNoWindow = true;
		processInfo.UseShellExecute = false;
		processInfo.RedirectStandardError = true;
		processInfo.Arguments = input;
		
		Process process = Process.Start(processInfo);

		string error;
		error = process.StandardError.ReadToEnd();
		process.WaitForExit();
		process.Close();

		

		UnityEngine.Debug.Log(error);
		UnityEngine.Debug.Log("Finished");
		
	}
	public static void Fetch() {

	}
}

