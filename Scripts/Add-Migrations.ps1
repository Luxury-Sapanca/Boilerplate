[string]$MigrationName = $null
while ([string]::IsNullOrWhitespace($MigrationName)) {
	$MigrationName = Read-Host "Migration name"
}

dotnet ef migrations add $MigrationName --project ../Libraries/Boilerplate.Data -s ../Presentation/Boilerplate.Api/Boilerplate.Api.csproj