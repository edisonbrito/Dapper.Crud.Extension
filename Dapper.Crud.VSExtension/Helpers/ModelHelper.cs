﻿namespace Dapper.Crud.VSExtension.Helpers
{
    public static class ModelHelper
    {
        public static object Generate(string[] content, string rawContent, string model)
        {
            var nspace = "";
            var o = new object();
            foreach (var line in content)
            {
                if (line.Contains("namespace"))
                {
                    nspace = line.Replace("namespace ", "");
                    break;
                }
            }
            o = AssemblyHelper.ExecuteCode(rawContent, nspace, model, false);

            return o;
        }
    }
}