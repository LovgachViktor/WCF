﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace KalturaTestWcfClient.RemoteService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="RemoteService.IStripeService")]
    public interface IStripeService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStripeService/TestConnection", ReplyAction="http://tempuri.org/IStripeService/TestConnectionResponse")]
        string TestConnection();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStripeService/TestConnection", ReplyAction="http://tempuri.org/IStripeService/TestConnectionResponse")]
        System.Threading.Tasks.Task<string> TestConnectionAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStripeService/Transact", ReplyAction="http://tempuri.org/IStripeService/TransactResponse")]
        bool Transact(string customerId, double amount, string currency, string cardId, System.Collections.Generic.Dictionary<string, string> extraData);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IStripeService/Transact", ReplyAction="http://tempuri.org/IStripeService/TransactResponse")]
        System.Threading.Tasks.Task<bool> TransactAsync(string customerId, double amount, string currency, string cardId, System.Collections.Generic.Dictionary<string, string> extraData);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IStripeServiceChannel : KalturaTestWcfClient.RemoteService.IStripeService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class StripeServiceClient : System.ServiceModel.ClientBase<KalturaTestWcfClient.RemoteService.IStripeService>, KalturaTestWcfClient.RemoteService.IStripeService {
        
        public StripeServiceClient() {
        }
        
        public StripeServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public StripeServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public StripeServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public StripeServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string TestConnection() {
            return base.Channel.TestConnection();
        }

        public System.Threading.Tasks.Task<string> TestConnectionAsync()
        {
            return base.Channel.TestConnectionAsync();
        }

        public bool Transact(string customerId, double amount, string currency, string cardId, System.Collections.Generic.Dictionary<string, string> extraData) {
            return base.Channel.Transact(customerId, amount, currency, cardId, extraData);
        }

        public System.Threading.Tasks.Task<bool> TransactAsync(string customerId, double amount, string currency, string cardId, System.Collections.Generic.Dictionary<string, string> extraData)
        {
            return base.Channel.TransactAsync(customerId, amount, currency, cardId, extraData);
        }
    }
}