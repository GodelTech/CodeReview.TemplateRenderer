# Introduction 

#### scriban
Create issue summary using provided manifest
<pre>
> dotnet CodeReview.TemplateRenderer.dll scriban -o result.txt -t template.liquid  -d data.json
</pre>
| Agruments     | Key       | Required   | Type      | Description agrument      |
| ------------- | --------- | ---------- | --------- | ------------------------- |
| --template    | -t        | true       | string    | Path to scriban template file |
| --data        | -d        | true       | string    | Path to data file         |
| --output      | -o        | true       | string    | Output file path          |