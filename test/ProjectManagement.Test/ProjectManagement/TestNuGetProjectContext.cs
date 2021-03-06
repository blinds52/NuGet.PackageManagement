﻿using NuGet.Packaging;
using NuGet.Packaging.Core;
using NuGet.ProjectManagement;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Test.Utility
{
    public class TestNuGetProjectContext : IMSBuildNuGetProjectContext
    {
        public TestExecutionContext TestExecutionContext { get; set; }
        public void Log(MessageLevel level, string message, params object[] args)
        {
            Console.WriteLine(message, args);
        }

        public FileConflictAction ResolveFileConflict(string message)
        {
            return FileConflictAction.IgnoreAll;
        }

        public PackageExtractionContext PackageExtractionContext
        {
            get;
            set;
        }

        public ISourceControlManagerProvider SourceControlManagerProvider
        {
            get
            {
                return null;
            }
        }

        public ExecutionContext ExecutionContext
        {
            get
            {
                return TestExecutionContext;
            }
        }

        public bool SkipAssemblyReferences
        {
            get;
            set;
        }

        public bool BindingRedirectsDisabled
        {
            get;
            set;
        }

        public void ReportError(string message)
        {            
        }
    }

    public class TestExecutionContext : ExecutionContext
    {
        public TestExecutionContext(PackageIdentity directInstall)
        {
            FilesOpened = new HashSet<string>();
            DirectInstall = directInstall;
        }
        public HashSet<string> FilesOpened { get; private set; }
        public override Task OpenFile(string fullPath)
        {
            FilesOpened.Add(fullPath);
            return Task.FromResult(0);
        }
    }
}
