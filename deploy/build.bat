docker run -i -t microsoft/dotnet:2.0-sdk-stretch /bin/bash
mkdir /home/buildit
cd /home/buildit
git clone https://github.com/alfpedraza/buildit.crawler
cd /home/buildit/buildit.crawler/src
dotnet build
