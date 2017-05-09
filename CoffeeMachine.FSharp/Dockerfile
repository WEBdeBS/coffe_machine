# Use an official Python runtime as a base image
FROM fsharp

# Set the working directory to /app
WORKDIR /CoffeeMachine

# Copy the current directory contents into the container at /app
ADD deploy /CoffeeMachine

# Make port 80 available to the world outside this container
EXPOSE 8081

# Define environment variable
ENV PORT 8081

# Run app.py when the container launches
CMD ["unzip" "deploy/Coffeemachine.zip"]
CMD ["mono", "build/CoffeeMachine.WebApi.exe"]


