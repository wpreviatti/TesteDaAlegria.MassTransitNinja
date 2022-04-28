namespace Company.StateMachines
{
    using System;
    using MassTransit;
    using MassTransit.RedisIntegration;

    public class OrderState :
        SagaStateMachineInstance ,
        ISagaVersion
    {
        public string CurrentState { get; set; }

        public string Value { get; set; }

        public string CustomerNumber { get; set; }

        public DateTime Updated { get; set; }

        public string TesteDeCampo  { get; set; }

        public Guid CorrelationId { get; set; }
        public int Version { get ; set ; }
        public DateTime? SubmitDate { get; internal set; }
    }
}