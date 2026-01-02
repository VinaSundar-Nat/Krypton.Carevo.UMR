#!/bin/bash
set -e
set -u
source ./migration-var.sh

# Function to increment minor version
increment_version() {
    local version=$1
    # Split version by underscore (e.g., "1_0_1" -> ["1", "0", "1"])
    IFS='_' read -ra VERSION_PARTS <<< "$version"
    local major=${VERSION_PARTS[0]}
    local minor=${VERSION_PARTS[1]}
    local patch=${VERSION_PARTS[2]}
    
    # Increment patch version
    patch=$((patch + 1))
    
    # Return new version
    echo "${major}_${minor}_${patch}"
}

# --context DesignTimeContext \
if [ "$script" = true ] ; then

 # Increment version and update migration-var.sh
    new_version=$(increment_version "$versionSuffix")
    echo "Incrementing version from $versionSuffix to $new_version"
    
    # Update migration-var.sh with new version
    sed -i.bak "s/versionSuffix=\".*\"/versionSuffix=\"$new_version\"/" ./migration-var.sh
    rm -f ./migration-var.sh.bak
    
    echo "Updated migration-var.sh with new version: $new_version"
    name="${migrationName}_${new_version}_$(date +%Y%m%d%H%M%S)"
    if [ "$netmigrate" = true ] ; then
         dotnet ef migrations add "$name" \
        --output-dir ../Kr.Carevo.UMR.Persistence/Migrations \
        --project ../Kr.Carevo.UMR.Persistence \
        --startup-project ../Kr.Carevo.UMR.Api \
        --verbose 
    fi
  
    if [ "$sqlscript" = true ] ; then
        echo "Generating SQL script for migration: $name"

         dotnet ef migrations script --idempotent --output "../.dbmigrate/sql/$name.sql" \
        --project ../Kr.Carevo.UMR.Persistence \
        --startup-project ../Kr.Carevo.UMR.Api \
        --verbose
    fi
   
    # Find the generated migration file
    migrationFile=$(find ../Kr.Carevo.UMR.Persistence/Migrations -name "*_${name}.cs" -type f | head -n 1)

    if [ -f "$migrationFile" ]; then
        echo "migration file: $migrationFile created."
    fi
    
   
fi

if [ "$apply" = true ] ; then
    dotnet ef database update \
        --project ../Kr.Carevo.UMR.Persistence \
        --startup-project ../Kr.Carevo.UMR.Api \
        --verbose

    # Move migration files to sql/migrations directory
    echo "Moving migration files to sql/migrations..."
    mkdir -p ./sql/migrations
    if [ -d "../Kr.Carevo.UMR.Persistence/Migrations" ]; then
        find ../Kr.Carevo.UMR.Persistence/Migrations -name "*.cs" -type f ! -name "CarevoDbContextModelSnapshot.cs" -exec mv {} ./sql/migrations/ \;
        echo "Migration files moved successfully."
    fi
    
fi