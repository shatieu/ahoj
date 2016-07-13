using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// :P
/// </summary>
namespace BasicForm.Models.DBRepresentations
{
    public abstract class ARepresentation
    {
        public abstract int ID { get; set; }
        public string _DBName;

        public ARepresentation(string DBName)
        {
            _DBName = DBName;
        }

        public abstract ARepresentation getNewInstance();

        public override string ToString()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            //Do something like this: Object 1  == Type.Name ID
            sb.Append(this.GetType().Name).Append(" ").Append(this.GetType().GetProperty("ID").GetValue(this)).Append(",\n");
            foreach (var property in this.GetType().GetProperties())
            {
                sb.Append(property.Name).Append(": ").Append(property.GetValue(this) == null ? "NULL" : property.GetValue(this)).Append(", ");
            }
            sb.Remove(sb.Length - 2, 1);
            sb.Append("\n");
            return sb.ToString();
        }

    }
}
