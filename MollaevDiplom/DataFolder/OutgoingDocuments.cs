//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MollaevDiplom.DataFolder
{
    using System;
    using System.Collections.Generic;
    
    public partial class OutgoingDocuments
    {
        public int IdOutgoingDocuments { get; set; }
        public int NumberOutgoing { get; set; }
        public System.DateTime DateOutgoing { get; set; }
        public int IdCategory { get; set; }
        public int IdPerformer { get; set; }
        public string NameOutgoingDocuments { get; set; }
        public string SummaryOutgoing { get; set; }
        public int IdStaff { get; set; }
        public System.DateTime ControlDate { get; set; }
        public System.DateTime ExecutionDate { get; set; }
        public int IdMarkExecution { get; set; }
        public int QuantityОfСopies { get; set; }
        public int QuantityPage { get; set; }
        public byte[] FileDocuments { get; set; }
    
        public virtual DocumentsCategory DocumentsCategory { get; set; }
        public virtual MarkExecution MarkExecution { get; set; }
        public virtual Performer Performer { get; set; }
        public virtual Staff Staff { get; set; }
    }
}
