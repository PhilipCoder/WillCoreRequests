﻿namespace CodeBuilder.IBuilder
{
    public interface IRenderble
    {
        string Template { get; set; }
        string GetText();
        void CleanTemplateUp();
    }
}