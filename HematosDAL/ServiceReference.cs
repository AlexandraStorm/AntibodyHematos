using System.Collections.Generic;
using System.ServiceModel;

namespace HematosDAL
{
    public class ServiceReference1
    {
        [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0"), System.ServiceModel.ServiceContractAttribute(ConfigurationName = "ServiceReference1.IEvolutionBatchListener")]
        public interface IEvolutionBatchListener
        {
            [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IEvolutionBatchListener/CreateBatchDataFile", ReplyAction = "http://tempuri.org/IEvolutionBatchListener/CreateBatchDataFileResponse")]
            string CreateBatchDataFile(string paramData);

            [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IEvolutionBatchListener/CreateExponentBatch", ReplyAction = "http://tempuri.org/IEvolutionBatchListener/CreateExponentBatchResponse")]
            string CreateExponentBatch(string paramData);

            [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IEvolutionBatchListener/CheckExponentStatus", ReplyAction = "http://tempuri.org/IEvolutionBatchListener/CheckExponentStatusResponse")]
            string[] CheckExponentStatus();

            [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IEvolutionBatchListener/GetBatchResults", ReplyAction = "http://tempuri.org/IEvolutionBatchListener/GetBatchResultsResponse")]
            string GetBatchResults(string paramData);

            [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IEvolutionBatchListener/GetAssayList", ReplyAction = "http://tempuri.org/IEvolutionBatchListener/GetAssayListResponse")]
            Dictionary<string, string> GetAssayList();

            [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IEvolutionBatchListener/CheckXponentStatus", ReplyAction = "http://tempuri.org/IEvolutionBatchListener/CheckXponentStatusResponse")]
            bool CheckXponentStatus();
        }

        [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
        public interface IEvolutionBatchListenerChannel : ServiceReference1.IEvolutionBatchListener, IClientChannel
        {
        }

        [System.Diagnostics.DebuggerStepThroughAttribute(), System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
        public partial class EvolutionBatchListenerClient : ClientBase<ServiceReference1.IEvolutionBatchListener>, ServiceReference1.IEvolutionBatchListener
        {
            public EvolutionBatchListenerClient()
                : base()
            {
            }

            public EvolutionBatchListenerClient(string endpointConfigurationName)
                : base(endpointConfigurationName)
            {
            }

            public EvolutionBatchListenerClient(string endpointConfigurationName, string remoteAddress)
                : base(endpointConfigurationName, remoteAddress)
            {
            }

            public EvolutionBatchListenerClient(string endpointConfigurationName, EndpointAddress remoteAddress)
                : base(endpointConfigurationName, remoteAddress)
            {
            }

            public EvolutionBatchListenerClient(System.ServiceModel.Channels.Binding binding, EndpointAddress remoteAddress)
                : base(binding, remoteAddress)
            {
            }

            public string CreateBatchDataFile(string paramData)
            {
                return Channel.CreateBatchDataFile(paramData);
            }

            public string CreateExponentBatch(string paramData)
            {
                return Channel.CreateExponentBatch(paramData);
            }

            public string[] CheckExponentStatus()
            {
                return Channel.CheckExponentStatus();
            }

            public string GetBatchResults(string paramData)
            {
                return Channel.GetBatchResults(paramData);
            }

            public Dictionary<string, string> GetAssayList()
            {
                return Channel.GetAssayList();
            }

            public bool CheckXponentStatus()
            {
                return Channel.CheckXponentStatus();
            }
        }
    }
}