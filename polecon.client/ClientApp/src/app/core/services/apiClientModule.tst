${
    // Enable extension methods by adding using Typewriter.Extensions.*
    using Typewriter.Extensions.Types;
    using Typewriter.Extensions.WebApi;

    Template(Settings settings) {
      settings
        .IncludeCurrentProject();
      settings.OutputFilenameFactory = c => {
        return "apiClients.module.ts";
      };
    }
    string GenerateFileName(Class c)
    {
        return $"{c.Name.Replace("Controller", "").ToLower()}.apiClient.ts";
    }
}
import { NgModule } from '@angular/core'
$Classes(*Controller)[import { $Name } from 'api/$GenerateFileName'][
]

@NgModule({
  providers: [   
    $Classes(*Controller)[$Name][,
    ]
  ]
})
export class ApiClientModule { }
