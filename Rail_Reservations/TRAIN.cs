//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Rail_Reservations
{
    using System;
    using System.Collections.Generic;
    
    public partial class TRAIN
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TRAIN()
        {
            this.BOOKING_TICKETS = new HashSet<BOOKING_TICKETS>();
            this.CANCEL_TICKET = new HashSet<CANCEL_TICKET>();
            this.CLASS_DETAILS = new HashSet<CLASS_DETAILS>();
        }
    
        public int TRAINNO { get; set; }
        public string TRAINNAME { get; set; }
        public string SOURCE { get; set; }
        public string DESTINATION { get; set; }
        public string STATUS { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BOOKING_TICKETS> BOOKING_TICKETS { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CANCEL_TICKET> CANCEL_TICKET { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CLASS_DETAILS> CLASS_DETAILS { get; set; }
    }
}
