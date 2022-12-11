using System;
using System.IO;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Xsl;
using NJsonSchema;
using NJsonSchema.CodeGeneration.TypeScript;
using NJsonSchema.Visitors;
using NSwag;
using NSwag.CodeGeneration.CSharp;
using NSwag.CodeGeneration.TypeScript;

namespace APIClientGenerator
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var openApiDocument = await OpenApiDocument.FromUrlAsync(args[0]);
            var settings = new TypeScriptClientGeneratorSettings();

            settings.TypeScriptGeneratorSettings.TypeStyle = TypeScriptTypeStyle.Interface;
            settings.TypeScriptGeneratorSettings.TypeScriptVersion = 4.9M;
            settings.TypeScriptGeneratorSettings.DateTimeType = TypeScriptDateTimeType.MomentJS;
            settings.ImportRequiredTypes = true;
            settings.TypeScriptGeneratorSettings.TypeStyle = TypeScriptTypeStyle.Class;
            settings.PromiseType = PromiseType.Promise;
            settings.TypeScriptGeneratorSettings.NullValue = TypeScriptNullValue.Null;

            settings.Template = TypeScriptTemplate.Axios;
            settings.WrapResponses = true;
            settings.UseTransformOptionsMethod = true;
            settings.ClientBaseClass = "ApiBase";
            settings.UseGetBaseUrlMethod = true;
            
            var generator = new TypeScriptClientGenerator(openApiDocument, settings);
            var code = generator.GenerateFile();
            var resultPath = Path.Combine(Directory.GetCurrentDirectory(), args[1]);
            code = $"import {{ ApiBase }} from './ApiBase';\r\n{code}";
            await File.WriteAllTextAsync(resultPath, code);

        }
      

      
    }
}