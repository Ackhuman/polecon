${
    // Enable extension methods by adding using Typewriter.Extensions.*
    using Typewriter.Extensions.Types;

    // Uncomment the constructor to change template settings.
    Template(Settings settings)
    {
        settings.OutputFilenameFactory = f => {
          string className = f.Classes.Any()
            ? f.Classes.First().Name
            : f.Interfaces.First().Name;
          return $"{GenerateFileName(className)}.ts";
        };
        settings.PartialRenderingMode = PartialRenderingMode.Combined;
    }
    string GenerateFileName(string className) {
        className = className.Replace("Model", "").TrimStart('I');
        return $"{className.Substring(0, 1).ToLower()}{className.Substring(1)}.model";
    }

    bool IsPropertyOptional(Property p)
    {
      return p.Type.IsNullable || p.Attributes.Any(a => a.Name == "Optional");
    }
    bool HasAttribute(Class  c, string attributeName)
    {    
        return c.Attributes.Any(a => a.Name == attributeName);
    }
    bool IsIncluded(Class c)
    {
        return HasAttribute(c, "ClientModel");
    }
    
    string Imports(Class c)
    {
        var builtInTypes = new [] { "number", "string", "boolean", "object", "Date", "File", "Blob", "any", "DataResult", "T" };
        var types = c.Properties
          .Where(p => !c.TypeParameters.Any(t => p.Type.Name != t.Name))
          .Select(p => p.Type.Name.Replace("[]", string.Empty))
          .Where(t => !builtInTypes.Contains(t))
          .Where(t => c.Name != t)
          .Select(t => $"import {{ {t} }} from 'core/models/{GenerateModelFileName(t)}';")
          .Distinct();
        return string.Join(Environment.NewLine, types);
    }
    string ExpectedReturnType(Method m)
    {
        var returnType = m.Attributes
            .Where(a => a.Name == "ProducesResponseType")
            .SelectMany(
                a => a.Arguments.Where(arg => arg.Type == "Type"),
                (a, arg) => typeTransforms.ContainsKey(arg.TypeValue.Name)
                    ? typeTransforms[arg.TypeValue.Name]
                    : arg.TypeValue.Name
            ).FirstOrDefault();
        return returnType ?? "any";
    }
    
    private static Dictionary<string, string> typeTransforms = new Dictionary<string, string>
    {
        { "Date", "Moment" },
        { "DataSourceResult", "DataResult" }
    };

    string GenerateModelFileName(string className)
    {
        className = className.Replace("ViewModel", "").TrimStart('I');
        return $"{className.Substring(0, 1).ToLower()}{className.Substring(1)}.model";
    }

    string AppType(Property p)
    {
        return typeTransforms.ContainsKey(p.Type)
          ? typeTransforms[p.Type]
          : p.Type;
    }
    bool IsPropertyIncluded(Property p)
    {
        return !p.Attributes.Any(a => a.Name == "JsonIgnore")
            //&& !p.IsVirtual
            && p.HasSetter
            || p.Attributes.Any(a => a.Name == "Include");
    }
    bool IsOptional(Property p)
    {
        return p.Type.IsNullable || !p.Type.IsPrimitive;
        //|| p.Attributes.Any(a => a.Name == "Optional");
    }
}
$Classes($IsIncluded)[
$Imports

export interface $Name$TypeParameters {
    $Properties($IsPropertyIncluded)[//$DocComment
    $name$IsOptional[?]: $AppType;][
    ]
}]
