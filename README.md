# Introduction 

How to build image 

docker build -t godeltech/codereview.template-renderer:0.0.3 -f src/CodeReview.TemplateRenderer/Dockerfile ./src
docker image tag godeltech/codereview.template-renderer:0.0.3 godeltech/codereview.template-renderer:latest
docker push godeltech/codereview.template-renderer:latest
docker push godeltech/codereview.template-renderer:0.0.3


Run:

docker run -v "/d/temp:/result"   --rm godeltech/codereview.template-renderer  run -p SonarAnalyzer.CSharp -o /result/result.yaml -j

Debug:

docker run -v "/d/temp:/result" -it --rm  --entrypoint /bin/bash  godeltech/codereview.template-renderer 