using System;
using System.Collections.Generic;
using System.Text;

namespace MotLookupApi.Framework.Models
{
  public class MotTest : Base
  {
    public DateTime CompletedDate { get; set; }
    public string? Result { get; set; }
    public DateTime ExpiryDate { get; set; }
    public int Mileage { get; set; }
    public long MotTestNumber { get; set; }
    public ICollection<Comment> Comments { get; set; }
    public string? OdometerResultType { get; set; }
    public string? OdometerUnit { get; set; }
  }
}
