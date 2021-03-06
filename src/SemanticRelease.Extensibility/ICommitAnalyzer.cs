using System;
using SemanticRelease.Extensibility.Model;

namespace SemanticRelease.Extensibility
{
    public interface ICommitAnalyzer
    {
        Release CalculateNextRelease();

        event EventHandler<CommitStatusEventArgs> CommitEvent;
    }
}