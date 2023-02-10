# UniPASS

### Setup

1. Clone the repo
2. Checkout to develop
3. Create a new branch off of develop branch.
4. Complete the assigned task
5. Commit your changes.
6. Push your changes.
    - `git push -u origin <branch-name>`
    - `<branch-name>` typically will be something like `feature/add-server-auth` , `fix/handle-404-error`, etc. No strict rules on this one though
7. Create a PR to merge your changes to develop branch
8. Wait for it to be approved or merged
9. When approved you can view your changes live on [here](https://unipass-dev.azurewebsites.net) _(Might take a few seconds)_.
10. Always **`git pull`** before you push to avoid merge conflicts.

### Requirements

.NET 6.0+

Node 18.12.1

### Local Setup

#### API

1. `cd api`
2. `dotnet run`: Will build and run your API, automatically solving required Nuget dependencies.
3. `dotnet watch run`: Performs same as do `dotnet run`, **but** with hot reload enabled.

#### React Client

1. `cd client`
2. `npm run build`
3. `npm install -g serve` _Might be optional_
4. `serve -s build`
