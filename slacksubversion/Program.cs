using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;

namespace slacksubversion
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string svnLook = args[1];
            string rev = args[2];
            string path = args[3];
            string channel = args[4];

            string log = GetOutput(svnLook, "log -r " + rev + " " + path);
            string author = GetOutput(svnLook, "author -r " + rev + " " + path);
            string[] changed = GetOutput(svnLook, "changed -r " + rev + " " + path).Split('\n');
            
            if (changed.Length > 10)
            {
                Array.Resize(ref changed, 11);
                changed[changed.Length - 1] = "\n...";
            }

            Payload payLoad = new Payload();
            payLoad.Markdown = false;
            payLoad.Username = "Subversion";
            payLoad.Text = log + "\n" + String.Join("\n",changed) + "\n- " + author;
            payLoad.Channel = channel;
            payLoad.Icon_url = "https://subversion.apache.org/images/svn-square.jpg";

            SlackClient slackClient = new SlackClient(args[0]);
            slackClient.PostMessage(payLoad);

        }


        private static string GetOutput(string executable, string arguments)
        {
            ProcessStartInfo start = new ProcessStartInfo();
            start.FileName = executable;
            start.UseShellExecute = false;
            start.RedirectStandardOutput = true;
            start.CreateNoWindow = true;
            start.Arguments = arguments;

            string result = "";
            using (Process process = Process.Start(start))
            {
                using (StreamReader reader = process.StandardOutput)
                {
                    result += reader.ReadToEnd();
                }
            }
            return result;
        }
    }

}
