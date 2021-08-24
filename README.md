### Generate tables

```
dotnet ef migrations add TestData --project Day6 --context TestingContext

dotnet ef database update TestData --project Day6 --context TestingContext
```
