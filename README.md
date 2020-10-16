
## Running Hit Point Manager in Docker

Open a command prompt from the folder with the Dockerfile
and run the following commands to build and launch the app:

```
docker build -t hpmanager .
docker run -d -p 8080:80 --name myapp hpmanager
```

Once the app is running, open a browser and navigate to 
http://localhost:8080/WeatherForecast
in order to verify the app is running.

## Testing the App

### Deal Damage

### Heal

### Add Temporary Hit Points 
