Build image:
docker build -t testpostgres .

Start image:
docker run --name testrunpsql -e POSTGRES_PASSWORD=mysecretpassword -e PATH_TO_INIT_SCRIPT=/initscript/init.sql -v /home/claude/AdHocTestenvironments/SampleWebapp/PostgresImage/initscript:/initscript -d testpostgres:latest

Stop container:
docker container stop testrunpsql

Delete Conainer:
docker container rm -f testrunpsql


Connect to server: 
docker exec -it testrunpsql /bin/sh

Logs:
docker logs testrunpsql