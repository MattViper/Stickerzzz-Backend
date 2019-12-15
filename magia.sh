eval $(ssh-agent)
ssh-add ~/.docker/machine/machines/default/id_rsa
rsync -avzhe ssh --relative --omit-dir-times --progress ./ docker@$(docker-machine ip default):$(pwd)
docker-compose up