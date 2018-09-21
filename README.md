# Buildit Crawler

## How to Build



## How to Test



## How to Run



## Documented Trade-Offs
  - I only implemented the "A Href" and "Img Src" regular expression patterns in the hyperlink extractor class, but this could be improved easily adding more regular expression patterns.
  - I didn't search for any javascript, stylesheet or any other client side related file. This could be implemented easily adding more regular expressions patterns to the hyperlink extractor class. 
  - I marked a page as visited so I don't crawl the same URL twice.
  - I could have added a flag to specify the depth of the crawling, potentially avoiding the crawling in a very deep and hierarchical web site.
 


## Further Development
  - Include more unit test (I only have 95% of the coverage but mostly because of the integration test)
  - Include more integration tests (I only include a smoke test to know that the crawler works)
  - Implement "Politeness Policy" as per https://en.wikipedia.org/wiki/Web_crawler#Politeness_policy
  - Implement "Parallelization Policy" as per https://en.wikipedia.org/wiki/Web_crawler#Parallelization_policy