using CodeBuilder.IBuilder;
using CodeBuilder.JS.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace CodeBuilder.JS.Builder
{
    public class JSCodeBuilder : IJSCodeBuilder
    {
        public List<IJSClass> Classes { get; set; }
        public JSCodeBuilder(JS.JavaScript container)
        {
            Debug.WriteLine($"WillCore.Requests: Starting code generation...");
            Classes = new List<IJSClass>();
            StringBuilder outputFileContents = new StringBuilder();
            string outputDirectoryContext = createDirectories();
            createAdditionalFiles(outputFileContents, outputDirectoryContext);
            createPOCOClasses(container, outputFileContents, outputDirectoryContext);
            createRequestContainers(container, outputFileContents, outputDirectoryContext);
            createSingleFile(outputFileContents, outputDirectoryContext);
            Debug.WriteLine($"WillCore.Requests: Finished...");
        }

        private static void createSingleFile(StringBuilder outputFileContents, string outputDirectoryContext)
        {
            if (!Configuration.Instance.multiFileOutput)
            {
                writeFile(outputFileContents.ToString(), Path.Combine(outputDirectoryContext, Configuration.Instance.SingleFileOutputName));
            }
        }

        private void createRequestContainers(JavaScript container, StringBuilder outputFileContents, string outputDirectoryContext)
        {
            foreach (var classDef in container.Classes)
            {
                var newClass = DI.Get<IJSClass>(classDef);
                Classes.Add(newClass);
                var jsCode = ((JSRenderble)newClass).GetText();
                jsCode = TemplateCleaner.CleanTemplate(jsCode);
                if (Configuration.Instance.multiFileOutput)
                {
                    writeFile(jsCode, Path.Combine(outputDirectoryContext, classDef.Name + ".js"));
                }
                else
                {
                    outputFileContents.AppendLine(jsCode);
                }
            }
        }

        private static void createPOCOClasses(JavaScript container, StringBuilder outputFileContents, string outputDirectoryContext)
        {
            foreach (var model in container.Models.Values)
            {
                var jsCode = ((JSRenderble)DI.Get<IJSClass>(model)).GetText();
                jsCode = TemplateCleaner.CleanTemplate(jsCode);
                var outputDir = Path.Combine(outputDirectoryContext, Configuration.Instance.ModelsFolder);
                if (Configuration.Instance.multiFileOutput)
                {
                    Directory.CreateDirectory(outputDir);
                    writeFile(jsCode, Path.Combine(outputDir, Configuration.Instance.ModelsNameFactory(model.TypeName) + ".js"));
                }
                else
                {
                    outputFileContents.AppendLine(jsCode);
                }
            }
        }

        private static void writeFile(string jsCode, string filePath)
        {
            Debug.WriteLine($"Writing file: {filePath}...");
            File.WriteAllText(filePath, jsCode);
        }

        private static void createAdditionalFiles(StringBuilder outputFileContents, string outputDirectoryContext)
        {
            foreach (var filePath in Configuration.Instance.AdditionalFiles.Keys)
            {
                var outputDir = Path.Combine(outputDirectoryContext, filePath);
                if (Configuration.Instance.multiFileOutput)
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(outputDir));
                    File.WriteAllText(outputDir, Configuration.Instance.AdditionalFiles[filePath]);
                }
                else
                {
                    outputFileContents.AppendLine(Configuration.Instance.AdditionalFiles[filePath]);
                }
            }
        }

        private static string createDirectories()
        {
            var outputDirectoryContext = Path.Combine(Directory.GetCurrentDirectory(), Configuration.Instance.OutputDirectory);
            if (Directory.Exists(outputDirectoryContext))
            {
                Directory.Move(outputDirectoryContext, Path.Combine(Directory.GetCurrentDirectory(), "deletedTemp"));
                Directory.Delete(Path.Combine(Directory.GetCurrentDirectory(), "deletedTemp"), true);
            }
            Directory.CreateDirectory(outputDirectoryContext);
            return outputDirectoryContext;
        }
    }
}
