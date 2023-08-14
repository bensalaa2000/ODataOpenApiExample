<![CDATA[
      Pour les migrations avec la ligne de commande
      dotnet tool run dotnet-ef 
		migrations add "Init" --startup-project .\src\DotNetCore.Axess\Presentation\Axess.Presentation\ --project .\src\DotNetCore.Axess\Infrastructure\Axess.Infrastructure\ -c ApplicationDbContext
	  dotnet tool run dotnet-ef 
		database update --startup-project .\src\DotNetCore.Axess\Presentation\Axess.Presentation\ --project .\src\DotNetCore.Axess\Infrastructure\Axess.Infrastructure\ -c ApplicationDbContext
]]>