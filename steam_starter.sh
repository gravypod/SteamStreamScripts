# Wake remote steam stream server.

HOST=192.168.0.0
MAC=00:00:00:00:00:00
PORT=1111
PASSWORD="PASSWORD"

HOST=${2:-$HOST}
PORT=${3:-$PORT}


# Wake remote server 
wakeonlan -i $HOST $MAC

# Bypass problem with drivers on AMD-APU
export LD_PRELOAD='/usr/$LIB/libstdc++.so.6' #Export so all child processes are affected as well
export DISPLAY=:0
#export LIBGL_DEBUG=verbose

shutdown_remote_server()
{
        echo "Sending shutdown packet"
        echo -ne "$(date -u +%Y-%m-%d:%H:%M)$PASSWORD" | xargs | md5sum | cut -d" " -f1 | nc -4u -w1 $HOST $PORT
}

# Start Steam and wait for Steam to close. When Steam closes. Stop remote server.
steam "$@" && shutdown_remote_server

