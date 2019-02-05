using System.Diagnostics;
using System.Threading;
using UnityEngine;
using System.Collections.Generic;
public static class GitManager
{
	public static Process GitCommand(string input)
	{
		ProcessStartInfo processInfo = new ProcessStartInfo("git.exe");
		processInfo.CreateNoWindow = true;
		processInfo.UseShellExecute = false;
		processInfo.RedirectStandardError = true;
		processInfo.RedirectStandardOutput = true;
		processInfo.Arguments = input;

		Process process = Process.Start(processInfo);

		//SHOULD READ BEFORE WAITING

		return process;
	}

	public static void Fetch()
	{
		GitCommand("fetch");
	}
	public static void Log()
	{
		DiffLocal();
		DiffRemote();
	}
	public static void DiffRemote()
	{
		Process p = GitCommand("diff HEAD...FETCH_HEAD --numstat");
		string output = p.StandardOutput.ReadToEnd(),
		error = p.StandardError.ReadToEnd();
		if (error.Length > 0)
		{
			UnityEngine.Debug.Log(error);
		}

		string[] lines = output.Split(new char[] { System.Environment.NewLine[0], System.Environment.NewLine[1] });
		fetch = new GitFile[lines.Length - 1];

		for (int i = 0; i < lines.Length - 1; i++)
		{
			fetch[i] = new GitFile();
			string[] a = lines[i].Split('	');
			fetch[i].additions = System.Convert.ToInt32(a[0]);
			fetch[i].deletions = System.Convert.ToInt32(a[1]);
			fetch[i].path = a[2];
			fetch[i].GetChanges();
		}

		p.WaitForExit();
		p.Close();
	}
	public static void DiffLocal()
	{
		Process p = GitCommand("diff --numstat");
		string output = p.StandardOutput.ReadToEnd(),
		error = p.StandardError.ReadToEnd();
		if (error.Length > 0)
		{
			UnityEngine.Debug.Log(error);
		}

		string[] lines = output.Split(new char[] { System.Environment.NewLine[0], System.Environment.NewLine[1] });
		head = new GitFile[lines.Length - 1];

		for (int i = 0; i < lines.Length - 1; i++)
		{
			head[i] = new GitFile();
			string[] a = lines[i].Split('	');
			head[i].additions = System.Convert.ToInt32(a[0]);
			head[i].deletions = System.Convert.ToInt32(a[1]);
			head[i].path = a[2];
			head[i].GetChanges();
		}

		p.WaitForExit();
		p.Close();
	}
	public static GitFile[] head, fetch;

}
public class GitFile
{
	public GitChange[] changes;
	public string path;
	public int additions, deletions;
	public void GetChanges()
	{
		Process p = GitManager.GitCommand("--no-pager log HEAD..FETCH_HEAD --format=\"%an%n%at\" --numstat " + path);
		string output = p.StandardOutput.ReadToEnd(),
		error = p.StandardError.ReadToEnd();
		if (error.Length > 0)
		{
			UnityEngine.Debug.Log(error);
		}

		string[] lines = output.Split(new char[] { System.Environment.NewLine[0], System.Environment.NewLine[1] });
		List<GitChange> changesL = new List<GitChange>();

		for (int i = 0; i < lines.Length - 4; i += 4)
		{
			GitChange change = new GitChange();
			change.author = lines[i];
			change.date = lines[i + 1];
			string[] a = lines[i + 3].Split('	');
			change.additions = System.Convert.ToInt32(a[0]);
			change.deletions = System.Convert.ToInt32(a[1]);
			change.path = a[2];
			changesL.Add(change);
		}
		changes = changesL.ToArray();
		p.WaitForExit();
		p.Close();
	}
}
public class GitChange
{
	public int additions, deletions;
	public string path, author, date, number, action, message;
	public GitChange()
	{
	}
}
public class GitRequest
{
	GitRequest()
	{

	}
	public bool error;
	public string errorMsg;
}
