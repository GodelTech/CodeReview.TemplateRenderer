# Introduction 

How to build image 

docker build -t diamonddragon/template-renderer -f src/ReviewItEasy.TemplateRenderer/Dockerfile ./src

Run:

docker run -v "/d/temp:/result"   --rm diamonddragon/evaluator  run -p SonarAnalyzer.CSharp -o /result/result.yaml -j

Debug:

docker run -v "/d/temp:/result" -it --rm  --entrypoint /bin/bash  diamonddragon/file-converter 