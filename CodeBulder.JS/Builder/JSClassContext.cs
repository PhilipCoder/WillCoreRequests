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
            var outputDirectoryContext = Path.Combine(Directory.GetCurrentDirectory(), container.Configuration.OutputDirectory);
            if (Directory.Exists(outputDirectoryContext))
            {
                Directory.Move(outputDirectoryContext, Path.Combine(Directory.GetCurrentDirectory(), "deletedTemp"));
                Directory.Delete(Path.Combine(Directory.GetCurrentDirectory(), "deletedTemp"), true);
            }
            Directory.CreateDirectory(outputDirectoryContext);
            var shouldCreateMultipuleFiles = container.Configuration.MultiFileOutput;
            foreach (var filePath in container.InstanceConfiguration.AdditionalFiles.Keys)
            {
                var outputDir = Path.Combine(outputDirectoryContext, filePath);
                if (shouldCreateMultipuleFiles)
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(outputDir));
                    File.WriteAllText(outputDir, container.InstanceConfiguration.AdditionalFiles[filePath]);
                }
                else
                {
                    outputFileContents.AppendLine(container.InstanceConfiguration.AdditionalFiles[filePath]);
                }
            }
            foreach (var model in container.Models.Values)
            {
                var newClass = container.InstanceConfiguration.CreateJSClassFromTypeStructure(model);
                var result = newClass.GetText();
                var outputDir = Path.Combine(outputDirectoryContext, container.Configuration.ModelsFolder);
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
                var newClass = container.InstanceConfiguration.CreateJSClassFromStructure(classDef);
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
                File.WriteAllText(Path.Combine(outputDirectoryContext, container.Configuration.SingleFileOutputName), outputFileContents.ToString());
            }
            
        }
    }
}
