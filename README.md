# Hit Point Manager

Hit Point Manager is a proof of concept API that loads a simple character,
calculates its starting hit points (using the rounded-up average method),
and manages its hit points.

Before building and running the application, you may modify the `characters.json`
file found in the `DDB.HitPointManager.Data/data` folder.

## Running Hit Point Manager in Docker

Open a command prompt from the folder with the Dockerfile
and run the following commands to build and launch the app:

```
docker build -t hpmanager .
docker run --rm -d -p 8080:80 --name ddbhpmanager hpmanager
```

Once the app is running, open a browser and navigate to
http://localhost:8080/api/v1/character/briv
in order to verify the app is running.

## Assumptions

- There is no indicator for which class is the character's starting class (used to determine max HP at level 1).
  A 'starting class' indicator could be added to the classes or the max HD could be used. For purposes of this demo,
  the first class in the list is used to determine level 1 HP.
- Assumed vulnerability should also be considered along with immunity and resistance.
- Assumed that stat bonuses stack and that multiple copies of an item are not included on a character.
- Assumed that handling character death was out of scope (excessive damage, no healing if dead)

## Testing the App

Exceptions are surfaced in this demo app. It is assumed they would be handled
appropriately by the UI in a production environment.

### Deal Damage

### Heal

### Add Temporary Hit Points

### Status

Returns the character's current health

### Reset

Restores the character to starting values

## Misc - Notes, Areas for Improvement

- Enums - Some properties such as damage types, and defense types (vulnerability, etc) were made into enums to
  assist with strongly typing things. However, it could be argued this reduces flexibility
  in the event new types are introduced. Modifer.AffectedValue was left as a string since it could potentially
  target various fields other than a character stat.
- Error handling - Wire up some global error handling so the API returns an appropriate response
  based on the error type and code does not have to worry about catching/handling errors.
- CharacterHealth responses could be improved to include other stats like Status (Alive, Unconscious, Dead),
  death saves, conditions, etc.
- Damage endpoint could check for excessive damage (character HP max) that causes character death.
