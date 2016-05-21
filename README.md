Steam Streaming Automation
==========================

A collection of utilities I use to make steam streaming acceptable.

This includes:

  * Sending a Wake On Lan packet to my Steam streaming machine
  * Starting Steam
  * An application (installed server side) that allows for remote shutdown
  * Automatically shutdown the remote server (into hibernate mode)

RemoteShutdown takes care of the 3rd item on the ajenda. 
steam_starter.sh takes care of the rest. 


Install:

  * Follow the install instructions in RemoteShutdown.
  * Copy the steam_starter.sh into /opt/ or something.
  * Read and configure the shell script to your liking.
  * Mark the file as executable. 
  * Edit '/usr/share/applications/steam.desktop' to launch 'steam_starter.sh'
