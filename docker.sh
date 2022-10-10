#!/bin/bash

# Get the username name.
echo "Enter username: "
read username

# Get the migrations repository name.
echo "Enter migrations repository name: "
read migrations_repo

# Get the website repository name.
echo "Enter website repository name: "
read website_repo

# Build the migrations image.
echo "Building migrations image..."
docker build -t $username/$migrations_repo:latest $(dirname "$(realpath $0)")/lucentrp-migrations
echo "Done!"

# Build the website image.
echo "Building website image..."
docker build -t $username/$website_repo:latest $(dirname "$(realpath $0)")/lucentrp-website
echo "Done!"

# Push the migrations image.
echo "Pushing migrations image..."
docker push $username/$migrations_repo:latest
echo "Done!"

# Push the website image.
echo "Pushing website image..."
docker push $username/$website_repo:latest
echo "Done!"
