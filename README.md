# Buildit Crawler

This is a simple web crawler utility that saves the crawling results to a file
in disk and prints it to the command line.

To build, test and run the code you will have to install Docker on your Ubuntu
machine. Then run the following commands to install the .NET SDK image and
download the project source code automatically:

```bash
# DOWNLOAD .NET SDK DOCKER IMAGE AND PROJECT SOURCE CODE
docker run -i -t microsoft/dotnet:2.0-sdk-stretch /bin/bash
mkdir /home/buildit
cd /home/buildit
git clone https://github.com/alfpedraza/buildit.crawler
```

## How to Build

Once you are in the Docker container bash command line, to build the code,
execute the following lines:

```bash
# BUILD THE CODE
cd /home/buildit/buildit.crawler/src
dotnet build
```

## How to Test

To execute both the unit tests and integration tests, run the following
commands:

```bash
# TEST THE CODE
cd /home/buildit/buildit.crawler/src/Buildit.Crawler.Test
dotnet test
```

## How to Run

Finally, to execute the application, just run the following commands:

```bash
#RUN THE CODE
cd /home/buildit/buildit.crawler/src/Buildit.Crawler
dotnet run https://buildit.wiprodigital.com /home/crawler.txt true
```

This command line utility accepts three parameters that can be modified
(although they don't allow spaces).

These are the parameters than can be changed:

  1. Domain to Crawl (e.g. https://buildit.wiprodigital.com)
  2. File Path to save the crawling result (e.g. /home/crawler.txt)
  3. Boolean Flag to wait for the user to press a key (e.g. true)
 

## Documented Trade-Offs
  - I only implemented the "A Href" and "Img Src" regular expression patterns
    in the hyperlink extractor class, but this could be improved easily adding
	more regular expression patterns.
  - I didn't search for any javascript, stylesheet or any other client side
    related file. This could be implemented easily adding more regular
	expressions patterns to the hyperlink extractor class. 
  - I marked a page as visited so I don't crawl the same URL twice.
  - I could have added a flag to specify the depth of the crawling, potentially
    avoiding the crawling in a very deep and hierarchical web site.
 


## Further Development
  - Include more unit test (I only have 95% of the coverage but mostly because
    of the integration test)
  - Include more integration tests (I only include a smoke test to know that
    the crawler works)
  - Implement "Politeness Policy" as per
    https://en.wikipedia.org/wiki/Web_crawler#Politeness_policy
  - Implement "Parallelization Policy" as per
    https://en.wikipedia.org/wiki/Web_crawler#Parallelization_policy