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
    
    public partial class Users
    {
        public int id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public Nullable<int> DriverId { get; set; }
        public string Role { get; set; }
    
        public virtual Drivers Drivers { get; set; }
    }
}
