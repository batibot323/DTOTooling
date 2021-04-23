# DTOTooling
POC for analyzing a legacy codebase in order to adapt a microservices architecture. Use it in helping analyze a large codebase. Makes organizing and profiling classes, DTO's and etc. much easier.

# Usage
Put files you want to analyze inside the `filesToParse` folder. Run the program and you'll see the `properties` and `methods` inside the `reults` folder. Then, it will analyze each file and compare it to every other file to list down commonalities between them. So this will run almost O(n^2).
