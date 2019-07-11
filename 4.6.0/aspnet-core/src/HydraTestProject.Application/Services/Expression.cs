using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Abp.Domain.Repositories;
using AutoMapper;
using Castle.DynamicProxy.Generators.Emitters;
using Castle.MicroKernel.Registration;
using HydraTestProject.DTO;
using HydraTestProject.Models.Core;
using HydraTestProject.Query;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;

namespace HydraTestProject.EntityFrameworkCore.Repositories
{
    public class Expression<TEntity> where TEntity : class
    {
        private readonly SortedDictionary<string, List<ExpressionDto>> expressionDto =
            new SortedDictionary<string, List<ExpressionDto>>();



        public async Task<Type> CreateType()
        {
            var build = GetDynamicTypeDefinition().ToString();
            var roslynScript = CSharpScript
                .Create(build, ScriptOptions.Default.WithReferences(typeof(HydraTestProjectCoreModule).Assembly))
                .ContinueWith<Type>($"return typeof({typeof(TEntity).Name}QueryResultType);");
            var result = await roslynScript.RunAsync();

            return result.ReturnValue;

        }

        public StringBuilder GetDynamicTypeDefinition()
        {
            var build = new StringBuilder();

            var ClassName = "Maja";

            build.AppendLine($"public class {ClassName} {{");

            foreach (var pair in expressionDto)
            {
                // public {Key}SubType {Key} { get; set; }
                build.AppendLine($"public {pair.Key}SubType {pair.Key} {{ get; set; }}");
            }

            build.AppendLine("}");

            return build;

        }




    }
}
