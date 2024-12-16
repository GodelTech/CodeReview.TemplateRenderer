# CodeReview.TemplateRenderer

Docker Image: https://hub.docker.com/r/godeltech/codereview.template-renderer

## Description

Command line template rending tool. It accepts JSON file as data source and .liquid template as template file.

## Usage

### How To build Docker Image

To build the Docker image, run the following command:

```bash
docker build -t codereview.template-renderer .
```

### How to run Docker Container

```bash
docker run codereview.template-renderer liquid -o result.txt -t template.liquid  -d data.json
```

### Commands And Parameters

#### liquid
Create issue summary using provided manifest
<pre>
> dotnet CodeReview.TemplateRenderer.dll liquid -o result.txt -t template.liquid  -d data.json
</pre>
| Arguments  | Key | Required | Type   | Description argument          |
|------------|-----|----------|--------|-------------------------------|
| --template | -t  | true     | string | Path to scriban template file |
| --data     | -d  | true     | string | Path to data file             |
| --output   | -o  | true     | string | Output file path              |

## License

This project is licensed under the MIT License. See the LICENSE file for more details.