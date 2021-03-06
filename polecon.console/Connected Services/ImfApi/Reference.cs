﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ImfApi
{
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://www.imf.org/SDMX", ConfigurationName="ImfApi.ASMXSDMXServiceSoap")]
    public interface ASMXSDMXServiceSoap
    {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.imf.org/SDMX/GetMaxSeriesInResult", ReplyAction="*")]
        System.Threading.Tasks.Task<int> GetMaxSeriesInResultAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.imf.org/SDMX/GetDataflow", ReplyAction="*")]
        System.Threading.Tasks.Task<ImfApi.GetDataflowResponse> GetDataflowAsync(ImfApi.GetDataflowRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.imf.org/SDMX/GetDataStructure", ReplyAction="*")]
        System.Threading.Tasks.Task<ImfApi.GetDataStructureResponse> GetDataStructureAsync(ImfApi.GetDataStructureRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.imf.org/SDMX/GetCompactData", ReplyAction="*")]
        System.Threading.Tasks.Task<ImfApi.GetCompactDataResponse> GetCompactDataAsync(ImfApi.GetCompactDataRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.imf.org/SDMX/GetMetadataStructure", ReplyAction="*")]
        System.Threading.Tasks.Task<ImfApi.GetMetadataStructureResponse> GetMetadataStructureAsync(ImfApi.GetMetadataStructureRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.imf.org/SDMX/GetGenericMetadata", ReplyAction="*")]
        System.Threading.Tasks.Task<ImfApi.GetGenericMetadataResponse> GetGenericMetadataAsync(ImfApi.GetGenericMetadataRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.imf.org/SDMX/GetServiceVersion", ReplyAction="*")]
        System.Threading.Tasks.Task<ImfApi.GetServiceVersionResponse> GetServiceVersionAsync(ImfApi.GetServiceVersionRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://www.imf.org/SDMX/GetCodeList", ReplyAction="*")]
        System.Threading.Tasks.Task<ImfApi.GetCodeListResponse> GetCodeListAsync(ImfApi.GetCodeListRequest request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetDataflowRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetDataflow", Namespace="http://www.imf.org/SDMX", Order=0)]
        public ImfApi.GetDataflowRequestBody Body;
        
        public GetDataflowRequest()
        {
        }
        
        public GetDataflowRequest(ImfApi.GetDataflowRequestBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute()]
    public partial class GetDataflowRequestBody
    {
        
        public GetDataflowRequestBody()
        {
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetDataflowResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetDataflowResponse", Namespace="http://www.imf.org/SDMX", Order=0)]
        public ImfApi.GetDataflowResponseBody Body;
        
        public GetDataflowResponse()
        {
        }
        
        public GetDataflowResponse(ImfApi.GetDataflowResponseBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://www.imf.org/SDMX")]
    public partial class GetDataflowResponseBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public System.Xml.XmlElement GetDataflowResult;
        
        public GetDataflowResponseBody()
        {
        }
        
        public GetDataflowResponseBody(System.Xml.XmlElement GetDataflowResult)
        {
            this.GetDataflowResult = GetDataflowResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetDataStructureRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetDataStructure", Namespace="http://www.imf.org/SDMX", Order=0)]
        public ImfApi.GetDataStructureRequestBody Body;
        
        public GetDataStructureRequest()
        {
        }
        
        public GetDataStructureRequest(ImfApi.GetDataStructureRequestBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://www.imf.org/SDMX")]
    public partial class GetDataStructureRequestBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public System.Xml.XmlElement queryMessage;
        
        public GetDataStructureRequestBody()
        {
        }
        
        public GetDataStructureRequestBody(System.Xml.XmlElement queryMessage)
        {
            this.queryMessage = queryMessage;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetDataStructureResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetDataStructureResponse", Namespace="http://www.imf.org/SDMX", Order=0)]
        public ImfApi.GetDataStructureResponseBody Body;
        
        public GetDataStructureResponse()
        {
        }
        
        public GetDataStructureResponse(ImfApi.GetDataStructureResponseBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://www.imf.org/SDMX")]
    public partial class GetDataStructureResponseBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public System.Xml.XmlElement GetDataStructureResult;
        
        public GetDataStructureResponseBody()
        {
        }
        
        public GetDataStructureResponseBody(System.Xml.XmlElement GetDataStructureResult)
        {
            this.GetDataStructureResult = GetDataStructureResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetCompactDataRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetCompactData", Namespace="http://www.imf.org/SDMX", Order=0)]
        public ImfApi.GetCompactDataRequestBody Body;
        
        public GetCompactDataRequest()
        {
        }
        
        public GetCompactDataRequest(ImfApi.GetCompactDataRequestBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://www.imf.org/SDMX")]
    public partial class GetCompactDataRequestBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public System.Xml.XmlElement queryMessage;
        
        public GetCompactDataRequestBody()
        {
        }
        
        public GetCompactDataRequestBody(System.Xml.XmlElement queryMessage)
        {
            this.queryMessage = queryMessage;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetCompactDataResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetCompactDataResponse", Namespace="http://www.imf.org/SDMX", Order=0)]
        public ImfApi.GetCompactDataResponseBody Body;
        
        public GetCompactDataResponse()
        {
        }
        
        public GetCompactDataResponse(ImfApi.GetCompactDataResponseBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://www.imf.org/SDMX")]
    public partial class GetCompactDataResponseBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public System.Xml.XmlElement GetCompactDataResult;
        
        public GetCompactDataResponseBody()
        {
        }
        
        public GetCompactDataResponseBody(System.Xml.XmlElement GetCompactDataResult)
        {
            this.GetCompactDataResult = GetCompactDataResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetMetadataStructureRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetMetadataStructure", Namespace="http://www.imf.org/SDMX", Order=0)]
        public ImfApi.GetMetadataStructureRequestBody Body;
        
        public GetMetadataStructureRequest()
        {
        }
        
        public GetMetadataStructureRequest(ImfApi.GetMetadataStructureRequestBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://www.imf.org/SDMX")]
    public partial class GetMetadataStructureRequestBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public System.Xml.XmlElement queryMessage;
        
        public GetMetadataStructureRequestBody()
        {
        }
        
        public GetMetadataStructureRequestBody(System.Xml.XmlElement queryMessage)
        {
            this.queryMessage = queryMessage;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetMetadataStructureResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetMetadataStructureResponse", Namespace="http://www.imf.org/SDMX", Order=0)]
        public ImfApi.GetMetadataStructureResponseBody Body;
        
        public GetMetadataStructureResponse()
        {
        }
        
        public GetMetadataStructureResponse(ImfApi.GetMetadataStructureResponseBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://www.imf.org/SDMX")]
    public partial class GetMetadataStructureResponseBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public System.Xml.XmlElement GetMetadataStructureResult;
        
        public GetMetadataStructureResponseBody()
        {
        }
        
        public GetMetadataStructureResponseBody(System.Xml.XmlElement GetMetadataStructureResult)
        {
            this.GetMetadataStructureResult = GetMetadataStructureResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetGenericMetadataRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetGenericMetadata", Namespace="http://www.imf.org/SDMX", Order=0)]
        public ImfApi.GetGenericMetadataRequestBody Body;
        
        public GetGenericMetadataRequest()
        {
        }
        
        public GetGenericMetadataRequest(ImfApi.GetGenericMetadataRequestBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://www.imf.org/SDMX")]
    public partial class GetGenericMetadataRequestBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public System.Xml.XmlElement queryMessage;
        
        public GetGenericMetadataRequestBody()
        {
        }
        
        public GetGenericMetadataRequestBody(System.Xml.XmlElement queryMessage)
        {
            this.queryMessage = queryMessage;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetGenericMetadataResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetGenericMetadataResponse", Namespace="http://www.imf.org/SDMX", Order=0)]
        public ImfApi.GetGenericMetadataResponseBody Body;
        
        public GetGenericMetadataResponse()
        {
        }
        
        public GetGenericMetadataResponse(ImfApi.GetGenericMetadataResponseBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://www.imf.org/SDMX")]
    public partial class GetGenericMetadataResponseBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public System.Xml.XmlElement GetGenericMetadataResult;
        
        public GetGenericMetadataResponseBody()
        {
        }
        
        public GetGenericMetadataResponseBody(System.Xml.XmlElement GetGenericMetadataResult)
        {
            this.GetGenericMetadataResult = GetGenericMetadataResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetServiceVersionRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetServiceVersion", Namespace="http://www.imf.org/SDMX", Order=0)]
        public ImfApi.GetServiceVersionRequestBody Body;
        
        public GetServiceVersionRequest()
        {
        }
        
        public GetServiceVersionRequest(ImfApi.GetServiceVersionRequestBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute()]
    public partial class GetServiceVersionRequestBody
    {
        
        public GetServiceVersionRequestBody()
        {
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetServiceVersionResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetServiceVersionResponse", Namespace="http://www.imf.org/SDMX", Order=0)]
        public ImfApi.GetServiceVersionResponseBody Body;
        
        public GetServiceVersionResponse()
        {
        }
        
        public GetServiceVersionResponse(ImfApi.GetServiceVersionResponseBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://www.imf.org/SDMX")]
    public partial class GetServiceVersionResponseBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string GetServiceVersionResult;
        
        public GetServiceVersionResponseBody()
        {
        }
        
        public GetServiceVersionResponseBody(string GetServiceVersionResult)
        {
            this.GetServiceVersionResult = GetServiceVersionResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetCodeListRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetCodeList", Namespace="http://www.imf.org/SDMX", Order=0)]
        public ImfApi.GetCodeListRequestBody Body;
        
        public GetCodeListRequest()
        {
        }
        
        public GetCodeListRequest(ImfApi.GetCodeListRequestBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://www.imf.org/SDMX")]
    public partial class GetCodeListRequestBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public System.Xml.XmlElement queryMessage;
        
        public GetCodeListRequestBody()
        {
        }
        
        public GetCodeListRequestBody(System.Xml.XmlElement queryMessage)
        {
            this.queryMessage = queryMessage;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetCodeListResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetCodeListResponse", Namespace="http://www.imf.org/SDMX", Order=0)]
        public ImfApi.GetCodeListResponseBody Body;
        
        public GetCodeListResponse()
        {
        }
        
        public GetCodeListResponse(ImfApi.GetCodeListResponseBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://www.imf.org/SDMX")]
    public partial class GetCodeListResponseBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public System.Xml.XmlElement GetCodeListResult;
        
        public GetCodeListResponseBody()
        {
        }
        
        public GetCodeListResponseBody(System.Xml.XmlElement GetCodeListResult)
        {
            this.GetCodeListResult = GetCodeListResult;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    public interface ASMXSDMXServiceSoapChannel : ImfApi.ASMXSDMXServiceSoap, System.ServiceModel.IClientChannel
    {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.2")]
    public partial class ASMXSDMXServiceSoapClient : System.ServiceModel.ClientBase<ImfApi.ASMXSDMXServiceSoap>, ImfApi.ASMXSDMXServiceSoap
    {
        
        /// <summary>
        /// Implement this partial method to configure the service endpoint.
        /// </summary>
        /// <param name="serviceEndpoint">The endpoint to configure</param>
        /// <param name="clientCredentials">The client credentials</param>
        static partial void ConfigureEndpoint(System.ServiceModel.Description.ServiceEndpoint serviceEndpoint, System.ServiceModel.Description.ClientCredentials clientCredentials);
        
        public ASMXSDMXServiceSoapClient(EndpointConfiguration endpointConfiguration) : 
                base(ASMXSDMXServiceSoapClient.GetBindingForEndpoint(endpointConfiguration), ASMXSDMXServiceSoapClient.GetEndpointAddress(endpointConfiguration))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public ASMXSDMXServiceSoapClient(EndpointConfiguration endpointConfiguration, string remoteAddress) : 
                base(ASMXSDMXServiceSoapClient.GetBindingForEndpoint(endpointConfiguration), new System.ServiceModel.EndpointAddress(remoteAddress))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public ASMXSDMXServiceSoapClient(EndpointConfiguration endpointConfiguration, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(ASMXSDMXServiceSoapClient.GetBindingForEndpoint(endpointConfiguration), remoteAddress)
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public ASMXSDMXServiceSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress)
        {
        }
        
        public System.Threading.Tasks.Task<int> GetMaxSeriesInResultAsync()
        {
            return base.Channel.GetMaxSeriesInResultAsync();
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<ImfApi.GetDataflowResponse> ImfApi.ASMXSDMXServiceSoap.GetDataflowAsync(ImfApi.GetDataflowRequest request)
        {
            return base.Channel.GetDataflowAsync(request);
        }
        
        public System.Threading.Tasks.Task<ImfApi.GetDataflowResponse> GetDataflowAsync()
        {
            ImfApi.GetDataflowRequest inValue = new ImfApi.GetDataflowRequest();
            inValue.Body = new ImfApi.GetDataflowRequestBody();
            return ((ImfApi.ASMXSDMXServiceSoap)(this)).GetDataflowAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<ImfApi.GetDataStructureResponse> ImfApi.ASMXSDMXServiceSoap.GetDataStructureAsync(ImfApi.GetDataStructureRequest request)
        {
            return base.Channel.GetDataStructureAsync(request);
        }
        
        public System.Threading.Tasks.Task<ImfApi.GetDataStructureResponse> GetDataStructureAsync(System.Xml.XmlElement queryMessage)
        {
            ImfApi.GetDataStructureRequest inValue = new ImfApi.GetDataStructureRequest();
            inValue.Body = new ImfApi.GetDataStructureRequestBody();
            inValue.Body.queryMessage = queryMessage;
            return ((ImfApi.ASMXSDMXServiceSoap)(this)).GetDataStructureAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<ImfApi.GetCompactDataResponse> ImfApi.ASMXSDMXServiceSoap.GetCompactDataAsync(ImfApi.GetCompactDataRequest request)
        {
            return base.Channel.GetCompactDataAsync(request);
        }
        
        public System.Threading.Tasks.Task<ImfApi.GetCompactDataResponse> GetCompactDataAsync(System.Xml.XmlElement queryMessage)
        {
            ImfApi.GetCompactDataRequest inValue = new ImfApi.GetCompactDataRequest();
            inValue.Body = new ImfApi.GetCompactDataRequestBody();
            inValue.Body.queryMessage = queryMessage;
            return ((ImfApi.ASMXSDMXServiceSoap)(this)).GetCompactDataAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<ImfApi.GetMetadataStructureResponse> ImfApi.ASMXSDMXServiceSoap.GetMetadataStructureAsync(ImfApi.GetMetadataStructureRequest request)
        {
            return base.Channel.GetMetadataStructureAsync(request);
        }
        
        public System.Threading.Tasks.Task<ImfApi.GetMetadataStructureResponse> GetMetadataStructureAsync(System.Xml.XmlElement queryMessage)
        {
            ImfApi.GetMetadataStructureRequest inValue = new ImfApi.GetMetadataStructureRequest();
            inValue.Body = new ImfApi.GetMetadataStructureRequestBody();
            inValue.Body.queryMessage = queryMessage;
            return ((ImfApi.ASMXSDMXServiceSoap)(this)).GetMetadataStructureAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<ImfApi.GetGenericMetadataResponse> ImfApi.ASMXSDMXServiceSoap.GetGenericMetadataAsync(ImfApi.GetGenericMetadataRequest request)
        {
            return base.Channel.GetGenericMetadataAsync(request);
        }
        
        public System.Threading.Tasks.Task<ImfApi.GetGenericMetadataResponse> GetGenericMetadataAsync(System.Xml.XmlElement queryMessage)
        {
            ImfApi.GetGenericMetadataRequest inValue = new ImfApi.GetGenericMetadataRequest();
            inValue.Body = new ImfApi.GetGenericMetadataRequestBody();
            inValue.Body.queryMessage = queryMessage;
            return ((ImfApi.ASMXSDMXServiceSoap)(this)).GetGenericMetadataAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<ImfApi.GetServiceVersionResponse> ImfApi.ASMXSDMXServiceSoap.GetServiceVersionAsync(ImfApi.GetServiceVersionRequest request)
        {
            return base.Channel.GetServiceVersionAsync(request);
        }
        
        public System.Threading.Tasks.Task<ImfApi.GetServiceVersionResponse> GetServiceVersionAsync()
        {
            ImfApi.GetServiceVersionRequest inValue = new ImfApi.GetServiceVersionRequest();
            inValue.Body = new ImfApi.GetServiceVersionRequestBody();
            return ((ImfApi.ASMXSDMXServiceSoap)(this)).GetServiceVersionAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<ImfApi.GetCodeListResponse> ImfApi.ASMXSDMXServiceSoap.GetCodeListAsync(ImfApi.GetCodeListRequest request)
        {
            return base.Channel.GetCodeListAsync(request);
        }
        
        public System.Threading.Tasks.Task<ImfApi.GetCodeListResponse> GetCodeListAsync(System.Xml.XmlElement queryMessage)
        {
            ImfApi.GetCodeListRequest inValue = new ImfApi.GetCodeListRequest();
            inValue.Body = new ImfApi.GetCodeListRequestBody();
            inValue.Body.queryMessage = queryMessage;
            return ((ImfApi.ASMXSDMXServiceSoap)(this)).GetCodeListAsync(inValue);
        }
        
        public virtual System.Threading.Tasks.Task OpenAsync()
        {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginOpen(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndOpen));
        }
        
        public virtual System.Threading.Tasks.Task CloseAsync()
        {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginClose(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndClose));
        }
        
        private static System.ServiceModel.Channels.Binding GetBindingForEndpoint(EndpointConfiguration endpointConfiguration)
        {
            if ((endpointConfiguration == EndpointConfiguration.ASMXSDMXServiceSoap))
            {
                System.ServiceModel.BasicHttpBinding result = new System.ServiceModel.BasicHttpBinding();
                result.MaxBufferSize = int.MaxValue;
                result.ReaderQuotas = System.Xml.XmlDictionaryReaderQuotas.Max;
                result.MaxReceivedMessageSize = int.MaxValue;
                result.AllowCookies = true;
                return result;
            }
            if ((endpointConfiguration == EndpointConfiguration.ASMXSDMXServiceSoap12))
            {
                System.ServiceModel.Channels.CustomBinding result = new System.ServiceModel.Channels.CustomBinding();
                System.ServiceModel.Channels.TextMessageEncodingBindingElement textBindingElement = new System.ServiceModel.Channels.TextMessageEncodingBindingElement();
                textBindingElement.MessageVersion = System.ServiceModel.Channels.MessageVersion.CreateVersion(System.ServiceModel.EnvelopeVersion.Soap12, System.ServiceModel.Channels.AddressingVersion.None);
                result.Elements.Add(textBindingElement);
                System.ServiceModel.Channels.HttpTransportBindingElement httpBindingElement = new System.ServiceModel.Channels.HttpTransportBindingElement();
                httpBindingElement.AllowCookies = true;
                httpBindingElement.MaxBufferSize = int.MaxValue;
                httpBindingElement.MaxReceivedMessageSize = int.MaxValue;
                result.Elements.Add(httpBindingElement);
                return result;
            }
            throw new System.InvalidOperationException(string.Format("Could not find endpoint with name \'{0}\'.", endpointConfiguration));
        }
        
        private static System.ServiceModel.EndpointAddress GetEndpointAddress(EndpointConfiguration endpointConfiguration)
        {
            if ((endpointConfiguration == EndpointConfiguration.ASMXSDMXServiceSoap))
            {
                return new System.ServiceModel.EndpointAddress("http://dataservices.imf.org/sdmx20/SDMX_Web_Service.asmx");
            }
            if ((endpointConfiguration == EndpointConfiguration.ASMXSDMXServiceSoap12))
            {
                return new System.ServiceModel.EndpointAddress("http://dataservices.imf.org/sdmx20/SDMX_Web_Service.asmx");
            }
            throw new System.InvalidOperationException(string.Format("Could not find endpoint with name \'{0}\'.", endpointConfiguration));
        }
        
        public enum EndpointConfiguration
        {
            
            ASMXSDMXServiceSoap,
            
            ASMXSDMXServiceSoap12,
        }
    }
}
