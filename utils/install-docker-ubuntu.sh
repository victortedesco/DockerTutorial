#!/bin/bash
# https://docs.docker.com/engine/install/ubuntu/#install-using-the-repository

RESET='\033[0m'
LIGHT_BLUE='\033[1;34m' 

for pkg in docker.io docker-doc docker-compose docker-compose-v2 podman-docker containerd runc; do sudo apt-get remove $pkg; done

sudo apt-get update
sudo apt-get install ca-certificates curl
sudo install -m 0755 -d /etc/apt/keyrings
sudo curl -fsSL https://download.docker.com/linux/ubuntu/gpg -o /etc/apt/keyrings/docker.asc
sudo chmod a+r /etc/apt/keyrings/docker.asc

echo \
  "deb [arch=$(dpkg --print-architecture) signed-by=/etc/apt/keyrings/docker.asc] https://download.docker.com/linux/ubuntu \
  $(. /etc/os-release && echo "$VERSION_CODENAME") stable" | \
  sudo tee /etc/apt/sources.list.d/docker.list > /dev/null
sudo apt-get update

echo -e "Adding$LIGHT_BLUE \"$USER\"$RESET to docker group."
sudo groupadd docker
sudo usermod -aG docker $USER

sudo apt-get install docker-ce docker-ce-cli containerd.io docker-buildx-plugin docker-compose-plugin

clear
echo -e "Docker sucessfully installed!"
echo "Please, restart the system or disconnect from your current session"
echo "Or else, you won't have permissions to use docker without the sudo command"
echo -e "You can check if Docker is working with$LIGHT_BLUE docker run hello-world"
echo -e "Press any key to continue...$RESET"

read -n 1 -s
