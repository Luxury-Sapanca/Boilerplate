[string]$MigrationName = $null
while ([string]::IsNullOrWhitespace($MigrationName)) {
	$MigrationName = Read-Host "Migration name"
}

dotnet ef migrations add $MigrationName