using CodeBulder.IBuilder;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace CodeBulder.JS.Builder
{
    public class JSClassContext<T>
    {
        public List<IJSClass> Classes { get; set; }
        public JSClassContext(JSClassContainer<T> container)
        {
            Classes = new List<IJSClass>();
            StringBuilder outputFileContents = new StringBuilder();
            var outputDirectoryContext = Path.Combine(Directory.GetCurrentDirectory(), Configuration.Instance.OutputDirectory);
            Directory.CreateDirectory(outputDirectoryContext);
            var shouldCreateMultipuleFiles = Configuration.Instance.MultiFileOutput;
            foreach (var filePath in JSBuilderIOCContainer.Instance.AdditionalFiles.Keys)
            {
                var outputDir = Path.Combine(outputDirectoryContext, filePath);
                if (shouldCreateMultipuleFiles)
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(outputDir));
                    File.WriteAllText(outputDir, JSBuilderIOCContainer.Instance.AdditionalFiles[filePath]);
                }
                else
                {
                    outputFileContents.AppendLine(JSBuilderIOCContainer.Instance.AdditionalFiles[filePath]);
                }
            }
            foreach (var model in container.Models.Values)
            {
                var newClass = JSBuilderIOCContainer.Instance.CreateJSClassFromTypeStructure(model);
                var result = newClass.GetText();
                var outputDir = Path.Combine(outputDirectoryContext, Configuration.Instance.ModelsFolder);
                if (shouldCreateMultipuleFiles)
                {
                    Directory.CreateDirectory(outputDir);
                    File.WriteAllText(Path.Combine(outputDir, model.TypeName + ".js"), result);
                }
                else
                {
                    outputFileContents.AppendLine(result);
                }
                Debug.WriteLine(result);
            }
            foreach (var classDef in container.Classes)
            {
                var newClass = JSBuilderIOCContainer.Instance.CreateJSClassFromStructure(classDef);
                Classes.Add(newClass);
                var result = newClass.GetText();
                if (shouldCreateMultipuleFiles)
                {
                    File.WriteAllText(Path.Combine(outputDirectoryContext, classDef.Name + ".js"), result);
                }
                else
                {
                    outputFileContents.AppendLine(result);
                }
                Debug.WriteLine(result);
            }
            if (!shouldCreateMultipuleFiles)
            {
                File.WriteAllText(Path.Combine(outputDirectoryContext, Configuration.Instance.SingleFileOutputName), outputFileContents.ToString());
            }
            
        }
    }
}
