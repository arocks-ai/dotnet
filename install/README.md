# DotNET SDK Install in Fedora

### Install the SDK
```
# Reference 1 : https://learn.microsoft.com/en-us/dotnet/core/install/linux-snap-sdk
# Reference 2 : https://medium.com/@alperonline/how-to-install-dotnet-6-to-ubuntu-154a9010fa9d

# Install SDK
sudo snap install dotnet-sdk --classic --channel 8.0/stable
sudo snap alias dotnet-sdk.dotnet dotnet # use if dotnet --info doesn't work
```


### Check Install and Path setup
```
# .NET sdk and runtime information
dotnet --info

# Check list of installed SDK
dotnet --list-sdks

# Check installed .NET runtime versions
dotnet --list-runtimes


# Path setup
export PATH=$PATH:$HOME/.dotnet/tools
#export DOTNET_ROOT=/snap/dotnet-sdk/current # NOTE - Path is Install dependent, run dotnet --info command and use the path before sdk string) 
export DOTNET_ROOT=/var/lib/snapd/snap/dotnet-sdk/252
export PATH=$PATH:$DOTNET_ROOT


# Export the install location
## The DOTNET_ROOT environment variable is often used by tools to determine where .NET is installed. 
export DOTNET_ROOT=/snap/dotnet-sdk/current

# Troubleshooting the SDK install issues
# Reference - https://github.com/dotnet/sdk/issues/11533
    sudo rm -rf /usr/share/dotnet
    sudo rm -rf /usr/bin/dotnet
    sudo rm -rf /etc/yum.repos.d/microsoft-prod.repo
    sudo dnf clean all
    sudo dnf upgrade
    sudo init 6 #reboots the machine
    sudo dnf install dotnet-sdk-8.0  # Verify inistall before install
```
