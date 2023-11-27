# Database:

## Employee tábla:
- Id
- Name
- Email
- Hashed password
- Role

## Project tábla:
- Id
- Name
- Description
- StartDate
- EndDate

## OnDuty tábla:
Employee és Project many-to-many kapcsolatához összekötő tábla
- EmployeeId
- ProjectId

## OnDutyDates tábla:
Az adott employee adott projektben melyik napokon ügyeletes
- OnDutyId
- DutyDay

# Business Logic:

## Authentication:
- JWT tokenek által (ez tartalmazni fogja a user adatait, ez alapján azonosítjuk a felhasználót és a jogosultságait - admin vagy sem)

## Email küldés:
- Jenkinses cron job hívja meg a megfelelő API endpointokat (https://plugins.jenkins.io/http_request/):
    - Ki a másnapi ügyeletes minden projekthez (napi 1x) -> egy dátum alapján (projekt, ügyeletes email címe) adatpárokból álló listát ad vissza
    - Ha van olyan projekt, aminél nincs véglegesített ügyeleti rend a következő hétre (heti 1x) -> egy projekt lista, illetve adminok email címének listája
- A megfelelő jobok a visszakapott listákon szereplő személyeknek küld emailt (https://plugins.jenkins.io/email-ext/)

## API:
- /login [GET] -> input: email, jelszó -> válasz: JWT token, bejelentkezés
- /registration [POST] -> input: email, jelszó, név -> válasz: sikeres/sikertelen regisztráció, Employee táblában új elem felvétele
- /profile [GET] -> input: employeeId -> válasz: employee adatai
    - /updateEmployee [PUT] -> input: employeeId, employeeName?, employeeEmail?, employeePwd? -> válasz: sikeres/sikertelen módosítás, Employee táblában elem módosítása
    - /removeEmployee [DELETE] -> input: employeeId -> válasz: sikeres/sikertelen törlés, Employee táblában elem törlése
- /admin ->
    ### GET
    - /getAllProjects -> input: null -> válasz: visszaadja az összes projektet
    - /getEmployeesForProject -> input: projectId -> válasz: visszaadja az összes dolgozót, aki hozzá van rendelve a projekthez
    - /getOnDutyDatesForProjects -> input: projectId -> válasz: visszaadja az ügyeleti beosztást az adott projekthez
    - /getOnDutyForProjectsOnDay -> input: onDutyDay -> válasz: visszaadja az összes projekthez, hogy ki a beosztott ügyeletes (az email címüket) az adott napra
    - /getNonFinalOnDutyForProjects -> input: null -> válasz: visszaadja az összes projektet, aminél nincs véglegesítve a beosztás, valamint az admin(ok) email cím listáját
    ### POST
    - /addEmployeeToProject -> input: projectId, employeeId -> válasz: sikeres/sikertelen felvétel, OnDuty táblában új elem felvétele
    - /addProject -> input: projectName, projectDescription, projectStartDate, projectEndDate -> válasz: sikeres/sikertelen felvétel, Project táblában új elem felvétele
    - /addOnDutyForProject -> input: onDutyId, onDutyDay -> válasz: sikeres/sikertelen felvétel, OnDutyDates táblában új elem felvétele
    ### PUT
    - /updateProject -> input: projectId, projectName?, projectDescription?, projectStartDate?, projectEndDate? -> válasz: sikeres/sikertelen módosítás, Project táblában elem módosítása
    ### DELETE
    - /removeEmployeeFromProject -> input: projectId, employeeId -> válasz: sikeres/sikertelen törlés, OnDuty, illetve OnDutyDates táblában elem(ek) törlése
    - /removeProject -> input: projectId ->  válasz: sikeres/sikertelen törlés, Project táblában elem törlése
    - /removeOnDutyForProject -> input: onDutyId, onDutyDay -> válasz: sikeres/sikertelen törlés, OnDutyDates táblában elem törlése
  
# Frontend:
### Login Page
Itt tud a felhasználó bejelentkezni (ha már korábban regisztrálva lett az adatbázisba) email cím és jelszó megadásával.

### Registration Page
Itt tud a felhasználó regisztrálni név, email cím és jelszó megadásával. Ha olyan email címet ad meg, ami már létezik, akkor hibaüzenetet kap.

### Admin Page
Itt tudja az admin role-al rendelkező felhasználó az adatbázisokban lévő adatokat módosítani:
- Projektek felvétele/módosítása/törlése
- Dolgozók és projektek összerendeléseinek felvétele/törlése
Alapértelmezetten egy projekt listát lát, amelyben ha lenyit egy projektet, akkor ott láthatja az ahhoz hozzárendelt dolgozókat.
Ezen felül a lenyitott projektnél lesz egy átirányító gomb az adott projekthez tartozó OnDuty Page-re

### OnDuty Page
Itt tudja az admin role-al rendelkező felhasználó az ügyeleti beosztást módosítani:
- Adott naphoz új ügyeletes felvétele
- Adott naphoz rendelt ügyeletes megváltoztatása
- Adott naphoz rendelt ügyeletes törlése
Egy naptárat lát, amiben minden napnál látszódik, hogy ki van beosztva. 
Ha ír/töröl/módosít a naptárban, akkor a módosításokat el tudja menteni egy Save gombbal, ami az összes változást elmenti az adatbázisban.

### Profile Page
Itt tudja a felhasználó megnézni, illetve megváltoztatni az adatait vagy törölni a profilját
	