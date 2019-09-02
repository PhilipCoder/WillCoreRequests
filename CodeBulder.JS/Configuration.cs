using CodeBuilder.IBuilder;
using CodeBuilder.JS.Builder;
using CodeBuilder.JS.Builder.Comments;
using CodeBuilder.JS.Builder.Objects.ImportsExports;
using CodeBuilder.JS.Builder.Properties;
using CodeBuilder.JS.Builder.Requests;
using CodeBuilder.JS.IBuilder;
using CodeBuilder.JS.IBuilder.Comments;
using CodeBuilder.JS.IBuilder.Properties;
using CodeBuilder.JS.IBuilder.Requests;
using CodeBuilder.JS.Properties;
using ICodeBuilder;
using ICodeBuilder.IOC;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeBuilder.JS
{
    public class Configuration : IConfiguration
    {
        private ESMode _esmode { get; set; }
        internal bool multiFileOutput { get; set; } = true;
        public IOCContainer IOCContainer { get; set; } = new IOCContainer();


        internal static Configuration Instance = new Configuration();
        /// <summary>
        /// When building a single ES5 file, this field indicates the JavaScript file's name that will be generated
        /// </summary>
        public String SingleFileOutputName { get; set; } = "requestContext.js";
        /// <summary>
        /// Folder the JavaScript files will be generated in. Relative to the executing assembly.
        /// </summary>
        public String OutputDirectory { get; set; } = "js";
        /// <summary>
        /// The folder the JavaScript models will be placed in. Relative to the output directory.
        /// </summary>
        public String ModelsFolder { get; set; } = "models";
        /// <summary>
        /// Factory action that returns the filenames for all generated RequestContexts
        /// </summary>
        public Func<string, string> RequestContextNameFactory { get; set; } = className => $"{className}RequestContainer";
        /// <summary>
        /// Factory action that returns the filenames for all generated models.
        /// </summary>
        public Func<string, string> ModelsNameFactory { get; set; } = className => $"{className}";
        /// <summary>
        /// Configuration for the generated comments
        /// </summary>
        public CommentConfiguration Comments { get; set; } = new CommentConfiguration();
        /// <summary>
        /// Dictionary containing the templates that will be used for code generation.
        /// </summary>
        public Dictionary<string, string> Templates { get; set; }
        /// <summary>
        /// Additional files that will be copied to the output directory.
        /// </summary>
        public Dictionary<string, string> AdditionalFiles = new Dictionary<string, string> {
            {"request\\request.js", Resources.request }
        };
        /// <summary>
        /// EcmaScript mode. 
        /// </summary>
        public ESMode ESMode
        {
            get { return _esmode; }
            set
            {
                if (value == ESMode.ES6)
                {
                    multiFileOutput = true;
                    setTemplate("JSRequestClass", Resources.classRequest);
                    setTemplate("JSPOCOClass", Resources.classPOKO);
                    setTemplate("NewDirectAssingmentArray", Resources.newArrayAssignment);
                    setTemplate("NewAssignmentArrayFromObj", Resources.newArrayAssignmentFromObject);
                    setTemplate("RunMethodRequest", Resources.runRequestMethod);
                    AdditionalFiles = new Dictionary<string, string> {{"request\\request.js", Resources.request }};
                }
                else
                {
                    multiFileOutput = false;
                    setTemplate("JSRequestClass", Resources.functionRequest);
                    setTemplate("JSPOCOClass", Resources.functionPOKO);
                    setTemplate("NewDirectAssingmentArray", Resources.newArrayAssignmentES5);
                    setTemplate("NewAssignmentArrayFromObj", Resources.newArrayAssignmentFromObjectES5);
                    setTemplate("RunMethodRequest", Resources.runRequestMethodFunction);
                    AdditionalFiles = new Dictionary<string, string> { { "request\\request.js", Resources.requestES5 } };
                }
                _esmode = value;
            }
        }
        /// <summary>
        /// Indicates if the web service's JSON has been configured for camel or paschal casing
        /// </summary>
        public bool APIConfiguredForCamelCase { get; set; } = true;

        public Configuration()
        {
            //Comments
            IOCContainer.TypeMapper
                .MapType<IParameterTypeComment, ParameterTypeComment>()
                .MapType<IPropertyComment, PropertyComment>()
                .MapType<IRequestContainerComment, RequestContainerComment>()
                .MapType<IClassConstructorComment, RequestContainerConstructorComment>()
                .MapType<IRunRequestMethodComment, RunRequestMethodComment>()
                .MapType<IClassConstructorComment, ClassConstructorComment>();
            //Properties
            IOCContainer.TypeMapper
                .MapType<IDirectAssignmentProperty, DirectAssignment>()
                .MapType<IDirectAssignmentProperty, NewDirectAssignment>()
                .MapType<IDirectAssignmentProperty, NewDirectAssingmentArray>()
                .MapType<IFromObjectAssignmentProperty, AssignmentFromObj>()
                .MapType<IFromObjectAssignmentProperty, NewAssignmentFromObj>()
                .MapType<IFromObjectAssignmentProperty, NewAssignmentArrayFromObj>();
            //Import and Export
            IOCContainer.TypeMapper
                .MapType<IExport, ExportPOCO>()
                .MapType<IExport, ExportRequestContext>()
                .MapType<IRequestContextImport, RequestContextImport>()
                .MapType<IImport, POCOImport>();
            //Request container
            IOCContainer.TypeMapper
                .MapType<IRunRequestProperty, RunRequestProperty>()
                .MapType<IRunRequestMethod, RunMethodRequest>()
                .MapType<IJSClass, JSRequestClass>();
            //POCO class
            IOCContainer.TypeMapper
                .MapType<IJSClass, JSPOCOClass>();
            //CodeBuilding
            IOCContainer.TypeMapper
                .MapType<IJSCodeBuilder, JSCodeBuilder>();

            //Mapping the templates to the builder classes
            Templates = new Dictionary<string, string> {
                    { "JSRequestClass", Resources.classRequest } ,
                    { "JSPOCOClass", Resources.classPOKO } ,
                    { "RunRequestProperty", Resources.runRequestProperty } ,
                    { "RunMethodRequest", Resources.runRequestMethod } ,
                    { "NewDirectAssingmentArray", Resources.newArrayAssignment } ,
                    { "NewDirectAssignment", Resources.newAssignment } ,
                    { "NewAssignmentArrayFromObj", Resources.newArrayAssignmentFromObject } ,
                    { "NewAssignmentFromObj", Resources.newAssignmentFromObject } ,
                    { "DirectAssignment", Resources.directAssignment } ,
                    { "AssignmentFromObj", Resources.assignmentFromObject } ,
                    { "RequestContextImport", Resources.import } ,
                    { "POCOImport", Resources.import } ,
                    { "ExportRequestContext", Resources.exports } ,
                    { "ExportPOCO", Resources.exports } ,
                    { "RunRequestMethodComment", Resources.runRequestMethodComment } ,
                    { "RequestContainerConstructorComment", Resources.classConstructorComment } ,
                    { "RequestContainerComment", Resources.requestContainerComment } ,
                    { "PropertyComment", Resources.propertyComment } ,
                    { "ParameterTypeComment", Resources.parameterTypeComment } ,
                    { "ClassConstructorComment", Resources.classConstructorComment }
                };
        }

        private void setTemplate(string key, string template)
        {
            if (Templates.ContainsKey(key))
            {
                Templates.Remove(key);
            }
            Templates.Add(key, template);
        }


    }
}
