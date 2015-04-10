# slack-svn-subversion-csharp
Slack commit hook for subversion on windows

This is a **post-commit** hook for **subversion** so it executes after each commit and sends commit information to a **slack** channel.

The following parameters are required:
- Slack hook
- Path to svnlook.exe
- REPOS_PATH (Path to the repository)
- REV (Revision number)
- Slack channel i.e. #general

# Example

slacksubversion.exe https://your_slack_hook C:\svn\svnlook.exe %2 %1 #channel_name
