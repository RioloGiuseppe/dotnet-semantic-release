using System;
using System.IO;
using SemanticRelease.CommitAnalyzer;
using McMaster.Extensions.CommandLineUtils;

namespace SemanticRelease.Tool.CLI
{
    [Command(Description = "Get version of target project")]
    public class CurrentVersionCli : ToolCliBase
    {
        private SemanticReleaseEntry Parent { get; set; }

        protected override int OnExecute(CommandLineApplication app)
        {
            var targetProject = TargetProject ?? Parent.TargetProject;

            var project = new DotnetProjectWrapper(targetProject ?? System.Environment.CurrentDirectory);

            Console.WriteLine(project.GetVersion());

            return 0;
        }
    }
}