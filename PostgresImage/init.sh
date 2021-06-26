#!/bin/bash
set -e

psql -v ON_ERROR_STOP=1 --username postgres <<-EOSQL
    CREATE DATABASE test;
EOSQL

psql -v ON_ERROR_STOP=1 --username postgres -d test -f $PATH_TO_INIT_SCRIPT