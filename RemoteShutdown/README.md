RemoteShutdown
==============

A simple process that waits and listens for a single UDP connection.

This UDP connection should contain the md5 of the current date and the password.

The date should be in the "years-months-days:hours:minute" format.

So, it would be md5sum("0000-00-00:00:00PASSWORD") for the beginning of time.

Before you tell me: Yes, this is not secure, I already know. This is just
secure enough for something as unimportant as my steam streaming server.

If you would like to implement something better, by all means I take commits.

Install
=======

  * Compile your binary with your settings
  * Copy it to your steam machine
  * Create a shortcut to the binary and drag it into the windows startup folder. 
