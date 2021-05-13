# Introduction 

Command line template rending tool. It accepts JSON file as data source and .liquid template as template file. 

## liquid
Create issue summary using provided manifest
<pre>
> dotnet CodeReview.TemplateRenderer.dll liquid -o result.txt -t template.liquid  -d data.json
</pre>
| Agruments     | Key       | Required   | Type      | Description agrument      |
| ------------- | --------- | ---------- | --------- | ------------------------- |
| --template    | -t        | true       | string    | Path to scriban template file |
| --data        | -d        | true       | string    | Path to data file         |
| --output      | -o        | true       | string    | Output file path          |