#!/bin/bash
set -e
set -u
source ./migration-var.sh

# --context DesignTimeContext \
if [ "$script" = true ] ; then
    dotnet ef migrations add "$migrationName" \
        --output-dir ../Kr.Carevo.UMR.Persistence/Migrations \
        --project ../Kr.Carevo.UMR.Persistence \
        --startup-project ../Kr.Carevo.UMR.Api \
        --verbose 

    # Find the generated migration file
    migrationFile=$(find ../Kr.Carevo.UMR.Persistence/Migrations -name "*_${migrationName}.cs" -type f | head -n 1)

    if [ -f "$migrationFile" ]; then
        echo "migration file: $migrationFile created."
    fi
fi

if [ "$apply" = true ] ; then
    dotnet ef database update \
        --project ../Kr.Carevo.UMR.Persistence \
        --startup-project ../Kr.Carevo.UMR.Api \
        --verbose
fi