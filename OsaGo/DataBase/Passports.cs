//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OsaGo.DataBase
{
    using System;
    using System.Collections.Generic;
    
    public partial class Passports
    {
        public int Id { get; set; }
        public int DriverId { get; set; }
        public string Series { get; set; }
        public string Number { get; set; }
        public System.DateTime DateOfIssue { get; set; }
    
        public virtual Drivers Drivers { get; set; }
    }
}
