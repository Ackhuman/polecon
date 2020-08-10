${
    // Enable extension methods by adding using Typewriter.Extensions.*
    using Typewriter.Extensions.Types;
    using Typewriter.Extensions.WebApi;

    Template(Settings settings) {
      settings
        .IncludeCurrentProject();

      settings.OutputFilenameFactory = c => {
        return GenerateFileName(c.Classes.First().Name);
      };
    }
    
    
    string Imports(Class c)
    {
        var builtInTypes = new [] { "number", "string", "boolean", "object", "Date", "File", "Blob", "any", "DataResult" };
        var paramTypes = c.Methods
            .Where(m => !IsMethodIgnored(m))
            .SelectMany(
                m => m.Parameters.Where(p => !p.Attributes.Any(a => a.Name == "ForceAnyType")),
                (m, p) => p.Type.Name.Replace("[]", string.Empty))
            .Where(t => !builtInTypes.Contains(t) && !typeTransforms.ContainsKey(t));
        var returnTypes = c.Methods
            .Where(m => !HasAttribute(m, "KendoDataMethod"))
            .Where(m => !IsMethodIgnored(m))
            .Select(m => ExpectedReturnType(m).Replace("[]", string.Empty))
            .Where(t => !builtInTypes.Contains(t));
        var allTypes = paramTypes.Union(returnTypes)            
            .Select(t => $"import {{ {t} }} from '../../core/models/{GenerateModelFileName(t)}';");
        var types = allTypes.Distinct();
        return string.Join(Environment.NewLine, types);
    }
    
    bool HasAttribute(Method m, string attributeName)
    {    
        return m.Attributes.Any(a => a.Name == attributeName);
    }


    bool IsMethodIgnored(Method m)
    {
        return HasAttribute(m, "ApiClientIgnoreMethod");
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

    string GenerateFileName(string className)
    {
        className = className.Replace("Controller", "").ToLower();
        return $"{className}.apiClient.ts";
    }

    string GenerateModelFileName(string className)
    {
        className = className.Replace("ViewModel", "").TrimStart('I');
        return $"{className.Substring(0, 1).ToLower()}{className.Substring(1)}.model";
    }

    private static Dictionary<string, string> typeTransforms = new Dictionary<string, string>
    {
        { "DataSourceRequest", "DataSourceRequestState" },
        { "DataSourceResult", "DataResult" }
    };
    string MappedType(Parameter p)
    {
        if(p.Attributes.Any(a => a.Name == "ForceAnyType")) {
            return "any";
        }
        var t = p.Type;
        return typeTransforms.ContainsKey(t.Name)
            ? typeTransforms[t.Name]
            : t.Name;
    }

    bool IsPostMethod(Method m)
    {
        return m.HttpMethod() == "post";
    }
}
import { Inject, Injectable } from '@angular/core';
import { Router } from '@angular/router';
//import { DataSourceRequestState, DataResult, toDataSourceRequestString } from '@progress/kendo-data-query';
import { HttpClient, HttpResponse, HttpHeaders } from '@angular/common/http';
import { Observable, BehaviorSubject, iif, of } from 'rxjs';
import { map, tap, catchError, share, finalize } from 'rxjs/operators';
$Classes(*Controller)[
$Imports
@Injectable()
export class $Name {
  constructor(
    private http: HttpClient,
    @Inject('BASE_URL') private baseUrl: string
  ) {
    this.postHeaderOptions = new HttpHeaders();
    this.postHeaderOptions.set("Content-Type", "application/json");
  }
  private postHeaderOptions: HttpHeaders;

  $Methods[
  // $DocComment
  $name = ($Parameters[$name$HasDefaultValue[?]: $MappedType][, ]): Observable<$ExpectedReturnType> => {
    return this.http.$HttpMethod(`${this.baseUrl}$Url`$IsPostMethod[, $RequestData ]);
  }]
}]
