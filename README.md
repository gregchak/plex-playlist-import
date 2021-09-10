# PLEX Playlist Import
This is an application written to help PLEX Media Server (PMS) owners import playlists into PLEX.  This is written as a Universal Windows Platform application.  Current builds are for x86 and x64 architectures only. This application is not currently available in the Microsoft Store.

# Motivation
Currently PMS does not have an GUI for importing playlists. However, there are exposed endpoints that can be utilized for this purpose.

# Installation
Currently the release is not signed with a certficate.  This is one of the reasons why its not currently on Microsoft Store.  In order to install you need to run the powershell script `Install.ps1`.  You may be asked to install a certificate during the installation process.  This is a development/testing certificate.

# Usage
This tool is quite simple in execution.  The most important thing is getting the correct information to fill into the form.  There are instructions in the app for getting Section ID and X-Plex Token and is also noted below.  

The "Path to playlist file" is very important to understand.  This path is an absolute path that is relative to the PMS server.  For example, if your PMS is hosted on Windows, the path would starting with something like `C:\plex\music...`.  If your PMS is hosted on Linux, like a NAS or something, the path would start with something like `/volume1/plex/music...`.  

In addition to making sure the Path to playlist file is correct, the path to the music files in your `.m3u` file need to be setup the same way.  

**Its important to note that in a scenario in which you are running the PLEX Playlist Import tool from a computer that is NOT your PMS, the above mentioned paths are NOT relative to the computer this tool is running on, but rather relative to the computer your on which your PMS is hosted.**

**To Get Section ID and X-Plex Token:**

Taken from reddit 
https://www.reddit.com/r/PleX/comments/ecirqa/how_to_manually_import_an_m3u_playlist_into_plex/
1. Open your Plex server web app , as normal.

2. In Plex , navigate to any file in the library for which you want to import a playlist. (e.g. in your 'Rock Music' library, navigate to 'BornToBeWild.mp3').

3. On the play bar for that library item, click the More Actions ellipses (...) , select Get Info , and then click View XML. A new browser window opens containing XML details about the library item.

4. From the URL, copy the X-Plex-Token value into a text editor.

5. From the page content, copy the librarySectionID value into a text editor.

# Screenshot
![image](https://user-images.githubusercontent.com/5882933/116017209-fe3a8b00-a60c-11eb-8667-b39be11241e8.png)

# Version History

**1.0.4.0** - 2021-04-25  
Initial release.

# License
GNU General Public License v3.0

https://github.com/gregchak/plex-playlist-import/blob/main/LICENSE

